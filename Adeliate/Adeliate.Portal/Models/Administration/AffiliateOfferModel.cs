using Adlite.Data;
using Adlite.Direct.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Administration
{
  public class AffiliateOfferModel : AdliteModelBase
  {
    public Affiliate Affiliate { get; set; }
    public List<CampaignGroup> CampaignGroups { get; set; }
    

    public AffiliateOfferModel()
    {
      CampaignGroups = CampaignGroup.CreateManager().Load();
      if (CampaignGroups == null)
        CampaignGroups = new List<CampaignGroup>();
    }

    public int GetCampaingCount(CampaignGroup group)
    {
      AdliteDirect ad = new AdliteDirect();
      List<int> campaignGroups = ad.LoadArrayInt(string.Format(@"SELECT CampaignGroupID from Adlite.core.CampaignGroup where FallbackGroupID = {0}", group.ID));
      campaignGroups.Add(group.ID);
      string list = string.Join<int>(",", campaignGroups);
      int? count =  ad.LoadInt(string.Format(@"SELECT COUNT(*) FROM Adlite.core.Campaign WHERE CampaignGroupID in ({0})", list));
      if (count.HasValue)
        return count.Value;
      else
        return 0;
    }
  }
}