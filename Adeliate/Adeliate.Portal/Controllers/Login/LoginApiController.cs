using Adeliate.Portal.Models.Login;
using Adeliate.Portal.Models.Login.Input;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adeliate.Portal.Controllers.Login
{
  public class LoginApiController : AdliteApiController
  {

    public ActionResult Login()
    {

      LoginInputModel inputModel = new LoginInputModel();

      if (inputModel.HasError)
        return Return(false, inputModel.ErrorMessage);
      

      AdliteContext.Current.Session.SessionData.Customer = inputModel.Customer;
      AdliteContext.Current.Session.SessionData.ValidUntil.AddMinutes(60);
      AdliteContext.Current.Session.SessionData.Update();

      LoginModel model = new LoginModel();
      model.RedirectUrl = "/";
      model.PredifinedUsername = inputModel.Username;

      return Return(true, "Login succesfully",model);
    }
    

  }
}