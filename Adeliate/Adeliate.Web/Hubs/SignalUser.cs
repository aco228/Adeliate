using Adlite.Data;
using Adlite.Direct.Data;
using Direct.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Web.Hubs
{
  public class SignalUser
  {
    private Guid _identifier = Guid.Empty;
    private List<string> _connectedHubs = null;
    private int _sessionID = -1;
    private int _customerID = -1;
    private int _affiliateID = -1;

    public Guid Guid { get { return this._identifier; } }
    public int SessionID { get { return this._sessionID; } }
    public int CustomerID { get { return this._customerID; } }
    public int AffiliateID { get { return this._affiliateID; } }
    public int ConnectedHubs { get { return this._connectedHubs.Count; } }

    public SignalUser(Guid guid, string hubName)
    {
      this._identifier = guid;
      this._connectedHubs = new List<string>();
      this._connectedHubs.Add(hubName);
      this.ReloadSession();
    }

    public void HubConnect(string hubName)
    {
      if (this._connectedHubs.Contains(hubName))
        return;
      this._connectedHubs.Add(hubName);
    }

    public void HubDisconect(string hubName)
    {
      if (!this._connectedHubs.Contains(hubName))
        return;
      this._connectedHubs.Remove(hubName);
    }

    public void ReloadSession()
    {
      DirectContainer dc = AdliteDirect.Instance.LoadContainer(string.Format(@"
        SELECT cs.CustomerSessionID, c.CustomerID, c.AffiliateID FROM Adlite.core.CustomerSession AS cs
        LEFT OUTER JOIN Adlite.core.Customer AS c ON cs.CustomerID=c.CustomerID
        WHERE cs.CustomerSessionGuid='{0}';", this._identifier.ToString()));

      if (!dc.HasValue || dc.RowsCount == 0)
        return;

      int? customerSessionID = dc.GetInt("CustomerSessionID");
      int? customerID = dc.GetInt("CustomerID");
      int? affiliateID = dc.GetInt("AffiliateID");

      if (customerSessionID.HasValue)
        this._sessionID = customerSessionID.Value;

      if (customerID.HasValue)
        this._customerID = customerID.Value;

      if (affiliateID.HasValue)
        this._affiliateID = affiliateID.Value;
    }

  }
}