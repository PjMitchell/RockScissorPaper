﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models.DataHandling
{
    public class RoundStatisticsMapper : IMapper
    {
        private RoundStatistic _result;
        
        public object Result
        {
            get { return _result; }
        }

        public void Map(DataTable dt, string sqlProceedureString)
        {
            _result = new RoundStatistic();
            foreach (DataRow dr in dt.Rows)
            {
                MappingHelper map = new MappingHelper(dr);
                RoshamboChoiceStatistic result = new RoshamboChoiceStatistic();
                result.Selection = (RoshamboSelection)map.MapInt32("SelectionId");
                result.Number = map.MapInt32("Count");
                _result.Choices.Add(result);
            }

            _result.TotalSelections = _result.Choices.Sum(m => m.Number);
            foreach (RoshamboChoiceStatistic item in _result.Choices)
            {
                item.Percentage = (double)item.Number / (double)_result.TotalSelections * 100;
            }

        }
    }
}