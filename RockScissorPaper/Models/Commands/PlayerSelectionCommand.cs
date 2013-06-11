using RockScissorPaper.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockScissorPaper.Models
{
    /// <summary>
    /// Player Selection for the round
    /// </summary>
    public class PlayerSelectionCommand
    {
        public int GameId { get; private set; }

        public RoshamboSelection PlayerOneSelection { get; set; }

        public RoshamboSelection PlayerTwoSelection { get; set; }

        public PlayerSelectionCommand(int gameId)
        {
            GameId = gameId;
        }
    }
}
