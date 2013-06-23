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
using RockScissorPaper.Model;
using System.Web.Mvc;

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
        public GameStateQuery Put(int id, GameAPIPutCommand command)
        {
            ExecuteMoveCommand moveCommand = new ExecuteMoveCommand
            {
                GameId = command.GameId,
                PlayerId = command.PlayerId,
                Selection = (GameSelection)command.Selection
            };
            command.GameId = id;
            GameStatus status = _service.ExecuteMove(moveCommand);
            GameStateQuery result = new GameStateQuery();
            if (status == GameStatus.EndOfGame)
            {
                result = _service.GetEndOfGame(id, command.PlayerId);
            }
            else
            {
                result = _service.GetLastestRoundResult(id, command.PlayerId);
                if (status == GameStatus.FinalRoundResult)
                {
                    _service.ExecuteMove(moveCommand);
                }
            }
            
            return result;
        }

    }
}