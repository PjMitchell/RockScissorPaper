using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models.DataHandling
{
    public class RoundStatisticsMapper : IMapper
    {
        private List<RoshamboChoiceStatistic> _result;
        
        public object Result
        {
            get { return _result; }
        }

        public void Map(DataTable dt, string sqlProceedureString)
        {
            _result = new List<RoshamboChoiceStatistic>();
            foreach (DataRow dr in dt.Rows)
            {
                MappingHelper map = new MappingHelper(dr);
                RoshamboChoiceStatistic result = new RoshamboChoiceStatistic();
                result.Selection = (RoshamboSelection)map.MapInt32("SelectionId");
                result.Number = map.MapInt32("Count");
                _result.Add(result);
            }

            int total = _result.Sum(m => m.Number);
            foreach (RoshamboChoiceStatistic item in _result)
            {
                item.Percentage = (double)item.Number / (double)total * 100;
            }

        }
    }
}