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

        public List<RoshamboChoiceStatistic> RetrieveRoundInformation()
        {
            RoundStatisticsMapper mapper = new RoundStatisticsMapper();
            _dataAccess.Get("Proc_Select_RoundStatisticsTotal", mapper);
            List<RoshamboChoiceStatistic> result = mapper.Result as List<RoshamboChoiceStatistic>;
            return result;
        }

        public List<RoshamboChoiceStatistic> RetrieveRoundInformation(int round)
        {
            List<StoreProceedureParameter> paras = new List<StoreProceedureParameter>();
            paras.Add(new StoreProceedureParameter("RoundNumberInput", round));
            RoundStatisticsMapper mapper = new RoundStatisticsMapper();
            _dataAccess.Get("Proc_Select_RoundStatistics", mapper);
            List<RoshamboChoiceStatistic> result = mapper.Result as List<RoshamboChoiceStatistic>;
            result.ForEach(m=> m.Order = round);
            return result;
        }
        
        public int RetrieveGamesPlayed()
        {
            int result = (int)_dataAccess.GetScalar("Proc_Select_GameTotal");
            return result;
        }

    }
}