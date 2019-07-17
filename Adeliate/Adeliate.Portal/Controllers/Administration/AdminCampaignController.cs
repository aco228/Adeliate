using Adeliate.Portal.Models;
using Adeliate.Portal.Models.Administration;
using Adeliate.Web;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adeliate.Portal.Controllers.Administration
{
  public class AdminCampaignController : AdministrationBaseController
  {

    [Route(AdliteRoute.Administration.CampaignGroupEdit)]
    public ActionResult EditCampaignGroup()
    {
      string _id = Request["id"] != null ? Request["id"].ToString() : "";
      
      UpdateCampaingGroupModel model = null;
      if (_id == "")
        model = new UpdateCampaingGroupModel();
      else
      {
        int id;
        if (!Int32.TryParse(_id, out id))
          return Content("Cannot parse id!");

        model = new UpdateCampaingGroupModel(id);
      }
      return View("~/Views/Administration/CampaignGroup/New.cshtml", model);
    }

    [Route(AdliteRoute.Administration.CampaignGroupAll)]
    public ActionResult AllCampaingGroups()
    {
      EditCampaignGroupModel model = new EditCampaignGroupModel();
      return View("~/Views/Administration/CampaignGroup/All.cshtml",model);
    }


    [Route(AdliteRoute.Administration.CampaignEdit)]
    public ActionResult EditCampaign()
    {
      EditCampaignModel model = null;
      string _id = Request["id"] != null ? Request["id"].ToString() : string.Empty;
      if(_id != "")
      {
        int id;
        if (!Int32.TryParse(_id, out id))
          return Content("Cannot parse id");

        model = new EditCampaignModel(id);
        if (model.Campaign == null)
        {
          ErrorModelBase errorModel = new ErrorModelBase();
          return View("~/Views/Error/Index.cshtml",errorModel);
        }
          

      }
      else
        model = new EditCampaignModel();

      return View("~/Views/Administration/Campaign/New.cshtml", model);
    }
    public ActionResult ImageMaps()
    {
      var _id = Request["id"] != null ? Request["id"].ToString() : string.Empty;
      int id;
      if (!Int32.TryParse(_id, out id))
        return Content("Cannot parse ID");

      EditCampaignModel model = new EditCampaignModel(id);
      if (model.Campaign == null)
        return Content("");

      return View("~/Views/Administration/Campaign/ImageMap.cshtml", model);
    }
    public ActionResult TestImage()
    {
     
      EditCampaignModel model = new EditCampaignModel();
      return View("~/Views/Administration/TestImage.cshtml", model);
    }
    public ActionResult AddCampaignImage(HttpPostedFileBase file)
    {
      string id = Request["ID"] != null ? Request["ID"].ToString() : "";

      int _id;
      if (!Int32.TryParse(id, out _id))
        return this.Content("ID could not be parsed");

      Campaign campaign = Campaign.CreateManager().Load(_id);
      if (campaign == null)
        return this.Content("Campaign is null");

      ImageUploadHelper imageUploadHelper = new ImageUploadHelper(file, 240, 240);
      if (imageUploadHelper.HasError)
        return this.Content(imageUploadHelper.ErrorMessage);

      List<CampaignImageMap> maps = CampaignImageMap.CreateManager().Load(campaign);
      if (maps == null)
        maps = new List<CampaignImageMap>();

      if(maps.Count == 0)
      {
        CampaignImageMap map = new CampaignImageMap(-1, campaign, imageUploadHelper.ImageData, true, DateTime.Now, DateTime.Now);
        map.Insert();

      }
      else
      {
        CampaignImageMap map = new CampaignImageMap(-1, campaign, imageUploadHelper.ImageData, false, DateTime.Now, DateTime.Now);
        map.Insert();
      }
      

      EditCampaignModel model = new EditCampaignModel(_id);
      return View("~/Views/Administration/Campaign/ImageMap.cshtml", model);
    }
    public ActionResult AddImage(HttpPostedFileBase file)
    {
      string id = Request["ID"] != null ? Request["ID"].ToString() : "";

      int _id;
      if (!Int32.TryParse(id, out _id))
        return this.Content("ID could not be parsed");

      Campaign campaign = Campaign.CreateManager().Load(_id);
      if (campaign == null)
        return this.Content("Campaign is null");

      ImageUploadHelper imageUploadHelper = new ImageUploadHelper(file, 240, 240);
      if (imageUploadHelper.HasError)
        return this.Content(imageUploadHelper.ErrorMessage);

      CampaignImageMap map = new CampaignImageMap(-1, campaign, imageUploadHelper.ImageData, true, DateTime.Now, DateTime.Now);
      map.Insert();

      return this.Content("Complete");
    }

  }
}