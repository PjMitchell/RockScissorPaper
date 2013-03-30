using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockScissorPaper.Models
{
    public class GameServiceCommand
    {
        public int GameId { get; private set; }

        public RoshamboSelection PlayerOneSelection { get; set; }

        public RoshamboSelection PlayerTwoSelection { get; set; }

        public GameServiceCommand(int gameId)
        {
            GameId = gameId;
        }
    }
}
