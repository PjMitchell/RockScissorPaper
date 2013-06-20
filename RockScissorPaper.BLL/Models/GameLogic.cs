using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaper.BLL
{
    public class GameLogic
    {
        public IGameRoundResolver RoundResolver;
        public IGameScoreResolver ScoreResolver;

        public GameLogic(IGameRoundResolver roundResolver, IGameScoreResolver scoreResolver)
        {
            RoundResolver = roundResolver;
            ScoreResolver = scoreResolver;
        }
    }
}
