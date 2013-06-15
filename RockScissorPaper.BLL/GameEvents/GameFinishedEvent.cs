using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaper.BLL
{
    public class GameFinishedEvent
    {
        public string message;

        public GameFinishedEvent(string msg)
        {
            message = msg;
        }
    }
}
