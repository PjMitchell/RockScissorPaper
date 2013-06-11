using RockScissorPaper.Core;
using System.Collections.Generic;

namespace RockScissorPaper.DataAccessLayer
{
    public interface IStatisticsRepository
    {
        RoundStatistic RetrieveRoundSummary();

        List<RoundStatistic> RetrieveRoundInformation();

        RoundStatistic RetrieveRoundInformation(int round);

        int RetrieveGamesPlayed();
    }
}