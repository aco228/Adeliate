using Adeliate.Portal.Code.Hubs;
using Adeliate.Portal.Code.Lite;
using Adeliate.Portal.Filter;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adeliate.Portal.Controllers.Notifications
{
  [LoginFilter]
  public class InternalNotificationsController : AdliteController
  {
    
    public ActionResult Test(string cid)
    {
      string text = Request["text"] != null ? Request["text"].ToString() : "This is test notification";
      string title = Request["title"] != null ? Request["title"].ToString() : "This is the title";

      NotificationsHub.Current.NewNotification(title, text, Campaign.CreateManager().Load(25), AdliteContext.Current.Customer.Affilite);
      return this.Content("OK");
    }
    
  }
}