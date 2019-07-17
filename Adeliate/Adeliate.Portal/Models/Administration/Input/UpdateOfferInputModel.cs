using Adeliate.Web;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Administration.Input
{
  public class UpdateOfferInputModel : InputModelBase
  {
    public int AffiliateID { get; set; }
    public int CampaignID { get; set; }
    public decimal PriceValue { get; set; }
    public int CurrencyID { get; set; }
    public Affiliate Affiliate { get; set; }
    public Campaign Campaign { get; set; }
    public Offer Offer { get; set; }
    public Currency Currency { get; set; }
    public int CapValue { get; set; }
    public Price Price { get; set; }
    public UpdateOfferInputModel() : base(false)
    {
    }

    public override void Validation()
    {
      if (AffiliateID == null)
      {
        this.HasError = true;
        this.ErrorMessage = "AffiliateID is not set";
        return;
      }
      if (CurrencyID == null)
      {
        this.HasError = true;
        this.ErrorMessage = "CurrencyID is not set";
        return;
      }
      if (PriceValue == null)
      {
        this.HasError = true;
        this.ErrorMessage = "PriceValue is not set";
        return;
      }
      if (CapValue == null)
      {
        this.HasError = true;
        this.ErrorMessage = "CapValue is not set";
        return;
      }
      if (CampaignID == null)
      {
        this.HasError = true;
        this.ErrorMessage = "CampaignID is not set";
        return;
      }
      Affiliate = Affiliate.CreateManager().Load(AffiliateID);
      if (Affiliate == null)
      {
        this.HasError = true;
        this.ErrorMessage = "Affiliate is not set";
        return;
      }
      Campaign = Campaign.CreateManager().Load(CampaignID);
      if (Campaign == null)
      {
        this.HasError = true;
        this.ErrorMessage = "Campaign is not set";
        return;
      }
      Offer = Offer.CreateManager().Load(Campaign, Affiliate);
      if (Offer == null)
      {
        this.HasError = true;
        this.ErrorMessage = "100";
        Offer = new Offer(-1, Campaign, Affiliate, null, null, "", false, DateTime.Now, DateTime.Now);
        Offer.Insert();
      }
      Currency = Currency.CreateManager().Load(CurrencyID);
      if (Offer == null)
      {
        this.HasError = true;
        this.ErrorMessage = "Currency is not set!";
        return;
      }

    }
  }
}