using RockScissorPaper.Domain;

namespace RockScissorPaper.DAL
{
    public interface IPlayerRepository
    {
        Player GetPlayer(int id);
        int CreatePlayer(string playerName, string ipAddress);
    }
}