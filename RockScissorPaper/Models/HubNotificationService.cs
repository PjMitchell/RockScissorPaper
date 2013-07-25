using Microsoft.AspNet.SignalR;
using RockScissorPaper.BLL;
using RockScissorPaper.Domain;
using RockScissorPaper.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models
{
    public class HubNotificationService
    {
        static HubNotificationService _instance;
        static object _lock = new object();

        IStatisticsService _statsService;
        IGameService _gameService;
        

        private HubNotificationService(GameEventManager eventManager, IStatisticsService statsService, IGameService gameService)
        {
            eventManager.Subscribe<GameFinishedEvent>(GameFinished);
            _statsService = statsService;
            _gameService = gameService;
        }

        public static HubNotificationService Instance(GameEventManager eventManager, IStatisticsService statsService, IGameService gameService)
        {
                lock(_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new HubNotificationService(eventManager, statsService, gameService);
                    }
                }
                return _instance;
        }

        public void GameFinished(GameFinishedEvent gameFinishedEvent)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<RoshamboHub>();
            GlobalResultsHubObject view = GetCurrentGlobalResults();
            context.Clients.All.refreshView(view);

            if (gameFinishedEvent.GameResult != null)
            {
                string message;
                if (gameFinishedEvent.GameResult.PlayerTwoOutcome == GameOutcome.Win)
                {
                    message = gameFinishedEvent.GameResult.PlayerTwo.Name + " has beaten " + gameFinishedEvent.GameResult.PlayerOne.Name;
                }
                else if (gameFinishedEvent.GameResult.PlayerOneOutcome == GameOutcome.Win)
                {
                    message = gameFinishedEvent.GameResult.PlayerOne.Name + " has beaten " + gameFinishedEvent.GameResult.PlayerTwo.Name;
                }
                else
                {
                    message = gameFinishedEvent.GameResult.PlayerOne.Name + " has drawn against " + gameFinishedEvent.GameResult.PlayerTwo.Name;
                }
                context.Clients.All.newGameReport(message);
            }

        }

        private GlobalResultsHubObject GetCurrentGlobalResults()
        {
            GlobalResultsHubObject result = new GlobalResultsHubObject();
            BotvsHumanStatistics query = _statsService.GetBotVsHumanScore();
            result.BotWins = query.BotWins;
            result.HumanWins = query.HumanWins;
            result.NumberOfPeopleConnected = _gameService.GetNumberofOpenGames();
            return result;
        }
    }
}