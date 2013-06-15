using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Domain
{
    public class RoundStatistic
    {
        public int RoundNumber { get; set; } //0 = Summary
        public int TotalSelections { get; set; }
        public DateTime TimeStamp { get; private set; }
        public List<RoshamboChoiceStatistic> Choices { get; set; }

        public RoundStatistic()
        {
            TimeStamp = DateTime.UtcNow;
            Choices = new List<RoshamboChoiceStatistic>();
        }
    }
}