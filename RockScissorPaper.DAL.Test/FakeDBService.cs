using HilltopDigital.SimpleDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaper.DAL.Test
{
    public class FakeDBService
    {
        private static object _lock = new object();
        private static FakeDBService _instance;

        private string _connectionstring;
        private string _dbBuiltScript = @"TestDBScripts\DatabaseStartUpScript.sql";
        private string _dbTearDownScript = @"TestDBScripts\UNDO-DatabaseStartUpScript.sql";
        private string _populateDbScript = @"TestDBScripts\PopulateFakeDB.sql";
        private IDatabaseConnector _dataConnector;
        


        public bool IsBuilt { get; private set; }

        private FakeDBService()
        {
            _connectionstring = "server=localhost; Port=3306; database=RockScissorPaperUntiTestDB; uid=root;Password=localtest;";
            _dataConnector = new MySQLDatabaseConnector(_connectionstring);
            
            TearDownDb();
            BuildDb();
        }

        public static FakeDBService GetInstance()
        {

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new FakeDBService();
                }
            }
            return _instance;
        }

        private void BuildDb()
        {
            _dataConnector.ExecuteNonQueryScript(_dbBuiltScript);
            _dataConnector.ExecuteNonQueryScript(_populateDbScript);
            IsBuilt = true;
        }

        private void TearDownDb()
        {
            _dataConnector.ExecuteNonQueryScript(_dbTearDownScript);
            IsBuilt = false;
        }

        public void ResetDb()
        {
            TearDownDb();
            BuildDb();
        }

        public IDatabaseConnector GetDatabaseConnector()
        {
            return _dataConnector;
        }

    }
}
