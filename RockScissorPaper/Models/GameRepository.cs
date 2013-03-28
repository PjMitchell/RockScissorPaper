using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models
{
    public class GameRepository
    {
        public List<RoshamboGame> OpenGames  {get; set;}

        public GameRepository()
        {
            OpenGames = new List<RoshamboGame>();
        }
    }
}