using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RockScissorPaper.Models.DataHandling;
using RockScissorPaper.Models;
using RockScissorPaper.Models.Bots;

namespace RockScissorPaper.Tests.Models
{
    [TestClass]
    public class GameSQLRepositoryTest
    {
        [TestMethod]
        public void TestAddNewGame()
        {
            GameSQLRepository repository = new GameSQLRepository(new MySQLDatabaseConnector());
            
            int playerid = 1;
            int botid = 2;
            Player one = new Player();
            one.Name = "Some Guy";
            one.PlayerId = playerid;
            Player two = new Player();
            two.Bot = new SimpleBot();
            two.Name = two.Bot.Name;
            two.PlayerId = botid;
            RoshamboGame game = new RoshamboGame(new GameRules(), one, two);
            
            repository.CreateNewGame(game);
            
            Assert.AreNotEqual(0, game.GameId);
        }

        [TestMethod]
        public void TestChangeStatus()
        {

            GameSQLRepository repository = new GameSQLRepository(new MySQLDatabaseConnector());
            repository.UpdateGameStatus(1, GameStatus.RoundResult);

            //To Determine no Execptions are thrown
        }
        [TestMethod]
        public void TestNewRound()
        {

            GameSQLRepository repository = new GameSQLRepository(new MySQLDatabaseConnector());
            
            int i = repository.CreateRound(5,1);
            
            Assert.AreNotEqual(0, i);
        }
           
        
    }
}
