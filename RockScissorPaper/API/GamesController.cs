﻿using RockScissorPaper.Domain;
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
        public Game Get(int id)
        {
            GameService service = new GameService(_gameRepository, _gameEventManager);
            PlayerService playerService = new PlayerService(_playerRepository);
            Game game = service.GetGame(id);
            return game;
        }

        // POST api/values
        public int Post(CreateGameCommand command)
        {
            GameService service = new GameService(_gameRepository, _gameEventManager);
            return service.CreateGame(command);
        }

        // PUT api/values/5
        public GameStateQuery Put(int id, ExecuteMoveCommand command)
        {
            command.GameId = id;
            GameService service = new GameService(_gameRepository, _gameEventManager);
            service.ExecuteMove(command);
            GameStateQuery result = service.GetGameState(id, command.PlayerId);
            
            return result;
        }

        // DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
    }
}