using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RockScissorPaper.Models.DataHandling;

namespace RockScissorPaper.Models
{
    public class StatisticsViewModelFactory
    {
        public StatisticsViewModel Result { get; set; }
        private IStatisticsRepository _repository;

        public StatisticsViewModelFactory(IStatisticsRepository repository)
        {
            _repository = repository;
            Build();
        }

        public void Build()
        {
            Result = new StatisticsViewModel();
            Result.RoundInformation = new List<RoundStatistic>();
            for (int i = 1; i <= 5; i++)
            {
                RoundStatistic roundstat = _repository.RetrieveRoundInformation(i);
                Result.RoundInformation.Add(roundstat);
            }
            Result.RoundInformation.OrderBy(r => r.RoundNumber);
            Result.Overview = _repository.RetrieveRoundInformation();
            Result.GamesPlayed = _repository.RetrieveGamesPlayed();
        }
    }
}