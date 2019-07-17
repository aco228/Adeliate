using Adeliate.Portal.Filter;
using Adeliate.Portal.Models.Clicks;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adeliate.Portal.Controllers.Statistics.Clicks
{
  [LoginFilter]
  [MerchantFilter]
  public class ClickController : AdliteController
  {

    [Route(AdliteRoute.ClickIndex)]
    public ActionResult Index()
    {
      string _clickID = Request["id"] != null ? Request["id"].ToString() : string.Empty;
      int id;
      ClicksIndexModel model = new ClicksIndexModel();
      if (_clickID != "")
      {
        if (!Int32.TryParse(_clickID, out id))
          return Content("Cannot parse id!");

        model.OfferID = id;
      }
      
      return this.View("~/Views/Clicks/Index.cshtml", model);
    }

    [Route(AdliteRoute.ClickInformation)]
    public ActionResult ClickInformation()
    {
      int clickID;
      if (Request["id"] == null || !Int32.TryParse(Request["id"], out clickID))
        return this.ErrorView("ClickInformation", "Parameters missing");

      Click click = Click.CreateManager().Load(clickID);
      if (click == null)
        return this.ErrorView("ClickInformation", "There is no click with ID: " + clickID);

      if (click.Affiliate.ID != AdliteContext.Current.Customer.Affilite.ID)
        return this.ErrorView("ClickInformation", "You dont have permissions");

      ClickInformationModel model = new ClickInformationModel();
      model.Click = click;
      model.ClickInformation = Adlite.Data.ClickInformation.CreateManager().Load(click);
      model.Transactions = Adlite.Data.Transaction.CreateManager().Load(click);
      model.PostbackData = Adlite.Data.PostbackData.CreateManager().Load(click);

      return this.View("~/Views/Clicks/ClickInformation.cshtml", model);
    }
    

  }
}