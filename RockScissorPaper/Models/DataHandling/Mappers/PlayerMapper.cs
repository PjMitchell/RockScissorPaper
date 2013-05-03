using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models.DataHandling
{
    public class PlayerMapper : IMapper
    {
        private Player _result;
        private BotPlayerFactory _botFactory;
        public object Result
        {
            get { return _result; }
        }

        public void Map(DataTable dt)
        {
            if (dt.Rows.Count == 0)
            {
                return;
            }
            MappingHelper map = new  MappingHelper(dt.Rows[0]);
            if (map.MapBool("IsBot"))
            {
                _botFactory = new BotPlayerFactory();
                _result = _botFactory.GetBotPlayer(map.MapString("BotType"));
                _result.Name = map.MapString("PlayerName");
                
            }
            else
            {
                _result = new Player();
                _result.PlayerId = map.MapInt32("PlayerId");
                _result.Name = map.MapString("PlayerName");
            }
            _result.PlayerId = map.MapInt32("PlayerId");
            
        }
    }
}