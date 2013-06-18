using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockScissorPaper.BLL;

namespace RockScissorPaper.BLL
{
    public interface IGameService
    {
        //commands
        int CreateGame(CreateGameCommand command);
        GameStatus ExecuteMove(ExecuteMoveCommand command);

        //queries
        IEnumerable<GameRules> GetGameRuleSets();
        GameRules GetGameRuleSetById(int id);
        Game GetGame(int id);
        GameStateQuery GetLastestRoundResult(int gameId, int playerId);
        GameStateQuery GetEndOfGame(int gameId, int playerId);
        GameStateQuery GetCurrentState(int gameId, int playerId);
    }
}
