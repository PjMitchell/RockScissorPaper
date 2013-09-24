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
        public void CanGetPlayer()
        {
            FakeDBService dbService = FakeDBService.GetInstance();
            PlayerSQLRepository _repository = new PlayerSQLRepository(dbService.GetDatabaseConnector());
            Player expectedPlayer = new Player() { Name = "Peter", AvatarImgFile = "Paper1.jpg" };
                
            Player testResult = _repository.GetPlayer(5);

            Assert.AreEqual(expectedPlayer.IsBot, testResult.IsBot);
            Assert.AreEqual(expectedPlayer.Name, testResult.Name);
            Assert.AreEqual(expectedPlayer.AvatarImgFile, testResult.AvatarImgFile);
            Assert.AreEqual(null, testResult.Bot);
        }

        [TestMethod]
        public void CanGetBot()
        {
            FakeDBService dbService = FakeDBService.GetInstance();
            PlayerSQLRepository _repository = new PlayerSQLRepository(dbService.GetDatabaseConnector());
            Player expectedPlayer = new Player() { Name = "Jack", AvatarImgFile = "BlueBot.jpg", Bot = new SimpleBot() };
            

            Player testResult = _repository.GetPlayer(1);

            Assert.AreEqual(expectedPlayer.IsBot, testResult.IsBot);
            Assert.AreEqual(expectedPlayer.Name, testResult.Name);
            Assert.AreEqual(expectedPlayer.AvatarImgFile, testResult.AvatarImgFile);
            Assert.AreEqual(expectedPlayer.Bot.GetType(), testResult.Bot.GetType());
           
        }

        [TestMethod]
        public void CanCreatePlayer()
        {
            FakeDBService dbService = FakeDBService.GetInstance();
            Player testPlayer = new Player(){ Name = "Tester Bob"  };
            string ip = "this.fake.add.ress";
            string avatar = "fakeAvatar";
            
            using (TransactionScope scope = new TransactionScope())
            { 
                PlayerSQLRepository _repository = new PlayerSQLRepository(dbService.GetDatabaseConnector());
            
                testPlayer.PlayerId = _repository.CreatePlayer(testPlayer.Name, ip, avatar);
                Player testResult = _repository.GetPlayer(testPlayer.PlayerId);

                Assert.AreEqual(testPlayer.Name, testResult.Name);
                Assert.AreEqual(false, testResult.IsBot);
            }
        }
        
    }
}
