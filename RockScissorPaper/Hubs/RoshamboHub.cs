using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RockScissorPaper.Hubs
{
    public class RoshamboHub : Hub
    {
        private static int _peopleConnected { get; set; }
        
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            
            Clients.All.broadcastMessage(name, message);
        }
        public override Task OnConnected()
        {
            _peopleConnected += 1;
            PeopleConnectedChanged();
            return base.OnConnected();
        }
        public override Task OnDisconnected()
        {
            _peopleConnected -= 1;
            PeopleConnectedChanged();
            return base.OnDisconnected();
        }

        private void PeopleConnectedChanged()
        {
            Clients.All.UpdatePeopleConnected(_peopleConnected);
        }

        public void GetInfo()
        {
            Clients.Caller.RefreshInfo(_peopleConnected);
        }
    }
}