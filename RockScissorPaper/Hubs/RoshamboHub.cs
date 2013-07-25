using HilltopDigital.SimpleDAL;
using Microsoft.AspNet.SignalR;
using RockScissorPaper.BLL;
using RockScissorPaper.DAL;
using RockScissorPaper.Domain;
using RockScissorPaper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RockScissorPaper.Hubs
{
    public class RoshamboHub : Hub //Does Time out while Idle
    {
        private readonly static HashSet<string> _connectionIds = new HashSet<string>();
        public static int PeopleConnected { get {return _connectionIds.Count; } }
        private HubNotificationService _notificationService;

        private IStatisticsService _statsService;
        private IGameService _gameService;

        public RoshamboHub(GameEventManager eventManager, IStatisticsService statsService, IGameService gameService)
        {
          //  eventManager.Subscribe<GameFinishedEvent>(GameFinished);
            _statsService = statsService;
            _gameService = gameService;
            _notificationService = HubNotificationService.Instance(eventManager, statsService, gameService);
        }

        //public void GameFinished(GameFinishedEvent gameFinishedEvent)
        //{
        //    var context = GlobalHost.ConnectionManager.GetHubContext<RoshamboHub>();
        //    GlobalResultsHubObject view = GetCurrentGlobalResults();
        //    context.Clients.All.refreshView(view);

        //    if(gameFinishedEvent.GameResult!=null)
        //    {
        //        string message;
        //        if (gameFinishedEvent.GameResult.PlayerTwoOutcome == GameOutcome.Win)
        //        {
        //            message = gameFinishedEvent.GameResult.PlayerTwo.Name + " has beaten " + gameFinishedEvent.GameResult.PlayerOne.Name;
        //        }
        //        else if (gameFinishedEvent.GameResult.PlayerOneOutcome == GameOutcome.Win)
        //        {
        //            message = gameFinishedEvent.GameResult.PlayerOne.Name + " has beaten " + gameFinishedEvent.GameResult.PlayerTwo.Name;
        //        }
        //        else
        //        {
        //            message = gameFinishedEvent.GameResult.PlayerOne.Name + " has drawn against " + gameFinishedEvent.GameResult.PlayerTwo.Name;
        //        }
        //        context.Clients.All.newGameReport(message);
        //    }

        //}

        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
        }

        public override Task OnConnected()
        {
            string s = Context.ConnectionId;
            lock(_connectionIds)
            {
                if (!_connectionIds.Contains(s))
                {
                    _connectionIds.Add(s);
                }
            }

            PeopleConnectedChanged();
            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {
            string s = Context.ConnectionId;

            lock (_connectionIds)
            {

                if (_connectionIds.Contains(s))
                {
                    _connectionIds.Remove(s);
                }

            }

            PeopleConnectedChanged();
            return base.OnDisconnected();
        }

        private void PeopleConnectedChanged()
        {
            Clients.All.UpdatePeopleConnected(PeopleConnected);
        }

        public void GetInfo()
        {
            GlobalResultsHubObject view = GetCurrentGlobalResults();
            Clients.Caller.refreshView(view);
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