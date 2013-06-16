using RockScissorPaper.DAL;
using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaper.BLL
{
    public class PlayerService : IPlayerService
    {
        private IPlayerRepository _repository;

        public PlayerService(IPlayerRepository repository)
        {
            _repository = repository;
        }

        #region Query

        public Player GetPlayer(int id)
        {
          return _repository.GetPlayer(id);
        }
 
        #endregion

        #region Commands

        public int CreatePlayer(CreatePlayerCommand command)
        {

            return _repository.CreatePlayer(command.PlayerName, command.IPAddress);
        }
        #endregion




    }
}
