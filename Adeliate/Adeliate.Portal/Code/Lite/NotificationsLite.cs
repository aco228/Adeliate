using Adlite.Data;
using Adlite.Direct.Data;
using Direct.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Code.Lite
{
  public class NotificationsLite
  {

    public string CampaignID;
    public string Title;
    public string Text;
    public string Created;

    public static List<NotificationsLite> Load(Affiliate affiliate)
    {
      AdliteDirect db = AdliteDirect.Instance;
      DirectContainer dc = db.LoadContainer(
        string.Format(@"SELECT TOP 5 CampaignID, Title, Text, Created FROM Adlite.core.AffiliateNotification WHERE AffiliateID={0} ORDER BY AffiliateNotificationID DESC", affiliate.ID));

      if (dc == null || !dc.HasValue || dc.RowsCount == 0)
        return new List<NotificationsLite>();

      List<NotificationsLite> result = new List<NotificationsLite>();

      foreach (DirectContainerRow row in dc.Rows)
        result.Add(new NotificationsLite() {
          CampaignID = row.GetString("CampaignID"),
          Text = row.GetString("Text"),
          Title = row.GetString("Title"),
          Created = row.GetString("Created")
        });

      return result;
    }

  }
}