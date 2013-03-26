using System;
using RockScissorPaper.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RockScissorPaper.Tests.Models
{
    [TestClass]
    public class RoshamboServiceTest
    {
        private string _rockWin = "Rock breaks Scissor";
        private string _scissorWin = "Scissors cut Paper";
        private string _paperWin = "Paper covers Rock";
        private string _draw = "Great minds think alike";
        #region All Combos
        [TestMethod]
        public void TestRockVsRock()
        {
            RoshamboService service = new RoshamboService();

            service.ResolveRound(RoshamboSelection.Rock, RoshamboSelection.Rock);

            Assert.AreEqual(GameOutcome.Draw, service.PlayerOneResult);
            Assert.AreEqual(_draw, service.Message);
        }
       
        [TestMethod]
        public void TestRockVsScissor()
        {
            RoshamboService service = new RoshamboService();

            service.ResolveRound(RoshamboSelection.Rock, RoshamboSelection.Scissor);

            Assert.AreEqual(GameOutcome.Win, service.PlayerOneResult);
            Assert.AreEqual(_rockWin, service.Message);
        }
        
        [TestMethod]
        public void TestRockVsPaper()
        {
            RoshamboService service = new RoshamboService();

            service.ResolveRound(RoshamboSelection.Rock, RoshamboSelection.Paper);

            Assert.AreEqual(GameOutcome.Lose, service.PlayerOneResult);
            Assert.AreEqual(_paperWin, service.Message);
        }
        
        [TestMethod]
        public void TestScissorVsRock()
        {
            RoshamboService service = new RoshamboService();

            service.ResolveRound(RoshamboSelection.Scissor, RoshamboSelection.Rock);

            Assert.AreEqual(GameOutcome.Lose, service.PlayerOneResult);
            Assert.AreEqual(_rockWin, service.Message);
        }
        
        [TestMethod]
        public void TestScissorVsScissor()
        {
            RoshamboService service = new RoshamboService();

            service.ResolveRound(RoshamboSelection.Scissor, RoshamboSelection.Scissor);
            Assert.AreEqual(GameOutcome.Draw, service.PlayerOneResult);
            Assert.AreEqual(_draw, service.Message);
           
        }
        
        [TestMethod]
        public void TestScissorVsPaper()
        {
            RoshamboService service = new RoshamboService();

            service.ResolveRound(RoshamboSelection.Scissor, RoshamboSelection.Paper);

            Assert.AreEqual(GameOutcome.Win, service.PlayerOneResult);
            Assert.AreEqual(_scissorWin, service.Message);
        }
        
        [TestMethod]
        public void TestPaperVsRock()
        {
            RoshamboService service = new RoshamboService();

            service.ResolveRound(RoshamboSelection.Paper, RoshamboSelection.Rock);

            Assert.AreEqual(GameOutcome.Win, service.PlayerOneResult);
            Assert.AreEqual(_paperWin, service.Message);
        }
        
        [TestMethod]
        public void TestPaperVsScissor()
        {
            RoshamboService service = new RoshamboService();

            service.ResolveRound(RoshamboSelection.Paper, RoshamboSelection.Scissor);
            
            Assert.AreEqual(GameOutcome.Lose, service.PlayerOneResult);
            Assert.AreEqual(_scissorWin, service.Message);
        }
        
        [TestMethod]
        public void TestPaperVsPaper()
        {
            RoshamboService service = new RoshamboService();

            service.ResolveRound(RoshamboSelection.Paper, RoshamboSelection.Paper);

            Assert.AreEqual(GameOutcome.Draw, service.PlayerOneResult);
            Assert.AreEqual(_draw, service.Message);

        }
        #endregion

        [TestMethod]
        public void TestMultipleResolvesWithOneInstance()
        {
            RoshamboService service = new RoshamboService();
            service.ResolveRound(RoshamboSelection.Rock, RoshamboSelection.Scissor);
            service.ResolveRound(RoshamboSelection.Rock, RoshamboSelection.Paper);

            Assert.AreEqual(GameOutcome.Lose, service.PlayerOneResult);
            Assert.AreEqual(_paperWin, service.Message);
        }
    }
}
