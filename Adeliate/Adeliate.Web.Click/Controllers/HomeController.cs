using Adeliate.Web.Click.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adeliate.Web.Click.Controllers
{
  [MainContextFilter]
  public class HomeController : Controller
  {

    public ActionResult Index()
    {
      return this.Content(AdliteContext.Current.REDIRECT_URL);
    }
    
  }
}