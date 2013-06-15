using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models
{
    public class PlayerGameInformation
    {
        public int PlayerId { get; set; }
        public GameSelection CurrentSelection { get; set; }
        public bool IsViewer { get; set; }
        public int CurrentScore { get; set; }
        public string PlayerMessage { get; set; }
    }
}