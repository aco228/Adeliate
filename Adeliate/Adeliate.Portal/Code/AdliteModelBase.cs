using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal
{
  public class AdliteModelBase
  {
    private AdliteContext _context = null;
    
    public AdliteContext Context
    {
      get
      {
        if (this._context != null)
          return this._context;
        this._context = AdliteContext.Current;
        return this._context;
      }
    }

    public string Username
    {
      get
      {
        if (this.Context.Customer != null)
          return this.Context.Customer.CustomerData.Username;
        return string.Empty;
      }
    }

    public string Select(string compare, string compareTo) { return compare.Equals(compareTo) ? "selected" : ""; }


  }
}