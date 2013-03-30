using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models
{
    public class GameRepository
    {
        //Add dispose methods
        private static List<GameService> OpenGameServices { get; set; }
        const int listLimit = 20;

        public static void Reset()
        {
            OpenGameServices = new List<GameService>();
        }

        public static void Add(GameService gameService)
        {
            lock (OpenGameServices)
            {
                if (OpenGameServices == null)
                {
                    OpenGameServices = new List<GameService>();
                }
                if (OpenGameServices.Count > listLimit)
                {
                    // do something
                }
                else
                {
                    OpenGameServices.Add(gameService);
                }
            }
        }

        public  static GameService Get(int id)
        {
            GameService result = OpenGameServices.FirstOrDefault(s => s.CurrentGame.GameId == id);
            return result;
        }
    }
}