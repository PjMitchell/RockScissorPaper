using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models
{
    public class GameState
    {
        public int GameId { get; set; }
        public PlayerState PlayerOne { get; set; }
        public PlayerState PlayerTwo { get; set; }
        public string RoundMessage { get; set; }
        public string BannerMessage { get; set; }

        public GameState()
        {
            PlayerOne = new PlayerState();
            PlayerTwo = new PlayerState();
        }
    }
}