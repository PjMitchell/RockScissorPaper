using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models.DataHandling
{
    public class StatisticsSQLRepository : IStatisticsRepository
    {
        IDatabaseConnector _dataAccess;

        public StatisticsSQLRepository(IDatabaseConnector databaseConnector)
        {
            _dataAccess = databaseConnector;
        }

        public List<List<RoshamboChoiceStatistic>> RetrieveRoundInformation()
        {
            throw new NotImplementedException();
        }

        public List<RoshamboChoiceStatistic> RetrieveOverview()
        {
            throw new NotImplementedException();
        }

        public int RetrieveGamesPlayed()
        {
            throw new NotImplementedException();
        }
    }
}