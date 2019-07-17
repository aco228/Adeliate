using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adeliate.Web.Click.Controllers
{
  public class CallbackController : Controller
  {


    public ActionResult Index()
    {
      return this.Content("ok");
    }

    public ActionResult Home()
    {
      return this.Content("ok");
    }

  }
}