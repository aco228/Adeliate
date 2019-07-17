using Adeliate.Core;
using Adlite.Data;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Callback.Code
{

  public class AdliteContext
  {

    #region #logging#
    private static ILog _log = null;

    protected static ILog Log
    {
      get
      {
        if (AdliteContext._log == null)
          AdliteContext._log = LogManager.GetLogger(typeof(AdliteContext));
        return AdliteContext._log;
      }
    }
    #endregion

    protected static readonly string AdliteHttpContextItemKey = "AdliteHttpContextItemKey";
    protected static readonly string AdliteLocalizationItemKey = "AdliteLocalizationItemKey";

    public static AdliteContext Current
    {
      get
      {
        HttpContext httpContext = HttpContext.Current;
        return AdliteContext.GetCurrent(httpContext);
      }
    }

    public static AdliteContext GetExisting()
    {
      HttpContext httpContext = HttpContext.Current;
      return httpContext.Items[AdliteHttpContextItemKey] as AdliteContext;
    }

    public static AdliteContext GetCurrent(HttpContext httpContext)
    {
      lock (httpContext.Request)
      {
        AdliteContext adliteContext = httpContext.Items[AdliteHttpContextItemKey] as AdliteContext;
        if (adliteContext != null)
          return adliteContext as AdliteContext;

        adliteContext = new AdliteContext(httpContext);
        httpContext.Items[AdliteHttpContextItemKey] = adliteContext;
        return adliteContext;
      }
    }

    public AdliteContext(HttpContext context)
    {
      this._httpContext = context;
      this.InitializeSession(context);
    }

    private bool _hasError = false;
    private string _errorMessage = string.Empty;
    private string _rawUrl = string.Empty;
    private string _actionName = string.Empty;
    private HttpContext _httpContext = null;
    private Click _click = null;
    private List<AdlitePostback> _postbackManagers = null;
    
    public bool HasError { get { return this._hasError; } }
    public string ErrorMessage { get { return this._errorMessage; } }
    public string ActionName { get { return this._actionName; } }
    public HttpContext HttpContext { get { return this._httpContext; } }
    public string RawUrl { get { return this._rawUrl; } }
    public Click Click { get { return this._click; } }
    public List<AdlitePostback> Managers { get { return this._postbackManagers; } }

    protected virtual void InitializeSession(HttpContext httpContext)
    {
      this._rawUrl = httpContext.Request.RawUrl;
      
      int clickID;
      if(httpContext.Request["click_id"] == null || !int.TryParse(httpContext.Request["click_id"], out clickID))
      {
        this._hasError = true;
        this._errorMessage = "click_id missing";
        return;
      }

      this._click = Click.CreateManager().Load(clickID);
      if(this._click == null)
      {
        this._hasError = true;
        this._errorMessage = "clickID could not be parsed";
        return;
      }
      
      List<Adlite.Data.Postback> postbacks = Adlite.Data.Postback.CreateManager().Load(this._click.Affiliate);
      if(postbacks == null || postbacks.Count == 0)
      {
        this._hasError = true;
        this._errorMessage = "Postbacks are not set for this affiliate";
        return;
      }

      this._postbackManagers = new List<AdlitePostback>();
      foreach (Adlite.Data.Postback postback in postbacks)
        if(postback.IsActive)
          this._postbackManagers.Add(PostbackConverter.Convert(postback));

      if(this._postbackManagers.Count == 0)
      {
        this._hasError = true;
        this._errorMessage = "Postback IOC conversion error";
        return;
      }

      this._actionName = this._httpContext.Request.RequestContext.RouteData.Values["action"].ToString();
      if(this._postbackManagers.ElementAt(0).GetType().GetMethod(this._actionName) == null)
      {
        this._hasError = true;
        this._errorMessage = "Manager does not implement method name: " + this._actionName;
        return;
      }
    }

   

  }
}