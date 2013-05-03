using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models.DataHandling
{
    public interface IStatisticsRepository
    {
        RoundStatistic RetrieveRoundSummary();

        List<RoundStatistic> RetrieveRoundInformation();

        RoundStatistic RetrieveRoundInformation(int round);

        int RetrieveGamesPlayed();
    }
}