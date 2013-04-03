using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models.DataHandling
{
    public interface IGameRepository
    {
        void Reset();

        void AddGame(RoshamboGame game);

        RoshamboGame GetGame(int id);
    }
}