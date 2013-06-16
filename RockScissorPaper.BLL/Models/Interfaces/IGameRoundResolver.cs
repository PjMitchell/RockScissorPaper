using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.BLL
{
    public interface IGameRoundResolver
    {
        GameOutcome PlayerOneResult { get; }
        GameOutcome PlayerTwoResult { get; }
        string Message { get; }
        void ResolveRound(GameRound round);
    }
}