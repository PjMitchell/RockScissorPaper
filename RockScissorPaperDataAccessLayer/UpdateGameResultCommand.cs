using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaper.DAL
{
    public class UpdateGameResultCommand
    {
        public int GameId {get; set;}
        public int PlayerOneId {get; set;}
        public GameOutcome PlayerOneGameOutcome { get; set; }
        public int PlayerOneGameScore {get; set;}
        public int PlayerTwoId {get; set;}
        public GameOutcome PlayerTwoGameOutcome {get; set;}
        public int PlayerTwoGameScore {get; set;}
    }                                  
}
