using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RockScissorPaper.DAL;

namespace RockScissorPaper.Tests.Models
{
    [TestClass]
    public class SelectionVsTimeQueryTest
    {
        [TestMethod]
        public void TestEmptyConstructor()
        {
            SelectionVsTimeQuery result = new SelectionVsTimeQuery(DateTime.Now);

            Assert.AreEqual(result.Paper, 0);
            Assert.AreEqual(result.Rock, 0);
            Assert.AreEqual(result.Scissor, 0);

        }

        [TestMethod]
        public void TestConstructor()
        {
            SelectionVsTimeQuery result = new SelectionVsTimeQuery(DateTime.Now, 1, 2, 3);
            double expectedRockPercentage = 1f / 6f * 100f;
            double expectedScissorPercentage = 2f / 6f * 100f;
            double expectedPaperPercentage = 3f / 6f * 100f;
           
            Assert.AreEqual(1,result.Rock);
            Assert.AreEqual(2, result.Scissor);
            Assert.AreEqual(3, result.Paper);
            Assert.AreEqual(expectedRockPercentage, result.RockPercentage);
            Assert.AreEqual(expectedScissorPercentage, result.ScissorPercentage);
            Assert.AreEqual(expectedPaperPercentage, result.PaperPercentage);
        }
    }
}
