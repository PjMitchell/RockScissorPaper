using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models.DataHandling
{
    public class StatisticsSQLRepository : IStatisticsRepository
    {
        IDatabaseConnector _dataAccess;
        private static List<RoundStatistic> _cachedRoundStatistics;
        private int _statisticUpdateInterval = -1;

        public StatisticsSQLRepository(IDatabaseConnector databaseConnector)
        {
            _dataAccess = databaseConnector;
            if (_cachedRoundStatistics == null)
            {
                _cachedRoundStatistics = new List<RoundStatistic>();
            }
        }

        private RoundStatistic CheckCache(int roundNumber = 0)
        {
            if (_cachedRoundStatistics.Any(r => r.RoundNumber == roundNumber))
            {
                lock (_cachedRoundStatistics)
                {
                    RoundStatistic stat = _cachedRoundStatistics.First(r => r.RoundNumber == roundNumber);
                    if (stat.TimeStamp > DateTime.UtcNow.AddHours(_statisticUpdateInterval))
                    {
                        return stat;
                    }
                    else
                    {
                        _cachedRoundStatistics.Remove(stat);
                    }
                }
            }
            return null;
        }

        public RoundStatistic RetrieveRoundInformation()
        {
            RoundStatistic result = CheckCache();
            if (result == null)
            {
                RoundStatisticsMapper mapper = new RoundStatisticsMapper();
                _dataAccess.Get("Proc_Select_RoundStatisticsTotal", mapper);
                result = mapper.Result as RoundStatistic;
                result.RoundNumber = 0;
                _cachedRoundStatistics.Add(result);
            }
            return result;
        }

        public RoundStatistic RetrieveRoundInformation(int round)
        {
            RoundStatistic result = CheckCache(round);
            if (result == null)
            {
                List<StoreProceedureParameter> paras = new List<StoreProceedureParameter>();
                paras.Add(new StoreProceedureParameter("RoundNumberInput", round));
                RoundStatisticsMapper mapper = new RoundStatisticsMapper();
                _dataAccess.Get("Proc_Select_RoundStatistics", mapper, paras);
                result = mapper.Result as RoundStatistic;
                result.RoundNumber = round;
                _cachedRoundStatistics.Add(result);
            }
            return result;
        }
        
        public int RetrieveGamesPlayed()
        {
            int result = Convert.ToInt32(_dataAccess.GetScalar("Proc_Select_GameTotal"));
            return result;
        }

    }
}