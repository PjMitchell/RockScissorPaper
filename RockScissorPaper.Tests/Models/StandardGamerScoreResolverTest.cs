using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using RockScissorPaper.Models;
using RockScissorPaper.Domain;

namespace RockScissorPaper.Tests.Models
{
    [TestClass]
    public class StandardGamerScoreResolverTest
    {
        [TestMethod]
        public void TestScoreP1Win()
        {
            List<GameRound> testSubject = new List<GameRound>();
            GameRound r1 = new GameRound();
            r1.PlayerOneOutcome = GameOutcome.Win;
            r1.PlayerTwoOutcome = GameOutcome.Lose;
            GameRound r2 = new GameRound();
            r2.PlayerOneOutcome = GameOutcome.Win;
            r2.PlayerTwoOutcome = GameOutcome.Lose;
            GameRound r3 = new GameRound();
            r3.PlayerOneOutcome = GameOutcome.Win;
            r3.PlayerTwoOutcome = GameOutcome.Lose;
            testSubject.Add(r1);
            testSubject.Add(r2);
            testSubject.Add(r3);
            StandardGameScoreResolver action = new StandardGameScoreResolver();
            action.ResolveGame(testSubject);
            Assert.AreEqual(3 , action.PlayerOneScore);
            Assert.AreEqual(0, action.PlayerTwoScore);

        }

        [TestMethod]
        public void TestScoreP2Win()
        {
            List<GameRound> testSubject = new List<GameRound>();
            GameRound r1 = new GameRound();
            r1.PlayerOneOutcome = GameOutcome.Lose;
            r1.PlayerTwoOutcome = GameOutcome.Win; 
            GameRound r2 = new GameRound();
            r2.PlayerOneOutcome = GameOutcome.Lose;
            r2.PlayerTwoOutcome = GameOutcome.Win; 
            GameRound r3 = new GameRound();
            r3.PlayerOneOutcome = GameOutcome.Lose;
            r3.PlayerTwoOutcome = GameOutcome.Win; 
            testSubject.Add(r1);
            testSubject.Add(r2);
            testSubject.Add(r3);
            StandardGameScoreResolver action = new StandardGameScoreResolver();
            action.ResolveGame(testSubject);
            Assert.AreEqual(0, action.PlayerOneScore);
            Assert.AreEqual(3, action.PlayerTwoScore);

        }

        [TestMethod]
        public void TestScoreDraw()
        {
            List<GameRound> testSubject = new List<GameRound>();
            GameRound r1 = new GameRound();
            r1.PlayerOneOutcome = GameOutcome.Lose;
            r1.PlayerTwoOutcome = GameOutcome.Win;
            GameRound r2 = new GameRound();
            r2.PlayerOneOutcome = GameOutcome.Draw;
            r2.PlayerTwoOutcome = GameOutcome.Draw;
            GameRound r3 = new GameRound();
            r3.PlayerOneOutcome = GameOutcome.Win;
            r3.PlayerTwoOutcome = GameOutcome.Lose;
            testSubject.Add(r1);
            testSubject.Add(r2);
            testSubject.Add(r3);
            StandardGameScoreResolver action = new StandardGameScoreResolver();
            action.ResolveGame(testSubject);
            Assert.AreEqual(1, action.PlayerOneScore);
            Assert.AreEqual(1, action.PlayerTwoScore);

        }
    }
}
