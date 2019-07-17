using Adeliate.Web.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Code.Hubs
{
  public class PortalHubUserIdProvider : AdliteHubUserIdProvider
  {
    public override string GetUserId(Microsoft.AspNet.SignalR.IRequest request)
    {
      return AdliteContext.Current.Session.SessionData.Guid.ToString();
    }
  }
}