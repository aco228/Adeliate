using Adeliate.Web;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Administration.Input
{
  public class UpdateMapsInputModel : InputModelBase
  {
    public string FlowData { get; set; }
    public string DeviceData { get; set; }
    public string MobileOperatorData { get; set; }
    public int CampaignID { get; set; }
    public Campaign Campaign { get; set; }

    public UpdateMapsInputModel() : base(false){}

    public override void Validation()
    {
      if(CampaignID == null)
      {
        this.HasError = true;
        this.ErrorMessage = "CampaignID is not set!";
        return;
      }

      Campaign = Campaign.CreateManager().Load(CampaignID);
      if (Campaign == null)
      {
        this.HasError = true;
        this.ErrorMessage = "Cannot load campaign with this ID!";
        return;
      }


    }
  }
}