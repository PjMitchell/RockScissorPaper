using Ninject;
using Ninject.Web.Common;
using RockScissorPaper.Models.DataHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RockScissorPaper
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : NinjectHttpApplication
    {
        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            kernel.Bind<IGameRepository>().ToConstructor(c => new GameSQLRepository(new MySQLDatabaseConnector(), new PlayerSQLRepository(new MySQLDatabaseConnector())));
            kernel.Bind<IStatisticsRepository>().ToConstructor(c => new StatisticsSQLRepository(new MySQLDatabaseConnector()));
            kernel.Bind<IPlayerRepository>().ToConstructor(c => new PlayerSQLRepository(new MySQLDatabaseConnector()));
            return kernel;
        }

        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();
            RouteTable.Routes.MapHubs();
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}