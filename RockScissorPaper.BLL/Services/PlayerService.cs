﻿using RockScissorPaper.DAL;
using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaper.BLL
{
    public class PlayerService : IPlayerService
    {
        private IPlayerRepository _repository;
        private IPlayerSessionRepository _playerSessionRepository;

        public PlayerService(IPlayerRepository repository, IPlayerSessionRepository playerSessionRepository)
        {
            _repository = repository;
            _playerSessionRepository = playerSessionRepository;
        }

        #region Query

        public Player GetPlayer(int id)
        {
          return _repository.GetPlayer(id);
        }

        public UserInfo GetCurrentUserInfo()
        {
            return _playerSessionRepository.GetCurrentUserInfo();
        }

        public List<Player> GetBotList()
        {
            List<Player> botList = _repository.GetBotList();
            Random rnd = new Random();
            botList.RemoveAll(m => m.Bot.Name == "RockBot");
            return botList;
        }

        public Player GetRandomBot()
        {
            List<Player> botList = GetBotList();
            Random rnd = new Random();
            int i = rnd.Next(0, botList.Count);
            return botList[i];
        }
 
        #endregion

        #region Commands

        public int CreatePlayer(CreatePlayerCommand command)
        {
            Validator.ValidateObject(command, new ValidationContext(command), true);
            return _repository.CreatePlayer(command.PlayerName, command.IPAddress, command.AvatarName);
        }

        public void Login(int id)
        {
            UserInfo user = new UserInfo();
            user.Id = id;
            _playerSessionRepository.SetCurrentUserInfo(user);
        }

        public void SetCurrentGame(int gameId)
        {
           UserInfo info = _playerSessionRepository.GetCurrentUserInfo();
           if (info.CurrentGameId != gameId)
           {
              info.CurrentGameId = gameId;
              _playerSessionRepository.SetCurrentUserInfo(info);
           }
        }
       
        #endregion



        
    }
}
