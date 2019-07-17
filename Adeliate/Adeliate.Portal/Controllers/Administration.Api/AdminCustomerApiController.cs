using Adeliate.Portal.Models.Administration.Input;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adeliate.Portal.Controllers.Administration.Api
{
  public class AdminCustomerApiController : AdminBaseApiController
  {


    public ActionResult EditCustomer()
    {
      
      string message = "";
      Customer customer = null;

      EditCustomerInputModel model = new EditCustomerInputModel();
      if (model.HasError)
        return this.Return(false, model.ErrorMessage);

      if(model.CustomerID == -1)
      {
        customer = new Customer(-1, model.Affiliate, model.CustomerType, model.CustomerStatus, model.Username, model.Password, model.IsActive, DateTime.Now, DateTime.Now);
        customer.Insert();
        message = "Inserted!";
      }
      else
      {
        customer = new Customer(model.CustomerID, model.Affiliate, model.CustomerType, model.CustomerStatus, model.Username, model.Password, model.IsActive, DateTime.Now, DateTime.Now);
        if (customer == null)
          return this.Return(false, "Cannot load customer with this id!");

        customer.Update();
        message = "Updated!";
      }

      return this.Return(true, message, new { id = customer.ID });
    }

  }
}