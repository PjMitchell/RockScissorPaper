using System;
using RockScissorPaper.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RockScissorPaper.BLL;

namespace RockScissorPaper.Tests.Models
{
    [TestClass]
    public class RoshamboResolverTest
    {
        private const string _rockWin = "Rock breaks Scissor";
        private const string _scissorWin = "Scissors cut Paper";
        private const string _paperWin = "Paper covers Rock";
        private const string _draw = "Great minds think alike";

        
        /// <summary>
        /// Test Determines If every Posible Combination resolves Correctly
        /// </summary>
        [TestMethod]
        public void RoshamboResolver()
        {
            RoshamboGameRoundResolver resolver = new RoshamboGameRoundResolver();

            TestRockVsRock(resolver);
            TestRockVsScissor(resolver);
            TestRockVsPaper(resolver);
            TestScissorVsRock(resolver);
            TestScissorVsScissor(resolver);
            TestScissorVsPaper(resolver);
            TestPaperVsRock(resolver);
            TestPaperVsScissor(resolver);
            TestPaperVsPaper(resolver);
        }
        
        #region All Combos
        private void TestRockVsRock(RoshamboGameRoundResolver resolver)
        {
            GameRound round = new GameRound()
            {
                PlayerOneSelection = GameSelection.Rock,
                PlayerTwoSelection = GameSelection.Rock
            };
            resolver.ResolveRound(round);

            Assert.AreEqual(GameOutcome.Draw, round.PlayerOneOutcome);
            Assert.AreEqual(_draw, resolver.Message);
        }

        private void TestRockVsScissor(RoshamboGameRoundResolver resolver)
        {
            GameRound round = new GameRound()
            {
                PlayerOneSelection = GameSelection.Rock,
                PlayerTwoSelection = GameSelection.Scissor
            };
            resolver.ResolveRound(round);

            Assert.AreEqual(GameOutcome.Win, round.PlayerOneOutcome);
            Assert.AreEqual(_rockWin, resolver.Message);
        }

        private void TestRockVsPaper(RoshamboGameRoundResolver resolver)
        {
            GameRound round = new GameRound()
            {
                PlayerOneSelection = GameSelection.Rock,
                PlayerTwoSelection = GameSelection.Paper
            };

            resolver.ResolveRound(round);

            Assert.AreEqual(GameOutcome.Lose, round.PlayerOneOutcome);
            Assert.AreEqual(_paperWin, resolver.Message);
        }

        private void TestScissorVsRock(RoshamboGameRoundResolver resolver)
        {
            GameRound round = new GameRound()
            {
                PlayerOneSelection = GameSelection.Scissor,
                PlayerTwoSelection = GameSelection.Rock
            };

            resolver.ResolveRound(round);

            Assert.AreEqual(GameOutcome.Lose, round.PlayerOneOutcome);
            Assert.AreEqual(_rockWin, resolver.Message);
        }

        private void TestScissorVsScissor(RoshamboGameRoundResolver resolver)
        {
            GameRound round = new GameRound()
            {
                PlayerOneSelection = GameSelection.Scissor,
                PlayerTwoSelection = GameSelection.Scissor
            };

            resolver.ResolveRound(round);
            
            Assert.AreEqual(GameOutcome.Draw, round.PlayerOneOutcome);
            Assert.AreEqual(_draw, resolver.Message);
           
        }

        private void TestScissorVsPaper(RoshamboGameRoundResolver resolver)
        {
            GameRound round = new GameRound()
            {
                PlayerOneSelection = GameSelection.Scissor,
                PlayerTwoSelection = GameSelection.Paper
            };

            resolver.ResolveRound(round);

            Assert.AreEqual(GameOutcome.Win, round.PlayerOneOutcome);
            Assert.AreEqual(_scissorWin, resolver.Message);
        }

        private void TestPaperVsRock(RoshamboGameRoundResolver resolver)
        {
            GameRound round = new GameRound()
            {
                PlayerOneSelection = GameSelection.Paper,
                PlayerTwoSelection = GameSelection.Rock
            };

            resolver.ResolveRound(round);

            Assert.AreEqual(GameOutcome.Win, round.PlayerOneOutcome);
            Assert.AreEqual(_paperWin, resolver.Message);
        }

        private void TestPaperVsScissor(RoshamboGameRoundResolver resolver)
        {
            GameRound round = new GameRound()
            {
                PlayerOneSelection = GameSelection.Paper,
                PlayerTwoSelection = GameSelection.Scissor
            };

            resolver.ResolveRound(round);

            Assert.AreEqual(GameOutcome.Lose, round.PlayerOneOutcome);
            Assert.AreEqual(_scissorWin, resolver.Message);
        }

        private void TestPaperVsPaper(RoshamboGameRoundResolver resolver)
        {
            GameRound round = new GameRound()
            {
                PlayerOneSelection = GameSelection.Paper,
                PlayerTwoSelection = GameSelection.Paper
            };

            resolver.ResolveRound(round);

            Assert.AreEqual(GameOutcome.Draw, round.PlayerOneOutcome);
            Assert.AreEqual(_draw, resolver.Message);

        }

        #endregion
        
    }
}
