using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Domain
{
    public class RockBot : BotBase
    {
        private static string name = "Rock Bot";
        public RockBot()
            : base(name)
        {
        }

        public override GameSelection Go()
        {
            return GameSelection.Rock;
        }
    }
}