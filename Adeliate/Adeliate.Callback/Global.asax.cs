using Adeliate.Web;
using Adeliate.Web.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Adeliate.Callback
{
  public class AdliteCallbackApplication : System.Web.HttpApplication
  {

    public static CurrencyManager CurrencyManager = null;

    protected void Application_Start()
    {
      Adlite.Data.Sql.Database dummy = null;
      Senti.Data.DataLayerRuntime r = new Senti.Data.DataLayerRuntime();
      log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(Server.MapPath("~/log4net.config")));

      AreaRegistration.RegisterAllAreas();
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);

      AdliteCallbackApplication.CurrencyManager = new CurrencyManager();
    }
  }
}
