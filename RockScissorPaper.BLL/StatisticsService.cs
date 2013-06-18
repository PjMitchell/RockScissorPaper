using RockScissorPaper.DAL;
using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaper.BLL
{
    public class StatisticsService : IStatisticsService
    {
        IStatisticsRepository _repository;

        public StatisticsService(IStatisticsRepository repository)
        {
            _repository = repository;
        }
        
        public StatisticsOverviewQuery GetOverview()
        {
            StatisticsOverviewQuery result = new StatisticsOverviewQuery();
            result.RoundInformation = _repository.GetRoundInformation();
            result.RoundInformation.OrderBy(r => r.RoundNumber);
            result.Overview = _repository.GetRoundSummary();
            result.GamesPlayed = _repository.GetGamesPlayed();
            return result;
        }


        public BotvsHumanStatistics GetBotVsHumanScore()
        {
            return _repository.GetBotVsHumanScore();
        }
    }
}
