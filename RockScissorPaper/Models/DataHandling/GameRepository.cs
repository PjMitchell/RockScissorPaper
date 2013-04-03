using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models.DataHandling
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

        /// <summary>
        /// Adds new empty game to data storage
        /// </summary>
        /// <param name="game"></param>
        public void CreateNewGame(RoshamboGame game)
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

        public RoshamboGame RetrieveGame(int id)
        {
            RoshamboGame result = OpenGames.FirstOrDefault(s => s.GameId == id);
            return result;
        }


        
        public void UpdateGameStatus(int gameId, GameStatus value)
        {
            
        }


        public int CreateRound(int roundNumber, int gameId)
        {
            return roundNumber;
        }


        public void CreateGameRoundResult(int playerID, int gameId, int gameRoundId, RoshamboSelection selection)
        {
            
        }
    }
}