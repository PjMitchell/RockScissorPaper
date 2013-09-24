using HilltopDigital.SimpleDAL;
using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace RockScissorPaper.DAL
{
    /// <summary>
    /// Required Stored Proceedures
    /// Proc_Select_RoundStatisticsTotal
    /// Proc_Select_RoundStatisticsByRoundNumber
    /// Proc_Select_GameTotalPlayed
    /// </summary>
    public class StatisticsSQLRepository : IStatisticsRepository
    {
        IDatabaseConnector _dataAccess;

        public StatisticsSQLRepository(IDatabaseConnector databaseConnector)
        {
            _dataAccess = databaseConnector;
        }

        public RoundStatistic GetRoundSummary()
        {
            RoundStatisticsMapper mapper = new RoundStatisticsMapper();
            _dataAccess.Get("GameRound_GetStatisticSummary", mapper);
            RoundStatistic result = mapper.Result as RoundStatistic;
            result.RoundNumber = 0;
            return result;
        }

        public List<RoundStatistic> GetRoundInformation()
        {
            ListOfRoundStatisticsMapper mapper = new ListOfRoundStatisticsMapper();
            _dataAccess.Get("GameRound_GetStatistic", mapper);
            List<RoundStatistic> result = mapper.Result as List<RoundStatistic>;
            return result;
        }

        public RoundStatistic GetRoundInformation(int round)
        {
            List<StoreProcedureParameter> paras = new List<StoreProcedureParameter>();
            paras.Add(new StoreProcedureParameter("RoundNumberInput", round));
            RoundStatisticsMapper mapper = new RoundStatisticsMapper();
            _dataAccess.Get("GameRound_GetStatisticByRoundNumber", mapper, paras);
            RoundStatistic result = mapper.Result as RoundStatistic;
            result.RoundNumber = round;
            return result;
        }
        
        public int GetGamesPlayed()
        {
            int result = Convert.ToInt32(_dataAccess.ExecuteScalar("Game_GetGamesPlayed"));
            return result;
        }

        /// <summary>
        /// Returns the Total wins of Human and Bot Players
        /// </summary>
        /// <returns></returns>
        public BotvsHumanStatistics GetBotVsHumanScore()
        {
            DataTable dt = _dataAccess.Get("GamePlayer_GetBotVsHumanVictoryCount");
            DataRow dr = dt.Rows[0];
            MappingHelper map = new MappingHelper(dr);
            BotvsHumanStatistics result = new BotvsHumanStatistics();
            result.BotWins = map.MapInt32("BotVictory");
            result.HumanWins = map.MapInt32("HumanVictory");
            return result;

        }

        public List<SelectionVsTimeQuery> GetSelectionVsTime()
        {
            SelectionVsTimeMapper mapper = new SelectionVsTimeMapper();
             _dataAccess.Get("Gameround_GetGroupedByDate", mapper);
             List<SelectionVsTimeQuery> result = mapper.Result as List<SelectionVsTimeQuery>;
            return result;
        }
    }
}