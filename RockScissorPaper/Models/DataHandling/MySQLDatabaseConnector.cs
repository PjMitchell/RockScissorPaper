using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models.DataHandling
{
    public class MySQLDatabaseConnector : IDatabaseConnector
    {
        private static readonly string _connectionString = GetConnectionString();

        private static string GetConnectionString()
        {

            string cs = PrivateValues.GetConnectionString();

            if (cs == null)
            {
                throw new ApplicationException("The app requires a connection string named 'TestDbConnection'!");
            }

            return cs.ConnectionString;
        }
        
        public System.Data.DataTable Get(string sql, List<DbParameter> sqlParams = null)
        {
            throw new NotImplementedException();
        }

        public void Get(string sql, IMapper output, List<DbParameter> sqlParams = null)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(string sql, List<DbParameter> sqlParams = null)
        {
            throw new NotImplementedException();
        }

        public object GetScalar(string sql, List<DbParameter> sqlParams = null)
        {
            throw new NotImplementedException();
        }
    }
}