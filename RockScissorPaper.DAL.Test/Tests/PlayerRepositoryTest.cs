using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RockScissorPaper.DAL;
using HilltopDigital.SimpleDAL;
using RockScissorPaper.Domain;
using System.Transactions;

namespace RockScissorPaper.DAL.Test
{
    [TestClass]
    public class PlayerRepositoryTest
    {
        [TestMethod]
        public void PlayerSQLRespositoryTest_CreateAndGet()
        {
            FakeDBService dbService = FakeDBService.GetInstance();
            using (TransactionScope scope = new TransactionScope())
            { 
            PlayerSQLRepository _repository = new PlayerSQLRepository(dbService.GetDatabaseConnector());
            
            Player testPlayer = new Player(){
            Name = "Tester Bob"
            };
            string ip = "this.fake.add.ress";
            string avatar = "fakeAvatar";
            
            testPlayer.PlayerId = _repository.CreatePlayer(testPlayer.Name, ip, avatar);
            Player testResult = _repository.GetPlayer(testPlayer.PlayerId);

            Assert.AreEqual(testPlayer.Name, testResult.Name);
            Assert.AreEqual(false, testResult.IsBot);
            
            }
            
        }
        
    }
}
