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
        PlayerSQLRepository _playerRepository = new PlayerSQLRepository(new MySQLDatabaseConnector());

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
            GameService service = new GameService(new GameSQLRepository(new MySQLDatabaseConnector()), new RoshamboGame(new GameRules(), one, two));
            GameViewModel view = new GameViewModel();
            view.PlayerOne = one;
            view.PlayerTwo = two;
            view.Id = service.CurrentGame.GameId;
            view.StateOfGame = service.GetGameStateViewModel(id);
            return View(view);
        }
    }
}
