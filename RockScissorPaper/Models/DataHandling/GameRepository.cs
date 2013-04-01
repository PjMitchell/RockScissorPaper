using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models
{
    public class GameRepository : IGameRepository
    {
        //Add dispose methods
        private static List<RoshamboGame> OpenGames = new List<RoshamboGame>();
        private static List<GameRound> UnresolvedRounds = new List<GameRound>();
        const int listLimit = 20;

        public void Reset()
        {
            OpenGames = new List<RoshamboGame>();
        }

        public void AddGame(RoshamboGame game)
        {
            lock (OpenGames)
            {
                
                if (OpenGames.Count > listLimit)
                {
                    // do something
                }
                else
                {
                    OpenGames.Add(game);
                }
            }
        }

        public RoshamboGame GetGame(int id)
        {
            RoshamboGame result = OpenGames.FirstOrDefault(s => s.GameId == id);
            return result;
        }
    }
}