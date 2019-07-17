using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Administration
{
  public class AllCustomerModel : AdliteModelBase
  {
    public List<Customer> Customers { get; set; }

    public AllCustomerModel()
    {

      Customers = Customer.CreateManager().Load();
      if (Customers == null)
        Customers = new List<Customer>();

    }


  }
}