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
        private RoshamboGame _result;
        
        public object Result { get {return _result;} }
       
        public RoshamboGameMapper()
        {
            _result = new RoshamboGame();
        }
        public void Map(DataTable dt)
        {
           if (dt.Rows.Count == 0)
            {
                _result = null;
                return;
            }
            MappingHelper map = new MappingHelper(dt.Rows[0]);

            _result.GameId = map.MapInt32("RoshamboGameId");
            // Player 1 info
            _result.PlayerOne = new Player();
            _result.PlayerOne.PlayerId = map.MapInt32("PlayerOneId");
            _result.PlayerOne.Name = map.MapString("PlayerOneName");
            if (_result.PlayerOne.Name == "Simple Jack")
            {
                _result.PlayerOne.Bot = new SimpleBot();
            }
            // Player 2 info
            _result.PlayerTwo = new Player();
            _result.PlayerTwo.PlayerId = map.MapInt32("PlayerTwoId");
            _result.PlayerTwo.Name = map.MapString("PlayerTwoName");
            if (_result.PlayerTwo.Name == "Simple Jack")
            {
                _result.PlayerTwo.Bot = new SimpleBot();
            }

            _result.Rules = new GameRules(); //To Reimplement when more rule sets are added
            int statusInt = map.MapInt32("GameStatus");
            _result.Status = (GameStatus)statusInt;

            }
        
    }
}
