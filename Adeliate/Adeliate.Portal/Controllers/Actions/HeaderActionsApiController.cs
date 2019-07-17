using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adeliate.Portal.Controllers.Actions
{
  public class HeaderActionsApiController : AdliteApiController
  {

    public ActionResult ReloadCache()
    {
      if (AdliteContext.Current.AdliteCache == null)
        return this.Return(false, "Cache null error");

      AdliteContext.Current.AdliteCache.Load();
      return this.Return(true, "Cache is successfully reloaded");
    }

    public ActionResult RestartApplication()
    {
      if (!MenuManager.CheckAccess(CustomerWeight.Administrator))
        return this.Return(false, "You dont have access");
      
      HttpRuntime.UnloadAppDomain();
      return this.Return(true, "Application will be restarted.");
    }

  }
}