using Adeliate.Web.Cache;
using Adeliate.Web.Stats;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Adeliate.Web.Click
{
  public class AdliteClickApplication : AdliteApplication
  {

    #region #logging#
    private static ILog _log = null;

    protected static ILog Log
    {
      get
      {
        if (AdliteClickApplication._log == null)
          AdliteClickApplication._log = LogManager.GetLogger(typeof(AdliteClickApplication));
        return AdliteClickApplication._log;
      }
    }
    #endregion

    public static AdliteCacheManager CacheManager = null;
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

      AdliteClickApplication.CacheManager = new AdliteCacheManager();
      AdliteClickApplication.StatManager = new CurrentStatManager(null);
      Log.Debug("Application started");
    }


    protected void Application_Error(object sender, EventArgs e)
    {
      Exception exception = Server.GetLastError();
      Server.ClearError();
      Log.Fatal("Application FATAL", exception);
      int a = 1;
    }
  }
}
