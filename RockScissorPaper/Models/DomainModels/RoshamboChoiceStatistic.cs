using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models
{
    public class RoshamboChoiceStatistic
    {
        public RoshamboSelection Selection { get; set; }
        public double Percentage { get; set; }
        public int Number { get; set; }
    }
}