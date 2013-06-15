using RockScissorPaper.Domain;
using System.Collections.Generic;

namespace RockScissorPaper.DAL
{
    public interface IStatisticsRepository
    {
        RoundStatistic RetrieveRoundSummary();

        List<RoundStatistic> RetrieveRoundInformation();

        RoundStatistic RetrieveRoundInformation(int round);

        int RetrieveGamesPlayed();
    }
}