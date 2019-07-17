using Adeliate.Portal.Code.Cache;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Code.Hubs
{
  public class ClickHub : AdlitePortalHubBase
  {
    public static string Name = "clickHub";

    public static ClickHub Current
    {
      get
      {
        return new ClickHub(GlobalHost.ConnectionManager.GetHubContext<ClickHub>());
      }
    }

    public ClickHub() : base(Name) { }
    public ClickHub(IHubContext context) : base(context, Name) { }
    
    public void NewClick(Adlite.Data.Click click)
    {
      this.Update("newClick",
        new { clickID = click.ID, offerID = click.Offer.ID, campaignID = click.Offer.Campaign.ID, campaignName = click.Offer.Campaign.Name },
        click.Affiliate.ID);
    }
    
    public void NewTransaction(Adlite.Data.Click click)
    {
      this.Update("newTransaction", 
        new { clickID = click.ID, offerID = click.Offer.ID, campaignID = click.Offer.Campaign.ID, campaignName = click.Offer.Campaign.Name }, 
        click.Affiliate.ID);
    }

    public void UpdateRevenue(string revenue, int affiliateID)
    {
      this.Update("updateRevenue", new { data = revenue }, affiliateID);
    }



  }
}