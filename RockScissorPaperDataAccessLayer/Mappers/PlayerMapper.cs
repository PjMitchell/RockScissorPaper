using HilltopDigital.SimpleDAL;
using RockScissorPaper.Domain;
using System.Data;

namespace RockScissorPaper.DAL
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
            }
            else
            {
                _result = new Player();
               
            }
            _result.PlayerId = map.MapInt32("PlayerId");
            _result.Name = map.MapString("PlayerName");
            _result.AvatarImgFile = map.MapString("AvatarImg");
            
        }
    }
}