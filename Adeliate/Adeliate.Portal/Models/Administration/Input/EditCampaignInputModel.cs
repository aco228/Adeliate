using Adeliate.Web;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Administration.Input
{
  public class EditCampaignInputModel : InputModelBase
  {
    public int CampaignID { get; set; }
    public int CampaignGroupID { get; set; }
    public int CampaignContentTypeID { get; set; }
    public int CountryID { get; set; }
    public decimal PriceValue { get; set; }
    public int CurrencyID { get; set; }
    public string Name { get; set; }
    public int CapValue { get; set; }
    public string Link { get; set; }
    public string FallbackLink { get; set; }
    public string Description { get; set; }
    public string Device { get; set; }
    public string Browser { get; set; }
    //public string IPRanges { get; set; }
    public CampaignGroup CampaignGroup { get; set; }
    public CampaignContentType CampaignContentType { get; set; }
    public Country Country { get; set; }
    public Price Price { get; set; }
    public Currency Currency { get; set; }
    public string RestrictCountryTraffic { get; set; }
    public string RestrictMobileTraffic { get; set; }
    public bool RCountryT { get; set; }
    public bool RMobileT { get; set; }

    public EditCampaignInputModel() : base(false)
    {
     
    }

    public override void Validation()
    {
      if (this.CampaignGroupID == null)
      {
        this.HasError = true;
        this.ErrorMessage = "CampaignCroupID is not set";
        return;
      }
      if (this.CampaignContentTypeID == null)
      {
        this.HasError = true;
        this.ErrorMessage = "CampaignContentTypeID is not set";
        return;
      }
      if (this.CountryID == null)
      {
        this.HasError = true;
        this.ErrorMessage = "CountryID is not set";
        return;
      }
      if (this.PriceValue == null)
      {
        this.HasError = true;
        this.ErrorMessage = "PriceValue is not set";
        return;
      }
      if (this.CurrencyID == null)
      {
        this.HasError = true;
        this.ErrorMessage = "CurrencyID is not set";
        return;
      }
      if (this.Name == null)
      {
        this.HasError = true;
        this.ErrorMessage = "Name is not set";
        return;
      }
      if (this.CapValue == null)
      {
        this.HasError = true;
        this.ErrorMessage = "CapValue is not set";
        return;
      }
      if (this.Link == null)
      {
        this.HasError = true;
        this.ErrorMessage = "Link is not set";
        return;
      }
      if (this.FallbackLink == null)
      {
        this.HasError = true;
        this.ErrorMessage = "FallbackLink is not set";
        return;
      }
      if (this.Device == null)
      {
        this.HasError = true;
        this.ErrorMessage = "Device is not set";
        return;
      }
      if (this.Browser == null)
      {
        this.HasError = true;
        this.ErrorMessage = "Browser is not set";
        return;
      }
      if (this.RestrictMobileTraffic == null)
      {
        this.HasError = true;
        this.ErrorMessage = "RestrictMobileTraffic is not set";
        return;
      }
      if (this.RestrictCountryTraffic == null)
      {
        this.HasError = true;
        this.ErrorMessage = "RestrictCountryTraffic is not set";
        return;
      }

      CampaignGroup = CampaignGroup.CreateManager().Load(CampaignGroupID);
      if(CampaignGroup == null)
      {
        this.HasError = true;
        this.ErrorMessage = "Cannot load campaignGroup with this id!";
        return;
      }
      Country = Country.CreateManager().Load(CountryID);
      if (Country == null)
      {
        this.HasError = true;
        this.ErrorMessage = "Cannot load Country with this id!";
        return;
      }
      CampaignContentType = (CampaignContentType)Enum.ToObject(typeof(CampaignContentType), CampaignContentTypeID);
      if (CampaignContentType == null)
      {
        this.HasError = true;
        this.ErrorMessage = "Cannot load CampaignContentType with this id!";
        return;
      }
      bool a;
      if(!Boolean.TryParse(RestrictMobileTraffic, out a))
      {
        this.HasError = true;
        this.ErrorMessage = "Cannot parse RestricMobileTraffic!";
        return;
      }
      RMobileT = a;
      bool b;
      if (!Boolean.TryParse(RestrictCountryTraffic, out b))
      {
        this.HasError = true;
        this.ErrorMessage = "Cannot parse RestrictCountryTraffic!";
        return;
      }
      RCountryT = b;
      Currency = Currency.CreateManager().Load(CurrencyID);
      if (Currency == null)
      {
        this.HasError = true;
        this.ErrorMessage = "Cannot load Currency with this id!";
        return;
      }

    }
  }
}