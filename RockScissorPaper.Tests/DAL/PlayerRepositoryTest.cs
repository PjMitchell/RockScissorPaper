using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RockScissorPaper.DAL;
using HilltopDigital.SimpleDAL;
using RockScissorPaper.Domain;

namespace RockScissorPaper.Tests
{
    [TestClass]
    public class PlayerRepositoryTest
    {
        [TestMethod]
        public void PlayerSQLRespositoryTest_CreateAndGet()
        {
            PlayerSQLRepository _repository = new PlayerSQLRepository(new MySQLDatabaseConnector());

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
