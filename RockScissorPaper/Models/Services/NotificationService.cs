using Microsoft.AspNet.SignalR;
using RockScissorPaper.Hubs;
using RockScissorPaper.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models
{
    
    public class NotificationService
    {
        IHubContext _context;
        IGameRepository _gameRepository;

        public NotificationService(IGameRepository repository)
        {
            _context = GlobalHost.ConnectionManager.GetHubContext<RoshamboHub>();
            _gameRepository = repository;
        }
        
        public void GameFinished()
        {
            CurrentGlobalResultsQuery view = _gameRepository.RetrieveBotVsHumanScore();
            view.NumberOfPeopleConnected = RoshamboHub.PeopleConnected;
            _context.Clients.All.refreshView(view);
        }
    }
}