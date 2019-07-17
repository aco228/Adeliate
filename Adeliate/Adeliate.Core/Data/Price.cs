using Senti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 

namespace Adlite.Data 
{
  public partial interface IPriceManager 
  {

    List<Price> Load();
    List<Price> Load(IConnectionInfo connection );

  }

  public partial class Price
  {
  }
}

