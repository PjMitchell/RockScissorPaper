using HilltopDigital.SimpleDAL;
using RockScissorPaper.Domain;
using System.Data;
using System.Linq;

namespace RockScissorPaper.DAL
{
    public class RoundStatisticsMapper : IMapper
    {
        private RoundStatistic _result;
        
        public object Result
        {
            get { return _result; }
        }

        public void Map(DataTable dt)
        {
            _result = new RoundStatistic();
            foreach (DataRow dr in dt.Rows)
            {
                MappingHelper map = new MappingHelper(dr);
                GameChoiceStatistic result = new GameChoiceStatistic();
                result.Selection = (GameSelection)map.MapInt32("SelectionId");
                result.Number = map.MapInt32("Count");
                _result.Choices.Add(result);
            }

            _result.TotalSelections = _result.Choices.Sum(m => m.Number);
            foreach (GameChoiceStatistic item in _result.Choices)
            {
                item.Percentage = (double)item.Number / (double)_result.TotalSelections * 100;
            }

        }
    }
}