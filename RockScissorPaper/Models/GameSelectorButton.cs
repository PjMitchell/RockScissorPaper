using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Model
{
    public class GameSelectorButton
    {
        public GameSelection Selection {get; private set;}
        public int SelectionId { get { return (int)Selection; } }
        public string SelectionText { get { return Convert.ToString(Selection); } }

        public GameSelectorButton(GameSelection selection)
        {
            Selection = selection;
        }
    }
}