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
    public class RoshamboHub : Hub
    {
        public static int PeopleConnected { get; private set; } //Does not work as intended!!!!Pm
        private IGameRepository _repository = new GameSQLRepository(new MySQLDatabaseConnector());
        
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            
            Clients.All.broadcastMessage(name, message);
        }
        public override Task OnConnected()
        {
            PeopleConnected += 1;
            PeopleConnectedChanged();
            return base.OnConnected();
        }
        public override Task OnDisconnected()
        {
            PeopleConnected -= 1;
            PeopleConnectedChanged();
            return base.OnDisconnected();
        }

        private void PeopleConnectedChanged()
        {
            Clients.All.UpdatePeopleConnected(PeopleConnected);
        }

        public void GetInfo()
        {
            RoshamboHubViewModel view = _repository.RetrieveBotVsHumanScore();
            view.NumberOfPeopleConnected = PeopleConnected;
            Clients.Caller.refreshView(view);
        }
    }
}