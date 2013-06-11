using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Core.Bots
{
    public class SimpleBot : BotBase
    {
        private static string name = "Simple Jack";
        private static Random _random = new Random((int)DateTime.Now.Ticks);

        public SimpleBot() :base(name)
        {
        }

        public override RoshamboSelection Go()
        {
            lock (_random)
            {
            int i = _random.Next(1,4);
            RoshamboSelection result = (RoshamboSelection)i;
            return result;
            }
        }
    }
}