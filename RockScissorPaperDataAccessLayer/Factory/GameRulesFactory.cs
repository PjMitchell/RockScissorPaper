using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.DAL
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
             _repository.GetGameRules(ruleId, this);
             result.Id = ruleId;
             return result;
        }

        public GameRules GetGameRules(GameType gameType, params GameRuleFactoryParameters[] args)
        {
            return GetGameRules(gameType, 5, "", args);
        }

        public GameRules GetGameRules(GameType gameType,  int numberOfRounds, params GameRuleFactoryParameters[] args)
        {
            return GetGameRules(gameType, numberOfRounds, "", args);
        }

        public GameRules GetGameRules(GameType gameType, string buttonBoxOrder, params GameRuleFactoryParameters[] args)
        {
            return GetGameRules(gameType, 5, buttonBoxOrder, args);
        }

        public GameRules GetGameRules(GameType gameType, int numberOfRounds, string buttonBoxOrder, params GameRuleFactoryParameters[] args)
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


            if (args.Contains(GameRuleFactoryParameters.RandomButtonAsignment))
            {
                buttonBoxOrder = SelectionButtonOrderRandomizer.GetButtonBoxOrder(gameType);
            }
            result.ButtonBox = GameSelectorButtonBoxFactory.GetButtonBox(gameType, buttonBoxOrder);

            if (!args.Contains(GameRuleFactoryParameters.NoIndexRequired))
            {
                result.Id = _repository.GetGameRuleId(result);
            }
            return result;
        }

        private void SetResolvers(GameType gametype)
        {
            switch (gametype)
            {
                case GameType.StandardGame:
                    result.GameScoreResolver = new StandardGameScoreResolver();
                    result.RoundResolver = new RoshamboGameResolver();
                    break;
                default :
                    result.GameScoreResolver = new StandardGameScoreResolver();
                    result.RoundResolver = new RoshamboGameResolver();
                    break;

            }
        }

    }
}