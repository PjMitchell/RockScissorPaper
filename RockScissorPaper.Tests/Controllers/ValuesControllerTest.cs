using System;
using System.IO;
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
using RockScissorPaper.DAL;
using HilltopDigital.SimpleDAL;
using RockScissorPaper.BLL;
using System.Configuration;

namespace RockScissorPaper.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {
        //[TestMethod]
        //public void Get()
        //{
        //    // Arrange
        //    GamesController controller = new GamesController();

        //    // Act
        //    IEnumerable<string> result = controller.Get();

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(2, result.Count());
        //    Assert.AreEqual("value1", result.ElementAt(0));
        //    Assert.AreEqual("value2", result.ElementAt(1));
        //}

        [TestMethod]
        public void GetById()
        {
           // string localPath = ConfigurationManager.AppSettings["LocalPath"];
            //MySQLDatabaseConnector connector = new MySQLDatabaseConnector();
            //List<string> paths = new List<string>();
           // paths.Add( localPath + "\\SQLScripts\\UNDO-DatabaseStartUpScript.sql");
            //paths.Add(localPath + "\\SQLScripts\\DatabaseStartUpScript.sql");
            //connector.ExecuteNonQueryScript(paths);
        }

        //[TestMethod]
        //public void Post()
        //{
            
        //}

        //[TestMethod]
        //public void Put()
        //{
        //     Arrange
        //    GamesController controller = new GamesController();
        //    OldGameService service = new OldGameService(new GameSQLRepository(new MySQLDatabaseConnector(), new PlayerSQLRepository(new MySQLDatabaseConnector())),new GameEventManager(), DummyGame.GetDummyGame());
        //    GameAPIPutCommand command = new GameAPIPutCommand();
        //    command.playerId = 1;
        //    command.selection = 3;

        //     Act
        //    GameStateQuery result = controller.Put(1, command);

        //     Assert
        //    Assert.AreEqual("You Win!", result.PlayerOne.PlayerMessage);
        //}

        //[TestMethod]
        //public void Delete()
        //{
        //    // Arrange
        //    GamesController controller = new GamesController();

        //    // Act
        //    controller.Delete(5);

        //    // Assert
        //}
    }
}
