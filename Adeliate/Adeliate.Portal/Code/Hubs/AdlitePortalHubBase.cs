using Adeliate.Web.Hubs;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Code.Hubs
{
  public abstract class AdlitePortalHubBase : AdliteHub
  {
    private string _name = string.Empty;

    public string Name { get { return this._name; } }

    public AdlitePortalHubBase(string name) : base() { this._name = name; }
    public AdlitePortalHubBase(IHubContext context, string name) : base(context) { this._name = name; }


    protected override void OnConnect(Guid guid)
    {
      // PortalApplication.SignalUsers.Connect(guid, this._name);
    }

    protected override void OnDisconect(Guid guid)
    {
      // PortalApplication.SignalUsers.Disconect(guid, this._name);
    }

    protected override void OnReconect(Guid guid)
    {
      // PortalApplication.SignalUsers.Connect(guid, this._name);
    }

    public void Update(string method, object data, int affiliateID)
    {
      SignalUpdateParams parameters = new SignalUpdateParams();
      parameters.MethodName = method;
      parameters.AffiliateID = affiliateID;
      parameters.Data = data;

      this.Update(parameters);
    }

  }
}