using Adeliate.Web;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Administration.Input
{
  public class EditOfferInputModel : InputModelBase
  {
    public EditOfferInputModel() : base(false)
    {
    }

    public int AffiliateID { get; set; }
    public int CampaignID { get; set; }
    public Affiliate Affiliate { get; set; }
    public Campaign Campaign { get; set; }
    public Offer Offer { get; set; }
    public List<Currency> Currencies { get; set; }
    public bool OfferExist { get; set; }

    public override void Validation()
    {
      
      
      if (AffiliateID == null)
      {
        this.HasError = true;
        this.ErrorMessage = "AffiliateID is not set";
        return;
      }
      if (CampaignID == null)
      {
        this.HasError = true;
        this.ErrorMessage = "CampaignID is not set";
        return;
      }
      Affiliate = Affiliate.CreateManager().Load(AffiliateID);
      if(Affiliate == null)
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
        Offer = new Offer();
        OfferExist = false;
      }
      else
        OfferExist = true;

      Currencies = Currency.CreateManager().Load();
      if (Currencies == null)
        Currencies = new List<Currency>();
    }
  }
}