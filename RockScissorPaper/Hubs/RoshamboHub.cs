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
        private IGameRepository _repository = new GameSQLRepository(new MySQLDatabaseConnector(), new PlayerSQLRepository(new MySQLDatabaseConnector()));

        public RoshamboHub(GameEventManager eventManager)
        {
            eventManager.Subscribe<GameFinishedEvent>(GameFinished);
        }

        public void GameFinished(GameFinishedEvent gameFinishedEvent)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<RoshamboHub>();
            gameFinishedEvent.CurrentGlobalResults.NumberOfPeopleConnected = RoshamboHub.PeopleConnected;
            context.Clients.All.refreshView(gameFinishedEvent.CurrentGlobalResults);
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
            CurrentGlobalResults view = _repository.RetrieveBotVsHumanScore();
            view.NumberOfPeopleConnected = PeopleConnected;
            Clients.Caller.refreshView(view);
        }
    }
}