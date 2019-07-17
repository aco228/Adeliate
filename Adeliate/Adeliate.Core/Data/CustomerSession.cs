using Senti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 

namespace Adlite.Data 
{
  public partial interface ICustomerSessionManager 
  {


    CustomerSession Load(Guid sessionGuid);
    CustomerSession Load(IConnectionInfo connection, Guid sessionGuid);


  }

  public partial class CustomerSession
  {
  }
}

