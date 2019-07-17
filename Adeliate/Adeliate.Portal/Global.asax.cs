using Adeliate.Portal.Code;
using Adeliate.Portal.Code.Cache;
using Adeliate.Web;
using Adeliate.Web.Cache;
using Adeliate.Web.Hubs;
using Adeliate.Web.Stats;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Adeliate.Portal
{
  public class PortalApplication : AdliteApplication
  {

    public static CacheManager CacheManager = null;
    public static SignalUserCollection SignalUsers = null;
    public static CurrencyManager CurrencyManager = null;
    public static CurrentStatManager StatManager = null;

    protected override void Init()
    {
      Adlite.Data.Sql.Database dummy = null;
      Senti.Data.DataLayerRuntime r = new Senti.Data.DataLayerRuntime();
      log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(Server.MapPath("~/log4net.config")));

      AreaRegistration.RegisterAllAreas();
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);
      //SqlDependency.Start(System.Configuration.ConfigurationManager.ConnectionStrings["Adlite"].ConnectionString);

      PortalApplication.CurrencyManager = new CurrencyManager();
      PortalApplication.StatManager = new CurrentStatManager(PortalApplication.CurrencyManager);
      PortalApplication.SignalUsers = new SignalUserCollection();
      PortalApplication.CacheManager = new CacheManager();

      PortalApplication.CacheManager.Add(new CountryCache());
      PortalApplication.CacheManager.Add(new MobileOperatorCache());
    }


  }
}
