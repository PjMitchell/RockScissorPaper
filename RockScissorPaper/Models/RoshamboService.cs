using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models
{
    public class RoshamboService
    {
        private string _message;
        private GameOutcome _playerOneResult;
        private string _rockWin = "Rock breaks Scissor";
        private string _scissorWin = "Scissors cut Paper";
        private string _paperWin = "Paper covers Rock";
        private string _draw = "Great minds think alike";
        public string Message { get { return _message; } }
        public GameOutcome PlayerOneResult { get { return _playerOneResult; } }

        public void ResolveRound(RoshamboSelection playerOne, RoshamboSelection playerTwo)
        {
            if (playerOne == playerTwo)
            {
                _playerOneResult = GameOutcome.Draw;
                _message = _draw;
            }
            else
            {
                switch (playerOne)
                {
                    case RoshamboSelection.Rock:
                        if (playerTwo == RoshamboSelection.Scissor)
                        {
                            _playerOneResult = GameOutcome.Win;
                            _message = _rockWin;
                        }
                        else
                        {
                            _playerOneResult = GameOutcome.Lose;
                            _message = _paperWin;
                        }
                        break;
                    case RoshamboSelection.Scissor:
                        if (playerTwo == RoshamboSelection.Paper)
                        {
                            _playerOneResult = GameOutcome.Win;
                            _message = _scissorWin;
                        }
                        else
                        {
                            _playerOneResult = GameOutcome.Lose;
                            _message = _rockWin;
                        }
                        break;
                    case RoshamboSelection.Paper:
                        if (playerTwo == RoshamboSelection.Rock)
                        {
                            _playerOneResult = GameOutcome.Win;
                            _message = _paperWin;
                        }
                        else
                        {
                            _playerOneResult = GameOutcome.Lose;
                            _message = _scissorWin;
                        }
                        break;
                }
            }
        }
    }
}