using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models
{
    public class GameStateViewInformation
    {
        public int GameId { get; set; }
        public PlayerStateViewInformation PlayerOne { get; set; }
        public PlayerStateViewInformation PlayerTwo { get; set; }
        public string RoundMessage { get; set; }
        public string BannerMessage { get; set; }

        public GameStateViewInformation()
        {
            PlayerOne = new PlayerStateViewInformation();
            PlayerTwo = new PlayerStateViewInformation();
        }
    }
}