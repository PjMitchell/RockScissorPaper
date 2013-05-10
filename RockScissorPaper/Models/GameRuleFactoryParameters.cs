using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models
{
    public enum GameRuleFactoryParameters
    {
        Null = 0,
        NoDrawAllowed =1, 
        RandomButtonAsignment=2,
        NoIndexRequired =3
    }
}