using RockScissorPaper.Models.Bots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public BotBase Bot { get; set; }
        public bool IsBot { get { return Bot != null; } }
    }
}