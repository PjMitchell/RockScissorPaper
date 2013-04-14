using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models.DataHandling
{
    public interface IStatisticsRepository
    {
        List<RoshamboChoiceStatistic> RetrieveRoundInformation();

        List<RoshamboChoiceStatistic> RetrieveRoundInformation(int round);

        int RetrieveGamesPlayed();
    }
}