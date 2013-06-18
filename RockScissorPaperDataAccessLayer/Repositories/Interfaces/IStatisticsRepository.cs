using RockScissorPaper.Domain;
using System.Collections.Generic;

namespace RockScissorPaper.DAL
{
    public interface IStatisticsRepository
    {
        RoundStatistic GetRoundSummary();

        List<RoundStatistic> GetRoundInformation();

        RoundStatistic GetRoundInformation(int round);

        int GetGamesPlayed();

        /// <summary>
        /// Returns the Total wins of Human and Bot Players
        /// </summary>
        /// <returns></returns>
        BotvsHumanStatistics GetBotVsHumanScore();
    }
}