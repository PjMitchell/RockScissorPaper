using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models
{
    public class GameRound
    {
        public int RoundId { get; set; }
        public int RoundNumber { get; set; }
        public RoshamboSelection PlayerOneSelection { get; set; }
        public RoshamboSelection PlayerTwoSelection { get; set; }
        public GameOutcome PlayerOneOutcome { get; set; }
        public GameOutcome PlayerTwoOutcome { get; set; }
    }
}