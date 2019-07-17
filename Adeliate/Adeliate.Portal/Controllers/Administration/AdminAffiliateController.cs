using Adeliate.Portal.Filter;
using Adeliate.Portal.Models;
using Adeliate.Portal.Models.Administration;
using Adeliate.Portal.Models.Administration.Input;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adeliate.Portal.Controllers.Administration
{
  public class AdminAffiliateController : AdministrationBaseController
  {

    [Route(AdliteRoute.Administration.AffiliateAll)]
    public ActionResult AllAffiliates()
    {
      AllAffiliateModel model = new AllAffiliateModel();
      return View("~/Views/Administration/Affiliate/All.cshtml", model);
    }

    [Route(AdliteRoute.Administration.AffiliateEdit)]
    public ActionResult EditAffiliate()
    {
      var _id = Request["id"] != null ? Request["id"].ToString() : string.Empty;

      EditAffiliateModel model = null;
      if (_id != "")
      {
        int id;
        if (!Int32.TryParse(_id, out id))
          return Content("Cannot parse id!");

        model = new EditAffiliateModel(id);
      }
      else
      {

        model = new EditAffiliateModel();
       
      }
      return View("~/Views/Administration/Affiliate/New.cshtml", model);
    }
    
    public ActionResult AffiliateOffer()
    {
      AffiliateOfferInputModel inputModel = new AffiliateOfferInputModel();
      if (inputModel.HasError)
      {
        ErrorModelBase errorModel = new ErrorModelBase();
        errorModel.ErrorTitle = "Error";
        errorModel.ErrorDescription = inputModel.ErrorMessage;
        return this.View("~/Views/Error/Index.cshtml", errorModel);
      }

      AffiliateOfferModel model = new AffiliateOfferModel();
      model.Affiliate = inputModel.Affiliate;

      return this.View("~/Views/Administration/Offers/Offers.cshtml", model);
    }
    
  }
}