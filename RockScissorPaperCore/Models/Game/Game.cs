using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Domain
{
    public class Game
    {
        public int GameId { get; set; }
        public GameRules Rules { get; set; }
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }
        public List<GameRound> Rounds { get; set; }
        public GameStatus Status { get; set; }

        public Game()
        {
        }

        public Game(GameRules rules, Player playerOne, Player playerTwo)
        {
            Rules = rules;
            PlayerOne = playerOne;
            PlayerTwo = playerTwo;
            Rounds = new List<GameRound>();
            Status = GameStatus.NewRound;
        }
        
    }
}