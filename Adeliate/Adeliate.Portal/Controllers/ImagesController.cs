using Adeliate.Portal.Code.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adeliate.Portal.Controllers
{
  public class ImagesController : AdliteController
  {

    // SUMMARY: Shared return method for the image
    private ActionResult Result(ImageCache cacheObject)
    {
      return File(cacheObject.Image.Data,
        cacheObject.Image.MimeType,
        string.Format("{0}.{1}", cacheObject.Image.ID, cacheObject.Image.Extension));
    }

    // SUMMARY: Get any image with ID of that image
    public ActionResult Get()
    {
      int id;
      if (this.Request["id"] == null || !int.TryParse(this.Request["id"], out id))
        return this.Content("Parameter missing");

      ImageCache cacheObject = ImageCache.Request(id);
      if (cacheObject == null)
        return this.Content("Image is unreachable");

      return this.Result(cacheObject);
    }

    // SUMMARY: Get customer default thumbnail by ID of that customer
    public ActionResult Customer()
    {
      int id;
      if (this.Request["id"] == null || !int.TryParse(this.Request["id"], out id))
      {
        if (AdliteContext.Current.Customer == null)
          return this.Content("Parameter missing");
        else
          id = AdliteContext.Current.Customer.CustomerData.ID;
      }

      Adlite.Data.Customer customer = Adlite.Data.Customer.CreateManager().Load(id);
      CustomerCacheImage cacheObject = CustomerCacheImage.Request(customer);

      if (cacheObject == null)
      {
        string imageUrl = string.Format("~/Images/defaults/no_customer_image.png");
        return (System.IO.File.Exists(Server.MapPath(imageUrl))) ? Redirect(imageUrl) : null;
      }

      return this.Result(cacheObject);
    }
    
    // SUMMARY: Get default campaign image by ID of that campaign
    public ActionResult Campaign()
    {
      int id;
      if (this.Request["id"] == null || !int.TryParse(this.Request["id"], out id))
        return this.Content("Parameter missing");

      Adlite.Data.Campaign campaign = Adlite.Data.Campaign.CreateManager().Load(id);
      CampaignCacheImage cacheObject = CampaignCacheImage.Request(campaign);
      
      if (cacheObject == null)
      {
        string imageUrl = string.Format("~/Images/defaults/no_image.png");
        return (System.IO.File.Exists(Server.MapPath(imageUrl))) ? Redirect(imageUrl) : null;
      }

      return this.Result(cacheObject);
    }
    
  }
}