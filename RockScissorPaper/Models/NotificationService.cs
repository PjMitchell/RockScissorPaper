using Microsoft.AspNet.SignalR;
using RockScissorPaper.Hubs;
using RockScissorPaper.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RockScissorPaper.Domain;
using RockScissorPaper.BLL;

namespace RockScissorPaper.Models
{
    
    public class NotificationService
    {
        IHubContext _context;
        IGameRepository _gameRepository;
        public GameEventManager GameEventManager;

        public NotificationService(IGameRepository repository, GameEventManager _gameEventManager)
        {
            _context = GlobalHost.ConnectionManager.GetHubContext<RoshamboHub>();
            _gameRepository = repository;
            _gameEventManager.Subscribe<GameFinishedEvent>(m => GameFinished());
        }
        
        public void GameFinished()
        {
            CurrentGlobalResults view = _gameRepository.GetBotVsHumanScore();
            view.NumberOfPeopleConnected = RoshamboHub.PeopleConnected;
            _context.Clients.All.refreshView(view);
        }


    }
}