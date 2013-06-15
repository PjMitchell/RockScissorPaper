using RockScissorPaper.Domain;

namespace RockScissorPaper.DAL
{
    public interface IPlayerRepository
    {
        Player RetrievePlayer(int id);
        int CreatePlayer(string playerName, string ipAddress);
    }
}