using Adeliate.Portal.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adeliate.Portal.Controllers.Statistics.Clicks
{
  [LoginFilter]
  [MerchantFilter]
  public class ClickApiController : AdliteApiController
  {

    public ActionResult Load()
    {
      Controllers.Clicks.Models.InputModel inputModel = new Controllers.Clicks.Models.InputModel();
      if (inputModel.HasError)
        return this.Return(false, inputModel.ErrorMessage);

      List<Controllers.Clicks.Models.ClickDataLite> result = Controllers.Clicks.Models.ClickDataLite.Load(inputModel);
      return this.PartialView("~/Views/Clicks/_Load.cshtml", result);
    }

  }
}