using RockScissorPaper.Models.GameModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models
{
    public class GameService
    {
        
        private RoshamboGame _currentgame;
        private GameRound _currentRound;
        private GameServiceStatus _status;
        public GameServiceStatus Status { get { return _status; } }
        public RoshamboGame CurrentGame { get { return _currentgame; } }

        public GameService(RoshamboGame game)
        {
            _currentgame = game;
            _status = GameServiceStatus.NewRound;
        }

        public GameServiceResult Execute(GameServiceCommand command)
        {
            if (_currentgame.GameId == command.Id)
            {
                switch (_status)
                {
                    case GameServiceStatus.NewRound:
                        return ProcessNewRound(command);
                    case GameServiceStatus.WaitingPlayerOne:
                        break;
                    case GameServiceStatus.WaitingPlayerTwo:
                        break;
                    case GameServiceStatus.RoundResult:
                        return ProcessRoundResult(command);
                    case GameServiceStatus.EndOfGame:
                        return ProcessEndOfGame(command);
                    default:
                        break;
                }

            }
            return null;
        }

        private GameServiceResult ProcessEndOfGame(GameServiceCommand command)
        {
            _currentgame.Rules.GameScoreResolver.ResolveGame(_currentgame.Rounds);
            GameServiceResult result = new GameServiceResult();
            result.PlayerOneOutcome = _currentgame.Rules.GameScoreResolver.PlayerOneOutcome;
            result.PlayerTwoOutcome = _currentgame.Rules.GameScoreResolver.PlayerTwoOutcome;
            return result;

        }

        private GameServiceResult ProcessRoundResult(GameServiceCommand command)
        {
            if (_currentRound == null)
            {
                _status = GameServiceStatus.NewRound;
                return null;
            }
            if (_currentRound.PlayerOneSelection == 0 )
            {
                _status = GameServiceStatus.WaitingPlayerOne;
                return null;
            }
            if (_currentRound.PlayerTwoSelection == 0)
            {
                _status = GameServiceStatus.WaitingPlayerTwo;
                return null;
            }
            _currentgame.Rules.RoundResolver.ResolveRound(_currentRound);
            _currentgame.Rounds.Add(_currentRound);

            //Postround decision
            GameServiceResult result = new GameServiceResult();
            result.Message = _currentgame.Rules.RoundResolver.Message;
            _currentgame.Rules.GameScoreResolver.ResolveGame(_currentgame.Rounds);
            if (_currentRound.RoundNumber < _currentgame.Rules.TotalRounds)
            {
                _status = GameServiceStatus.NewRound;
                return result;
            }
            else
            {
                if (_currentgame.Rules.AllowDraw ||_currentgame.Rules.GameScoreResolver.PlayerOneOutcome != GameOutcome.Draw)
                {
                    _status = GameServiceStatus.EndOfGame;
                    return result;
                }
                else
                {
                    _status = GameServiceStatus.NewRound;
                    return result;
                }
            }


        }

        private GameServiceResult ProcessNewRound(GameServiceCommand command)
        {
            if (command.PlayerOneSelection == 0 && command.PlayerTwoSelection == 0)
            {
                return null;
            }
               
            _currentRound = new GameRound();
            _currentRound.RoundNumber = _currentgame.Rounds.Count + 1;

            //player one choice
            if (command.PlayerOneSelection != 0)
            {
                _currentRound.PlayerOneSelection = command.PlayerOneSelection;
                if (_currentgame.PlayerTwo.IsBot)
                {
                    _currentRound.PlayerTwoSelection = _currentgame.PlayerTwo.Bot.Go();
                    command.PlayerTwoSelection = _currentRound.PlayerTwoSelection;
                    _status = GameServiceStatus.RoundResult;
                    return Execute(command);
                }
                else
                {
                    _status = GameServiceStatus.WaitingPlayerTwo;
                    GameServiceResult result = new GameServiceResult();
                    result.Message = "Waiting for player two";
                    return result;
                }
            }

            if (command.PlayerTwoSelection != 0)
            {
                _currentRound.PlayerTwoSelection = command.PlayerTwoSelection;
                if (_currentgame.PlayerOne.IsBot)
                {
                    _currentRound.PlayerOneSelection = _currentgame.PlayerOne.Bot.Go();
                    command.PlayerOneSelection = _currentRound.PlayerOneSelection;
                    _status = GameServiceStatus.RoundResult;
                    return Execute(command);
                }
                else
                {
                    _status = GameServiceStatus.WaitingPlayerOne;
                    GameServiceResult result = new GameServiceResult();
                    result.Message = "Waiting for player one";
                    return result;
                }
            }
            return null;
        }

        

        
    }
}