using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.BLL
{
    public class SelectionButtonOrderRandomizer
    {
        private enum StandardGameButtonOrder
        {
            RSP = 1, RPS = 2, PRS = 3, PSR = 4, SPR = 5, SRP = 6
        }

        public static string GetButtonBoxOrder(GameType gametype)
        {

            switch (gametype)
            {

                case GameType.StandardGame:
                        Random rnd = new Random();
                        StandardGameButtonOrder order = (StandardGameButtonOrder)rnd.Next(1, 7);
                        return Convert.ToString(order);
                 default:
                    return "RSP";
            }
        }
    }
}