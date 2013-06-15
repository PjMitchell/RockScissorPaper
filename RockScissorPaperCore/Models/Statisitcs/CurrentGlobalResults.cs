using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaper.Domain
{
    public class CurrentGlobalResults
    {
        public int NumberOfPeopleConnected { get; set; }
        public int BotWins { get; set; }
        public int HumanWins { get; set; }
    }
}
