using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models
{
    public interface IRoshamboResolver
    {
        GameOutcome PlayerOneResult { get; }
        GameOutcome PlayerTwoResult { get; }
        string Message { get; }
        void ResolveRound(GameRound round);
    }
}