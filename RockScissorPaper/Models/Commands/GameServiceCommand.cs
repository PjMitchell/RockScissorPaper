using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockScissorPaper.Models
{
    public class GameServiceCommand
    {
        private int _id;
        public int Id { get { return _id; } }

        public RoshamboSelection PlayerOneSelection { get; set; }

        public RoshamboSelection PlayerTwoSelection { get; set; }

        public GameServiceCommand(int gameId)
        {
            _id = gameId;
        }
    }
}
