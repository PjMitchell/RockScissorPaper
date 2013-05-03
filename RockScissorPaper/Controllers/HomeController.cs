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
        /// <summary>
        /// replace with ninject
        /// </summary>
        private static IDatabaseConnector _connector = new MySQLDatabaseConnector();
        private static IPlayerRepository _playerRepository = new PlayerSQLRepository(_connector);
        private static IGameRepository _gameRepository = new GameSQLRepository(_connector, _playerRepository);
        private static IStatisticsRepository _statisticsRepository = new StatisticsSQLRepository(_connector); 

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

        public ActionResult GameLobby(int id)
        {
            int botid = 1;
            Player one = _playerRepository.RetrievePlayer(id);
            Player two = _playerRepository.RetrievePlayer(botid);
            GameService service = new GameService(_gameRepository, new RoshamboGame(new GameRules(), one, two));
            return RedirectToAction("Game", new { id = service.CurrentGame.GameId });
        }

        public ActionResult Game(int id)
        {
            
            GameService service = new GameService(_gameRepository, id);
            GameViewModel view = new GameViewModel();
            view.PlayerOne = _playerRepository.RetrievePlayer(service.CurrentGame.PlayerOne.PlayerId);
            view.PlayerTwo = _playerRepository.RetrievePlayer(service.CurrentGame.PlayerTwo.PlayerId);
            view.Id = service.CurrentGame.GameId;
            view.StateOfGame = service.GetGameStateViewModel(id);
            return View(view);
        }

        [OutputCache(Location=OutputCacheLocation.Server, Duration=5)]
        public ActionResult Statistics()
        {
            StatisticsOverviewViewInformation view = new StatisticsOverviewViewInformation();
            view.RoundInformation = new List<RoundStatistic>();
            for (int i = 1; i <= 5; i++)
            {
                RoundStatistic roundstat = _statisticsRepository.RetrieveRoundInformation(i);
                view.RoundInformation.Add(roundstat);
            }
            view.RoundInformation.OrderBy(r => r.RoundNumber);
            view.Overview = _statisticsRepository.RetrieveRoundSummary();
            view.GamesPlayed = _statisticsRepository.RetrieveGamesPlayed();
            return View(view);
        }
    }
}
