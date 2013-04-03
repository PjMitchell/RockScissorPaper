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

        void UpdateGame(RoshamboGame game);

        RoshamboGame GetGame(int id);

        void UpdateGameStatus(int gameId, GameStatus value);

        int CreateRound(int roundNumber, int gameId);
    }
}