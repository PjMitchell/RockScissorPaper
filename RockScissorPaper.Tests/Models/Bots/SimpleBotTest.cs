using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RockScissorPaper.Models.Bots;
using RockScissorPaper.Models;
using RockScissorPaper.Models.Statistics;
using System.Collections.Generic;

namespace RockScissorPaper.Tests.Models.Bots
{
    [TestClass]
    public class SimpleBotTest
    {
        [TestMethod]
        public void TestBotName()
        {
            SimpleBot bot = new SimpleBot();
            Assert.AreEqual("Simple Jack", bot.Name);
            
        }
        /// <summary>
        /// Note This test will fail 1% of the time due to chance
        /// </summary>
        [TestMethod]
        public void TestRandomSlection()
        {
            SimpleBot bot = new SimpleBot();
            int countRock = 0;
            int countPaper = 0;
            int countScissor = 0;
            for (int i = 0; i < 99; i++)
            {
                RoshamboSelection selection = bot.Go();
                switch (selection)
                {
                    case RoshamboSelection.Rock:
                        countRock += 1;
                        break;
                    case RoshamboSelection.Scissor:
                        countScissor += 1;
                        break;
                    case RoshamboSelection.Paper:
                        countPaper += 1;
                        break;
                }
            }
            List<PearsonChiSquaredSample> sample = new List<PearsonChiSquaredSample>();
            sample.Add(new PearsonChiSquaredSample(33,countRock));
            sample.Add(new PearsonChiSquaredSample(33, countPaper));
            sample.Add(new PearsonChiSquaredSample(33, countScissor));
            PearsonChiSquaredTest test= new PearsonChiSquaredTest(sample);

            Assert.IsTrue(test.Probability > 0.01m);
        }

    }
}
