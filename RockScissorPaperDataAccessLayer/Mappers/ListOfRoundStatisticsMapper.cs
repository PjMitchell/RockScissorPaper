using HilltopDigital.SimpleDAL;
using RockScissorPaper.Domain;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RockScissorPaper.DAL
{
    class ListOfRoundStatisticsMapper : IMapper
    {
        private List<RoundStatistic> _result;
        public object Result
        {
            get { return _result; }
        }

        public void Map(DataTable dt)
        {
            if (dt.Rows.Count >0)
            {
                _result = new List<RoundStatistic>();
                foreach (DataRow dr in dt.Rows)
                {
                    MappingHelper mh = new MappingHelper(dr);
                    RoundStatistic result = new RoundStatistic();
                    result.RoundNumber = mh.MapInt32("RoundNumber");
                    GameChoiceStatistic rock = new GameChoiceStatistic();
                    rock.Selection = GameSelection.Rock;
                    rock.Number = mh.MapInt32("Rock");
                    GameChoiceStatistic scissor = new GameChoiceStatistic();
                    scissor.Selection = GameSelection.Scissor;
                    scissor.Number = mh.MapInt32("Scissor");
                    GameChoiceStatistic paper = new GameChoiceStatistic();
                    paper.Selection = GameSelection.Paper;
                    paper.Number = mh.MapInt32("Paper");
                    result.Choices.Add(rock);
                    result.Choices.Add(scissor);
                    result.Choices.Add(paper);
                    result.TotalSelections = result.Choices.Sum(m => m.Number);
                    foreach (GameChoiceStatistic item in result.Choices)
                        {
                            item.Percentage = (double)item.Number / (double)result.TotalSelections * 100;
                        }
                    _result.Add(result);
                }
            }
        }
    }
}
