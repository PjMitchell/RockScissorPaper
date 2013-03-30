using RockScissorPaper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RockScissorPaper.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public GameState Get(int id, int playerId, RoshamboSelection selection)
        {
            GameService service = GameRepository.OpenGameServices.FirstOrDefault(s => s.CurrentGame.GameId == id);
            if (service == null)
            {
                return null;
            }
            lock (service)
            {
                GameServiceCommand command = new GameServiceCommand(id);
                if (playerId == service.CurrentGame.PlayerOne.PlayerId)
                {
                    command.PlayerOneSelection = selection;
                }
                else if (playerId == service.CurrentGame.PlayerTwo.PlayerId)
                {
                    command.PlayerOneSelection = selection;
                }
                else
                {
                    return null;
                }
                service.Execute(command);
                return service.GetGameState(playerId);
            }
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}