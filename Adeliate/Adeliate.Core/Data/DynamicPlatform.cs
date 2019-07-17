using Senti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 

namespace Adlite.Data 
{
  public partial interface IDynamicPlatformManager 
  {


    DynamicPlatform Load(string name);
    DynamicPlatform Load(IConnectionInfo connection, string name);


    List<DynamicPlatform> Load();
    List<DynamicPlatform> Load(IConnectionInfo connection);







  }

  public partial class DynamicPlatform
  {
  }
}

