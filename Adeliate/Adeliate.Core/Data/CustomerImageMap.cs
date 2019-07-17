using Senti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 

namespace Adlite.Data 
{
  public partial interface ICustomerImageMapManager 
  {


    CustomerImageMap Load(Customer customer);
    CustomerImageMap Load(IConnectionInfo connection, Customer customer);


   
  }

  public partial class CustomerImageMap
  {
  }
}

