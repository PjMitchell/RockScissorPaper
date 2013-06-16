using System;
using System.Collections.Generic;
using System.Linq;
using RockScissorPaper.Domain;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaper.DAL
{
    public interface IPlayerSessionRepository
    {
        UserInfo GetCurrentUserInfo();
        void SetCurrentUserInfo(UserInfo item);
    }
}
