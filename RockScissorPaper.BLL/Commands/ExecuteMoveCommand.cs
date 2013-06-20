using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RockScissorPaper.Domain;
using System.ComponentModel.DataAnnotations;

namespace RockScissorPaper.BLL
{
    public class ExecuteMoveCommand
    {
        [Required]
        public int PlayerId { get; set; }

        [Required]
        public int GameId { get; set; }

        public GameSelection  Selection { get; set; }
    }
}
