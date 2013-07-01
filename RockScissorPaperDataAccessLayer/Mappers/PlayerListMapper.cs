using HilltopDigital.SimpleDAL;
using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaper.DAL
{
    public class PlayerListMapper : IMapper
    {
        public object Result
        {
            get { return _results; }
        }

        private List<Player> _results;
        private BotPlayerFactory _botFactory;

        public void Map(DataTable dt)
        {
            if (dt.Rows.Count == 0)
            {
                return;
            }

            _results = new List<Player>();
            foreach (DataRow dr in dt.Rows)
            {
                Player result;
                MappingHelper map = new MappingHelper(dr);
                if (map.MapBool("IsBot"))
                {
                    _botFactory = new BotPlayerFactory();
                    result = _botFactory.GetBotPlayer(map.MapString("BotType"));
                }
                else
                {
                    result = new Player();

                }
                result.PlayerId = map.MapInt32("PlayerId");
                result.Name = map.MapString("PlayerName");
                result.AvatarImgFile = map.MapString("AvatarImg");
                _results.Add(result);
            }
        }
    }
}
