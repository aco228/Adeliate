using Adeliate.Portal.Code.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adeliate.Portal.Controllers.Hubs
{
  public class TestHubsController : Controller
  {

    public ActionResult Index()
    {
      return this.View("~/Views/Tests/Hubs.cshtml", new AdliteModelBase());
    }

    public ActionResult Send()
    {
      string text = Request["data"] != null ? Request["data"].ToString() : string.Empty;
      if (string.IsNullOrEmpty(text))
        return this.Content("data is not set");

      //PortalHub.Current.Update(text);
      return this.Content("OK");
    }

  }
}