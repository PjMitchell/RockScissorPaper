using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Model
{
    public class GameAPIPutCommand
    {
        public int PlayerId { get; set; }
        public int Selection { get; set; }
    }
}