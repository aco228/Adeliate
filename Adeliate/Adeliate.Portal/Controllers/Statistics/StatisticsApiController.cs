using Adeliate.Portal.Models.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adeliate.Portal.Controllers.Statistics
{
  public class StatisticsApiController : AdliteApiController
  {
    public ActionResult Load()
    {
      StatisticsInputModel inputModel = new StatisticsInputModel();
      if (inputModel.HasError)
        return this.Return(false, inputModel.ErrorMessage);

      StatisticsLoadModel model = new StatisticsLoadModel(inputModel);


      return PartialView("~/Views/Statistics/_Load.cshtml",model);

    }
  }
}