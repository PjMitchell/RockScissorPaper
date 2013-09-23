using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RockScissorPaper.DAL.Test
{
    [TestClass]
    public class StatisticsSQLRepositoryTests
    {
        [TestMethod]
        public void TestGetGamesPlayed()
        {
            FakeDBService service = FakeDBService.GetInstance();
            StatisticsSQLRepository repository = new StatisticsSQLRepository(service.GetDatabaseConnector());
            int expected = 0;

            int result = repository.GetGamesPlayed();

            Assert.AreEqual(expected, result);
        }
    }
}
