using RockScissorPaper.Models;
using RockScissorPaper.Models.DataHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RockScissorPaper.Controllers
{
    public class GamesController : ApiController
    {
        // GET api/values
        private static IDatabaseConnector _connector = new MySQLDatabaseConnector();
        private static IPlayerRepository _playerRepository = new PlayerSQLRepository(_connector);
        private static IGameRepository _gameRepository = new GameSQLRepository(_connector, _playerRepository);
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        public GameViewModel Get(int id, int currentUserId)
        {

            GameService service = new GameService(_gameRepository, id);
            GameViewModel result = new GameViewModel();
            result.PlayerOne = _playerRepository.RetrievePlayer(service.CurrentGame.PlayerOne.PlayerId);
            result.PlayerTwo = _playerRepository.RetrievePlayer(service.CurrentGame.PlayerTwo.PlayerId);
            result.CurrentUserId = currentUserId;
            result.StateOfGame = service.GetGameStateViewModel(currentUserId);
            return result;

        }

        // POST api/values
        //public void Post()
        //{
            
        //}

        // PUT api/values/5
        public GameStateViewInformation Put(int id, GameAPIPutCommand apiCommand)
        {
            int playerId =  apiCommand.playerId;
            int selection = apiCommand.selection;
            RoshamboSelection playerSelection = (RoshamboSelection)selection;
            GameService service = new GameService(_gameRepository, id);
            if (service == null)
            {
                return null;
            }
            PlayerSelectionCommand command = new PlayerSelectionCommand(id);
            if (playerId == service.CurrentGame.PlayerOne.PlayerId)
            {
                command.PlayerOneSelection = playerSelection;
            }
            else if (playerId == service.CurrentGame.PlayerTwo.PlayerId)
            {
                command.PlayerOneSelection = playerSelection;
            }
            else
            {
                return null;
            }
            
            service.Execute(command);
            GameStateViewInformation result = service.GetGameStateViewModel(playerId);
            
            return result;
        }

        // DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
    }
}