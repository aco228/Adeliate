using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adeliate.Web.Hubs
{
  public abstract class AdliteHub : Hub 
  {
    private IHubContext _context = null;

    protected IHubContext HubContext { get { return this._context; } }

    public AdliteHub() { }
    public AdliteHub(IHubContext context)
    {
      this._context = context;
    }

    public override Task OnConnected()
    {
      Guid guid = Guid.Empty;
      Guid.TryParse(Context.ConnectionId, out guid);
      if (!Guid.Empty.Equals(guid))
        this.OnConnect(guid);

      return base.OnConnected();
    }

    public override Task OnReconnected()
    {
      Guid guid = Guid.Empty;
      Guid.TryParse(Context.ConnectionId, out guid);
      if (!Guid.Empty.Equals(guid))
        this.OnConnect(guid);

      return base.OnReconnected();
    }

    public override Task OnDisconnected(bool stopCalled)
    {
      Guid guid = Guid.Empty;
      Guid.TryParse(Context.ConnectionId, out guid);
      if (!Guid.Empty.Equals(guid))
        this.OnConnect(guid);

      return base.OnDisconnected(stopCalled);
    }

    protected abstract void OnConnect(Guid guid);
    protected abstract void OnDisconect(Guid guid);
    protected abstract void OnReconect(Guid guid);

    public void Update(SignalUpdateParams data)
    {
      if (this.HubContext != null)
        this.HubContext.Clients.All.update(data);
      else
        this.Clients.All.update(data);
    }


  }
}
