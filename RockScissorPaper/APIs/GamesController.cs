using RockScissorPaper.Domain;
using RockScissorPaper.DAL;
using RockScissorPaper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HilltopDigital.SimpleDAL;
using RockScissorPaper.BLL;

namespace RockScissorPaper.Controllers
{
    public class GamesController : ApiController
    {
        // GET api/values
        private static IDatabaseConnector _connector = new MySQLDatabaseConnector();
        private static IPlayerRepository _playerRepository = new PlayerSQLRepository(_connector);
        private static IGameRepository _gameRepository = new GameSQLRepository(_connector, _playerRepository);
        private GameEventManager _gameEventManager;

        public GamesController(GameEventManager gameEventManager)
        {
            _gameEventManager = gameEventManager;
        }
       // private static NotificationService _notificationService = new NotificationService(_gameRepository, _gameEventManager);
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        public GameViewModel Get(int id, int currentUserId)
        {

            GameService service = new GameService(_gameRepository, _gameEventManager, id);
            GameViewModel result = new GameViewModel();
            result.PlayerOne = _playerRepository.GetPlayer(service.CurrentGame.PlayerOne.PlayerId);
            result.PlayerTwo = _playerRepository.GetPlayer(service.CurrentGame.PlayerTwo.PlayerId);
            result.CurrentUserId = currentUserId;
            result.StateOfGame = service.GetGameStateViewModel(currentUserId);
            return result;

        }

        // POST api/values
        //public void Post()
        //{
            
        //}

        // PUT api/values/5
        public GameStateQuery Put(int id, GameAPIPutCommand apiCommand)
        {
            int playerId =  apiCommand.playerId;
            int selection = apiCommand.selection;
            GameSelection playerSelection = (GameSelection)selection;
            GameService service = new GameService(_gameRepository, _gameEventManager, id);
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
            GameStateQuery result = service.GetGameStateViewModel(playerId);
            
            return result;
        }

        // DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
    }
}