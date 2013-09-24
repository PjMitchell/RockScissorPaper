using System.Collections.Generic;
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


            List<SelectionVsTimeQuery> result = repository.GetSelectionVsTime();
            int rowCount = result.Count;

            Assert.AreEqual(expectedRowCount, rowCount);
            
        }

        [TestMethod]
        public void GetSelectionVsTime_FirstRowIsCorrect()
        {
            FakeDBService service = FakeDBService.GetInstance();
            StatisticsSQLRepository repository = new StatisticsSQLRepository(service.GetDatabaseConnector());
            int expectedFirstRowRockCount = 3;
            int expectedFirstRowScissorCount = 4;
            int expectedFirstRowPaperCount = 3;
            int expectedFirstRowDayOfTheMonth = 25;


            List<SelectionVsTimeQuery> result = repository.GetSelectionVsTime();
            
            Assert.AreEqual(expectedFirstRowRockCount,result[0].Rock);
            Assert.AreEqual(expectedFirstRowScissorCount,result[0].Scissor);
            Assert.AreEqual(expectedFirstRowPaperCount,result[0].Paper);
            Assert.AreEqual(expectedFirstRowDayOfTheMonth, result[0].Date.Day);
        }
    }
}
