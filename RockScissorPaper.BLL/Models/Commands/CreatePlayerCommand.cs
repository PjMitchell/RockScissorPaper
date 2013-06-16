using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaper.BLL
{
    public class CreatePlayerCommand
    {
        public string PlayerName { get; set; }
        public string IPAddress { get; set; }
    }
}
