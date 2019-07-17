using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Administration
{
  public class EditCustomerModel : AdliteModelBase
  {
    public Customer Customer { get; set; }
    public List<Affiliate> Affiliates { get; set; }
    public List<CustomerType> CustomerTypes { get; set; }
    public CustomerImageMap CustomerImageMap { get; set; }

    public EditCustomerModel()
    {
      Affiliates = Affiliate.CreateManager().Load();
      if (Affiliates == null)
        Affiliates = new List<Affiliate>();

      CustomerTypes = CustomerType.CreateManager().Load();
      if (CustomerTypes == null)
        CustomerTypes = new List<CustomerType>();

      Customer = new Customer();

      
    }
    public EditCustomerModel(int id)
    {
      Affiliates = Affiliate.CreateManager().Load();
      if (Affiliates == null)
        Affiliates = new List<Affiliate>();

      CustomerTypes = CustomerType.CreateManager().Load();
      if (CustomerTypes == null)
        CustomerTypes = new List<CustomerType>();

      Customer = Customer.CreateManager().Load(id);
      if (Customer == null)
        Customer = new Customer();

      CustomerImageMap = CustomerImageMap.CreateManager().Load(Customer);
      if (CustomerImageMap == null)
        CustomerImageMap = new CustomerImageMap();

    }
  }
  
}