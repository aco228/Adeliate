using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Administration
{
  public class EditCampaignModel: AdliteModelBase
  {
    public List<CampaignGroup> CampaignGroups { get; set; }
    public List<Country> Countries { get; set; }
    public List<Currency> Currencies { get; set; }
    public Campaign Campaign { get; set; }
    public List<MobileOperator> MobileOperators { get; set; }
    public List<CampaignFlowMap> CampaignFlowMaps { get; set; }
    public List<CampaignDeviceMap> CampaignDeviceMaps { get; set; }
    public List<CampaignMobileOperatorMap> CampaignMobileOperatorMaps { get; set; }
    public List<CampaignImageMap> CampaignImageMaps { get; set; }


    public EditCampaignModel()
    {

      LoadData();
      Campaign = new Campaign();

    }
    public EditCampaignModel(int id)
    {
      LoadData();
      Campaign = Campaign.CreateManager().Load(id);
      if (Campaign == null)
        return;

      CampaignFlowMaps = CampaignFlowMap.CreateManager().Load(Campaign);
      if (CampaignFlowMaps == null)
        CampaignFlowMaps = new List<CampaignFlowMap>();

      CampaignDeviceMaps = CampaignDeviceMap.CreateManager().Load(Campaign);
      if (CampaignDeviceMaps == null)
        CampaignDeviceMaps = new List<CampaignDeviceMap>();

      CampaignMobileOperatorMaps = CampaignMobileOperatorMap.CreateManager().Load(Campaign);
      if (CampaignMobileOperatorMaps == null)
        CampaignMobileOperatorMaps = new List<CampaignMobileOperatorMap>();

      CampaignImageMaps = CampaignImageMap.CreateManager().Load(Campaign);
      if (CampaignDeviceMaps == null)
        CampaignDeviceMaps = new List<CampaignDeviceMap>();

      MobileOperators = MobileOperator.CreateManager().Load(Campaign);
      if (MobileOperators == null)
        MobileOperators = new List<MobileOperator>();

    }

    private void LoadData() {

      CampaignGroups = CampaignGroup.CreateManager().Load();
      if (CampaignGroups == null)
        CampaignGroups = new List<CampaignGroup>();

      Countries = Country.CreateManager().Load();
      if (Countries == null)
        Countries = new List<Country>();

      Currencies = Currency.CreateManager().Load();
      if (Currencies == null)
        Currencies = new List<Currency>();

      MobileOperators = MobileOperator.CreateManager().Load();
      if (MobileOperators == null)
        MobileOperators = new List<MobileOperator>();
    }

  }
 
}