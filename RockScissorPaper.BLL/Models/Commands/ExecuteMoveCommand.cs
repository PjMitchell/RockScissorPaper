using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockScissorPaper.BLL
{
    public class ExecuteMoveCommand
    {
        public int PlayerId { get; set; }
        public int Selection { get; set; }
        public int GameId { get; set; }
    }
}
