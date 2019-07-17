using Adeliate.Callback.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Adeliate.Callback.Controllers
{
  [MainContextFilter]
  public class HomeController : Controller
  {

    public ActionResult Index()
    {
      return this.Content("prc");
    }

  }
}