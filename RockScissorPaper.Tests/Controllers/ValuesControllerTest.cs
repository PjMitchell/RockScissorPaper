using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RockScissorPaper;
using RockScissorPaper.Controllers;
using RockScissorPaper.Models;
using RockScissorPaper.Tests.Models;
using RockScissorPaper.Models.DataHandling;

namespace RockScissorPaper.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Arrange
            GamesController controller = new GamesController();

            // Act
            IEnumerable<string> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("value1", result.ElementAt(0));
            Assert.AreEqual("value2", result.ElementAt(1));
        }

        [TestMethod]
        public void GetById()
        {
            
        }

        [TestMethod]
        public void Post()
        {
            
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            GamesController controller = new GamesController();
            GameService service = new GameService(new GameSQLRepository(new MySQLDatabaseConnector()), DummyGame.GetDummyGame());
            GameAPIPutCommand command = new GameAPIPutCommand();
            command.playerId = 1;
            command.selection = 3;

            // Act
            GameStateViewInformation result = controller.Put(1, command);

            // Assert
            Assert.AreEqual("You Win!", result.PlayerOne.PlayerMessage);
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            GamesController controller = new GamesController();

            // Act
            controller.Delete(5);

            // Assert
        }
    }
}
