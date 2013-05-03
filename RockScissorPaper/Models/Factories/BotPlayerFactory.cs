using RockScissorPaper.Models.Bots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models
{
    public class BotPlayerFactory
    {
        
        public Player GetBotPlayer(string BotType)
        {
            
            switch (BotType)
            {
                case  "SimpleBot":
                    return CreateSimpleBot();
                default: 
                    return CreateSimpleBot();
            }

           
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