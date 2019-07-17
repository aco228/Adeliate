using Adeliate.Portal.Code.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adeliate.Portal.Controllers.Dashboard
{
  public class DashboardApiController : AdliteApiController
  {

    public ActionResult DashboardOffersLoadMore()
    {
      int take, skip;
      if (!Int32.TryParse(Request["skip"] != null ? Request["skip"] : string.Empty, out skip))
        return this.Return(false, "Parameter skip missing");
      if (!Int32.TryParse(Request["take"] != null ? Request["take"] : string.Empty, out take))
        return this.Return(false, "Parameter take missing");

      AffiliateOfferSearchFilter filter = new AffiliateOfferSearchFilter();
      filter.Take = take;
      filter.Skip = skip;
      filter.Type = AffiliateOfferSearchFilter.DescendingType.DescendingByClicks;

      List<AffiliateOfferCache> offers = AdliteContext.Current.AdliteCache.GetOffers(filter);

      return this.PartialView("~/Views/Dashboard/_Partial/_OfferLoad.cshtml", offers);
    }

  }
}