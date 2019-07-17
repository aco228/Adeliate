using Adeliate.Web;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Administration.Input
{
  public class AddAffiliateInputModel : InputModelBase
  {

    public int AffiliateID { get; set; }
    public int AffiliateTypeID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string SubIDName { get; set; }
    public string Contact { get; set; }
    public string Web { get; set; }
    public bool IsActive { get; set; }

    public AffiliateType AffiliateType { get; set; }

    public AddAffiliateInputModel() : base(false) { }

    public override void Validation()
    {
      if (AffiliateID == null)
      {
        this.HasError = true;
        this.ErrorMessage = "AffiliateID is not set";
        return;
      }
      if (AffiliateTypeID == null)
      {
        this.HasError = true;
        this.ErrorMessage = "AffiliateTypeID is not set";
        return;
      }

      if (string.IsNullOrEmpty(Name))
      {
        this.HasError = true;
        this.ErrorMessage = "Name is not set";
        return;
      }

      if (string.IsNullOrEmpty(Contact))
      {
        this.HasError = true;
        this.ErrorMessage = "Contact is not set";
        return;
      }

      if (string.IsNullOrEmpty(Web))
      {
        this.HasError = true;
        this.ErrorMessage = "Web is not set";
        return;
      }

      AffiliateType = Adlite.Data.AffiliateType.CreateManager().Load(AffiliateTypeID);

    }
  }
}