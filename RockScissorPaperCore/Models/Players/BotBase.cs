﻿
namespace RockScissorPaper.Domain
{
    public abstract class BotBase
    {
        private string _name;
        public string Name { get { return _name; } }

        public BotBase(string name)
        {
            _name = name;
        }

        public abstract GameSelection Go();
       


    }
}