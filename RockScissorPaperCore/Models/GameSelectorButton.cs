using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Domain
{
    public class GameSelectorButton
    {
        public RoshamboSelection Selection {get; private set;}
        public int SelectionId { get { return (int)Selection; } }
        public string SelectionText { get { return Convert.ToString(Selection); } }

        public GameSelectorButton(RoshamboSelection selection)
        {
            Selection = selection;
        }
    }
}