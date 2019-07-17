using Adeliate.Portal.Code.Cache;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Offers
{
  public class OfferInfoModel : AdliteModelBase
  {

    private AffiliateOfferCache _offerCache = null;

    public Offer Offer { get; set; }
    public List<CampaignDeviceMap> CampaignDeviceMaps { get; set; }
    public List<CampaignMobileOperatorMap> CampaignMobileOperatorMaps { get; set; }
    public List<CampaignFlowMap> CampaignFlowMaps { get; set; }
    public List<CampaignImageMap> CampaingImageMaps { get; set; }

    public int CurrentTransactions { get { return this._offerCache.Transactions; } }
    public int Cap { get { return this._offerCache.Cap; } }

    public OfferInfoModel(int id)
    {
      Offer = Offer.CreateManager().Load(id);

      if (Offer != null)
      {
        CampaignDeviceMaps = CampaignDeviceMap.CreateManager().Load(Offer.Campaign);
        CampaignMobileOperatorMaps = CampaignMobileOperatorMap.CreateManager().Load(Offer.Campaign);
        CampaignFlowMaps = CampaignFlowMap.CreateManager().Load(Offer.Campaign);
        CampaingImageMaps = CampaignImageMap.CreateManager().Load(Offer.Campaign);
      }

      if (CampaignDeviceMaps == null)
        CampaignDeviceMaps = new List<CampaignDeviceMap>();

      if (CampaignMobileOperatorMaps == null)
        CampaignMobileOperatorMaps = new List<CampaignMobileOperatorMap>();

      if (CampaignFlowMaps == null)
        CampaignFlowMaps = new List<CampaignFlowMap>();

      if (CampaingImageMaps == null)
        CampaingImageMaps = new List<CampaignImageMap>();


      this._offerCache = (from o in AdliteContext.Current.AdliteCache.Offers where o.OfferID == this.Offer.ID select o).FirstOrDefault();
    }
  }
}