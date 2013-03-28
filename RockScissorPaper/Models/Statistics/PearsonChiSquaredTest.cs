using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models.Statistics
{
    public class PearsonChiSquaredTest
    {
        private int degreesOfFreedom;
        private int totalNumberOfObservedResults;
        private int totalNumberOfExpectedResults;
        private List<PearsonChiSquaredSample> sample;
        private decimal probability;
        private decimal chiSqaredNumber;
        private static readonly decimal[,] PROBABILITYTABLE = new decimal[11, 8]
        {
            {0.10m,     0.05m,	0.025m,	0.02m, 	0.01m,	0.005m,	0.0025m,	0.001m}, //Probabilities
            {2.71m,	    3.84m,	5.02m,	5.41m,	6.63m, 	7.88m,	9.14m,	    10.83m}, // 1 Degree of freedom
            {4.61m,	    5.99m,	7.38m,	7.82m,	9.21m, 	10.60m,	11.98m,	    13.82m}, // 2 Degrees of freedom etc..
            {6.25m,	    7.81m,	9.35m,	9.84m,	11.34m, 12.84m,	14.32m,	    16.27m}, // 3
            {7.78m,	    9.49m,	11.14m,	11.67m,	13.23m, 14.86m,	16.42m,	    18.47m}, //4
            {9.24m,	    11.07m,	12.83m,	13.33m,	15.09m, 16.75m,	18.39m,	    20.51m}, //5
            {10.64m,	12.53m,	14.45m,	15.03m,	16.81m, 13.55m,	20.25m,	    22.46m}, //6
            {12.02m,	14.07m,	16.01m,	16.62m,	18.48m,	20.28m,	22.04m,	    24.32m}, //7
            {13.36m,	15.51m,	17.53m,	18.17m,	20.09m,	21.95m,	23.77m,	    26.12m}, //8
            {14.68m,	16.92m,	19.02m,	19.63m,	21.67m,	23.59m,	25.46m,	    27.83m}, //9
            {15.99m,	18.31m,	20.48m,	21.16m,	23.21m,	25.19m,	27.11m,	    29.59m} //10
        };
        


        public decimal ChiSqaredNumber { get { return chiSqaredNumber; } }
        public decimal Probability { get { return probability; } }

        public PearsonChiSquaredTest(List<PearsonChiSquaredSample> testSample)
        {
            totalNumberOfExpectedResults = testSample.Sum(s => s.Expected);
            totalNumberOfObservedResults = testSample.Sum(s => s.Observed);
            if (totalNumberOfExpectedResults != totalNumberOfObservedResults)
            {
                InvalidOperationException e = new InvalidOperationException("The total number of expected results must match the total number of observed results");
                throw e;
            }
            degreesOfFreedom = testSample.Count - 1;
            if (degreesOfFreedom > 10)
            {
                InvalidOperationException e = new InvalidOperationException("This method cannot handle more that 10 degrees of freedom");
                throw e;
            }
            sample = testSample;
            getChiSqaredNumber();
            getProbability();

        }

        private void getChiSqaredNumber()
        {
            chiSqaredNumber = 0;
            foreach( PearsonChiSquaredSample item in sample)
            {
                decimal d = item.Expected-item.Observed;
                d = d*d;
                chiSqaredNumber += d/item.Expected;
            }
        }

        private void getProbability()
        {
            for (int i = 8; i < 0; i--)
            {
                if (PROBABILITYTABLE[degreesOfFreedom, i] >= chiSqaredNumber)
                {
                    probability = PROBABILITYTABLE[0, i];
                    return;
                }
            }
            probability = 0.15m;
        }
    }
}