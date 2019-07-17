using Senti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 

namespace Adlite.Data 
{
  public partial interface ICustomerTypeManager 
  {

    List<CustomerType> Load();
    List<CustomerType> Load(IConnectionInfo connection);


  }

  public partial class CustomerType
  {
  }
}

