using System;
using RockScissorPaper.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RockScissorPaper.Tests.Models
{
    [TestClass]
    public class RoshamboResolverTest
    {
        private const string _rockWin = "Rock breaks Scissor";
        private const string _scissorWin = "Scissors cut Paper";
        private const string _paperWin = "Paper covers Rock";
        private const string _draw = "Great minds think alike";

        #region All Combos
        
        [TestMethod]
        public void TestRockVsRock()
        {
            RoshamboResolver service = new RoshamboResolver();
            service.ResolveRound(RoshamboSelection.Rock, RoshamboSelection.Rock);

            Assert.AreEqual(GameOutcome.Draw, service.PlayerOneResult);
            Assert.AreEqual(_draw, service.Message);
        }
       
        [TestMethod]
        public void TestRockVsScissor()
        {
            RoshamboResolver service = new RoshamboResolver();

            service.ResolveRound(RoshamboSelection.Rock, RoshamboSelection.Scissor);

            Assert.AreEqual(GameOutcome.Win, service.PlayerOneResult);
            Assert.AreEqual(_rockWin, service.Message);
        }
        
        [TestMethod]
        public void TestRockVsPaper()
        {
            RoshamboResolver service = new RoshamboResolver();

            service.ResolveRound(RoshamboSelection.Rock, RoshamboSelection.Paper);

            Assert.AreEqual(GameOutcome.Lose, service.PlayerOneResult);
            Assert.AreEqual(_paperWin, service.Message);
        }
        
        [TestMethod]
        public void TestScissorVsRock()
        {
            RoshamboResolver service = new RoshamboResolver();

            service.ResolveRound(RoshamboSelection.Scissor, RoshamboSelection.Rock);

            Assert.AreEqual(GameOutcome.Lose, service.PlayerOneResult);
            Assert.AreEqual(_rockWin, service.Message);
        }

        [TestMethod]
        public void TestScissorVsScissor()
        {
            RoshamboResolver service = new RoshamboResolver();

            service.ResolveRound(RoshamboSelection.Scissor, RoshamboSelection.Scissor);
            Assert.AreEqual(GameOutcome.Draw, service.PlayerOneResult);
            Assert.AreEqual(_draw, service.Message);
           
        }
        
        [TestMethod]
        public void TestScissorVsPaper()
        {
            RoshamboResolver service = new RoshamboResolver();

            service.ResolveRound(RoshamboSelection.Scissor, RoshamboSelection.Paper);

            Assert.AreEqual(GameOutcome.Win, service.PlayerOneResult);
            Assert.AreEqual(_scissorWin, service.Message);
        }
        
        [TestMethod]
        public void TestPaperVsRock()
        {
            RoshamboResolver service = new RoshamboResolver();

            service.ResolveRound(RoshamboSelection.Paper, RoshamboSelection.Rock);

            Assert.AreEqual(GameOutcome.Win, service.PlayerOneResult);
            Assert.AreEqual(_paperWin, service.Message);
        }
        
        [TestMethod]
        public void TestPaperVsScissor()
        {
            RoshamboResolver service = new RoshamboResolver();

            service.ResolveRound(RoshamboSelection.Paper, RoshamboSelection.Scissor);
            
            Assert.AreEqual(GameOutcome.Lose, service.PlayerOneResult);
            Assert.AreEqual(_scissorWin, service.Message);
        }
        
        [TestMethod]
        public void TestPaperVsPaper()
        {
            RoshamboResolver service = new RoshamboResolver();

            service.ResolveRound(RoshamboSelection.Paper, RoshamboSelection.Paper);

            Assert.AreEqual(GameOutcome.Draw, service.PlayerOneResult);
            Assert.AreEqual(_draw, service.Message);

        }

        #endregion

        [TestMethod]
        public void TestMultipleResolvesWithOneInstance()
        {
            RoshamboResolver service = new RoshamboResolver();
            service.ResolveRound(RoshamboSelection.Rock, RoshamboSelection.Scissor);
            service.ResolveRound(RoshamboSelection.Rock, RoshamboSelection.Paper);

            Assert.AreEqual(GameOutcome.Lose, service.PlayerOneResult);
            Assert.AreEqual(_paperWin, service.Message);
        }
    }
}
