using Adeliate.Portal.Code.Lite.Administration;
using Adlite.Data;
using Adlite.Direct.Data;
using Direct.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Administration
{
  public class EditCampaignGroupModel : AdliteModelBase
  {
    public List<CampaignGroup> CampaignGroups { get; set; }
    public List<Campaign> Campaigns { get; set; }
    public List<Offer> Offer { get; set; }
    public int AffiliateID { get; set; }

    private List<OfferCampaignIsActiveLite> _offerCampaignIsActiveLite = null;

    public EditCampaignGroupModel()
    {
      CampaignGroups = CampaignGroup.CreateManager().Load();
      if (CampaignGroups == null)
        CampaignGroups = new List<CampaignGroup>();
    }

    public EditCampaignGroupModel(int id)
    {
      AdliteDirect database = new AdliteDirect();
      List<int> container = database.LoadArrayInt(string.Format(@"SELECT CampaignGroupID FROM Adlite.core.CampaignGroup WHERE FallbackGroupID = {0}", id));
      if(container != null)
      {
        this.CampaignGroups = new List<CampaignGroup>();
        foreach (int groupid in container)
        {
          CampaignGroup group = CampaignGroup.CreateManager().Load(groupid);
          if (group != null)
            this.CampaignGroups.Add(group);
        }
      }
      if (this.CampaignGroups == null)
        this.CampaignGroups = new List<CampaignGroup>();
      
      CampaignGroup cg = CampaignGroup.CreateManager().Load(id);
      if (cg != null)
        this.Campaigns = Campaign.CreateManager().Load(cg);

      if (this.Campaigns == null)
        this.Campaigns = new List<Campaign>();

      if (this.Campaigns == null || this.Campaigns.Count == 0)
        return;

      string ids = "";
      foreach(Campaign c in this.Campaigns)
      {
        if (!ids.Equals("")) ids += ",";
        ids += c.ID.ToString();
      }
      string query = string.Format("SELECT OfferID, CampaignID, IsActive, AffiliateID FROM Adlite.core.Offer WHERE CampaignID IN ({0})", ids);
      DirectContainer dc = database.LoadContainer(query);

      if (dc == null || !dc.HasValue)
        return;

      this._offerCampaignIsActiveLite = new List<OfferCampaignIsActiveLite>();
      foreach (DirectContainerRow row in dc.Rows)
        _offerCampaignIsActiveLite.Add(new OfferCampaignIsActiveLite()
        {
          OfferID = row.GetInt("OfferID").Value,
          CampaignID = row.GetInt("CampaignID").Value,
          IsActive = row.GetBool("IsActive").Value,
          AffiliateID = row.GetInt("AffiliateID").Value
        });
    }

    public bool IsOfferActive(Campaign cid, int affiliateID)
    {
      if (this._offerCampaignIsActiveLite == null)
        return false;

      return (from l in this._offerCampaignIsActiveLite where l.CampaignID == cid.ID && l.AffiliateID == affiliateID select l.IsActive).FirstOrDefault();
    }

    public int GetCampaingCount(CampaignGroup group)
    {
      AdliteDirect ad = new AdliteDirect();
      List<int> campaignGroups = ad.LoadArrayInt(string.Format(@"SELECT CampaignGroupID from Adlite.core.CampaignGroup where FallbackGroupID = {0}", group.ID));
      campaignGroups.Add(group.ID);
      string list = string.Join<int>(",", campaignGroups);
      int? count = ad.LoadInt(string.Format(@"SELECT COUNT(*) FROM Adlite.core.Campaign WHERE CampaignGroupID in ({0})", list));
      if (count.HasValue)
        return count.Value;
      else
        return 0;
    }

  }
}