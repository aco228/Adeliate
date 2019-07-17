using Adlite.Data;
using Adlite.Direct.Data;
using Direct.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Code.Cache
{
  public class AffiliateNotificationsCache
  {
    public static int LIMIT = 5;

    public int? CampaignIDValue;

    public int ID;
    public int CampaignID { get { return this.CampaignIDValue.HasValue ? this.CampaignIDValue.Value : -1; } }
    public string Title;
    public string Text;
    public string Created;

    public static List<AffiliateNotificationsCache> Load(AffiliateCache affiliate)
    {
      AdliteDirect db = AdliteDirect.Instance;
      DirectContainer dc = db.LoadContainer(string.Format(
        @"SELECT TOP {0} AffiliateNotificationID, CampaignID, Title, Text, Created 
          FROM  Adlite.core.AffiliateNotification 
          WHERE AffiliateID={1}
          ORDER BY AffiliateNotificationID DESC", AffiliateNotificationsCache.LIMIT, affiliate.Affiliate.ID));

      if (dc == null || !dc.HasValue)
        return new List<AffiliateNotificationsCache>();

      List<AffiliateNotificationsCache> result = new List<AffiliateNotificationsCache>();
      foreach (DirectContainerRow row in dc.Rows)
        result.Add(new AffiliateNotificationsCache()
        {
          ID = row.GetInt("AffiliateNotificationID").Value,
          CampaignIDValue = row.GetInt("CampaignID"),
          Title = row.GetString("Title"),
          Text = row.GetString("Text"),
          Created = row.GetString("Created")
        });

      return result;
    }
  }
}