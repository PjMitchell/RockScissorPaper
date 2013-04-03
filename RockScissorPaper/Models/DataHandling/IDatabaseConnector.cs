using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace RockScissorPaper.Models.DataHandling
{
    public interface IDatabaseConnector
    {
        public DataTable Get(string sql, List<DbParameter> sqlParams = null);
        public void Get(string sql, IMapper output, List<DbParameter> sqlParams = null);
        public int ExecuteNonQuery(string sql, List<DbParameter> sqlParams = null);
        public object GetScalar(string sql, List<DbParameter> sqlParams = null);
    }
}