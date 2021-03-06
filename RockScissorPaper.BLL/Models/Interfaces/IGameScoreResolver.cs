﻿using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.BLL
{
    public interface IGameScoreResolver
    {
        int PlayerOneScore { get; }
        GameOutcome PlayerOneOutcome { get; }
        GameOutcome PlayerTwoOutcome { get; }
        int PlayerTwoScore { get; }
        void ResolveGame(List<GameRound> rounds);
    }
}