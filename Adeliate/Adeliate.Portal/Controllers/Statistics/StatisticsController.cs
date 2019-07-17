using Adeliate.Portal.Models.Offers;
using Adeliate.Portal.Models.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adeliate.Portal.Controllers.Statistics
{
  public class StatisticsController : AdliteController
  {
    public ActionResult Index()
    {
      var _id = Request["id"] != null ? Request["id"].ToString() : string.Empty;
      StatisticsModel model = new StatisticsModel();
      if (_id != "")
      {
        int id;
        if (!Int32.TryParse(_id, out id))
          return Content("Cannot parse id");

        
        model.AffiliateID = 1;
        model.CampaignID = id;
        return View("~/Views/Statistics/Index.cshtml", model);
      }

      model.AffiliateID = 1;
      model.CampaignID = -1;
      return View("~/Views/Statistics/Index.cshtml",model);
    }
  }
}