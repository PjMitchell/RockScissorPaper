using System.Collections.Generic;

namespace RockScissorPaper.Model
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