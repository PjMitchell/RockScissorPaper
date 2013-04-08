using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models.DataHandling
{
    public interface IPlayerRepository
    {
        Player RetrievePlayer(int id);
        int CreatePlayer(string playerName, string ipAddress);
    }
}