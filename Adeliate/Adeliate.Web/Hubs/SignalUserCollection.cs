using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Web.Hubs
{
  public class SignalUserCollection
  {
    private static object LOCK_OBJECT = new object();
    protected SortedDictionary<Guid, SignalUser> _customers = null;

    public SignalUser this[Guid guid] { get { return (from c in this._customers where c.Key == guid select c.Value).FirstOrDefault(); } }

    public SignalUserCollection()
    {
      this._customers = new SortedDictionary<Guid, SignalUser>();
    }

    public void Connect(Guid guid, string hubName)
    {
      lock(SignalUserCollection.LOCK_OBJECT)
      {
        if (this._customers.ContainsKey(guid))
        {
          this._customers[guid].HubConnect(hubName);
          return;
        }

        this._customers.Add(guid, new SignalUser(guid, hubName));
      }
    }

    public void Disconect(Guid guid, string hubName)
    {
      lock(SignalUserCollection.LOCK_OBJECT)
      {
        if (!this._customers.ContainsKey(guid))
          return;

        this._customers.Remove(guid);
      }
    }


  }
}