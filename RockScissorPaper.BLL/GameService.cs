using RockScissorPaper.DAL;
using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaper.BLL
{
    public class GameService : IGameService
    {
        IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }
        
        
        
        public void CreateGame(CreateGameCommand command)
        {
            _gameRepository.CreateNewGame(command.PlayerOneId, command.PlayerTwoId, command.RuleId);
        }

        public IEnumerable<Domain.GameRules> GetGameRuleSets()
        {
            throw new NotImplementedException();
        }

        public GameRules GetGameRuleSetById(int id)
        {
            _gameRepository.GetGameRules
        }

        public Game GetGame(int id)
        {
            throw new NotImplementedException();
        }

        public GameStateQuery ExecuteMove(ExecuteMoveCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
