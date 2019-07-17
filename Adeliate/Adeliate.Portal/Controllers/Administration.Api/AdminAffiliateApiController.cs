using Adeliate.Portal.Models.Administration.Input;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adeliate.Portal.Controllers.Administration.Api
{
  public class AdminAffiliateApiController : AdminBaseApiController
  {
    
    public ActionResult AddPostback()
    {
      AddPostbackInputModel inputModel = new AddPostbackInputModel();
      if (inputModel.HasError)
        return this.Return(false, inputModel.ErrorMessage);

      Postback postback = null;
      string message = "";

      if(inputModel.PostbackID == -1)
      {
        postback = new Postback(-1, inputModel.PostbackType, inputModel.Affiliate, inputModel.Url, inputModel.IsActive, DateTime.Now, DateTime.Now);
        if (postback == null)
          return this.Return(false, "Cannot create postback with these data!");
        postback.Insert();
        message = "Inserted!";
      }
      else
      {
        postback = new Postback(inputModel.PostbackID, inputModel.PostbackType, inputModel.Affiliate, inputModel.Url, inputModel.IsActive, DateTime.Now, DateTime.Now);
        if (postback == null)
          return this.Return(false, "Cannot create postback with these data!");
        postback.Update();
        message = "Updated!";
      }

      return this.Return(true, message, new { id = postback.ID });
    }
   
    public ActionResult DeletePostback()
    {
      DeletePostbackInputModel model = new DeletePostbackInputModel();
      if (model.HasError)
        return Return(false, model.ErrorMessage);


      Postback postback = model.Postback;
      if (postback == null)
        return Return(false, "Cannot load postback!");
      
      postback.Delete();

      return Return(true, "Deleted!");
    }
    public ActionResult EditAffiliate()
    {
      AddAffiliateInputModel inputModel = new AddAffiliateInputModel();
      if (inputModel.HasError)
        return Return(false, inputModel.ErrorMessage);

      string message = "";
      Affiliate affiliate = null;

      if(inputModel.AffiliateID == -1)
      {
        affiliate = new Affiliate(-1, inputModel.AffiliateType, inputModel.Name, inputModel.Description, inputModel.SubIDName, inputModel.Contact, inputModel.Web, inputModel.IsActive, DateTime.Now, DateTime.Now);
        if (affiliate == null)
          return this.Return(false, "Cannot create affiliate with these data!");
        affiliate.Insert();
        message = "Inserted";
      }
      else
      {
        affiliate = new Affiliate(inputModel.AffiliateID, inputModel.AffiliateType, inputModel.Name, inputModel.Description, inputModel.SubIDName, inputModel.Contact, inputModel.Web, inputModel.IsActive, DateTime.Now, DateTime.Now);
        if (affiliate == null)
          return this.Return(false, "Cannot create affiliate with these data!");
        affiliate.Update();
        message = "Updated";
      }      
      
      

      return this.Return(true, message, new { id = affiliate.ID });

    }

  }
}