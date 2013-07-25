using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaper.BLL
{
    public class GameFinishedEvent
    {
        private GameServiceResult _gameResult;

        public GameServiceResult GameResult { get { return _gameResult; } }

        public GameFinishedEvent() { }

        public GameFinishedEvent(GameServiceResult result)
        {
            _gameResult = result;
        }

    }
}
