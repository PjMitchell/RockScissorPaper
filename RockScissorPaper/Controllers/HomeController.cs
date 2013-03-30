using RockScissorPaper.Models;
using RockScissorPaper.Models.Bots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RockScissorPaper.Controllers
{
    public class HomeController : Controller
    {
        

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Game()
        {
            GameRepository.Reset();
            int playerid = 1;
            int botid = 2;
            Player one = new Player();
            one.Name = "Some Guy";
            one.PlayerId = playerid;
            Player two = new Player();
            two.Bot = new SimpleBot();
            two.Name = two.Bot.Name;
            two.PlayerId = botid;
            GameService service = new GameService(new RoshamboGame(1, new GameRules(), one, two));
            GameRepository.Add(service);
            GameViewModel view = new GameViewModel();
            view.PlayerOne = one;
            view.PlayerTwo = two;
            view.Id = service.CurrentGame.GameId;
            view.StateOfGame = service.GetGameState(playerid);
            return View(view);
        }
    }
}
