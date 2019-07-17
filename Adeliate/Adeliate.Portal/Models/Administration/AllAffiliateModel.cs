using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Administration
{
  public class AllAffiliateModel: AdliteModelBase
  {
    public List<Affiliate> Affiliates { get; set; }

    public AllAffiliateModel()
    {
      Affiliates = Affiliate.CreateManager().Load();
      if (Affiliates == null)
        Affiliates = new List<Affiliate>();
    }
  }
}