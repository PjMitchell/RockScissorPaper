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
        GameRepository _repository = new GameRepository();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Game()
        {
            Player one = new Player();
            one.Name = "Some Guy";
            Player two = new Player();
            two.Bot = new SimpleBot();
            two.Name = two.Bot.Name;
            RoshamboGame game = new RoshamboGame(1, new GameRules(), one, two);
            _repository.OpenGames.Add(game);

            return View();
        }
    }
}
