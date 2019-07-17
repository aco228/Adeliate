using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Code.Cache
{
  public class AffiliateOfferSearchFilter
  {
    public enum DescendingType
    {
      None,
      DescendingByClicks,
      DescendingByTransactions,
      DescendingByCreated
    }

    public int Take = 20;
    public int Skip = 0;
    public bool SkipOnesWithoutTransactions = false;
    public DescendingType Type = AffiliateOfferSearchFilter.DescendingType.DescendingByCreated;

  }
}