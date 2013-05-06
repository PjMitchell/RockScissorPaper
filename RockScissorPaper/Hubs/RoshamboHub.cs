using Microsoft.AspNet.SignalR;
using RockScissorPaper.Models;
using RockScissorPaper.Models.DataHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RockScissorPaper.Hubs
{
    public class RoshamboHub : Hub //Does Time out while Idle
    {
        private static HashSet<string> _connectionIds { get; set ;}
        public static int PeopleConnected { get {return _connectionIds.Count; } }
        private IGameRepository _repository = new GameSQLRepository(new MySQLDatabaseConnector(), new PlayerSQLRepository(new MySQLDatabaseConnector()));
        
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            
            Clients.All.broadcastMessage(name, message);
        }
        public override Task OnConnected()
        {
            string s = Context.ConnectionId;
            if (_connectionIds == null)
                {
                    _connectionIds = new HashSet<string>();
                }
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
            if (_connectionIds != null)
            {
                lock (_connectionIds)
                {

                    if (_connectionIds.Contains(s))
                    {
                        _connectionIds.Remove(s);
                    }

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
            RoshamboHubViewInformation view = _repository.RetrieveBotVsHumanScore();
            view.NumberOfPeopleConnected = PeopleConnected;
            Clients.Caller.refreshView(view);
        }
    }
}