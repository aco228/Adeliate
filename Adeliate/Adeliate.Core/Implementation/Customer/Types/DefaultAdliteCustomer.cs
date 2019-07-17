using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adlite.Data;

namespace Adeliate.Core.Implementation.Customer.Types
{
  public class DefaultAdliteCustomer : AdliteCustomer
  {
    public DefaultAdliteCustomer(Adlite.Data.Customer customer) : base(customer, CustomerWeight.Unknown)
    {}

  }
}
