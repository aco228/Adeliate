using Adeliate.Core.Implementation.Affiliate.Outputs;
using Adeliate.Web.Click.Code.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Adeliate.Web.Click.Code
{
  public class MainContextFilter : ActionFilterAttribute
  {
    private AdliteContext _context = null;

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      this._context = AdliteContext.Current;

      if (this._context.RawUrl.ToLower().Contains("callback"))
      {
        this.CallbackHandler();
        return;
      }

      if(this._context.HasError)
      {
        this.Error(filterContext);
        return;
      }
      
      BeforeClickOutput affiliteBeforeClickOutput = this._context.Affiliate.BeforeClick(this._context.Click);
      if (!affiliteBeforeClickOutput.ShouldRedirect)
      {
        if (affiliteBeforeClickOutput.DefaultRedirect)
        {
          AdliteContext.Insert(this._context);
          this.Error(filterContext);
          return;
        }
        else
        {
          this._context.ErrorMessage = "Affiliate redirection";
          this._context.ClickInformation.RedirectUrl = affiliteBeforeClickOutput.RedirectUrl;
          this._context.REDIRECT_URL = affiliteBeforeClickOutput.RedirectUrl;
          AdliteContext.Insert(this._context);

          filterContext.Result = new RedirectResult(affiliteBeforeClickOutput.RedirectUrl);
          return;
        }
      }

      new Thread(() => {
        this._context.Affiliate.OnClick(this._context.Click);
      }).Start();

      AdliteContext.Insert(this._context);
      this._context.REDIRECT_URL = this._context.Click.RedirectUrl;
      filterContext.Result = new RedirectResult(this._context.Click.RedirectUrl);
      return;
    }

    public void CallbackHandler()
    {
      string[] callbackParams = this._context.RawUrl.Split('/');
      if (callbackParams.Length <= 2)
        return;

      switch (callbackParams[2].ToLower())
      {
        case "campaign":
          string key = callbackParams.Length > 3 ? callbackParams[3] : string.Empty;
          if (string.IsNullOrEmpty(key))
            return;

          OfferCache cache = AdliteClickApplication.CacheManager.Get(key);
          if (cache == null)
            return;

          cache.Reload();
          break;
        default:
          break;
      }
    }

    public void Error(ActionExecutingContext filterContext)
    {
      ViewResult errorResult = new ViewResult();
      errorResult.ViewName = "~/Views/Error.cshtml";
      filterContext.Result = errorResult;
    }


  }
}