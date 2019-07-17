using Adeliate.Portal.Code.Cache;
using Adlite.Data;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Code.Hubs
{
  public class NotificationsHub : AdlitePortalHubBase
  {
    private const string Name = "notificationHub";


    public static NotificationsHub Current
    {
      get
      {
        return new NotificationsHub(GlobalHost.ConnectionManager.GetHubContext<NotificationsHub>());
      }
    }

    public NotificationsHub() : base(Name) { }
    public NotificationsHub(IHubContext context) : base(context, Name) { }

    // SUMMARY: Shared function for adding and distributing notifications to all affiliate clients
    private AffiliateNotification ProcessNotification(string title, string message, Campaign campaign, Affiliate affiliate)
    {
      AffiliateNotification notification = new AffiliateNotification(-1, affiliate, campaign, title, message, DateTime.Now, DateTime.Now);
      notification.Insert();
      AffiliateCache cache = AffiliateCache.Request(affiliate.ID);
      if (cache != null)
        cache.AddNotification(notification);
      return notification;
    }

    /*
       PUBLIC METHODS
    */

    public void NewNotification(string title, string message, Affiliate affiliate)
    {
      AffiliateNotification notification = this.ProcessNotification(title, message, null, affiliate);
      this.Update(notification);
    }

    public void NewNotification(string title, string message, Campaign campaign, Affiliate affiliate)
    {
      AffiliateNotification notification = this.ProcessNotification(title, message, null, affiliate);
      this.Update(notification);
    }

    public void OfferAdded(Offer offer)
    {
      AffiliateCache affiliateCache = AffiliateCache.Request(offer.Affiliate.ID);
      if (affiliateCache == null)
        return;

      affiliateCache.UpdateOffers();

      AffiliateNotification notification = this.ProcessNotification(
        "New offer",
        string.Format("Offer '{0}' added", offer.Campaign.Name),
        offer.Campaign, offer.Affiliate);
      this.Update(notification);
    }

    public void OfferMultipleAdded(Affiliate affiliate)
    {
      AffiliateCache affiliateCache = AffiliateCache.Request(affiliate.ID);
      if (affiliateCache == null)
        return;

      affiliateCache.UpdateOffers();

      AffiliateNotification notification = this.ProcessNotification(
        "New offers",
        string.Format("New offers added for you traffic"),
        null, affiliate);
      this.Update(notification);
    }

    public void OfferModifications(Offer offer)
    {
      AffiliateCache affiliateCache = AffiliateCache.Request(offer.ID);
      if (affiliateCache == null)
        return;

      AffiliateOfferCache offerCache = (from c in affiliateCache.Offers where c.OfferID == offer.ID select c).FirstOrDefault();
      if (offerCache != null)
      {
        offerCache.Cap = offer.CurrentCap;
        offerCache.Price = offer.CurrentPriceValue;
      }

      string title = "Offer modification";
      string description = "Offer '" + offer.Campaign.Name + "' have modifications.";

      AffiliateNotification notification = this.ProcessNotification(title, description, offer.Campaign, offer.Affiliate);
      this.Update(notification);
    }

    public void CapReached(Offer offer)
    {
      AffiliateNotification notification = this.ProcessNotification(
        "Cap reached", 
        string.Format("'{0}' reached cap of {1}. Turn off the traffic", offer.Campaign.Name, offer.CurrentCap), 
        offer.Campaign, 
        offer.Affiliate);
      this.Update(notification, "cap");
    }


    // SUMMARY: Shared method for distribution
    private void Update(AffiliateNotification notification, string subType = "")
    {
      this.Update("notify", new { subType = subType, campaignID = notification.CampaignIDValue, title = notification.Title, text = notification.Text }, notification.Affiliate.ID);
    }
    
  }
}