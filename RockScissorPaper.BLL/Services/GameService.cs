using RockScissorPaper.DAL;
using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaper.BLL
{
    public class GameService : IGameService
    {
        IGameRepository _gameRepository;
        GameEventManager _gameEventManager;

        public GameService(IGameRepository gameRepository, GameEventManager gameEventManager)
        {
            _gameRepository = gameRepository;
            _gameEventManager = gameEventManager;
        }
        
        
        
        public int CreateGame(CreateGameCommand command)
        {
            GameRules rule = _gameRepository.GetGameRules(command.RuleId);
            string buttonOrder = SelectionButtonOrderRandomizer.GetButtonBoxOrder(rule.GameType);
            return _gameRepository.CreateNewGame(command.PlayerOneId, command.PlayerTwoId, command.RuleId, buttonOrder);
        }

        public IEnumerable<GameRules> GetGameRuleSets()
        {
            List<GameRules> results = new List<GameRules>();
            results.Add(_gameRepository.GetGameRules(1)); //TODO Added when new Stored Proceedure is ready
            return results;
        }

        public GameRules GetGameRuleSetById(int id)
        {
            return  _gameRepository.GetGameRules(id);
        }

        public Game GetGame(int id)
        {
            Game result = _gameRepository.GetGame(id);
            GameLogic logic = GameLogicFactory.Build(result.Rules.GameType);
            foreach (GameRound round in result.Rounds)
            {
                logic.RoundResolver.ResolveRound(round);
            }
            logic.ScoreResolver.ResolveGame(result.Rounds);
            return result;

        }

        public void ExecuteMove(ExecuteMoveCommand command)
        {
            Game game = GetGame(command.GameId);

            switch (game.Status)
            {
                case GameStatus.NewRound:
                     ProcessNewRound(game, command);
                     return;
                case GameStatus.WaitingPlayerOne:
                    break;
                case GameStatus.WaitingPlayerTwo:
                    break;
                case GameStatus.RoundResult:
                    ProcessRoundResult(game, command);
                    return; 
                case GameStatus.EndOfGame:
                    return;
                default:
                    break;
            }
            return;
        }

        
        #region executeMove Submethods


        private void ProcessRoundResult(Game game, ExecuteMoveCommand command)
        {
            GameRound currentRound = game.Rounds.LastOrDefault();
            if (currentRound == null)
            {
                return;
            }

            //Postround decision

            GameLogic logic = GameLogicFactory.Build(game.Rules.GameType);

            if (currentRound.RoundNumber < game.Rules.TotalRounds)
            {
                game.Status = GameStatus.NewRound;
                _gameRepository.UpdateGameStatus(game.GameId, game.Status);
                return;
            }
            else
            {
                logic.ScoreResolver.ResolveGame(game.Rounds);
                if (game.Rules.AllowDraw || logic.ScoreResolver.PlayerOneOutcome != GameOutcome.Draw)
                {
                    _gameRepository.UpdateGameResult(game.GameId, game.PlayerOne.PlayerId, logic.ScoreResolver.PlayerOneOutcome, logic.ScoreResolver.PlayerOneScore);
                    _gameRepository.UpdateGameResult(game.GameId, game.PlayerTwo.PlayerId, logic.ScoreResolver.PlayerTwoOutcome, logic.ScoreResolver.PlayerTwoScore);
                    //_gameStateService.SetAsFinalRoundResult(); TODO add this to service;
                    var ev = new GameFinishedEvent();
                    ev.CurrentGlobalResults = _gameRepository.GetBotVsHumanScore();
                    _gameEventManager.Publish(ev);
                    game.Status = GameStatus.EndOfGame;
                    _gameRepository.UpdateGameStatus(game.GameId, game.Status);
                    return;
                }
                else
                {
                     game.Status = GameStatus.NewRound;
                    _gameRepository.UpdateGameStatus(game.GameId, game.Status);
                    return;
                }
            }
        }

        private void ProcessNewRound(Game game, ExecuteMoveCommand command)
        {
            if (command.Selection == 0)
            {
                return;
            }

            GameRound _currentRound = new GameRound();
            _currentRound.RoundNumber = game.Rounds.Count + 1;
            _currentRound.RoundId = _gameRepository.CreateRound(_currentRound.RoundNumber, game.GameId);

            //player one choice   *Duplication here pass to one method?PM*
            if (command.PlayerId == game.PlayerOne.PlayerId)
            {
                _currentRound.PlayerOneSelection = (GameSelection)command.Selection;
                _gameRepository.CreateGameRoundResult(game.PlayerOne.PlayerId, game.GameId, _currentRound.RoundId, (GameSelection)command.Selection);

                if (game.PlayerTwo.IsBot)
                {
                    _currentRound.PlayerTwoSelection = game.PlayerTwo.Bot.Go();
                    _gameRepository.CreateGameRoundResult(game.PlayerTwo.PlayerId, game.GameId, _currentRound.RoundId, _currentRound.PlayerTwoSelection);
                    game.Status = GameStatus.RoundResult;
                    _gameRepository.UpdateGameStatus(game.GameId, game.Status);
                    ExecuteMove(command);
                    return; 
                }
                else
                {
                    game.Status = GameStatus.WaitingPlayerTwo;
                    _gameRepository.UpdateGameStatus(game.GameId, game.Status);
                    return;
                }
            }
            //player two choice
            if (command.PlayerId == game.PlayerTwo.PlayerId)
            {
                _currentRound.PlayerTwoSelection = (GameSelection)command.Selection;
                _gameRepository.CreateGameRoundResult(game.PlayerTwo.PlayerId, game.GameId, _currentRound.RoundId, (GameSelection)command.Selection);

                if (game.PlayerOne.IsBot)
                {
                    _currentRound.PlayerOneSelection = game.PlayerOne.Bot.Go();
                    _gameRepository.CreateGameRoundResult(game.PlayerOne.PlayerId, game.GameId, _currentRound.RoundId, _currentRound.PlayerOneSelection);
                    game.Status = GameStatus.RoundResult;
                    _gameRepository.UpdateGameStatus(game.GameId, game.Status);
                    ExecuteMove(command);
                    return;
                }
                else
                {
                    game.Status = GameStatus.WaitingPlayerOne;
                    _gameRepository.UpdateGameStatus(game.GameId, game.Status);
                    return;
                }
            }

            return;
        }

        #endregion

        public GameStateQuery GetGameState(int gameId, int playerId)
        {
            Game game = GetGame(gameId);
            GameStateService gameState = new GameStateService(game);
            gameState.Update(game.Status);
            gameState.SetObservingPlayer(playerId);
            return gameState.GameState;
        }

    }
}
