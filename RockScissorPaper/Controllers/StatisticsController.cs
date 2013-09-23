using RockScissorPaper.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace RockScissorPaper.Controllers
{
    public class StatisticsController : Controller
    {
        private IStatisticsService _statsService;

        public StatisticsController(IStatisticsService statsService)
        {
            _statsService = statsService;
        }
        
        [OutputCache(Location = OutputCacheLocation.Server, Duration = 5)]
        public ActionResult Index()
        {
            StatisticsOverviewQuery view = _statsService.GetOverview();
            return View(view);
        }

    }
}
