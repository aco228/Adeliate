using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adeliate.Web
{

  interface ICustomerSession
  {

  }

  public class AdliteSession
  {
    private CustomerSession _customerSession = null;
    private AdliteCustomer _customer = null;

    public int ID { get { return this._customerSession.ID; } }
    public Guid Guid { get { return this._customerSession.Guid; } }
    public CustomerSession SessionData { get { return this._customerSession; } set { this._customerSession = value; } }
    public AdliteCustomer Customer
    {
      set { this._customer = value; }
      get
      {
        if (this._customer != null)
          return this._customer;

        if (this._customerSession.Customer == null)
          return null;

        this._customer = this._customerSession.Customer.Instantiate();
        return this._customer;
      }
    }


    public AdliteSession(CustomerSession session)
    {
      this._customerSession = session;
    }
    
  }
}
