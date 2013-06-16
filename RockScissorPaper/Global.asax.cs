using HilltopDigital.SimpleDAL;
using Microsoft.AspNet.SignalR;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Mvc;
using RockScissorPaper.BLL;
using RockScissorPaper.DAL;
using System;
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
            kernel.Load(Assembly.GetExecutingAssembly());
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            kernel.Bind<IDatabaseConnector>().To<MySQLDatabaseConnector>();
            kernel.Bind<IGameRepository>().To<GameSQLRepository>();
            kernel.Bind<IStatisticsRepository>().To<StatisticsSQLRepository>();
            kernel.Bind<IPlayerRepository>().To<PlayerSQLRepository>();
            kernel.Bind<GameEventManager>().ToConstant(new GameEventManager());
            kernel.Bind<IPlayerService>().To<PlayerService>();

            GlobalConfiguration.Configuration.DependencyResolver = new LocalNinjectDependencyResolver(kernel);

            GlobalHost.DependencyResolver = new SignalRNinjectDependencyResolver(kernel);
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