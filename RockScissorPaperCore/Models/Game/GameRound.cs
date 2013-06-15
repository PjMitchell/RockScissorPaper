using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Domain
{
    public class GameRound
    {
        public int RoundId { get; set; }
        public int RoundNumber { get; set; }
        public GameSelection PlayerOneSelection { get; set; }
        public GameSelection PlayerTwoSelection { get; set; }
        public GameOutcome PlayerOneOutcome { get; set; }
        public GameOutcome PlayerTwoOutcome { get; set; }
    }
}