using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models.Bots
{
    public class SimpleBot : BotBase
    {
        private static string name = "Simple Jack";
        public SimpleBot() :base(name)
        {
        }

        public override RoshamboSelection Go()
        {
            Random random = new Random();
            int i = random.Next(1, 3);
            RoshamboSelection result = (RoshamboSelection)i;
            return base.Go();
        }
    }
}