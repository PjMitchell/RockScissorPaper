using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace RockScissorPaper.Models
{
    public class GameSelectorButtonBoxFactory
    {
        public static GameSelectorButtonBox GetButtonBox(GameType gametype, string buttonArrangement="")
        {
            switch (gametype)
            {
                case GameType.StandardGame :
                    return buildStandardGame(buttonArrangement);
                default :
                    return buildStandardGame(buttonArrangement);
            }
        }
        


        private static GameSelectorButtonBox buildStandardGame(string buttonArrangement="")
        {
            
            if (!Regex.IsMatch(buttonArrangement, "RSP|RPS|PRS|PSR|SPR|SRP"))
            {
                buttonArrangement="RSP";
            }

            GameSelectorButtonBox result = new GameSelectorButtonBox();
            result.Id = buttonArrangement;
            char[] buttonArray = buttonArrangement.ToCharArray();
            foreach (char c in buttonArray)
            {
                result.Buttons.Add(loadButton(c));
            }
            return result;
        }

        /// <summary>
        /// Rock Scissor Paper
        /// </summary>
        /// <returns></returns>
        private static GameSelectorButton loadButton(char c)
        {
            switch (c)
            {
                case 'R' :
                    return new GameSelectorButton(RoshamboSelection.Rock);
                case 'S':
                    return new GameSelectorButton(RoshamboSelection.Scissor);
                case 'P' :
                    return new GameSelectorButton(RoshamboSelection.Paper);
                default :
                    return null;
            }
        }
    }
}