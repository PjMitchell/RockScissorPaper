using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockScissorPaper.BLL
{
    public class GameServiceResult
    {
        public bool HasGameFinished { get; set; }
        public string Message { get; set; }
        public GameOutcome PlayerOneOutcome { get; set; }
        public GameOutcome PlayerTwoOutcome { get; set; }
    }
}
