using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockScissorPaper.Models
{
    public class GameServiceResult
    {
        public string Message { get; set; }
        public GameOutcome PlayerOneOutcome { get; set; }
        public GameOutcome PlayerTwoOutcome { get; set; }
    }
}
