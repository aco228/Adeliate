using Adeliate.Portal.Code.Cache;
using Adeliate.Web;
using Adlite.Data;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal
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
      if (httpContext == null)
        return null;

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

    private AdliteSession _session = null;
    private AffiliateCache _affiliateCache = null;

    public AdliteSession Session { get { return this._session; } }
    public AdliteCustomer Customer { get { return this._session.Customer; } }

    public AffiliateCache AdliteCache
    {
      get
      {
        if (this._affiliateCache != null)
          return this._affiliateCache;
        if (this._session.Customer == null)
          return null;

        this._affiliateCache = AffiliateCache.Request(this._session.Customer.Affilite);
        return this._affiliateCache;
      }
    }

    public bool ShouldLogin
    {
      get
      {
        if (this.Customer == null) return true;
        return (this.Customer.CustomerData.IsActive && this.Customer.CustomerData.CustomerStatus == CustomerStatus.Active) ? false : true;
      }
    }
    
    public AdliteContext(HttpContext context)
    {
      this.InitializeSession(context);
    }
    
    protected virtual void InitializeSession(HttpContext httpContext)
    {
      string sessionID = (httpContext.Session != null ? httpContext.Session.SessionID : Guid.Empty.ToString());
      Guid sessionGuid = Guid.Empty;
      if (!Guid.TryParseExact(sessionID, "N", out sessionGuid) || sessionGuid.Equals(Guid.Empty))
      {
        Log.Warn("Invalid session ID" + sessionGuid);

        HttpCookie sessionCookie = httpContext.Request.Cookies[Constants.SESSION_COOKIE];
        if(sessionCookie != null && !Guid.TryParse(sessionCookie.Value, out sessionGuid))
          sessionGuid = Guid.NewGuid();
      }
      
      CustomerSession session = CustomerSession.CreateManager().Load(sessionGuid);

      if (session != null && session.IPAddress != httpContext.Request.UserHostAddress)
        Log.Warn("Possible session hijack! Session IP address different than specified in request");

      if (session == null && httpContext.Request[Constants.SESSION_AJAX_IDENTIFIER] != null)
      {
        Guid sessionGuidFromRequest;
        if(Guid.TryParse(httpContext.Request[Constants.SESSION_AJAX_IDENTIFIER].ToString(), out sessionGuidFromRequest))
          session = CustomerSession.CreateManager().Load(sessionGuidFromRequest);
      }

      if (session == null)
      {
        session = new CustomerSession(-1,
          sessionGuid,
          null,
          httpContext.Request.UserHostAddress,
          httpContext.Request.UserAgent,
          DateTime.Now.AddMinutes(60),
          DateTime.Now, DateTime.Now);
        session.Insert();
        
        // store sessionID in cookie, in case we need it sometime
        HttpCookie cookie = new HttpCookie(Web.Constants.SESSION_COOKIE, sessionGuid.ToString());
        cookie.Expires = DateTime.Now.AddMinutes(60);
        cookie.Path = "/";
        httpContext.Response.Cookies.Add(cookie);

        //INFO: DO NOT DO ANYTHING WITH THIS LINE BELOW!!!
        httpContext.Session["someValue"] = "bla";
      }

      this._session = new AdliteSession(session);
    }
  }
}