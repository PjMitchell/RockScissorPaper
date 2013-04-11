using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models.DataHandling
{
    public interface IGameRepository
    {
        /// <summary>
        /// Adds new empty game to data storage
        /// </summary>
        /// <param name="game"></param>
        void CreateNewGame(RoshamboGame game);

        /// <summary>
        /// Retrives Game by Game Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RoshamboGame RetrieveGame(int id);

        /// <summary>
        /// Updates the Current game Status of selected gameId with set value
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="value"></param>
        void UpdateGameStatus(int gameId, GameStatus value);

        /// <summary>
        /// Adds new round To GameRound Table and returns Id
        /// </summary>
        /// <param name="roundNumber"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>
        int CreateRound(int roundNumber, int gameId);

        /// <summary>
        /// Creates RoundResult for Player
        /// </summary>
        /// <param name="playerID">Id of player makeing the choice</param>
        /// <param name="gameId">The Id of the Game being played</param>
        /// <param name="gameRoundId">The Id of the current Round </param>
        /// <param name="selection">The Players selection</param>
        void CreateGameRoundResult(int playerID, int gameId, int gameRoundId, RoshamboSelection selection);

        /// <summary>
        /// Updates the GamePlayer Table with Players' game result and score
        /// </summary>
        /// <param name="game"></param>
        void UpdateGameResult(RoshamboGame game);

        /// <summary>
        /// Returns the Total wins of Human and Bot Players
        /// </summary>
        /// <returns></returns>
        RoshamboHubViewModel RetrieveBotVsHumanScore();
    }
}