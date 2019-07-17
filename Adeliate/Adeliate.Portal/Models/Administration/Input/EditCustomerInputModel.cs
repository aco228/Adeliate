using Adeliate.Web;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Administration.Input
{
  public class EditCustomerInputModel : InputModelBase
  {
    public int CustomerID { get; set; }
    public int AffiliateID { get; set; }
    public int CustomerTypeID { get; set; }
    public int CustomerStatusID { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string IsChecked { get; set; }
    public bool IsActive { get; set; }
    public Affiliate Affiliate { get; set; }
    public CustomerType CustomerType { get; set; }
    public CustomerStatus CustomerStatus { get; set; }
    public EditCustomerInputModel() : base(false)
    {

    }

    public override void Validation()
    {
      if (this.CustomerID == null)
      {
        this.HasError = true;
        this.ErrorMessage = "CsutomerID is not set!";
        return;
      }
      if (this.AffiliateID == null)
      {
        this.HasError = true;
        this.ErrorMessage = "AffiliateId is not set!";
        return;
      }
      if (this.CustomerTypeID == null)
      {
        this.HasError = true;
        this.ErrorMessage = "CustomerTypeID is not set!";
        return;
      }
      if (this.CustomerStatusID == null)
      {
        this.HasError = true;
        this.ErrorMessage = "CustomerStatusID is not set!";
        return;
      }
      if (string.IsNullOrEmpty(Username))
      {
        this.HasError = true;
        this.ErrorMessage = "Username is not set!";
        return;
      }
      if (string.IsNullOrEmpty(Password))
      {
        this.HasError = true;
        this.ErrorMessage = "Password is not set!";
        return;
      }

      Affiliate = Affiliate.CreateManager().Load(AffiliateID);
      if(Affiliate == null)
      {
        this.HasError = true;
        this.ErrorMessage = "Cannot create Affiliate with this id!";
        return;
      }

      CustomerType = CustomerType.CreateManager().Load(CustomerTypeID);
      if (CustomerType == null)
      {
        this.HasError = true;
        this.ErrorMessage = "CustomerType create Affiliate with this id!";
        return;
      }

      CustomerStatus = (CustomerStatus)Enum.ToObject(typeof(CustomerStatus), CustomerStatusID);
      if (CustomerStatus == null)
      {
        this.HasError = true;
        this.ErrorMessage = "Cannot load CustomerStatus with this id!";
        return;
      }

      bool b;
      if (!Boolean.TryParse(IsChecked, out b))
      {
        this.HasError = true;
        this.ErrorMessage = "Cannot parse IsChecked!";
        return;
      }
      IsActive = b;

    }
  }
}