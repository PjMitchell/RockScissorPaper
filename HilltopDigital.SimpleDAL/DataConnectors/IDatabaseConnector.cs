using System.Collections.Generic;
using System.Data;

namespace HilltopDigital.SimpleDAL
{
    public interface IDatabaseConnector
    {
        /// <summary>
        /// Gets DataTable from Database using SQL
        /// </summary>
        /// <param name="sql">SQL Query or Stored proceedure name</param>
        /// <param name="sqlParams">Stored proceedure parameters</param>
        /// <returns></returns>
        DataTable Get(string sql, List<StoreProcedureParameter> sqlParams = null);

        /// <summary>
        /// Passes data to IMapper from Database using SQL
        /// </summary>
        /// <param name="sql">SQL Query or Stored proceedure name</param>
        /// <param name="output">IMapper required to map the result</param>
        /// <param name="sqlParams">Stored proceedure parameters</param>
        /// <returns></returns>
        void Get(string sql, IMapper output, List<StoreProcedureParameter> sqlParams = null);

        /// <summary>
        /// Executes NonQuery from DataBase using SQL
        /// </summary>
        /// <param name="sql">SQL Query or Stored proceedure name</param>
        /// <param name="sqlParams">Stored proceedure parameters</param>
        /// <returns></returns>
        int ExecuteNonQuery(string sql, List<StoreProcedureParameter> sqlParams = null);

        /// <summary>
        /// Returns single field result from SQL Query
        /// </summary>
        /// <param name="sql">SQL Query or Stored proceedure name</param>
        /// <param name="sqlParams">Stored proceedure parameters</param>
        /// <returns></returns>
        object ExecuteScalar(string sql, List<StoreProcedureParameter> sqlParams = null);

    }
}