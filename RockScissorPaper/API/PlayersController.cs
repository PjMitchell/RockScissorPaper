using RockScissorPaper.BLL;
using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace RockScissorPaper.API
{
    public class PlayersController : ApiController
    {
        IPlayerService _service;

        public PlayersController(IPlayerService service)
        {
            _service = service;
        }
        
        public Player Get(int id)
        {
            return _service.GetPlayer(id);
        }

        public int Post(string name)
        {
            string ip = ((HttpContextBase)Request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            CreatePlayerCommand command = new CreatePlayerCommand();
            command.IPAddress = ip;
            command.PlayerName = name;
            return _service.CreatePlayer(command);
        }
    }
}
