using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace RockScissorPaper.BLL
{
    /// <summary>
    /// Player Selection for the round
    /// </summary>
    public class PlayerSelectionCommand
    {
        [Required]
        public int GameId { get; private set; }
        [Required]
        public GameSelection PlayerOneSelection { get; set; }
        [Required]
        public GameSelection PlayerTwoSelection { get; set; }

        public PlayerSelectionCommand(int gameId)
        {
            GameId = gameId;
        }
    }
}
