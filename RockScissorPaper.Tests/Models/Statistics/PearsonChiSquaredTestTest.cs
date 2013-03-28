using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using RockScissorPaper.Models.Statistics;

namespace RockScissorPaper.Tests.Models.Statistics
{
    [TestClass]
    public class PearsonChiSquaredTestTest
    {
        [TestMethod]
        public void TestKnownChiSquaredNumber()
        {
            List<PearsonChiSquaredSample> sample = new List<PearsonChiSquaredSample>();
            sample.Add(new PearsonChiSquaredSample(50, 44));
            sample.Add(new PearsonChiSquaredSample(50, 56));
            PearsonChiSquaredTest test = new PearsonChiSquaredTest(sample);

            Assert.AreEqual(1.44m, test.ChiSqaredNumber);
        }

        [TestMethod]
        public void TestKnownChiSquaredProbabilities()
        {
            List<PearsonChiSquaredSample> s1 = new List<PearsonChiSquaredSample>();
            s1.Add(new PearsonChiSquaredSample(50, 44));
            s1.Add(new PearsonChiSquaredSample(50, 56));
            PearsonChiSquaredTest t1 = new PearsonChiSquaredTest(s1);

            Assert.AreEqual(0.15m, t1.Probability);
        }
    }
}
