using Adeliate.Portal.Code.Cache;
using Adeliate.Portal.Code.Hubs;
using Adeliate.Portal.Models.Administration;
using Adeliate.Portal.Models.Administration.Input;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adeliate.Portal.Controllers.Administration.Api
{
  public class AdminCampaignApiController : AdminBaseApiController
  {
    public object NotificationHub { get; private set; }

    public ActionResult UpdateCampaignGroup()
    {
      string _key = Guid.NewGuid().ToString().Replace("-", string.Empty);

      UpdateCampaignGroupInputModel model = new UpdateCampaignGroupInputModel();
      if (model.HasError)
        return this.Return(false, model.ErrorMessage);

      CampaignGroup group = model.CampaignGroup != null ? model.CampaignGroup : null;

      string message = "";
      CampaignGroup campaignGroup = null;
      if (model.CampaignGroupID == -1)
      {
        campaignGroup = new CampaignGroup(-1, group, _key, model.Name, model.Description, DateTime.Now, DateTime.Now);
        campaignGroup.Insert();
        message = "Inserted!";
      }
      else
      {
        campaignGroup = new CampaignGroup(model.CampaignGroupID, group, _key, model.Name, model.Description, DateTime.Now, DateTime.Now);
        campaignGroup.Update();
        message = "Updated!";
      }

      return this.Return(true, message, new { id = campaignGroup.ID });
      

    

    }
    
    public ActionResult EditCampaign()
    {
      EditCampaignInputModel model = new EditCampaignInputModel();
      if (model.HasError)
        return this.Return(false, model.ErrorMessage);

      Price price = new Price(-1, model.Currency, model.PriceValue, DateTime.Now, DateTime.Now);
      price.Insert();
      var message = "";
      Campaign campaign = null;
      if(model.CampaignID == -1)
      {
        campaign = new Campaign(-1, model.CampaignGroup, model.CampaignContentType, model.Country, price, model.Name, model.CapValue, model.Link, model.FallbackLink, model.Description, model.Device, model.Browser, null, model.RCountryT, model.RMobileT, DateTime.Now, DateTime.Now);
        campaign.Insert();
        message = "Campaign is succesfully added!";
      }else
      {
        
        campaign = Campaign.CreateManager().Load(model.CampaignID);
        campaign.CampaignGroup = model.CampaignGroup;
        campaign.CampaignContentType = model.CampaignContentType;
        campaign.Country = model.Country;
        campaign.Price = price;
        campaign.Name = model.Name;
        campaign.CapValue = model.CapValue;
        campaign.Link = model.Link;
        campaign.FallbackLink = model.FallbackLink;
        campaign.Description = model.Description;
        campaign.Device = model.Device;
        campaign.Browser = model.Browser;
        campaign.RestrictMobileTraffic = model.RMobileT;
        campaign.RestrictCountryTraffic = model.RCountryT;
        campaign.Updated = DateTime.Now;
        campaign.Update();
        message = "Campaign is succesfully updated!";
      }
     
      return this.Return(true, message, new { id = campaign.ID });
    }

    public ActionResult UpdateIPRanges()
    {
      var _id = Request["campaignID"] != null ? Request["campaignID"].ToString() : string.Empty;
      var _ipRanges = Request["ipRanges"] != null ? Request["ipRanges"].ToString() : string.Empty;

      int id;
      if (!Int32.TryParse(_id, out id))
        return Return(false, "Cannot parse id");

      Campaign campaign = Campaign.CreateManager().Load(id);
      campaign.IPRanges = _ipRanges;
      campaign.Update();

      return Return(true, "IpRanges updated succesfully!");

    }

    public ActionResult GetCampaignGroupByID()
    {
      var _id = Request["id"] != null ? Request["id"].ToString() : string.Empty;
      var _affiliateID = Request["affiliateID"] != null ? Request["affiliateID"].ToString() : string.Empty;

      int id;
      if (!Int32.TryParse(_id, out id))
        return Content("Cannot parse id! ");

      int affiliateID;
      if (!Int32.TryParse(_affiliateID, out affiliateID))
        return Content("Cannot parse afiliateID! ");

      EditCampaignGroupModel model = new EditCampaignGroupModel(id);
      model.AffiliateID = affiliateID;

      if (model.CampaignGroups.Count == 0 && model.Campaigns.Count == 0)
        return Content("");

      return this.PartialView("~/Views/Administration/CampaignGroup/_CampaignGroups.cshtml", model);
    }
    public ActionResult GetCampaignGroup()
    {
      var _id = Request["id"] != null ? Request["id"].ToString() : string.Empty;

      int id;
      if (!Int32.TryParse(_id, out id))
        return Content("Cannot parse id! ");

      EditCampaignGroupModel model = new EditCampaignGroupModel(id);
      if (model.CampaignGroups.Count == 0 && model.Campaigns.Count == 0)
        return Content("");

      return this.PartialView("~/Views/Administration/CampaignGroup/_SubGroups.cshtml", model);
    }
    
    public ActionResult UpdateActive()
    {
      OffersInputModel model = new OffersInputModel();
      if (model.HasError)
        return this.Return(false, model.ErrorMessage);

      List<Offer> addedOffers = new List<Offer>();

      foreach (int input in model.Inputs)
      {
        Offer offer = (from oo in model.Offers where oo != null && oo.Campaign.ID == input select oo).FirstOrDefault();

        if (offer != null && offer.IsActive == false)
        {
          offer.IsActive = true;
          offer.Update();
          addedOffers.Add(offer);
        }
        else if(offer == null)
        {
          Campaign campaign = Campaign.CreateManager().Load(input);
          if (campaign == null)
            return this.Return(false, "Cannot load campaign with this input!");

          string guid = Guid.NewGuid().ToString("N").ToUpper();
          offer = new Offer(-1, campaign, model.Affiliate, null, null,guid, true, DateTime.Now, DateTime.Now);
          offer.Insert();

          addedOffers.Add(offer);
        }
      }

      if (addedOffers.Count == 1)
        NotificationsHub.Current.OfferAdded(addedOffers.ElementAt(0));
      else
        NotificationsHub.Current.OfferMultipleAdded(model.Affiliate);

      foreach (Offer of in model.LoadedOffers)
      {
        if(of != null)
        {
          if ((from i in model.Inputs where i == of.Campaign.ID select i).FirstOrDefault() == 0)
          {
            of.IsActive = false;
            of.Update();
          }
        }
        
      }
        

      return Return(true, "Success");
    }

    public ActionResult EditOfferDialog()
    {
      EditOfferInputModel model = new EditOfferInputModel();
      if (model.HasError && model.ErrorMessage != "100")
        return this.Content(model.ErrorMessage);


      return this.PartialView("~/Views/Administration/Offers/_EditOffer.cshtml", model);

    }

    public ActionResult UpdateImageMap()
    {
      UpdateImageMapInputModel model = new UpdateImageMapInputModel();
      if (model.HasError)
        return this.Return(false, model.ErrorMessage);

      foreach (CampaignImageMap map in model.CampaignImageMaps)
      {
        if(map.ID == model.CampaignImageMapID)
        {
          map.IsDefault = true;
          map.Update();

          CampaignCacheImage cacheObject = CampaignCacheImage.Request(map.Campaign);
          if(cacheObject != null)
            cacheObject.Image = map.Image;
        }
        else
        {
          map.IsDefault = false;
          map.Update();
        }
      }

      if (!string.IsNullOrEmpty(model.ToDelete))
      {
        List<int> toDelete = model.ToDelete.Split(',').Select(Int32.Parse).ToList();
        foreach(int i in toDelete)
        {
          foreach (CampaignImageMap map in model.CampaignImageMaps)
          {
            if (map.ID == i)
            {
              map.Delete();
            }
              
          }
        }
      }

      int count = 0;
      if(model.CampaignImageMaps.Count >= 1)
      {
        foreach(CampaignImageMap map in model.CampaignImageMaps)
        {
          if (map.IsDefault == true)
            count++;
        }
        if(count == 0)
        {
          CampaignImageMap camp = model.CampaignImageMaps[0];
          camp.IsDefault = true;
          camp.Update();
        }
      }
      

      return this.Return(true, "Updated!");
    }

    public ActionResult UpdateOffer()
    {
      UpdateOfferInputModel model = new UpdateOfferInputModel();
      if (model.HasError && model.ErrorMessage != "100")
        return this.Return(false, model.ErrorMessage);

      Offer offer = model.Offer;
      Price price = null;
      

      if (model.PriceValue != 0)
      {
        price = new Price(-1, model.Currency,model.PriceValue, DateTime.Now, DateTime.Now);
        price.Insert();
      }
      
      offer.Price = price;

      if (model.CapValue == 0)
        offer.CapValue = null;
      else
        offer.CapValue = model.CapValue;
      

      NotificationsHub.Current.OfferModifications(offer);
      
      offer.Update();
      return this.Return(true, "Success");
    }

    public ActionResult UpdateMaps()
    {
      UpdateMapsInputModel model = new UpdateMapsInputModel();
      if (model.HasError)
        return this.Return(false, model.ErrorMessage);

      UpdateFlows(model);
      UpdateDevices(model);
      UpdateMobileOperators(model);

      return Return(true, "Success!");
    }
    private void UpdateFlows(UpdateMapsInputModel model)
    {
      List<CampaignFlowMap> allCampaignFlows = CampaignFlowMap.CreateManager().Load(model.Campaign);
      CampaignFlowMap flowMap = null;
      List<int> existFlows = new List<int>();
      List<int> flows = new List<int>();

      if (!string.IsNullOrEmpty(model.FlowData))
        flows = model.FlowData.Split(',').Select(Int32.Parse).ToList();

      if (flows.Count != 0)
      {
        foreach (int i in flows)
        {
          CampaignFlow flow = (CampaignFlow)Enum.ToObject(typeof(CampaignFlow), i);
          flowMap = CampaignFlowMap.CreateManager().Load(flow, model.Campaign);

          if (flowMap == null)
          {
            flowMap = new CampaignFlowMap(-1, model.Campaign, flow, DateTime.Now, DateTime.Now);
            flowMap.Insert();
            existFlows.Add(flowMap.ID);
          }
          else
          {
            existFlows.Add(flowMap.ID);
          }

        }
        foreach (CampaignFlowMap map in allCampaignFlows)
        {
          if ((from i in existFlows where i == map.ID select i).FirstOrDefault() == 0)
            map.Delete();
        }
      }
      else
      {
        foreach (CampaignFlowMap flow in allCampaignFlows)
          flow.Delete();
      }
    }
    private void UpdateDevices(UpdateMapsInputModel model)
    {
      List<CampaignDeviceMap> allCampaignDevices = CampaignDeviceMap.CreateManager().Load(model.Campaign);
      CampaignDeviceMap deviceMap = null;
      List<int> exitsDevices = new List<int>();
      List<int> devices = new List<int>();

      if (!string.IsNullOrEmpty(model.DeviceData))
        devices = model.DeviceData.Split(',').Select(Int32.Parse).ToList();

      if (devices.Count != 0)
      {
        foreach (int i in devices)
        {
          CampaignDevice device = (CampaignDevice)Enum.ToObject(typeof(CampaignDevice), i);
          deviceMap = CampaignDeviceMap.CreateManager().Load(device, model.Campaign);

          if (deviceMap == null)
          {
            deviceMap = new CampaignDeviceMap(-1, model.Campaign, device, DateTime.Now, DateTime.Now);
            deviceMap.Insert();
            exitsDevices.Add(deviceMap.ID);
          }
          else
          {
            exitsDevices.Add(deviceMap.ID);
          }

        }
        foreach (CampaignDeviceMap map in allCampaignDevices)
        {
          if ((from i in exitsDevices where i == map.ID select i).FirstOrDefault() == 0)
            map.Delete();
        }
      }
      else
      {
        foreach (CampaignDeviceMap flow in allCampaignDevices)
          flow.Delete();
      }
    }
    private void UpdateMobileOperators(UpdateMapsInputModel model)
    {
      List<CampaignMobileOperatorMap> allCampaignMobileOperators = CampaignMobileOperatorMap.CreateManager().Load(model.Campaign);
      CampaignMobileOperatorMap mobileOperatorMap = null;
      List<int> exitsMobileOperators = new List<int>();
      List<int> mobileOperators = new List<int>();

      if (!string.IsNullOrEmpty(model.MobileOperatorData))
        mobileOperators = model.MobileOperatorData.Split(',').Select(Int32.Parse).ToList();

      if (mobileOperators.Count != 0)
      {
        foreach (int i in mobileOperators)
        {
          MobileOperator mobileOperator = MobileOperator.CreateManager().Load(i);
          mobileOperatorMap = CampaignMobileOperatorMap.CreateManager().Load(mobileOperator, model.Campaign);

          if (mobileOperatorMap == null)
          {
            mobileOperatorMap = new CampaignMobileOperatorMap(-1, model.Campaign, mobileOperator , DateTime.Now, DateTime.Now);
            mobileOperatorMap.Insert();
            exitsMobileOperators.Add(mobileOperatorMap.ID);
          }
          else
          {
            exitsMobileOperators.Add(mobileOperatorMap.ID);
          }

        }
        foreach (CampaignMobileOperatorMap map in allCampaignMobileOperators)
        {
          if ((from i in exitsMobileOperators where i == map.ID select i).FirstOrDefault() == 0)
            map.Delete();
        }
      }
      else
      {
        foreach (CampaignMobileOperatorMap map in allCampaignMobileOperators)
          map.Delete();
      }
    }
  }
}