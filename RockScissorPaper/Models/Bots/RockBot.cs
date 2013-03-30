using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models.Bots
{
    public class RockBot : BotBase
    {
        private static string name = "Rock Bot";
        public RockBot()
            : base(name)
        {
        }

        public override RoshamboSelection Go()
        {
            return RoshamboSelection.Rock;
        }
    }
}