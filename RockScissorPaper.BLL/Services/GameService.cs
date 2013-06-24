using RockScissorPaper.DAL;
using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        #region Commands

        public int CreateGame(CreateGameCommand command)
        {
            Validator.ValidateObject(command, new ValidationContext(command));
            GameRules rule = _gameRepository.GetGameRules(command.RuleId);
            string buttonOrder = SelectionButtonOrderRandomizer.GetButtonBoxOrder(rule.GameType);
            return _gameRepository.CreateNewGame(command.PlayerOneId, command.PlayerTwoId, command.RuleId, buttonOrder);
        }

        public GameStatus ExecuteMove(ExecuteMoveCommand command)
        {
            Validator.ValidateObject(command, new ValidationContext(command));
            Game game = GetGame(command.GameId);

            switch (game.Status)
            {
                case GameStatus.NewRound:
                    ProcessNewRound(game, command);
                    break;
                case GameStatus.RoundResult:
                    ProcessRoundResult(game, command);
                    break;
                case GameStatus.FinalRoundResult:
                    ProcessFinalRoundResult(game);
                    break;
                default:
                    break;
            }
            return game.Status;
        }

        #region ExecuteMove Submethods

        private void ProcessFinalRoundResult(Game game)
        {
            SetGameStatus(game, GameStatus.EndOfGame);
        }

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
                SetGameStatus(game, GameStatus.NewRound);
            }
            else
            {
                logic.ScoreResolver.ResolveGame(game.Rounds);
                if (game.Rules.AllowDraw || logic.ScoreResolver.PlayerOneOutcome != GameOutcome.Draw)
                {
                    UpdateGameResultCommand updateGameCommand = new UpdateGameResultCommand()
                    {
                        GameId = game.GameId,
                        PlayerOneId = game.PlayerOne.PlayerId,
                        PlayerOneGameOutcome = logic.ScoreResolver.PlayerOneOutcome,
                        PlayerOneGameScore = logic.ScoreResolver.PlayerOneScore,
                        PlayerTwoId = game.PlayerTwo.PlayerId,
                        PlayerTwoGameOutcome = logic.ScoreResolver.PlayerTwoOutcome,
                        PlayerTwoGameScore = logic.ScoreResolver.PlayerTwoScore
                    };
                    _gameRepository.UpdateGameResult(updateGameCommand);

                    var ev = new GameFinishedEvent();
                    _gameEventManager.Publish(ev);
                    SetGameStatus(game, GameStatus.FinalRoundResult);
                }
                else
                {
                    SetGameStatus(game, GameStatus.NewRound);
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
                    SetGameStatus(game, GameStatus.RoundResult);
                    ExecuteMove(command);
                }
                else
                {
                    SetGameStatus(game, GameStatus.WaitingPlayerTwo);
                }
            }
            //player two choice
            else if (command.PlayerId == game.PlayerTwo.PlayerId)
            {
                _currentRound.PlayerTwoSelection = (GameSelection)command.Selection;
                _gameRepository.CreateGameRoundResult(game.PlayerTwo.PlayerId, game.GameId, _currentRound.RoundId, (GameSelection)command.Selection);

                if (game.PlayerOne.IsBot)
                {
                    _currentRound.PlayerOneSelection = game.PlayerOne.Bot.Go();
                    _gameRepository.CreateGameRoundResult(game.PlayerOne.PlayerId, game.GameId, _currentRound.RoundId, _currentRound.PlayerOneSelection);
                    SetGameStatus(game, GameStatus.RoundResult);
                    ExecuteMove(command);
                }
                else
                {
                    SetGameStatus(game, GameStatus.WaitingPlayerOne);
                }
            }
        }

        private void SetGameStatus(Game game, GameStatus status)
        {
            game.Status = status;
            _gameRepository.UpdateGameStatus(game.GameId, game.Status);
        }

        #endregion

        #endregion

        #region Queries

        public IEnumerable<GameRules> GetGameRuleSets()
        {
            List<GameRules> results = new List<GameRules>();
            results.Add(_gameRepository.GetGameRules(1)); //TODO Added when new Stored Procedure is ready
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
        
        public GameStateQuery GetLastestRoundResult(int gameId, int playerId)
        {
            Game game = GetGame(gameId);
            GameStateQuery gameState = new GameStateQuery();
            GameLogic logic = GameLogicFactory.Build(game.Rules.GameType);

            gameState.GameId = game.GameId;

            GameRound currentRound = game.Rounds.LastOrDefault();

            if (currentRound != null)
            {
                logic.RoundResolver.ResolveRound(currentRound);

                gameState.BannerMessage = logic.RoundResolver.Message;
                int currentRoundNumber = game.Rounds.Count;
                gameState.RoundMessage = string.Format("{0} / {1}", currentRoundNumber, game.Rules.TotalRounds);
                logic.ScoreResolver.ResolveGame(game.Rounds);

                //Sets Player One State
                gameState.PlayerOne.PlayerId = game.PlayerOne.PlayerId;
                gameState.PlayerOne.CurrentSelection = currentRound.PlayerOneSelection;
                gameState.PlayerOne.CurrentScore = game.Rounds.Count(r => r.PlayerOneOutcome == GameOutcome.Win);
                gameState.PlayerOne.PlayerMessage = SetWinLoseDrawMessage(logic.RoundResolver.PlayerOneResult);

                //Sets Player Two State
                gameState.PlayerTwo.PlayerId = game.PlayerTwo.PlayerId;
                gameState.PlayerTwo.CurrentSelection = currentRound.PlayerTwoSelection;
                gameState.PlayerTwo.CurrentScore = game.Rounds.Count(r => r.PlayerTwoOutcome == GameOutcome.Win);
                gameState.PlayerTwo.PlayerMessage = SetWinLoseDrawMessage(logic.RoundResolver.PlayerTwoResult);

                gameState.FinalRoundResult = game.Status == GameStatus.FinalRoundResult;
                SetObservingPlayer(gameState, playerId);
                return gameState;
            }
            return null;
        }

        public GameStateQuery GetEndOfGame(int gameId, int playerId)
        {
            Game game = GetGame(gameId);
            GameStateQuery gameState = new GameStateQuery();
            GameLogic logic = GameLogicFactory.Build(game.Rules.GameType);
            

            gameState.GameId = game.GameId;
            int currentRoundNumber = game.Rounds.Count;
            gameState.RoundMessage = string.Format("{0} / {1}", currentRoundNumber, game.Rules.TotalRounds);

            logic.ScoreResolver.ResolveGame(game.Rounds);

            //Sets Player One State
            gameState.PlayerOne.PlayerId = game.PlayerOne.PlayerId;
            gameState.PlayerOne.CurrentScore = game.Rounds.Count(r => r.PlayerOneOutcome == GameOutcome.Win);
            gameState.PlayerOne.PlayerMessage = SetWinLoseDrawMessage(logic.ScoreResolver.PlayerOneOutcome);
            gameState.PlayerOne.PlayerMessage = "<a href=\"/home/GameLobby/" + game.PlayerOne.PlayerId + "\">Play again?</a>";
            //Sets Player Two State
            gameState.PlayerTwo.PlayerId = game.PlayerTwo.PlayerId;
            gameState.PlayerTwo.CurrentScore = game.Rounds.Count(r => r.PlayerTwoOutcome == GameOutcome.Win);
            gameState.PlayerTwo.PlayerMessage = "<a href=\"/home/GameLobby/" + game.PlayerTwo.PlayerId + "\">Play again?</a>";

            if (logic.ScoreResolver.PlayerOneOutcome == GameOutcome.Win)
            {
                gameState.BannerMessage = game.PlayerOne.Name + " wins the game!";
            }
            else if (logic.ScoreResolver.PlayerTwoOutcome == GameOutcome.Win)
            {
                gameState.BannerMessage = game.PlayerTwo.Name + " wins the game!";
            }
            else
            {
                gameState.BannerMessage = "Its a draw.";
            }
            SetObservingPlayer(gameState, playerId);
            return gameState;
        }
        
        public GameStateQuery GetCurrentState(int gameId, int playerId)
        {
            Game game = GetGame(gameId);
            if (game.Status == GameStatus.EndOfGame)
            {
                return GetEndOfGame(gameId, playerId);
            }
            GameStateQuery gameState = new GameStateQuery();
            //TodDO add handlers for Waiting PLayer one /two
            gameState.GameId = game.GameId;
            gameState.BannerMessage = " Round Start";
            int currentRound = game.Rounds.Count + 1;
            gameState.RoundMessage = string.Format("{0} / {1}", currentRound, game.Rules.TotalRounds);

            gameState.PlayerOne = SetInitialPlayerState(game.PlayerOne.PlayerId);
            gameState.PlayerTwo = SetInitialPlayerState(game.PlayerOne.PlayerId);
            SetObservingPlayer(gameState, playerId);
            return gameState;
        }
       
        #region GameState submethods

        private PlayerGameInformation SetInitialPlayerState(int playerId)
        {
            PlayerGameInformation state = new PlayerGameInformation();
            state.PlayerId = playerId;
            state.CurrentScore = 0;
            state.PlayerMessage = "Go!";
            return state;
        }

        private string SetWinLoseDrawMessage(GameOutcome gameOutcome)
        {
            switch (gameOutcome)
            {
                case GameOutcome.Win:
                    return "You Win!";
                case GameOutcome.Lose:
                    return "You Lose!";
                case GameOutcome.Draw:
                    return "Its a tie!";
                default:
                    return null;
            }
        }

        private void SetObservingPlayer(GameStateQuery gameState, int playerId)
        {
            gameState.PlayerOne.IsViewer = playerId == gameState.PlayerOne.PlayerId;
            gameState.PlayerTwo.IsViewer = playerId == gameState.PlayerTwo.PlayerId;
        }
        
        #endregion

        #endregion





        
    }
}
