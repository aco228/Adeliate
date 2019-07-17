using Adeliate.Web;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Administration.Input
{
  public class AffiliateOfferInputModel : InputModelBase
  {
    public int ID { get; set; }
    public Affiliate Affiliate { get; set; }

    public AffiliateOfferInputModel() : base(false)
    {
    }


    public override void Validation()
    {
      if(this.ID == null)
      {
        this.HasError = true;
        this.ErrorMessage = "Internal error";
        return;
      }

      this.Affiliate = Adlite.Data.Affiliate.CreateManager().Load(this.ID);
      if(this.Affiliate == null)
      {
        this.HasError = true;
        this.ErrorMessage = "There is no affiliate with provided ID";
        return;
      }
    }
  }
}