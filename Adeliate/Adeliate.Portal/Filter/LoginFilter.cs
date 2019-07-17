using Adeliate.Portal.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adeliate.Portal.Filter
{
  public class LoginFilter : ActionFilterAttribute
  {

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      if(AdliteContext.Current.ShouldLogin)
      {
        ViewResult loginResult = new ViewResult();
        LoginModel model = new LoginModel();
        model.RedirectUrl = filterContext.HttpContext.Request.RawUrl;
        loginResult.ViewData.Model = model;
        loginResult.ViewName = "~/Views/Login/Index.cshtml";
        filterContext.Result = loginResult;
        return;
      }

      if(AdliteContext.Current.Session.SessionData.ValidUntil < DateTime.Now)
      {
        ViewResult loginResult = new ViewResult();
        LoginModel model = new LoginModel();
        model.RedirectUrl = filterContext.HttpContext.Request.RawUrl;
        model.PredifinedUsername = (AdliteContext.Current.Customer != null ? AdliteContext.Current.Customer.CustomerData.Username : "");
        loginResult.ViewData.Model = model;
        loginResult.ViewName = "~/Views/Login/Index.cshtml";
        filterContext.Result = loginResult;
        return;
      }

    }

  }
}