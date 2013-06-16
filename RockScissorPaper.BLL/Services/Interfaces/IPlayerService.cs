using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaper.BLL
{
    public interface IPlayerService
    {
        Player GetPlayer(int id);
        void Login(int id);
        void SetCurrentGame(int gameId);
        UserInfo GetCurrentUserInfo();
        int CreatePlayer(CreatePlayerCommand command);
    }
}
