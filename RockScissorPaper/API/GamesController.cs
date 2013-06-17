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

namespace RockScissorPaper.API
{
    public class GamesController : ApiController
    {
        // GET api/values
        private IGameService _service;

        public GamesController(IGameService gameService)
        {
            _service = gameService;
        }
       // private static NotificationService _notificationService = new NotificationService(_gameRepository, _gameEventManager);
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        public Game Get(int id)
        {
            Game game = _service.GetGame(id);
            return game;
        }

        // POST api/values
        public int Post(CreateGameCommand command)
        {
            return _service.CreateGame(command);
        }

        // PUT api/values/5
        public GameStateQuery Put(int id, ExecuteMoveCommand command)
        {
            command.GameId = id;
            _service.ExecuteMove(command);
            GameStateQuery result = _service.GetGameState(id, command.PlayerId);
            
            return result;
        }

        // DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
    }
}