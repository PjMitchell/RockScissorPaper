using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RockScissorPaper.DAL;
using RockScissorPaper.Models;
using RockScissorPaper.Domain;
using HilltopDigital.SimpleDAL;

namespace RockScissorPaper.Tests.Models
{
    [TestClass]
    public class GameSQLRepositoryTest
    {
        [TestMethod]
        public void TestAddNewGame()
        {
            GameSQLRepository repository = new GameSQLRepository(new MySQLDatabaseConnector(), new PlayerSQLRepository(new MySQLDatabaseConnector()));
            
            int playerid = 1;
            int botid = 2;
            Player one = new Player();
            one.Name = "Some Guy";
            one.PlayerId = playerid;
            Player two = new Player();
            two.Bot = new SimpleBot();
            two.Name = two.Bot.Name;
            two.PlayerId = botid;
            Game game = new Game(new GameRules(), one, two);
            
            repository.CreateNewGame(game);
            
            Assert.AreNotEqual(0, game.GameId);
        }

        [TestMethod]
        public void TestChangeStatus()
        {

            GameSQLRepository repository = new GameSQLRepository(new MySQLDatabaseConnector(), new PlayerSQLRepository(new MySQLDatabaseConnector()));
            repository.UpdateGameStatus(1, GameStatus.RoundResult);

            //To Determine no Execptions are thrown
        }
        [TestMethod]
        public void TestNewRound()
        {

            GameSQLRepository repository = new GameSQLRepository(new MySQLDatabaseConnector(), new PlayerSQLRepository(new MySQLDatabaseConnector()));
            
            int i = repository.CreateRound(5,1);
            
            Assert.AreNotEqual(0, i);
        }
        [TestMethod]
        public void TestNewGameRoundResult()
        {

            GameSQLRepository repository = new GameSQLRepository(new MySQLDatabaseConnector(), new PlayerSQLRepository(new MySQLDatabaseConnector()));

            repository.CreateGameRoundResult(1,1,1, GameSelection.Rock);

            //To Determine no Execptions are thrown
        }
           
        
    }
}
