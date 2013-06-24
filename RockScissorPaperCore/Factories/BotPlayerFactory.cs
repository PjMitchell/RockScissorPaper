using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Domain
{
    public class BotPlayerFactory
    {
        public Player GetBotPlayer(string botType)
        {
            switch (botType)
            {
                case  "SimpleBot":
                    return CreateSimpleBot();
                case "RockBot":
                    return CreateRockBot();
                default: 
                    return CreateSimpleBot();
            }
        }

        private Player CreateRockBot()
        {
            Player result = new Player();
            result.Bot = new RockBot();
            result.Name = result.Bot.Name;
            return result;
        }

        private Player CreateSimpleBot()
        {
            Player result = new Player();
            result.Bot = new SimpleBot();
            result.Name = result.Bot.Name;
            return result;
        }
    } 
}