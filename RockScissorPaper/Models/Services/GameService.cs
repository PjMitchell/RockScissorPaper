using RockScissorPaper.Models.DataHandling;
using RockScissorPaper.Models.GameModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models
{
    /// <summary>
    /// Starts new games, Processes selections, calculates results and supplies GameStateViewModel on request
    /// </summary>
    public class GameService
    {

        private IGameRepository _repository;
        private GameRound _currentRound;
        private GameStatus _status;
        public GameStatus Status 
        { 
            get { return _status; } 
            private set 
            { 
                _status = value;
                CurrentGame.Status = _status;
                if (CurrentGame.GameId > 0)
                {
                    _repository.UpdateGameStatus(CurrentGame.GameId, value);
                }
            } 
        }
        public RoshamboGame CurrentGame { get; private set; }
        private GameStateViewModelFactory _gameStateViewModelFactory { get; set; }

        #region Contructors

        public GameService(IGameRepository repository, RoshamboGame game)
        {
            
            _repository = repository;
            CurrentGame = game;
            _repository.CreateNewGame(CurrentGame);
            _gameStateViewModelFactory = new GameStateViewModelFactory(CurrentGame);
            _status = CurrentGame.Status;
            _gameStateViewModelFactory.Update(Status);

        }
        public GameService(IGameRepository repository, int id)
        {
            
            _repository = repository;
            CurrentGame = _repository.GetGame(id);
            _gameStateViewModelFactory = new GameStateViewModelFactory(CurrentGame);
            _status = CurrentGame.Status;
            _gameStateViewModelFactory.Update(Status);

        }
        
        #endregion

        /// <summary>
        /// Executes the game service to process Player Slection Command depending on the current game Status
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public GameServiceResult Execute(PlayerSelectionCommand command)
        {
            if (CurrentGame.GameId == command.GameId)
            {
                switch (Status)
                {
                    case GameStatus.NewRound:
                        return ProcessNewRound(command);
                    case GameStatus.WaitingPlayerOne:
                        break;
                    case GameStatus.WaitingPlayerTwo:
                        break;
                    case GameStatus.RoundResult:
                        return ProcessRoundResult(command);
                    case GameStatus.EndOfGame:
                        return ProcessEndOfGame(command);
                    default:
                        break;
                }

            }
            return null;
        }

        #region ExecuteProcesses

        /// <summary>
        /// Process the End of Game Result
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private GameServiceResult ProcessEndOfGame(PlayerSelectionCommand command)
        {
            CurrentGame.Rules.GameScoreResolver.ResolveGame(CurrentGame.Rounds);
            GameServiceResult result = new GameServiceResult();
            result.PlayerOneOutcome = CurrentGame.Rules.GameScoreResolver.PlayerOneOutcome;
            result.PlayerTwoOutcome = CurrentGame.Rules.GameScoreResolver.PlayerTwoOutcome;
            _gameStateViewModelFactory.Update(Status);
            return result;

        }
        /// <summary>
        /// Processes the individual round result and determines if it is the end of the game
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private GameServiceResult ProcessRoundResult(PlayerSelectionCommand command)
        {
            if (_currentRound == null)
            {
                Status = GameStatus.NewRound;
                return null;
            }
            if (_currentRound.PlayerOneSelection == 0 )
            {
                Status = GameStatus.WaitingPlayerOne;
                return null;
            }
            if (_currentRound.PlayerTwoSelection == 0)
            {
                Status = GameStatus.WaitingPlayerTwo;
                return null;
            }
            CurrentGame.Rules.RoundResolver.ResolveRound(_currentRound);
            CurrentGame.Rounds.Add(_currentRound);
            _gameStateViewModelFactory.Update(Status, _currentRound);

            //Postround decision
            GameServiceResult result = new GameServiceResult();
            result.Message = CurrentGame.Rules.RoundResolver.Message;
            CurrentGame.Rules.GameScoreResolver.ResolveGame(CurrentGame.Rounds);
            if (_currentRound.RoundNumber < CurrentGame.Rules.TotalRounds)
            {
                Status = GameStatus.NewRound;
                return result;
            }
            else
            {
                if (CurrentGame.Rules.AllowDraw || CurrentGame.Rules.GameScoreResolver.PlayerOneOutcome != GameOutcome.Draw)
                {
                    Status = GameStatus.EndOfGame;
                    return result;
                }
                else
                {
                    Status = GameStatus.NewRound;
                    return result;
                }
            }


        }

        /// <summary>
        /// Process and player input at the start of the round.
        /// Will determine if other player is a bot and will run bot.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private GameServiceResult ProcessNewRound(PlayerSelectionCommand command)
        {
            if (command.PlayerOneSelection == 0 && command.PlayerTwoSelection == 0)
            {
                return null;
            }
               
            _currentRound = new GameRound();
            _currentRound.RoundNumber = CurrentGame.Rounds.Count + 1;
            _currentRound.RoundId = _repository.CreateRound(_currentRound.RoundNumber, CurrentGame.GameId);

            //player one choice   *Duplication here pass to one method?PM*
            if (command.PlayerOneSelection != 0)
            {
                _currentRound.PlayerOneSelection = command.PlayerOneSelection;
                
                if (CurrentGame.PlayerTwo.IsBot)
                {
                    _currentRound.PlayerTwoSelection = CurrentGame.PlayerTwo.Bot.Go();
                    command.PlayerTwoSelection = _currentRound.PlayerTwoSelection;
                    Status = GameStatus.RoundResult;
                    return Execute(command);
                }
                else
                {
                    Status = GameStatus.WaitingPlayerTwo;
                    GameServiceResult result = new GameServiceResult();
                    result.Message = "Waiting for player two";
                    return result;
                }
            }
            //player two choice
            if (command.PlayerTwoSelection != 0)
            {
                _currentRound.PlayerTwoSelection = command.PlayerTwoSelection;
                if (CurrentGame.PlayerOne.IsBot)
                {
                    _currentRound.PlayerOneSelection = CurrentGame.PlayerOne.Bot.Go();
                    command.PlayerOneSelection = _currentRound.PlayerOneSelection;
                    Status = GameStatus.RoundResult;
                    return Execute(command);
                }
                else
                {
                    Status = GameStatus.WaitingPlayerOne;
                    GameServiceResult result = new GameServiceResult();
                    result.Message = "Waiting for player one";
                    return result;
                }
            }
            return null;
        }
        
        #endregion

        /// <summary>
        /// Returns the viewmodel for the the current game for specified player
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public GameStateViewModel GetGameStateViewModel(int playerId)
        {
            _gameStateViewModelFactory.SetObservingPlayer(playerId);
            return _gameStateViewModelFactory.GameState;
        }

    }
}