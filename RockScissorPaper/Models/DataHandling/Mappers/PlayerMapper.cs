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
        
        public object Result
        {
            get { return _result; }
        }

        public void Map(DataTable dt, string sqlProceedureString)
        {
            if (dt.Rows.Count == 0)
            {
                return;
            }
            MappingHelper map = new  MappingHelper(dt.Rows[0]);
            _result = new Player();
            _result.PlayerId = map.MapInt32("PlayerId");
            _result.Name = map.MapString("PlayerName");
            
        }
    }
}