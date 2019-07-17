using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adeliate
{
  public interface IAdliteCustomer
  { }
  
  public abstract class AdliteCustomer
  {

    private Adlite.Data.Customer _customer = null;
    private CustomerWeight _customerWeight = CustomerWeight.Unknown;

    public int ID { get { return this._customer.ID; } }
    public Affiliate Affilite { get { return this._customer.Affiliate; } }
    public Adlite.Data.Customer CustomerData { get { return this._customer; } }
    public CustomerWeight Weight { get { return this._customerWeight; } }

    public AdliteCustomer(Adlite.Data.Customer customer, CustomerWeight weight)
    {
      this._customerWeight = weight;
      this._customer = customer;
    }

  }
}
