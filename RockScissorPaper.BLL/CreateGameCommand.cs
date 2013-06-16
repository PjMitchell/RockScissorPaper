using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaper.BLL
{
    public class CreateGameCommand
    {
        public int RuleId { get; set; }
        public int PlayerOneId { get; set; }
        public int PlayerTwoId { get; set; }
    }
}
