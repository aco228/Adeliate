using Adeliate.Portal.Code.Cache;
using Adeliate.Portal.Filter;
using Adeliate.Portal.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adeliate.Portal.Controllers
{
  [LoginFilter]
  public class HomeController : AdliteController
  {
    public ActionResult Index()
    {
      DashboardModel model = new DashboardModel();
      return this.View("~/Views/Dashboard/Index.cshtml", model);
    }


  }
}