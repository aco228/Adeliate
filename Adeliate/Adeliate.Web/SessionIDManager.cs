using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace Adeliate.Web
{
  public class AdeliateSessionIDManager : ISessionIDManager
  {
    private PaywallSessionGuidManager _manager = new PaywallSessionGuidManager();

    public AdeliateSessionIDManager()
    {

    }

    public string GetSessionID(System.Web.HttpContext context)
    {
      HttpCookie sessionCookie = context.Request.Cookies[Constants.SESSION_COOKIE];
      if (sessionCookie == null)
        return null;

      string sessionID = sessionCookie.Value;
      if (sessionID != null)
        return sessionID;

      return null;
    }

    public bool Validate(string id)
    {
      return this._manager.Validate(id);
    }

    public string CreateSessionID(System.Web.HttpContext context)
    {
      this._manager.SessionID = this.GetSessionID(context);
      return this._manager.CreateSessionID(context);
    }

    public void Initialize()
    {
      this._manager.Initialize();
    }

    public bool InitializeRequest(System.Web.HttpContext context, bool suppressAutoDetectRedirect, out bool supportSessionIDReissue)
    {
      return this._manager.InitializeRequest(context, suppressAutoDetectRedirect, out supportSessionIDReissue);
    }

    public void RemoveSessionID(System.Web.HttpContext context)
    {
      this._manager.RemoveSessionID(context);
    }

    public void SaveSessionID(System.Web.HttpContext context, string id, out bool redirected, out bool cookieAdded)
    {
      this._manager.SaveSessionID(context, id, out redirected, out cookieAdded);
    }

    private class PaywallSessionGuidManager : SessionIDManager
    {
      public string SessionID { get; set; }

      public override string CreateSessionID(System.Web.HttpContext context)
      {
        if (!string.IsNullOrEmpty(this.SessionID))
          return this.SessionID;
        return Guid.NewGuid().ToString().Replace("-", string.Empty);
      }

      public override bool Validate(string id)
      {
        Guid tmp = Guid.Empty;
        return Guid.TryParseExact(id, "N", out tmp);
      }
    }
  }
}
