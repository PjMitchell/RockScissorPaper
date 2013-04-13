using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models.Statistics
{
    public class GammaFunction
    {
        public static double Gamma(uint x)
        {
            double g = 1;
            for (int i = 1; i < x; i++)
            {
                g *= i;
            }
            return g;
        }
        /// <summary>
        /// Calculates gamma function for x/y;
        /// y can only be 1 or 2 at the moment;
        /// </summary>
        /// <param name="numerator"></param>
        /// <param name="denominator"></param>
        /// <returns></returns>
        public static double Gamma(uint numerator, uint denominator)
        {
            if (numerator <= 0) return 0;
            switch (denominator)
            {
                case 0 :
                    throw new DivideByZeroException();
                case 1 :
                    return Gamma(numerator);
                case 2:
                    return Gammahalf(numerator);
                default :
                    return 0;
            }
        }

        private static double Gammahalf(uint x)
        {
            if (x % 2 == 0) return Gamma(x / 2);
            double g = Math.Sqrt(Math.PI);
            if (x <= 1) return 0;

            uint y = x / 2;
            for (int i = 1; i < x; i++)
            {
                
                g *= i;
            }
            return g;
        }

    }
}