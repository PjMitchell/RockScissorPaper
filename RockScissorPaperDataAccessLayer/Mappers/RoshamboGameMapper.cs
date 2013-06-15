using RockScissorPaper.Domain;
using System.Data;

namespace RockScissorPaper.DAL
{
    class RoshamboGameMapper : IMapper
    {
        private Game _result;
        
        public object Result { get {return _result;} }
       
        public RoshamboGameMapper()
        {
            _result = new Game();
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
            
            // Player 2 info
            _result.PlayerTwo = new Player();
            _result.PlayerTwo.PlayerId = map.MapInt32("PlayerTwoId");
            

            _result.Rules = new GameRules(); 
            _result.Rules.Id = map.MapInt32("RuleSet");
            int statusInt = map.MapInt32("GameStatus");
            _result.Status = (GameStatus)statusInt;

            }
        
    }
}
