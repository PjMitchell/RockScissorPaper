using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models
{
    public class GameAPIPutCommand
    {
        public int playerId  { get; set; }
        public int selection { get; set; }
    }
}