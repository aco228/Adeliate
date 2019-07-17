using Adeliate.Portal.Code.Cache;
using Adeliate.Portal.Code.Hubs;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Adeliate.Portal.Controllers.Notifications
{
  public class CallbackController : Controller
  {
    private enum TypeNotification { Click, Transaction }

    public ActionResult OnClick()
    {
      string clickID = Request["click_id"] != null ? Request["click_id"] : string.Empty;
      if (string.IsNullOrEmpty(clickID))
        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);

      int _clickID;
      if(!int.TryParse(clickID, out _clickID))
        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);

      this.Process(_clickID, TypeNotification.Click);
      return new HttpStatusCodeResult(HttpStatusCode.OK);
    }

    public ActionResult OnTransaction()
    {
      string clickID = Request["click_id"] != null ? Request["click_id"] : string.Empty;
      if (string.IsNullOrEmpty(clickID))
        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);

      int _clickID;
      if (!int.TryParse(clickID, out _clickID))
        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
      
      this.Process(_clickID, TypeNotification.Transaction);
      return new HttpStatusCodeResult(HttpStatusCode.OK);
    }

    private void Process(int clickID, TypeNotification type)
    {
      new Thread(() =>
      {
        Click click = Click.CreateManager().Load(clickID);
        if (click == null)
          return;
        AffiliateCache affiliateCache = AffiliateCache.Request(click.Affiliate.ID);

        if(type == TypeNotification.Click)
        {
          ClickHub.Current.NewClick(click);
          if (affiliateCache != null)
            affiliateCache.NewClick(click);
        }
        else
        {
          ClickHub.Current.NewTransaction(click);
          if (affiliateCache != null)
            affiliateCache.NewTransaction(click);
        }
      }).Start();
    }


    public ActionResult CapReached()
    {
      string offer_id = Request["offer_id"] != null ? Request["offer_id"].ToString() : string.Empty;
      int offerID;
      if(!int.TryParse(offer_id, out offerID))
        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);

      new Thread(() =>
      {
        Offer offer = Offer.CreateManager().Load(offerID);
        if (offer == null)
          return;

        NotificationsHub.Current.CapReached(offer);

      }).Start();

      return new HttpStatusCodeResult(HttpStatusCode.OK);
    }

  }
}