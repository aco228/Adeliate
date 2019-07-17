using Adeliate.Portal.Code.Cache;
using Adeliate.Portal.Code.Lite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Offers
{
  public class OfferLoadModel : AdliteModelBase
  {
    public OffersLoadInputModel InputModel;
    public List<OfferLoadLite> Offers;
    public List<AffiliateOfferCache> CacheOffers = null;

    public OfferLoadModel()
    {

    }
    

  }
}