using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models
{
    public class GameStateViewModel
    {
        public int GameId { get; set; }
        public PlayerViewState PlayerOne { get; set; }
        public PlayerViewState PlayerTwo { get; set; }
        public string RoundMessage { get; set; }
        public string BannerMessage { get; set; }

        public GameStateViewModel()
        {
            PlayerOne = new PlayerViewState();
            PlayerTwo = new PlayerViewState();
        }
    }
}