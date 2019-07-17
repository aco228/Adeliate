using Senti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 

namespace Adlite.Data 
{
  public partial interface IAffiliateTypeManager 
  {

    List<AffiliateType> Load();
    List<AffiliateType> Load(IConnectionInfo connection);

  }

  public partial class AffiliateType
  {
  }
}

