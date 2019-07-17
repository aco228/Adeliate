using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adeliate.Portal
{
  public class AdliteApiController : AdliteController
  {
    
    public ActionResult Return(bool status, string message, object data = null)
    {
      return this.Json(new { status = status, message = message, data = data }, JsonRequestBehavior.AllowGet);
    }

    protected override void OnException(ExceptionContext filterContext)
    {
      base.OnException(filterContext);
    }

  }
}