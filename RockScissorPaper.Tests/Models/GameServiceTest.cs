using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RockScissorPaper.Models;
using RockScissorPaper.Models.GameModels;
using RockScissorPaper.Models.Bots;

namespace RockScissorPaper.Tests.Models
{
    [TestClass]
    public class GameServiceTest
    {
        [TestMethod]
        public void TestVsRockBot()
        {
            RoshamboGame game = new RoshamboGame(1, new GameRules(), new Player(), new Player());
            game.PlayerTwo.Bot = new RockBot();
            GameService service = new GameService(game);
            
            GameServiceCommand c1 = new GameServiceCommand(game.GameId);
            c1.PlayerOneSelection = RoshamboSelection.Paper;
            GameServiceCommand c2 = new GameServiceCommand(game.GameId);
            c2.PlayerOneSelection = RoshamboSelection.Scissor;
            GameServiceCommand c3 = new GameServiceCommand(game.GameId);
            c3.PlayerOneSelection = RoshamboSelection.Rock;

            GameServiceResult r1 = service.Execute(c1);
            
            Assert.AreEqual("Paper covers Rock", r1.Message);
            Assert.AreEqual(GameServiceStatus.NewRound, service.Status);
           
            GameServiceResult r2 = service.Execute(c2);
            Assert.AreEqual("Rock breaks Scissor", r2.Message);
            Assert.AreEqual(GameServiceStatus.NewRound, service.Status);

            GameServiceResult r3 = service.Execute(c3);
            Assert.AreEqual("Great minds think alike", r3.Message);
            Assert.AreEqual(GameServiceStatus.NewRound, service.Status);

            GameServiceResult r4 = service.Execute(c1);
            Assert.AreEqual("Paper covers Rock", r1.Message);
            Assert.AreEqual(GameServiceStatus.NewRound, service.Status);

            GameServiceResult r5 = service.Execute(c1);
            Assert.AreEqual("Paper covers Rock", r1.Message);
            Assert.AreEqual(GameServiceStatus.EndOfGame, service.Status);

            GameServiceResult r6 = service.Execute(c1);
            Assert.AreEqual(GameOutcome.Win, r6.PlayerOneOutcome);
            Assert.AreEqual(GameOutcome.Lose, r6.PlayerTwoOutcome);
            Assert.AreEqual(GameServiceStatus.EndOfGame, service.Status);



        }
    }
}
