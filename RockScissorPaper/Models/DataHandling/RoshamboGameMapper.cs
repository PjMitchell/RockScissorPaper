using RockScissorPaper.Models.Bots;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace RockScissorPaper.Models.DataHandling
{
    class RoshamboGameMapper : IMapper
    {
        public RoshamboGame Result { get; private set; }
        
        public void Map(DataTable dt)
        {
            if (dt.Rows.Count == 1)
            {

                Result.GameId = 1;//
                // Player 1 info
                Result.PlayerOne.PlayerId = 1;//
                Result.PlayerOne.Name = ""; //
                if (Result.PlayerOne.Name == "Simple Jack")
                {
                    Result.PlayerOne.Bot = new SimpleBot();
                }
                // Player 2 info
                Result.PlayerTwo.PlayerId = 1; //
                Result.PlayerTwo.Name = "";//
                if (Result.PlayerTwo.Name == "Simple Jack")
                {
                    Result.PlayerTwo.Bot = new SimpleBot();
                }

                Result.Rules = new GameRules(); //To Reimplement when more rule sets are added
                Result.Status = GameStatus.NewRound; //

            }
            throw new NotImplementedException();
        }
    }
}
