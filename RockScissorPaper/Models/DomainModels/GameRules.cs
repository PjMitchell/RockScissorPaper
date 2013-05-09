using RockScissorPaper.Models.GameModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models
{
    public  class GameRules
    {

        public int Id { get; set; }
        public int TotalRounds { get; set; }
        public IRoshamboResolver RoundResolver { get; set; }
        public IGameScoreResolver GameScoreResolver { get; set; }
        public bool AllowDraw { get; set; }
        public GameType GameType { get; set; }
        public GameSelectorButtonBox ButtonBox {get; set;}

        
        
    }
}