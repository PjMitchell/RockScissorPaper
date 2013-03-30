﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models.Bots
{
    public abstract class BotBase
    {
        private string _name;
        public string Name { get { return _name; } }

        public BotBase(string name)
        {
            _name = name;
        }

        public abstract RoshamboSelection Go();
       


    }
}