using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaper.BLL
{
    public interface IGameService
    {
        void CreateGame(CreateGameCommand command);
        IEnumerable<GameRules> GetGameRuleSets();
        GameRules GetGameRuleSetById(int id);
        Game GetGame(int id);
        GameStateQuery ExecuteMove(ExecuteMoveCommand command);

    }
}
