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

        
        /// <summary>
        /// Retrives Game by Game Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoshamboGame RetrieveGame(int id)
        {
            List<StoreProceedureParameter> parameters = new List<StoreProceedureParameter>();
            parameters.Add(new StoreProceedureParameter("GameIdInput", id));
            RoshamboGameMapper mapper = new RoshamboGameMapper();
            _dataAccess.Get("",mapper,parameters);
            RoshamboGame result = mapper.Result;
            return result;

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

        /// <summary>
        /// Creates RoundResult for Player
        /// </summary>
        /// <param name="playerID">Id of player makeing the choice</param>
        /// <param name="gameId">The Id of the Game being played</param>
        /// <param name="gameRoundId">The Id of the current Round </param>
        /// <param name="selection">The Players selection</param>
        public void CreateGameRoundResult(int playerID, int gameId, int gameRoundId, RoshamboSelection selection)
        {
            List<StoreProceedureParameter> parameters = new List<StoreProceedureParameter>();
            parameters.Add(new StoreProceedureParameter("PlayerIdInput", playerID));
            parameters.Add(new StoreProceedureParameter("RoshamboGameIdInput", gameId));
            parameters.Add(new StoreProceedureParameter("GameRoundIdInput", gameRoundId));
            parameters.Add(new StoreProceedureParameter("SelectionIdInput", (int)selection));
            _dataAccess.ExecuteNonQuery("Proc_Create_GameRoundResult", parameters);
            
        }
    }
}