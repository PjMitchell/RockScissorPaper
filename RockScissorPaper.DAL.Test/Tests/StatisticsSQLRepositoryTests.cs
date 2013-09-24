using System;
using System.Data;
using System.Data.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RockScissorPaper.DAL.Test
{
    [TestClass]
    public class StatisticsSQLRepositoryTests
    {
        [TestMethod]
        public void CanGetTotalGamesPlayed()
        {
            FakeDBService service = FakeDBService.GetInstance();
            StatisticsSQLRepository repository = new StatisticsSQLRepository(service.GetDatabaseConnector());
            int expected = 1;

            int result = repository.GetGamesPlayed();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetSelectionVsTime_TableDimensionsAreCorrect()
        {
            FakeDBService service = FakeDBService.GetInstance();
            StatisticsSQLRepository repository = new StatisticsSQLRepository(service.GetDatabaseConnector());
            int expectedRowCount = 1;
            int expectedColumnCount = 4;


            DataTable result = repository.GetSelectionVsTime();
            int rowCount = result.Rows.Count;
            int columnCount = result.Columns.Count;

            Assert.AreEqual(expectedRowCount, rowCount);
            Assert.AreEqual(expectedColumnCount, columnCount);
        }
    }
}
