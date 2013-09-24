using HilltopDigital.SimpleDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaper.DAL
{
    public class SelectionVsTimeMapper : IMapper
    {
        List<SelectionVsTimeQuery> _result;

        public object Result
        {
            get { return _result; }
        }

        public void Map(DataTable dt)
        {
            _result = new List<SelectionVsTimeQuery>();
            foreach(DataRow dr in dt.Rows)
            {
                MappingHelper mapper = new MappingHelper(dr);
                DateTime time = mapper.MapDateTime("Date");
                int rock = mapper.MapInt32("Rock");
                int scissor = mapper.MapInt32("Scissor");
                int paper = mapper.MapInt32("Paper");
                _result.Add(new SelectionVsTimeQuery(time, rock, scissor, paper));
            }

        }
    }
}
