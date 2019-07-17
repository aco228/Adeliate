using Adeliate.Web;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Administration.Input
{
  public class UpdateCampaignGroupInputModel : InputModelBase
  {
    public int CampaignGroupID { get; set; }
    public int FallbackGroupID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public CampaignGroup CampaignGroup { get; set; }

    public UpdateCampaignGroupInputModel() : base(false)
    {

    }

    public override void Validation()
    {
      if (CampaignGroupID == null)
      {
        this.HasError = true;
        this.ErrorMessage = "CampaignGroupID is not set!";
        return;
      }
      if (FallbackGroupID == null)
      {
        this.HasError = true;
        this.ErrorMessage = "FallbackGroupID is not set!";
        return;
      }
      if (string.IsNullOrEmpty(Name))
      {
        this.HasError = true;
        this.ErrorMessage = "Name is not set!";
        return;
      }

      if (string.IsNullOrEmpty(Description))
      {
        this.HasError = true;
        this.ErrorMessage = "Description is not set!";
        return;
      }
      CampaignGroup = CampaignGroup.CreateManager().Load(FallbackGroupID);
      
    }
  }
}