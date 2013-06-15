using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Domain
{
    public class GameChoiceStatistic
    {
        public GameSelection Selection { get; set; }
        public double Percentage { get; set; }
        public int Number { get; set; }
    }
}