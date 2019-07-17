using Adeliate;
using Senti.ComponentModel;
using Senti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 

namespace Adlite.Data 
{
  public partial interface IAffiliateManager 
  {

    Affiliate Load(string name);
    Affiliate Load(IConnectionInfo connection, string name);
    List<Affiliate> Load();
    List<Affiliate> Load(IConnectionInfo connection);

  }

  public partial class Affiliate
  {
    
    public AdliteAffiliate Instantiate()
    {
      return TypeFactory.Instantiate<AdliteAffiliate, Affiliate>(this.AffiliateType.TypeName, this);
    }
    
  }
}

