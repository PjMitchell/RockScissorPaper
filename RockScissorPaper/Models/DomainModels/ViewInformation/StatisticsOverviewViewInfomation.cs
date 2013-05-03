using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models
{
    public class StatisticsOverviewViewInformation
    {
        public List<RoundStatistic> RoundInformation { get; set; }
        public RoundStatistic Overview { get; set; }
        public int GamesPlayed { get; set; }
    }
}