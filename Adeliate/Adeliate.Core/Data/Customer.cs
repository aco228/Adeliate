using Adeliate;
using Senti.ComponentModel;
using Senti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 

namespace Adlite.Data 
{
  public partial interface ICustomerManager 
  {

    Customer Load(string username);
    Customer Load(IConnectionInfo connection, string username);


    List<Customer> Load();
    List<Customer> Load(IConnectionInfo connection );


   


  }

  public partial class Customer
  {

    public AdliteCustomer Instantiate()
    {
      return TypeFactory.Instantiate<AdliteCustomer, Customer>(this.CustomerType.TypeName, this);
    }

  }
}

