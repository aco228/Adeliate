using Adeliate.Portal.Code.Lite;
using Adeliate.Portal.Filter;
using Adeliate.Portal.Models.Offers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Adeliate.Portal.Models.Administration.Input;

namespace Adeliate.Portal.Controllers.Offers
{
  [MerchantFilter]
  public class OffersController : AdliteController
  {

    public ActionResult Index()
    {
      OffersModel model = new OffersModel();
      return this.View("~/Views/Offers/Index.cshtml", model);
    }

    public ActionResult Load()
    {
      OffersLoadInputModel loadModel = new OffersLoadInputModel();
      if (loadModel.HasError)
        return this.Content("Model error");

      OfferLoadModel model = new OfferLoadModel();
      model.InputModel = loadModel;
      model.Offers = OfferLoadLite.Load(loadModel);
      if (model.Offers == null)
        model.Offers = new List<OfferLoadLite>();

      return this.PartialView("~/Views/Offers/_Load.cshtml", model);
    }

    public ActionResult Info()
    {
      var _id = Request["id"] != null ? Request["id"] : string.Empty;

      int id;
      if (!Int32.TryParse(_id, out id))
        return Content("Cannot parse id!");


      OfferInfoModel model = new OfferInfoModel(id);
      if (model.Offer == null)
        return Content("Cannot load offer with this ID!");

      return this.View("~/Views/Offers/Info.cshtml", model);
    }
    
   

  }
}