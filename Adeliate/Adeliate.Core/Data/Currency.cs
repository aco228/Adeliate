using Senti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 

namespace Adlite.Data 
{
  public partial interface ICurrencyManager 
  {

    List<Currency> Load();
    List<Currency> Load(IConnectionInfo connection);



  }

  public partial class Currency
  {
  }
}

