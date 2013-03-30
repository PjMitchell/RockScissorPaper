using RockScissorPaper.Models;
using RockScissorPaper.Models.Bots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaper.Tests.Models
{
    class DummyGame
    {
        /// <summary>
        /// Returns Game with Id 1, standard rules, player one (id=1) and player two as Rockbot (id=2)
        /// </summary>
        /// <returns></returns>
        public static RoshamboGame GetDummyGame()
        {
            return new RoshamboGame(1, new GameRules(), DummyPlayerOne(), DummyBot());
        }

        /// <summary>
        /// Returns Fixed Player called "RandomGuy" whose Id is 1
        /// </summary>
        /// <returns></returns>
        public static Player DummyPlayerOne()
        {
            Player player = new Player();
            player.Name = "RandomGuy";
            player.PlayerId = 1;
            return player;
        }

        /// <summary>
        /// Returns Fixed Rock Bot  whose Id is 2
        /// </summary>
        /// <returns></returns>
        public static Player DummyBot()
        {
            Player player = new Player();
            RockBot rockbot = new RockBot();
            player.Name = rockbot.Name;
            player.PlayerId = 2;
            player.Bot = rockbot;
            return player;
        }
    }
}
