using RockScissorPaper.Models.GameModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models
{
    public  class GameRules
    {
        private byte _totalRounds;
        private IRoshamboResolver _roundResolver;
        private IGameScoreResolver _gameScoreResolver;
        private bool _allowDraw;

        public int TotalRounds { get { return _totalRounds; } }
        public IRoshamboResolver RoundResolver { get { return _roundResolver; } }
        public IGameScoreResolver GameScoreResolver { get { return _gameScoreResolver; } }
        public bool AllowDraw { get { return _allowDraw; } }


        public GameRules()
        {
            _totalRounds = 5;
            _roundResolver = new RoshamboResolver();
            _gameScoreResolver = new StandardGameScoreResolver();
            _allowDraw = true;
        }

        public GameRules(byte numberofrounds = 5, bool allowDraw = true, IRoshamboResolver roshamboResolver = null, IGameScoreResolver gameScoreResolver=null)
        {
            _totalRounds = numberofrounds;
            _roundResolver = roshamboResolver ?? new RoshamboResolver();
            _gameScoreResolver = gameScoreResolver ?? new StandardGameScoreResolver();
            _allowDraw = allowDraw;
        }
        
    }
}