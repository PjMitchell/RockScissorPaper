﻿using HilltopDigital.SimpleDAL;
using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RockScissorPaper.DAL
{
    public class GameRoundMapper : IMapper
    {
        private List<GameRound> _result;
        public object Result
        {
            get { return _result; }
        }


        public void Map(DataTable dt)
        {
            _result = new List<GameRound>();
            if (dt.Rows.Count == 0)
            {
                return;
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    MappingHelper map = new MappingHelper(dr);
                    GameRound round = new GameRound();
                    round.RoundId = map.MapInt32("GameRoundId");
                    round.RoundNumber = map.MapInt32("RoundNumber");
                    round.PlayerOneSelection = (GameSelection)map.MapInt32("PlayerOneChoice");
                    round.PlayerTwoSelection = (GameSelection)map.MapInt32("PlayerTwoChoice");
                    _result.Add(round);
                }
            }
        }
    }
}