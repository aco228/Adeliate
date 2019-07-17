using Adeliate.Web;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Administration.Input
{
  public class UpdateImageMapInputModel : InputModelBase
  {
    public int CampaignID { get; set; }
    public int CampaignImageMapID { get; set; }
    public Campaign Campaign { get; set; }
    public string ToDelete { get; set; }
    public List<CampaignImageMap> CampaignImageMaps { get; set; }

    public UpdateImageMapInputModel() : base(false)
    {
    }

    public override void Validation()
    {
      if(CampaignID == null)
      {
        this.HasError = true;
        this.ErrorMessage = "CampaignID is not set!";
        return;
      }
      if(CampaignImageMapID == null)
      {
        this.HasError = true;
        this.ErrorMessage = "CampaignImageMapID is not set!";
        return;
      }
      Campaign = Campaign.CreateManager().Load(CampaignID);
      if(Campaign == null)
      {
        this.HasError = true;
        this.ErrorMessage = "Cannot load Campaign with this ID!";
      }
      CampaignImageMaps = CampaignImageMap.CreateManager().Load(Campaign);
      if (CampaignImageMaps == null)
        CampaignImageMaps = new List<CampaignImageMap>();

    }
  }
}