using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Core
{
    public class StandardGameScoreResolver : IGameScoreResolver
    {
        int _playerOneScore;
        int _playerTwoScore;
        private GameOutcome _playerOneOutcome;
        private GameOutcome _playerTwoOutcome;

        public int PlayerOneScore
        {
            get { return _playerOneScore; }
        }

        public int PlayerTwoScore
        {
            get { return _playerTwoScore; }
        }

        public GameOutcome PlayerOneOutcome
        {
            get { return _playerOneOutcome;} 
        }

        public GameOutcome PlayerTwoOutcome
        {
            get { return _playerTwoOutcome; }
        }
        public void ResolveGame(List<GameRound> rounds)
        {
            int onewins = rounds.Count(r=>r.PlayerOneOutcome == GameOutcome.Win);
            int twowins = rounds.Count(r=>r.PlayerTwoOutcome == GameOutcome.Win);
            if (onewins == twowins)
            {
                _playerOneScore = 1;
                _playerOneOutcome = GameOutcome.Draw;
                _playerTwoScore = _playerOneScore;
                _playerTwoOutcome = GameOutcome.Draw;
            }
            else if (onewins > twowins)
            {
                _playerOneScore = 3;
                _playerOneOutcome = GameOutcome.Win;
                _playerTwoScore = 0;
                _playerTwoOutcome = GameOutcome.Lose;
            }
            else
            {
                _playerOneScore = 0;
                _playerOneOutcome = GameOutcome.Lose;
                _playerTwoScore = 3;
                _playerTwoOutcome = GameOutcome.Win;
            }
            
        }


        
    }
}