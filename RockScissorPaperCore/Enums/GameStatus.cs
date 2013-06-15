using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Domain
{
    public enum GameStatus
    {
        NewRound =1,
        WaitingPlayerOne = 2,
        WaitingPlayerTwo =3,
        RoundResult = 4,
        EndOfGame = 5
    }
}