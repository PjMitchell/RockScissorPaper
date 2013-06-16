using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaper.BLL
{
    public class GameLogicFactory
    {
        public static GameLogic Build(GameType gameType)
        {
            switch (gameType)
            {
                case GameType.StandardGame:
                    return new GameLogic(new RoshamboGameRoundResolver(), new StandardGameScoreResolver());
                default:
                    return new GameLogic(new RoshamboGameRoundResolver(), new StandardGameScoreResolver());
            }
        }
    }
}
