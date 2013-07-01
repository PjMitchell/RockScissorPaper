using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Domain
{
    public class SimpleBot : BotBase
    {
        private static string name = "SimpleBot";
        private static Random _random = new Random((int)DateTime.Now.Ticks);

        public SimpleBot() :base(name)
        {
        }

        public override GameSelection Go()
        {
            lock (_random)
            {
            int i = _random.Next(1,4);
            GameSelection result = (GameSelection)i;
            return result;
            }
        }
    }
}