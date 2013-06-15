using System;
using RockScissorPaper.Domain;
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
            RoshamboGameResolver service = new RoshamboGameResolver();
            service.ResolveRound(GameSelection.Rock, GameSelection.Rock);

            Assert.AreEqual(GameOutcome.Draw, service.PlayerOneResult);
            Assert.AreEqual(_draw, service.Message);
        }
       
        [TestMethod]
        public void TestRockVsScissor()
        {
            RoshamboGameResolver service = new RoshamboGameResolver();

            service.ResolveRound(GameSelection.Rock, GameSelection.Scissor);

            Assert.AreEqual(GameOutcome.Win, service.PlayerOneResult);
            Assert.AreEqual(_rockWin, service.Message);
        }
        
        [TestMethod]
        public void TestRockVsPaper()
        {
            RoshamboGameResolver service = new RoshamboGameResolver();

            service.ResolveRound(GameSelection.Rock, GameSelection.Paper);

            Assert.AreEqual(GameOutcome.Lose, service.PlayerOneResult);
            Assert.AreEqual(_paperWin, service.Message);
        }
        
        [TestMethod]
        public void TestScissorVsRock()
        {
            RoshamboGameResolver service = new RoshamboGameResolver();

            service.ResolveRound(GameSelection.Scissor, GameSelection.Rock);

            Assert.AreEqual(GameOutcome.Lose, service.PlayerOneResult);
            Assert.AreEqual(_rockWin, service.Message);
        }

        [TestMethod]
        public void TestScissorVsScissor()
        {
            var service = new RoshamboGameResolver();

            service.ResolveRound(GameSelection.Scissor, GameSelection.Scissor);
            Assert.AreEqual(GameOutcome.Draw, service.PlayerOneResult);
            Assert.AreEqual(_draw, service.Message);
           
        }
        
        [TestMethod]
        public void TestScissorVsPaper()
        {
            RoshamboGameResolver service = new RoshamboGameResolver();

            service.ResolveRound(GameSelection.Scissor, GameSelection.Paper);

            Assert.AreEqual(GameOutcome.Win, service.PlayerOneResult);
            Assert.AreEqual(_scissorWin, service.Message);
        }
        
        [TestMethod]
        public void TestPaperVsRock()
        {
            RoshamboGameResolver service = new RoshamboGameResolver();

            service.ResolveRound(GameSelection.Paper, GameSelection.Rock);

            Assert.AreEqual(GameOutcome.Win, service.PlayerOneResult);
            Assert.AreEqual(_paperWin, service.Message);
        }
        
        [TestMethod]
        public void TestPaperVsScissor()
        {
            RoshamboGameResolver service = new RoshamboGameResolver();

            service.ResolveRound(GameSelection.Paper, GameSelection.Scissor);
            
            Assert.AreEqual(GameOutcome.Lose, service.PlayerOneResult);
            Assert.AreEqual(_scissorWin, service.Message);
        }
        
        [TestMethod]
        public void TestPaperVsPaper()
        {
            RoshamboGameResolver service = new RoshamboGameResolver();

            service.ResolveRound(GameSelection.Paper, GameSelection.Paper);

            Assert.AreEqual(GameOutcome.Draw, service.PlayerOneResult);
            Assert.AreEqual(_draw, service.Message);

        }

        #endregion

        [TestMethod]
        public void TestMultipleResolvesWithOneInstance()
        {
            RoshamboGameResolver service = new RoshamboGameResolver();
            service.ResolveRound(GameSelection.Rock, GameSelection.Scissor);
            service.ResolveRound(GameSelection.Rock, GameSelection.Paper);

            Assert.AreEqual(GameOutcome.Lose, service.PlayerOneResult);
            Assert.AreEqual(_paperWin, service.Message);
        }
    }
}
