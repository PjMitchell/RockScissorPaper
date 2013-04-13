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
            Result.RoundInformation = _repository.RetrieveRoundInformation();
            Result.Overview = _repository.RetrieveOverview();
            Result.GamesPlayed = _repository.RetrieveGamesPlayed();
        }
    }
}