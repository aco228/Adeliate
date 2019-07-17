using Adeliate.Web;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Administration.Input
{
  public class OffersInputModel : InputModelBase
  {
    public int AffiliateID { get; set; }
    public string Boxes { get; set; }
    public string LoadedCampaigns { get; set; }
    public List<int> LoadedCampaignsInt { get; set; }
    public List<int> Inputs { get; set; }
    public List<Offer> Offers { get; set; }
    public List<Offer> LoadedOffers { get; set; }
    public Affiliate Affiliate { get; set; }
    public OffersInputModel() : base(false)
    {
    }

    public override void Validation()
    {
      if (AffiliateID == null)
      {
        this.HasError = true;
        this.ErrorMessage = "AffiliateID are not set";
        return;
      }
      if (Boxes == null)
      {
        this.HasError = true;
        this.ErrorMessage = "Boxes are not set";
        return;
      }
      if(LoadedCampaigns == null)
      {
        this.HasError = true;
        this.ErrorMessage = "LoadedOffer is not set";
        return;
      }

      if(Boxes != "")
        Inputs = Boxes.Split(',').Select(int.Parse).ToList();

      if (Inputs == null)
        Inputs = new List<int>();

      if (LoadedCampaigns != "")
        LoadedCampaignsInt = LoadedCampaigns.Split(',').Select(int.Parse).ToList();

      if (LoadedCampaignsInt == null)
        LoadedCampaignsInt = new List<int>();

      Affiliate = Affiliate.CreateManager().Load(AffiliateID);

      Offers = Offer.CreateManager().Load(Affiliate);
      if (Offers == null)
        Offers = new List<Offer>();

      LoadedOffers = new List<Offer>();
      Offer offer = null;
      Campaign campaign = null;
      foreach (int i in LoadedCampaignsInt)
      {
        campaign = Campaign.CreateManager().Load(i);
        offer = Offer.CreateManager().Load(campaign,Affiliate);
        LoadedOffers.Add(offer);
      }


    }
  }
}