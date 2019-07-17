using Adlite.Direct.Data;
using Direct.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Code.Cache
{
  public class AffiliateTransactionsCache
  {
    public static int LIMIT = 15;

    public int CampaignID;
    public string Name;
    public string Created;

    public static List<AffiliateTransactionsCache> Load(AffiliateCache affiliate)
    {
      AdliteDirect db = AdliteDirect.Instance;
      DirectContainer dc = db.LoadContainer(string.Format(
        @"SELECT TOP 15 c.CampaignID, c.Name, t.Created  FROM Adlite.core.[Transaction] AS t
        LEFT OUTER JOIN Adlite.core.Campaign AS c ON t.CampaignID=c.CampaignID
        WHERE t.AffiliateID={0}", affiliate.Affiliate.ID));

      if (dc == null || !dc.HasValue)
        return new List<AffiliateTransactionsCache>();

      List<AffiliateTransactionsCache> result = new List<AffiliateTransactionsCache>();

      foreach (DirectContainerRow row in dc.Rows)
        result.Add(new AffiliateTransactionsCache()
        {
          CampaignID = row.GetInt("CampaignID").Value,
          Name = row.GetString("Name"),
          Created = row.GetString("Created")
        });

      return result;
    }

  }
}