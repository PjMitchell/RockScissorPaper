using RockScissorPaper.Models;
using RockScissorPaper.Models.DataHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RockScissorPaper.Controllers
{
    public class GamesController : ApiController
    {
        // GET api/values
        
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            
            return "value";
            
        }

        // POST api/values
        public void Post()
        {
            
        }

        // PUT api/values/5
        public GameStateViewModel Put(int id, GameAPIPutCommand apiCommand)
        {
            int playerId =  apiCommand.playerId;
            int selection = apiCommand.selection;
            RoshamboSelection playerSelection = (RoshamboSelection)selection;
            GameService service = new GameService(new GameRepository(), id);
            if (service == null)
            {
                return null;
            }
            PlayerSelectionCommand command = new PlayerSelectionCommand(id);
            if (playerId == service.CurrentGame.PlayerOne.PlayerId)
            {
                command.PlayerOneSelection = playerSelection;
            }
            else if (playerId == service.CurrentGame.PlayerTwo.PlayerId)
            {
                command.PlayerOneSelection = playerSelection;
            }
            else
            {
                return null;
            }
            service.Execute(command);
            GameStateViewModel result = service.GetGameStateViewModel(playerId);
            return result;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}