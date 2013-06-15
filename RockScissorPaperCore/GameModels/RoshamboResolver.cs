using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Domain
{
    public class RoshamboResolver : IRoshamboResolver
    {
        private string _message;
        private GameOutcome _playerOneResult;
        private GameOutcome _playerTwoResult;
        private string _rockWin = "Rock breaks Scissor";
        private string _scissorWin = "Scissors cut Paper";
        private string _paperWin = "Paper covers Rock";
        private string _draw = "Great minds think alike";
        public string Message { get { return _message; } }
        public GameOutcome PlayerOneResult { get { return _playerOneResult; } }
        public GameOutcome PlayerTwoResult { get { return _playerTwoResult; } }

        public void ResolveRound(RoshamboSelection playerOne, RoshamboSelection playerTwo)
        {
            if (playerOne == playerTwo)
            {
                SetResult(GameOutcome.Draw);
                _message = _draw;
            }
            else
            {
                switch (playerOne)
                {
                    case RoshamboSelection.Rock:
                        if (playerTwo == RoshamboSelection.Scissor)
                        {
                            SetResult(GameOutcome.Win);
                            _message = _rockWin;
                        }
                        else
                        {
                            SetResult(GameOutcome.Lose);
                            _message = _paperWin;
                        }
                        break;
                    case RoshamboSelection.Scissor:
                        if (playerTwo == RoshamboSelection.Paper)
                        {
                            SetResult(GameOutcome.Win);
                            _message = _scissorWin;
                        }
                        else
                        {
                            SetResult(GameOutcome.Lose);
                            _message = _rockWin;
                        }
                        break;
                    case RoshamboSelection.Paper:
                        if (playerTwo == RoshamboSelection.Rock)
                        {
                             SetResult(GameOutcome.Win);
                            _message = _paperWin;
                        }
                        else
                        {
                            SetResult(GameOutcome.Lose);
                            _message = _scissorWin;
                        }
                        break;
                }
            }
        }
        public void ResolveRound(GameRound round)
        {
            ResolveRound(round.PlayerOneSelection, round.PlayerTwoSelection);
            round.PlayerOneOutcome = _playerOneResult;
            round.PlayerTwoOutcome = _playerTwoResult;
        }

        private void SetResult(GameOutcome playerOneResult)
        {
            switch (playerOneResult)
            {
                case GameOutcome.Draw:
                    _playerOneResult = GameOutcome.Draw;
                    _playerTwoResult = GameOutcome.Draw;
                    break;
                case GameOutcome.Lose:
                    _playerOneResult = GameOutcome.Lose;
                    _playerTwoResult = GameOutcome.Win;
                    break;
                case GameOutcome.Win:
                    _playerOneResult = GameOutcome.Win;
                    _playerTwoResult = GameOutcome.Lose;
                     break;
            }
        }
    }
}