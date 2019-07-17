using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Adeliate.Callback.Code
{
  public class MainContextFilter : ActionFilterAttribute
  {

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      AdliteContext context = AdliteContext.Current;
      PostbackRequest request = new PostbackRequest(-1, PostbackRequestType.In, context.RawUrl, null, true, DateTime.Now, DateTime.Now);

      if (context.HasError)
      {
        request.IsSuccessful = false;
        request.Note = context.ErrorMessage;
        request.Insert();

        filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        return;
      }

      request.Insert();

      foreach (AdlitePostback postback in context.Managers)
        postback.Call(context);

      new Thread(() => {
        
      }).Start();


      filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.OK);
    }

  }
}