using Adeliate.Web;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Login.Input
{
  public class LoginInputModel : InputModelBase
  {

    public string Username { get; set; }
    public string Password { get; set; }
    public Customer Customer { get; set; }
    public LoginInputModel() : base(false)
    {
    }

    public override void Validation()
    {
      Customer = Adlite.Data.Customer.CreateManager().Load(Username);
      if(Customer == null)
      {
        HasError = true;
        ErrorMessage = "Customer with this username is not registred!";
        return;
      }
      else{
        if(Customer.Password != Password)
        {
          HasError = true;
          ErrorMessage = "Wrong password!";
          return; 
        }
        else if(Customer.IsActive == false)
        {
          HasError = true;
          ErrorMessage = "Customer is not active!";
          return;
        }

      }  
    }
  }
}