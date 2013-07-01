using RockScissorPaper.Domain;
using System.Collections.Generic;

namespace RockScissorPaper.DAL
{
    public interface IPlayerRepository
    {
        Player GetPlayer(int id);
        int CreatePlayer(string playerName, string ipAddress);
        List<Player> GetBotList();
    }
}