using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models
{
    public class GameViewModel
    {
        public int Id { get; set; }
        public Player PlayerTwo { get; set; }
        public Player PlayerOne { get; set; }
       

        
    }
}