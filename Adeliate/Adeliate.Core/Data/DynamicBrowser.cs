using Senti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 

namespace Adlite.Data 
{
  public partial interface IDynamicBrowserManager 
  {

    DynamicBrowser Load(string name);
    DynamicBrowser Load(IConnectionInfo connection, string name);


    List<DynamicBrowser> Load();
    List<DynamicBrowser> Load(IConnectionInfo connection);







  }

  public partial class DynamicBrowser
  {
  }
}

