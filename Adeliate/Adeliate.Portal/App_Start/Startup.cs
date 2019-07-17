using Adeliate.Portal.Code.Hubs;
using Microsoft.AspNet.SignalR;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal
{
  public class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => new PortalHubUserIdProvider());
      GlobalHost.Configuration.ConnectionTimeout = TimeSpan.FromMinutes(1);
      GlobalHost.Configuration.DisconnectTimeout = TimeSpan.FromMinutes(1);
      GlobalHost.Configuration.TransportConnectTimeout = TimeSpan.FromMinutes(10);
      GlobalHost.Configuration.DefaultMessageBufferSize = 500;

      app.Map("/signalr", map =>
      {
        var hubConfiguration = new HubConfiguration{};
        hubConfiguration.EnableDetailedErrors = true;
        map.RunSignalR(hubConfiguration);
      });
      //app.MapSignalR();
    }
  }
}