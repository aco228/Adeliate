using Adeliate.Portal.Code.Cache;
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
  public class AdminCustomerController : AdministrationBaseController
  {
    
    [Route(AdliteRoute.Administration.CustomerEdit)]
    public ActionResult EditCustomer()
    {
      EditCustomerModel model = null;
      string _id = Request["id"] != null ? Request["id"].ToString() : string.Empty;
      if (_id != "")
      {
        int id;
        if (!Int32.TryParse(_id, out id))
          return Content("Cannot parse id");

        model = new EditCustomerModel(id);
      }
      else 
       model = new EditCustomerModel();

      return View("~/Views/Administration/Customer/New.cshtml", model);
    }

    [Route(AdliteRoute.Administration.CustomerAll)]
    public ActionResult AllCustomers()
    {
      AllCustomerModel model = new AllCustomerModel();
      return View("~/Views/Administration/Customer/All.cshtml", model);
    }

    public ActionResult CustomerThumnail()
    {
      string id = Request["ID"] != null ? Request["ID"].ToString() : "";

      int _id;
      if (!Int32.TryParse(id, out _id))
        return this.Content("ID could not be parsed");

      EditCustomerModel model = new EditCustomerModel(_id);
      return View("~/Views/Administration/Customer/_AddThumbnail.cshtml", model);
    }

    public ActionResult AddCustomerThumbnail(HttpPostedFileBase file)
    {
      string id = Request["ID"] != null ? Request["ID"].ToString() : "";

      int _id;
      if (!Int32.TryParse(id, out _id))
        return this.Content("ID could not be parsed");

      Customer customer = Customer.CreateManager().Load(_id);
      if (customer == null)
        return Content("Customer is null");

      ImageUploadHelper imageUploadHelper = new ImageUploadHelper(file, 240, 240);
      if (imageUploadHelper.HasError)
        return this.Content(imageUploadHelper.ErrorMessage);

      CustomerImageMap map = null;

      map = CustomerImageMap.CreateManager().Load(customer);
      if (map != null)
        map.Delete();

     
      map = new CustomerImageMap(-1, customer, imageUploadHelper.ImageData, DateTime.Now, DateTime.Now);
      map.Insert();

      CustomerCacheImage cacheObject = CustomerCacheImage.Request(map.Customer);
      if (cacheObject != null)
        cacheObject.Image = map.Image;

      EditCustomerModel model = new EditCustomerModel(_id);
      return View("~/Views/Administration/Customer/_AddThumbnail.cshtml", model);

    }


  }
}