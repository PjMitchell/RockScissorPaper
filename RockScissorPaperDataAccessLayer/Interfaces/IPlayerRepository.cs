using RockScissorPaper.Core;

namespace RockScissorPaper.DataAccessLayer
{
    public interface IPlayerRepository
    {
        Player RetrievePlayer(int id);
        int CreatePlayer(string playerName, string ipAddress);
    }
}