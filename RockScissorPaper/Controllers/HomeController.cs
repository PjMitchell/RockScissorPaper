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
        
        private readonly IPlayerRepository _playerRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IStatisticsRepository _statisticsRepository;
        private readonly GameEventManager _gameEventManager;
        private readonly NotificationService _notificationService;
        

        public HomeController(IPlayerRepository playerRepository, IGameRepository gameRepository, IStatisticsRepository statisticsRepository)
        {
            _playerRepository = playerRepository;
            _gameRepository = gameRepository;
            _statisticsRepository = statisticsRepository;
            _gameEventManager = new GameEventManager();
            _notificationService = new NotificationService(_gameRepository, _gameEventManager);
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
                int playerId = _playerRepository.CreatePlayer(username, ipAddress);
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
            int botid = 1;
            Player one = _playerRepository.GetPlayer(id);
            Player two = _playerRepository.GetPlayer(botid);
            GameRulesFactory _factory = new GameRulesFactory(_gameRepository);
            GameRules gameRules = _factory.GetGameRules(GameType.StandardGame, GameRuleFactoryParameters.RandomButtonAsignment);
            gameRules.ButtonBox = GameSelectorButtonBoxFactory.GetButtonBox(gameRules.GameType, SelectionButtonOrderRandomizer.GetButtonBoxOrder(gameRules.GameType));
            GameService service = new GameService(_gameRepository,_gameEventManager, new Game(gameRules, one, two));

            return RedirectToAction("Game", new { id = service.CurrentGame.GameId, currentUserId = id });
        }
        /// <summary>
        /// Game Is setup
        /// </summary>
        /// <param name="id">Game Id</param>
        /// <returns></returns>
        public ActionResult Game(int id, int currentUserId)
        {
            
            GameService service = new GameService(_gameRepository,_gameEventManager, id);
            GameViewModel view = new GameViewModel();
            view.PlayerOne = _playerRepository.GetPlayer(service.CurrentGame.PlayerOne.PlayerId);
            view.PlayerTwo = _playerRepository.GetPlayer(service.CurrentGame.PlayerTwo.PlayerId);
            view.CurrentUserId = currentUserId;
            view.StateOfGame = service.GetGameStateViewModel(currentUserId);
            view.ButtonBox = service.CurrentGame.Rules.ButtonBox;
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
