using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Administration
{
  public class UpdateCampaingGroupModel : AdliteModelBase
  {
    public CampaignGroup CampaignGroup { get; set; }
    public List<CampaignGroup> CampaignGroups { get; set; }

    public UpdateCampaingGroupModel(int id)
    {
      CampaignGroup = CampaignGroup.CreateManager().Load(id);
      if (CampaignGroup == null)
        CampaignGroup = new CampaignGroup();
       
      CampaignGroups = CampaignGroup.CreateManager().Load();
    }
    public UpdateCampaingGroupModel()
    {
      CampaignGroup = new CampaignGroup();
      CampaignGroups = CampaignGroup.CreateManager().Load();
    }
  }
  
}