using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models.DataHandling
{
    public interface IStatisticsRepository
    {
        List<List<RoshamboChoiceStatistic>> RetrieveRoundInformation();

        List<RoshamboChoiceStatistic> RetrieveOverview();

        int RetrieveGamesPlayed();
    }
}