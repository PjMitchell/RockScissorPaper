using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models
{
    public class GameStateQuery
    {
        public int GameId { get; set; }
        public PlayerGameInformation PlayerOne { get; set; }
        public PlayerGameInformation PlayerTwo { get; set; }
        public string RoundMessage { get; set; }
        public string BannerMessage { get; set; }
        public GameStatus Status { get; set; }
        public bool FinalRoundResult { get; set; }

        public GameStateQuery()
        {
            PlayerOne = new PlayerGameInformation();
            PlayerTwo = new PlayerGameInformation();
            FinalRoundResult = false;
        }
    }
}