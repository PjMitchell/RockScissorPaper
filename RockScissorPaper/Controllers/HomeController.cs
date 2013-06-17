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
        
        private readonly IStatisticsRepository _statisticsRepository; //TODO Create StatsService
        private IPlayerService _playerService;
        private IGameService _gameService;
        

        public HomeController(IPlayerService playerService, IGameService gameService, IStatisticsRepository statisticsRepository)
        {
            _playerService = playerService;
            _gameService = gameService;
            _statisticsRepository = statisticsRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username)
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
                    IPAddress = ipAddress
                };
                int playerId = _playerService.CreatePlayer(command);
                return RedirectToAction("GameLobby", new { id = playerId });
            }
        }
        /// <summary>
        /// Player Is matched up with Oppenent
        /// </summary>
        /// <param name="id">Player Id</param>
        /// <returns></returns>
        public ActionResult GameLobby(int id)
        {
            int botId = 1;
            int ruleId = 1;
            CreateGameCommand command = new CreateGameCommand();
            command.PlayerOneId = id;
            command.PlayerTwoId = botId;
            command.RuleId = ruleId;
            int gameId = _gameService.CreateGame(command);
            return RedirectToAction("Game", new { id = gameId, currentUserId = id });
        }
        /// <summary>
        /// Game Is setup
        /// </summary>
        /// <param name="id">Game Id</param>
        /// <returns></returns>
        public ActionResult Game(int id, int currentUserId)
        {
            
            Game game = _gameService.GetGame(id);
            
            GameViewModel view = new GameViewModel();
            view.PlayerOne = _playerService.GetPlayer(game.PlayerOne.PlayerId);
            view.PlayerTwo = _playerService.GetPlayer(game.PlayerTwo.PlayerId);
            view.CurrentUserId = currentUserId;
            view.StateOfGame = _gameService.GetGameState(id, currentUserId);
            view.ButtonBox = GameSelectorButtonBoxFactory.GetButtonBox(game.Rules.GameType, game.ButtonOrder);
            return View(view);
        }

        [OutputCache(Location=OutputCacheLocation.Server, Duration=5)]
        public ActionResult Statistics()
        {
            StatisticsViewModel view = new StatisticsViewModel();
            view.RoundInformation = _statisticsRepository.GetRoundInformation();
            view.RoundInformation.OrderBy(r => r.RoundNumber);
            view.Overview = _statisticsRepository.GetRoundSummary();
            view.GamesPlayed = _statisticsRepository.GetGamesPlayed();
            return View(view);
        }
    }

}
