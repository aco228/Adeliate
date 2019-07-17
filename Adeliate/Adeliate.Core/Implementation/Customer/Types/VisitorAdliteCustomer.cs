using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adlite.Data;

namespace Adeliate.Core.Implementation.Customer.Types
{
  public class VisitorAdliteCustomer : AdliteCustomer
  {
    public VisitorAdliteCustomer(Adlite.Data.Customer customer) : base(customer, CustomerWeight.Visitor)
    {
    }
  }
}
