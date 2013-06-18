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
        int CreateGame(CreateGameCommand command);
        IEnumerable<GameRules> GetGameRuleSets();
        GameRules GetGameRuleSetById(int id);
        Game GetGame(int id);
        GameStatus ExecuteMove(ExecuteMoveCommand command);
        GameStateQuery GetGameState(int gameId, int playerId);
    }
}
