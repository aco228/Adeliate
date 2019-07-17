using Adeliate.Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adeliate.Portal
{
  public class AdliteController : Controller
  {

    private AdliteContext _context = null;

    public AdliteContext Context
    {
      get
      {
        if (this._context != null)
          return this._context;
        this._context = AdliteContext.Current;
        return this._context;
      }
    }

    protected ActionResult ErrorView(string title, string message)
    {
      ErrorModelBase model = new ErrorModelBase();
      model.Code = "8";
      model.Title = title;
      model.ErrorDescription = message;

      return this.View("~/Views/Error/Index.cshtml", model);
    }
      


  }
}