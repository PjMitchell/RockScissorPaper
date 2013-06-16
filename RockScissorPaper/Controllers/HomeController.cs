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
            int botId = 1;
            int ruleId = 1;
            GameService sevice = new GameService(_gameRepository, _gameEventManager);
            CreateGameCommand command = new CreateGameCommand();
            command.PlayerOneId = id;
            command.PlayerTwoId = botId;
            command.RuleId = ruleId;
            int gameId = sevice.CreateGame(command);
            return RedirectToAction("Game", new { id = gameId, currentUserId = id });
        }
        /// <summary>
        /// Game Is setup
        /// </summary>
        /// <param name="id">Game Id</param>
        /// <returns></returns>
        public ActionResult Game(int id, int currentUserId)
        {
            GameService service = new GameService(_gameRepository, _gameEventManager);
            Game game = service.GetGame(id);
            
            GameViewModel view = new GameViewModel();
            view.PlayerOne = _playerRepository.GetPlayer(game.PlayerOne.PlayerId);
            view.PlayerTwo = _playerRepository.GetPlayer(game.PlayerTwo.PlayerId);
            view.CurrentUserId = currentUserId;
            view.StateOfGame = service.GetGameState(id, currentUserId);
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
