using System.Data;

namespace RockScissorPaper.Models.DataHandling
{
    public interface IMapper
    {
        void Map(DataTable dt);
    }
}