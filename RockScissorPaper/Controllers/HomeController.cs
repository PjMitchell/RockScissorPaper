using Ninject;
using Ninject.Modules;
using RockScissorPaper.Domain;
using RockScissorPaper.DAL;
using RockScissorPaper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using RockScissorPaper.BLL;

namespace RockScissorPaper.Controllers
{
    
    public class HomeController : Controller
    {
        
        private IPlayerService _playerService;
        private IGameService _gameService;
        private IStatisticsService _statsService;

        public HomeController(IPlayerService playerService, IGameService gameService, IStatisticsService statsService)
        {
            _playerService = playerService;
            _gameService = gameService;
            _statsService = statsService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string avatar)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return View();
            }
            else
            {
                string ipAddress = Request.UserHostAddress;
                CreatePlayerCommand command = new CreatePlayerCommand
                {
                    PlayerName = username,
                    IPAddress = ipAddress,
                    AvatarName = avatar
                };
                int playerId = _playerService.CreatePlayer(command);
                _playerService.Login(playerId);
                return RedirectToAction("GameLobby");
            }
        }
        /// <summary>
        /// Player Is matched up with Oppenent
        /// </summary>
        /// <param name="id">Player Id</param>
        /// <returns></returns>
        public ActionResult GameLobby()
        {
            int botId = (_playerService.GetRandomBot()).PlayerId;
            int ruleId = 1;
            CreateGameCommand command = new CreateGameCommand();
            UserInfo info = _playerService.GetCurrentUserInfo();
            if (info == null)
            {
                return RedirectToAction("Index");
            }
            command.PlayerOneId = info.Id;
            command.PlayerTwoId = botId;
            command.RuleId = ruleId;
            int gameId = _gameService.CreateGame(command);
            _playerService.SetCurrentGame(gameId);
            return RedirectToAction("Game", new { id = gameId});
        }
        /// <summary>
        /// Game Is setup
        /// </summary>
        /// <param name="id">Game Id</param>
        /// <returns></returns>
        public ActionResult Game(int id)
        {
            UserInfo info = _playerService.GetCurrentUserInfo();
            Game game = _gameService.GetGame(id);
            
            GameViewModel view = new GameViewModel();
            view.PlayerOne = _playerService.GetPlayer(game.PlayerOne.PlayerId);
            view.PlayerTwo = _playerService.GetPlayer(game.PlayerTwo.PlayerId);
            view.CurrentUserId = info.Id;
            view.StateOfGame = _gameService.GetCurrentState(id, info.Id);
            view.ButtonBox = GameSelectorButtonBoxFactory.GetButtonBox(game.Rules.GameType, game.ButtonOrder);
            return View(view);
        }

        [OutputCache(Location=OutputCacheLocation.Server, Duration=5)]
        public ActionResult Statistics()
        {
            StatisticsOverviewQuery view = _statsService.GetOverview();
            return View(view);
        }
    }

}
