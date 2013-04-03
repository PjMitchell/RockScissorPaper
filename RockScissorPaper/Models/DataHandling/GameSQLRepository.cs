using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models.DataHandling
{
    public class GameSQLRepository : IGameRepository 
    {
        private IDatabaseConnector _dataAccess;

        public GameSQLRepository(IDatabaseConnector dataConnector)
        {
            _dataAccess = dataConnector;
        }

        /// <summary>
        /// Adds new empty game to data storage
        /// </summary>
        /// <param name="game"></param>
        public void CreateNewGame(RoshamboGame game)
        {
            List<StoreProceedureParameter> parameters = new List<StoreProceedureParameter>();
            parameters.Add(new StoreProceedureParameter("PlayerOneIdInput", game.PlayerOne.PlayerId));
            parameters.Add(new StoreProceedureParameter("PlayerTwoIdInput", game.PlayerTwo.PlayerId));
            parameters.Add(new StoreProceedureParameter("RuleSetIdInput", game.Rules.Id));
            game.GameId = Convert.ToInt32(_dataAccess.GetScalar("Proc_Create_NewGame", parameters));
        }

        public void UpdateGame(RoshamboGame game)
        {
            throw new NotImplementedException();
        }

        public RoshamboGame GetGame(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the Current game Status of selected gameId with set value
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="value"></param>
        public void UpdateGameStatus(int gameId, GameStatus value)
        {
            List<StoreProceedureParameter> parameters = new List<StoreProceedureParameter>();
            parameters.Add(new StoreProceedureParameter("GameIdInput", gameId));
            parameters.Add(new StoreProceedureParameter("NewStatusInput", (int)value));
            _dataAccess.ExecuteNonQuery("Proc_Update_GameStatus", parameters);
        }

        /// <summary>
        /// Adds new round To GameRound Table and returns Id
        /// </summary>
        /// <param name="roundNumber"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public int CreateRound(int roundNumber, int gameId)
        {
            List<StoreProceedureParameter> parameters = new List<StoreProceedureParameter>();
            parameters.Add(new StoreProceedureParameter("GameIdInput", gameId));
            parameters.Add(new StoreProceedureParameter("RoundNumberInput", roundNumber));
            return Convert.ToInt32(_dataAccess.GetScalar("Proc_Create_GameRound", parameters));
        }
    }
}