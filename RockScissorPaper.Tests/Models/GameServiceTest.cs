using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RockScissorPaper.Models;
using RockScissorPaper.Domain;
using RockScissorPaper.DAL;
using HilltopDigital.SimpleDAL;

namespace RockScissorPaper.Tests.Models
{
    [TestClass]
    public class GameServiceTest
    {
        [TestMethod]
        public void TestVsRockBot()
        {
            Game game = new Game(new GameRules(), new Player(), new Player());
            game.PlayerTwo.Bot = new RockBot();
            GameService service = new GameService(new GameSQLRepository(new MySQLDatabaseConnector(), new PlayerSQLRepository(new MySQLDatabaseConnector())), game);
            
            PlayerSelectionCommand c1 = new PlayerSelectionCommand(game.GameId);
            c1.PlayerOneSelection = GameSelection.Paper;
            PlayerSelectionCommand c2 = new PlayerSelectionCommand(game.GameId);
            c2.PlayerOneSelection = GameSelection.Scissor;
            PlayerSelectionCommand c3 = new PlayerSelectionCommand(game.GameId);
            c3.PlayerOneSelection = GameSelection.Rock;

            GameServiceResult r1 = service.Execute(c1);
            
            Assert.AreEqual("Paper covers Rock", r1.Message);
            Assert.AreEqual(GameStatus.NewRound, service.Status);
           
            GameServiceResult r2 = service.Execute(c2);
            Assert.AreEqual("Rock breaks Scissor", r2.Message);
            Assert.AreEqual(GameStatus.NewRound, service.Status);

            GameServiceResult r3 = service.Execute(c3);
            Assert.AreEqual("Great minds think alike", r3.Message);
            Assert.AreEqual(GameStatus.NewRound, service.Status);

            GameServiceResult r4 = service.Execute(c1);
            Assert.AreEqual("Paper covers Rock", r1.Message);
            Assert.AreEqual(GameStatus.NewRound, service.Status);

            GameServiceResult r5 = service.Execute(c1);
            Assert.AreEqual("Paper covers Rock", r1.Message);
            Assert.AreEqual(GameStatus.EndOfGame, service.Status);

            GameServiceResult r6 = service.Execute(c1);
            Assert.AreEqual(GameOutcome.Win, r6.PlayerOneOutcome);
            Assert.AreEqual(GameOutcome.Lose, r6.PlayerTwoOutcome);
            Assert.AreEqual(GameStatus.EndOfGame, service.Status);



        }
    }
}
