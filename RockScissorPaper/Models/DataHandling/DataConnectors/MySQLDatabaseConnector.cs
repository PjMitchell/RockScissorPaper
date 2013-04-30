using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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

            string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            if (cs == null)
            {
                throw new ApplicationException("The app requires a connection string 'DefaultConnection'");
            }

            return cs;
        }

        /// <summary>
        /// Gets DataTable from Database using SQL
        /// </summary>
        /// <param name="sql">SQL Query or Stored proceedure name</param>
        /// <param name="sqlParams">Stored proceedure parameters</param>
        /// <returns></returns>
        public System.Data.DataTable Get(string sql, List<StoreProceedureParameter> sqlParams = null)
        {
            List<MySqlParameter> mySqlparams = getParameters(sqlParams);
            DataTable dt = new DataTable();

            using (var da = new MySqlDataAdapter(sql, _connectionString))
            {
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (mySqlparams != null)
                {
                    da.SelectCommand
                        .Parameters
                        .AddRange(mySqlparams.ToArray());
                }
                da.Fill(dt);
            }

            return dt;
        }

        /// <summary>
        /// Passes data to IMapper from Database using SQL
        /// </summary>
        /// <param name="sql">SQL Query or Stored proceedure name</param>
        /// <param name="output">IMapper required to map the result</param>
        /// <param name="sqlParams">Stored proceedure parameters</param>
        /// <returns></returns>
        public void Get(string sql, IMapper output, List<StoreProceedureParameter> sqlParams = null)
        {
            List<MySqlParameter> mySqlparams = getParameters(sqlParams);
            DataTable dt = new DataTable();

            using (var da = new MySqlDataAdapter(sql, _connectionString))
            {
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (mySqlparams != null)
                {
                    da.SelectCommand
                        .Parameters
                        .AddRange(mySqlparams.ToArray());
                }
                da.Fill(dt);
            }
            output.Map(dt, sql);
            
        }

        /// <summary>
        /// Executes NonQuery from DataBase using SQL
        /// </summary>
        /// <param name="sql">SQL Query or Stored proceedure name</param>
        /// <param name="sqlParams">Stored proceedure parameters</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, List<StoreProceedureParameter> sqlParams = null)
        {
            List<MySqlParameter> mySqlparams = getParameters(sqlParams);
            int resultCount = 0;
            using (var connection = new MySqlConnection(_connectionString))
            using (var cmd = new MySqlCommand(sql, connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                if (mySqlparams != null)
                {
                    cmd.Parameters.AddRange(mySqlparams.ToArray());
                }
                resultCount = cmd.ExecuteNonQuery();
            }

            return resultCount;
        }

        /// <summary>
        /// Returns single field result from SQL Query
        /// </summary>
        /// <param name="sql">SQL Query or Stored proceedure name</param>
        /// <param name="sqlParams">Stored proceedure parameters</param>
        /// <returns></returns>
        public object GetScalar(string sql, List<StoreProceedureParameter> sqlParams = null)
        {
            List<MySqlParameter> mySqlparams = getParameters(sqlParams);
            object result;
            using (var connection = new MySqlConnection(_connectionString))
            using (var cmd = new MySqlCommand(sql, connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                if (mySqlparams != null)
                {
                    cmd.Parameters.AddRange(mySqlparams.ToArray());
                }
                result = cmd.ExecuteScalar();
            }
            return result;
        }

        /// <summary>
        /// Converts Store Proceedure Parameters to MySql Parameters
        /// </summary>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        private List<MySqlParameter> getParameters(List<StoreProceedureParameter> sqlParams)
        {
            if (sqlParams == null)
            {
                return null;
            }
            else
            {
                List<MySqlParameter> results = new List<MySqlParameter>();
                foreach (StoreProceedureParameter para in sqlParams)
                {
                    results.Add(new MySqlParameter(para.ParameterName, para.Value));
                }
                return results;
            }

        }
    }
}