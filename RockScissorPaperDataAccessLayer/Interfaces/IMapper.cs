using System.Data;

namespace RockScissorPaper.DataAccessLayer
{
    /// <summary>
    /// Interface For Database mapper class.
    /// Map method will map the provided datatable into desired object;
    /// </summary>
    public interface IMapper
    {
        object Result { get; }
        
        /// <summary>
        /// Maps Datatable to object
        /// Can use Proceedure string for function.
        /// </summary>
        /// <param name="dt"></param>
        void Map(DataTable dt);
    }
}