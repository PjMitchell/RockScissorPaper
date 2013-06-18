using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models
{
    public class GlobalResultsHubObject
    {
        public int NumberOfPeopleConnected { get; set; }
        public int BotWins { get; set; }
        public int HumanWins { get; set; }
    }
}