using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using RockScissorPaper.Models;
using RockScissorPaper.Domain;
using RockScissorPaper.BLL;

namespace RockScissorPaper.Tests.Models
{
    [TestClass]
    public class StandardGamerScoreResolverTest
    {
        [TestMethod]
        public void StandardGamerScoreResolver()
        {
            #region Arrange
            
            StandardGameScoreResolver resolver = new StandardGameScoreResolver();

            List<GameRound> gameP1Win = new List<GameRound>();
            GameRound r1 = new GameRound();
            r1.PlayerOneOutcome = GameOutcome.Win;
            r1.PlayerTwoOutcome = GameOutcome.Lose;
            gameP1Win.Add(r1);
            gameP1Win.Add(r1);
            gameP1Win.Add(r1);

            List<GameRound> gameP2Win = new List<GameRound>();
            GameRound r2 = new GameRound();
            r2.PlayerOneOutcome = GameOutcome.Lose;
            r2.PlayerTwoOutcome = GameOutcome.Win;
            gameP2Win.Add(r2);
            gameP2Win.Add(r2);
            gameP2Win.Add(r2);

            List<GameRound> gameDraw = new List<GameRound>();
            GameRound r3 = new GameRound();
            r3.PlayerOneOutcome = GameOutcome.Draw;
            r3.PlayerTwoOutcome = GameOutcome.Draw;
            gameDraw.Add(r1);
            gameDraw.Add(r2);
            gameDraw.Add(r3);
            
            #endregion

            resolver.ResolveGame(gameP1Win);
            Assert.AreEqual(3, resolver.PlayerOneScore);
            Assert.AreEqual(0, resolver.PlayerTwoScore);
            Assert.AreEqual(GameOutcome.Win, resolver.PlayerOneOutcome);
            Assert.AreEqual(GameOutcome.Lose, resolver.PlayerTwoOutcome);

            resolver.ResolveGame(gameP2Win);

            Assert.AreEqual(0, resolver.PlayerOneScore);
            Assert.AreEqual(3, resolver.PlayerTwoScore);
            Assert.AreEqual(GameOutcome.Lose, resolver.PlayerOneOutcome);
            Assert.AreEqual(GameOutcome.Win, resolver.PlayerTwoOutcome);

            resolver.ResolveGame(gameDraw);
            Assert.AreEqual(1, resolver.PlayerOneScore);
            Assert.AreEqual(1, resolver.PlayerTwoScore);
            Assert.AreEqual(GameOutcome.Draw, resolver.PlayerOneOutcome);
            Assert.AreEqual(GameOutcome.Draw, resolver.PlayerTwoOutcome);
        }

    }
}
