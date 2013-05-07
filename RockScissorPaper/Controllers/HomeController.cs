using Ninject;
using Ninject.Modules;
using RockScissorPaper.Models;
using RockScissorPaper.Models.Bots;
using RockScissorPaper.Models.DataHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace RockScissorPaper.Controllers
{
    
    public class HomeController : Controller
    {
        
        private readonly IPlayerRepository _playerRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IStatisticsRepository _statisticsRepository;

        public HomeController(IPlayerRepository playerRepository, IGameRepository gameRepository, IStatisticsRepository statisticsRepository)
        {
            _playerRepository = playerRepository;
            _gameRepository = gameRepository;
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
            Player one = _playerRepository.RetrievePlayer(id);
            Player two = _playerRepository.RetrievePlayer(botid);
            GameService service = new GameService(_gameRepository, new RoshamboGame(new GameRules(), one, two));
            return RedirectToAction("Game", new { id = service.CurrentGame.GameId, currentUserId = id });
        }
        /// <summary>
        /// Game Is setup
        /// </summary>
        /// <param name="id">Game Id</param>
        /// <returns></returns>
        public ActionResult Game(int id, int currentUserId)
        {
            
            GameService service = new GameService(_gameRepository, id);
            GameViewModel view = new GameViewModel();
            view.PlayerOne = _playerRepository.RetrievePlayer(service.CurrentGame.PlayerOne.PlayerId);
            view.PlayerTwo = _playerRepository.RetrievePlayer(service.CurrentGame.PlayerTwo.PlayerId);
            view.CurrentUserId = currentUserId;
            view.StateOfGame = service.GetGameStateViewModel(id);
            return View(view);
        }

        [OutputCache(Location=OutputCacheLocation.Server, Duration=5)]
        public ActionResult Statistics()
        {
            StatisticsOverviewViewInformation view = new StatisticsOverviewViewInformation();
            view.RoundInformation = _statisticsRepository.RetrieveRoundInformation();
            view.RoundInformation.OrderBy(r => r.RoundNumber);
            view.Overview = _statisticsRepository.RetrieveRoundSummary();
            view.GamesPlayed = _statisticsRepository.RetrieveGamesPlayed();
            return View(view);
        }
    }

}
