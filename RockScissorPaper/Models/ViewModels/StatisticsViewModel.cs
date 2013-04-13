using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models
{
    public class StatisticsViewModel
    {
        public List<List<RoshamboChoiceStatistic>> RoundInformation { get; set; }
        public List<RoshamboChoiceStatistic> Overview { get; set; }
        public int GamesPlayed { get; set; }
    }
}