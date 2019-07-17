using Adeliate.Core.Implementation.Customer.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal
{
  public class MenuManager
  {
    
  
    public static bool CheckAccess(CustomerWeight level)
    {
      if (AdliteContext.Current.Customer == null)
        return false;

      int levelValue = (int)level;
      int customerValue = (int)AdliteContext.Current.Customer.Weight;

      return (customerValue >= levelValue);
    }



  }
}