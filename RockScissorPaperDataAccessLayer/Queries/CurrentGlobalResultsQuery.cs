using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaper.DataAccessLayer
{
    public class CurrentGlobalResultsQuery
    {
        public int NumberOfPeopleConnected { get; set; }
        public int BotWins { get; set; }
        public int HumanWins { get; set; }
    }
}
