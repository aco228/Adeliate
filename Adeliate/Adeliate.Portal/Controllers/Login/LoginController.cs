using Adeliate.Portal.Models.Login;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adeliate.Portal.Controllers
{
  public class LoginController : AdliteController
  {

    public ActionResult Index() 
    {
      if (!AdliteContext.Current.ShouldLogin)
        return Redirect("/");

      LoginModel model = new LoginModel();
      model.RedirectUrl = "/";
      return View("~/Views/Login/Index.cshtml");
    }

    public ActionResult Logout()
    {
      if(AdliteContext.Current.Customer != null)
      {
        AdliteContext.Current.Session.SessionData.Customer = null;
        AdliteContext.Current.Session.SessionData.Update();
      }

      return Redirect("/");
    }
    
  }
}