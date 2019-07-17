using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Code.Lite.Administration
{
  public class OfferCampaignIsActiveLite
  {
    public int OfferID { get;set;}
    public int CampaignID { get;set;}
    public bool IsActive { get; set; }
    public int AffiliateID { get; set; }
  }
}