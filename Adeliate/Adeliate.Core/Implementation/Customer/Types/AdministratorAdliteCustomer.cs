using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adlite.Data;

namespace Adeliate.Core.Implementation.Customer.Types
{
  public class AdministratorAdliteCustomer : AdliteCustomer
  {
    public AdministratorAdliteCustomer(Adlite.Data.Customer customer) : base(customer, CustomerWeight.Administrator)
    {
    }
  }
}
