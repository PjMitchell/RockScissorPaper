using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaper.DAL
{
    public class SelectionVsTimeQuery
    {
        public DateTime Date { get; private set; }
        
        public int Rock { get; private set; }
        public int Scissor { get; private set; }
        public int Paper { get; private set; }
        public int Sum { get; private set; }

        public double RockPercentage { get; private set; }
        public double ScissorPercentage { get; private set; }
        public double PaperPercentage { get; private set; }

        public SelectionVsTimeQuery(DateTime date)
        {
            Date = date;
            Rock = 0;
            Scissor = 0;
            Paper = 0;
            Sum = 0;

            RockPercentage = 0f;
            ScissorPercentage = 0f;
            PaperPercentage = 0f;
        }

        public SelectionVsTimeQuery(DateTime date, int rock, int scissor, int paper)
        {
            Date = date;
            Rock = rock;
            Scissor = scissor;
            Paper = paper;

            Sum = Rock + Scissor + Paper;

            RockPercentage = percentage(Rock);
            ScissorPercentage = percentage(Scissor);
            PaperPercentage = percentage(Paper);
        }

        private double percentage(int item)
        {
            return (double)item / Sum * 100;
        }

    }
}
