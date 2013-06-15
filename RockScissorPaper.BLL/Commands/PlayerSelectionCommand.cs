using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockScissorPaper.BLL
{
    /// <summary>
    /// Player Selection for the round
    /// </summary>
    public class PlayerSelectionCommand
    {
        public int GameId { get; private set; }

        public GameSelection PlayerOneSelection { get; set; }

        public GameSelection PlayerTwoSelection { get; set; }

        public PlayerSelectionCommand(int gameId)
        {
            GameId = gameId;
        }
    }
}
