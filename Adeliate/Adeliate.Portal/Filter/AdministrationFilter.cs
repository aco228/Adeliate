using Adeliate.Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adeliate.Portal.Filter
{
  public class AdministrationFilter : ActionFilterAttribute
  {

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      if(AdliteContext.Current.Customer.Weight != CustomerWeight.Administrator)
      {
        ViewResult errorResult = new ViewResult();
        ErrorModelBase model = new ErrorModelBase();
        model.ErrorTitle = "Permission error";
        model.ErrorDescription = "You dont have permission";
        errorResult.ViewData.Model = model;
        errorResult.ViewName = "~/Views/Error/Index.cshtml";
        filterContext.Result = errorResult;
        return;
      }
    }

  }
}