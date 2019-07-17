using Adeliate.Web.Hubs;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Code.Hubs
{
  public class PortalHub : AdlitePortalHubBase
  {
    public static string Name = "portalHub";

    public static PortalHub Current
    {
      get
      {
        return new PortalHub(GlobalHost.ConnectionManager.GetHubContext<PortalHub>());
      }
    }
    

    public PortalHub() : base(Name) { }
    public PortalHub(IHubContext context) : base(context, Name) { }

    public void SendMessage(string message)
    {
    }

  }
}