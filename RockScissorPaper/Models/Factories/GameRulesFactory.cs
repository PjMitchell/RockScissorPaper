using RockScissorPaper.Models.DataHandling;
using RockScissorPaper.Models.GameModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models
{
    public class GameRulesFactory
    {
        private IGameRepository _repository;
        private GameRules result;

        public GameRulesFactory(IGameRepository repository)
        {
            _repository = repository;
        }

        public GameRules GetGameRules(int ruleId)
        {
            return _repository.RetrieveGameRules(ruleId, this);
        }

        public GameRules GetGameRules(GameType gameType, int numberOfRounds=5, params GameRuleFactoryParameters[] args)
        {
            result = new GameRules();
            result.GameType = gameType;
            result.TotalRounds = numberOfRounds;
            SetResolvers(gameType);
            if (args.Contains(GameRuleFactoryParameters.NoDrawAllowed))
            {
                result.AllowDraw = false;
            }
            else
            {
                result.AllowDraw = true;
            }

            string s = "";

            if (args.Contains(GameRuleFactoryParameters.RandomButtonAsignment))
            {
                s = SelectionButtonOrderRandomizer.GetButtonBoxOrder(gameType);
            }
            result.ButtonBox = GameSelectorButtonBoxFactory.GetButtonBox(gameType, s);

            if (!args.Contains(GameRuleFactoryParameters.NoIndexRequired))
            {
                result.Id = _repository.RetrieveGameRuleId(result);
            }
            return result;
        }

        private void SetResolvers(GameType gametype)
        {
            switch (gametype)
            {
                case GameType.StandardGame:
                    result.GameScoreResolver = new StandardGameScoreResolver();
                    result.RoundResolver = new RoshamboResolver();
                    break;
                default :
                    result.GameScoreResolver = new StandardGameScoreResolver();
                    result.RoundResolver = new RoshamboResolver();
                    break;

            }
        }

    }
}