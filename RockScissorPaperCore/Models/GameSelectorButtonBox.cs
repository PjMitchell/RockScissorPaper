using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Core
{
    public class GameSelectorButtonBox
    {
        public string Id { get; set; }
        public List<GameSelectorButton> Buttons { get; set; }

        public GameSelectorButtonBox()
        {
            Buttons = new List<GameSelectorButton>();
        }
    }
}