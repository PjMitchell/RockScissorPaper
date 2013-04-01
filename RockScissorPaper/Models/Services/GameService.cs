using RockScissorPaper.Models.GameModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models
{
    public class GameService
    {

        private IGameRepository _repository;
        private GameRound _currentRound;
        private GameStatus _status;
        public GameStatus Status { get { return _status; } private set { _status = value; CurrentGame.Status = _status; } }
        public RoshamboGame CurrentGame { get; private set; }
        private GameStateViewModelFactory _gameStateService { get; set; }

        public GameService(IGameRepository repository, RoshamboGame game)
        {
            
            _repository = repository;
            _repository.Reset();//To be removed
            CurrentGame = game;
            _repository.AddGame(CurrentGame);
            _gameStateService = new GameStateViewModelFactory(CurrentGame);
            _status = CurrentGame.Status;
            _gameStateService.Update(Status);

        }
        public GameService(IGameRepository repository, int id)
        {
            
            _repository = repository;
            CurrentGame = _repository.GetGame(id);
            _gameStateService = new GameStateViewModelFactory(CurrentGame);
            _status = CurrentGame.Status;
            _gameStateService.Update(Status);

        }
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

        private GameServiceResult ProcessEndOfGame(PlayerSelectionCommand command)
        {
            CurrentGame.Rules.GameScoreResolver.ResolveGame(CurrentGame.Rounds);
            GameServiceResult result = new GameServiceResult();
            result.PlayerOneOutcome = CurrentGame.Rules.GameScoreResolver.PlayerOneOutcome;
            result.PlayerTwoOutcome = CurrentGame.Rules.GameScoreResolver.PlayerTwoOutcome;
            _gameStateService.Update(Status);
            return result;

        }

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
            _gameStateService.Update(Status, _currentRound);

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

        private GameServiceResult ProcessNewRound(PlayerSelectionCommand command)
        {
            if (command.PlayerOneSelection == 0 && command.PlayerTwoSelection == 0)
            {
                return null;
            }
               
            _currentRound = new GameRound();
            _currentRound.RoundNumber = CurrentGame.Rounds.Count + 1;

            //player one choice
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

        public GameStateViewModel GetGameState(int playerId)
        {
            _gameStateService.SetObservingPlayer(playerId);
            return _gameStateService.GameState;
        }

    }
}