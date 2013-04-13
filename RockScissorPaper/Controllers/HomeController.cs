using RockScissorPaper.Models;
using RockScissorPaper.Models.Bots;
using RockScissorPaper.Models.DataHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RockScissorPaper.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// replace with ninject
        /// </summary>
        private static IDatabaseConnector _connector = new MySQLDatabaseConnector();
        private static IPlayerRepository _playerRepository = new PlayerSQLRepository(_connector);
        private static IGameRepository _gameRepository = new GameSQLRepository(_connector);
        private static IStatisticsRepository _statisticsRepository = new StatisticsSQLRepository(_connector); 

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string username)
        {
            if (username == null || username =="")
            {
                return View();
            }
            else
            {
                
                string ipAddress = Request.UserHostAddress;
                int playerId = _playerRepository.CreatePlayer(username, ipAddress);
                return RedirectToAction("Game", new { id = playerId });
            }
        }

        public ActionResult Game(int id)
        {
            
            int botid = 2;
            Player one = _playerRepository.RetrievePlayer(id);
            Player two = new Player();
            two.Bot = new SimpleBot();
            two.Name = two.Bot.Name;
            two.PlayerId = botid;
            GameService service = new GameService(_gameRepository, new RoshamboGame(new GameRules(), one, two));
            GameViewModel view = new GameViewModel();
            view.PlayerOne = one;
            view.PlayerTwo = two;
            view.Id = service.CurrentGame.GameId;
            view.StateOfGame = service.GetGameStateViewModel(id);
            return View(view);
        }
        public ActionResult Statistics()
        {
            StatisticsViewModelFactory factory = new StatisticsViewModelFactory(_statisticsRepository);
            StatisticsViewModel view = factory.Result;
            return View(view);
        }
    }
}
