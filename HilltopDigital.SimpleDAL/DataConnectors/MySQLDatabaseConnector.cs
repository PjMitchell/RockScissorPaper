using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;

namespace HilltopDigital.SimpleDAL
{
    public class MySQLDatabaseConnector : IDatabaseConnector
    {
        private readonly string _connectionString;
        private static string GetConnectionString()
        {

            var cs = ConfigurationManager.ConnectionStrings["DefaultConnection"];

            if (cs == null ||  string.IsNullOrWhiteSpace(cs.ConnectionString))
            {
                throw new ApplicationException("The app requires a connection string 'DefaultConnection'");
            }

            return cs.ConnectionString;
        }

        public MySQLDatabaseConnector()
        {
            _connectionString = GetConnectionString();
        }

        public MySQLDatabaseConnector(string connectionString)
        {
            _connectionString = GetConnectionString();
        }

        /// <summary>
        /// Gets DataTable from Database using SQL
        /// </summary>
        /// <param name="sql">SQL Query or Stored proceedure name</param>
        /// <param name="sqlParams">Stored proceedure parameters</param>
        /// <returns></returns>
        public System.Data.DataTable Get(string sql, List<StoreProcedureParameter> sqlParams = null)
        {
            List<MySqlParameter> mySqlparams = GetParameters(sqlParams);
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
        public void Get(string sql, IMapper output, List<StoreProcedureParameter> sqlParams = null)
        {
            List<MySqlParameter> mySqlparams = GetParameters(sqlParams);
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
            output.Map(dt);
            
        }

        /// <summary>
        /// Executes NonQuery from DataBase using SQL
        /// </summary>
        /// <param name="sql">SQL Query or Stored proceedure name</param>
        /// <param name="sqlParams">Stored proceedure parameters</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, List<StoreProcedureParameter> sqlParams = null)
        {
            List<MySqlParameter> mySqlparams = GetParameters(sqlParams);
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
        public object ExecuteScalar(string sql, List<StoreProcedureParameter> sqlParams = null)
        {
            List<MySqlParameter> mySqlparams = GetParameters(sqlParams);
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

        public void ExecuteNonQueryScript(string path)
        {
            List<string> list = new List<string>();
            list.Add(path);
            ExecuteNonQueryScript(list);
        }

        public void ExecuteNonQueryScript(List<string> paths)
        {
            List<string> scripts = new List<string>();
            foreach (string path in paths)
            {
                FileInfo file = new FileInfo(path);
                if (file.Exists && file.Extension == ".sql")
                {
                    string script = file.OpenText().ReadToEnd();
                    scripts.Add(script);
                }
            }

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                foreach (string script in scripts)
                {
                    using (var cmd = new MySqlCommand(script, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        /// <summary>
        /// Converts Store Proceedure Parameters to MySql Parameters
        /// </summary>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        private List<MySqlParameter> GetParameters(List<StoreProcedureParameter> sqlParams)
        {
            if (sqlParams == null)
            {
                return null;
            }
            else
            {
                List<MySqlParameter> results = new List<MySqlParameter>();
                foreach (StoreProcedureParameter para in sqlParams)
                {
                    results.Add(new MySqlParameter(para.ParameterName, para.Value));
                }
                return results;
            }

        }
        
    }
}