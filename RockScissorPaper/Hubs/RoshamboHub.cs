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
        private RoshamboHubNotificationService _notificationService;

        private IStatisticsService _statsService;
        private IGameService _gameService;

        public RoshamboHub(RoshamboHubNotificationService notificationService, IStatisticsService statsService, IGameService gameService)
        {
            _statsService = statsService;
            _gameService = gameService;
            _notificationService = notificationService;
        }

        

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

        public void LoadMessageBox()
        {
            string[] messagebox = _notificationService.MessageBox.Get().ToArray();
            Clients.Caller.loadMessageBox(messagebox);
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