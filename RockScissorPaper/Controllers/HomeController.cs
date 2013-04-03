﻿using RockScissorPaper.Models;
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
        

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Game()
        {
            int playerid = 1;
            int botid = 2;
            Player one = new Player();
            one.Name = "Some Guy";
            one.PlayerId = playerid;
            Player two = new Player();
            two.Bot = new SimpleBot();
            two.Name = two.Bot.Name;
            two.PlayerId = botid;
            GameService service = new GameService(new GameRepository(), new RoshamboGame(new GameRules(), one, two));
            GameViewModel view = new GameViewModel();
            view.PlayerOne = one;
            view.PlayerTwo = two;
            view.Id = service.CurrentGame.GameId;
            view.StateOfGame = service.GetGameStateViewModel(playerid);
            return View(view);
        }
    }
}
