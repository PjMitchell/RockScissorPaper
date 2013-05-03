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

        public RoundStatistic RetrieveRoundSummary()
        {
            RoundStatisticsMapper mapper = new RoundStatisticsMapper();
            _dataAccess.Get("Proc_Select_RoundStatisticsTotal", mapper);
            RoundStatistic result = mapper.Result as RoundStatistic;
            result.RoundNumber = 0;
            return result;
        }

        public List<RoundStatistic> RetrieveRoundInformation()
        {
            RoundStatisticsMapper mapper = new RoundStatisticsMapper();
            _dataAccess.Get("Proc_Select_RoundStatisticsTotal", mapper);
            RoundStatistic result = mapper.Result as RoundStatistic;
            result.RoundNumber = 0;
            return null;
        }

        public RoundStatistic RetrieveRoundInformation(int round)
        {
            List<StoreProceedureParameter> paras = new List<StoreProceedureParameter>();
            paras.Add(new StoreProceedureParameter("RoundNumberInput", round));
            RoundStatisticsMapper mapper = new RoundStatisticsMapper();
            _dataAccess.Get("Proc_Select_RoundStatisticsByRoundNumber", mapper, paras);
            RoundStatistic result = mapper.Result as RoundStatistic;
            result.RoundNumber = round;
            return result;
        }
        
        public int RetrieveGamesPlayed()
        {
            int result = Convert.ToInt32(_dataAccess.GetScalar("Proc_Select_GameTotalPlayed"));
            return result;
        }

    }
}