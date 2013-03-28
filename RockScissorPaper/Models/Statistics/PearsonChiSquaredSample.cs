using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models.Statistics
{
    public class PearsonChiSquaredSample
    {
        private int _expected;
        private int _observed;
        public int Expected { get { return _expected; } }
        public int Observed { get { return _observed; } }

        public PearsonChiSquaredSample(int expected, int observed)
        {
            _expected = expected;
            _observed = observed;
        }
    }
}