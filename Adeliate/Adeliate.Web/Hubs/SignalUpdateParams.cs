using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adeliate.Web.Hubs
{
  public class SignalUpdateParams
  {
    private string _methodName = string.Empty;
    private int _affiliateID = -1;
    private bool _filterAffiliate = false;
    private object _data = null;

    public string MethodName { get { return this._methodName; } set { this._methodName = value; } }
    public int AffiliateID { get { return this._affiliateID; } set { this._affiliateID = value; } }
    public bool FilterAffiliate { get { return this._filterAffiliate; } set { this._filterAffiliate = value; } }
    public object Data { get { return this._data; } set { this._data = value; } }
  }
}
