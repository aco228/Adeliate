using Adeliate.Web;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Administration.Input
{
  public class AddPostbackInputModel : InputModelBase
  {
    public int PostbackID { get; set; }
    public int AffiliateID { get; set; }
    public string Url { get; set; }
    public bool IsActive { get; set; }
    public int PostbackTypeID { get; set; }
    

    public Affiliate Affiliate { get; set; }
    public PostbackType PostbackType { get; set; }

    public AddPostbackInputModel() : base(false) { }

    public override void Validation()
    {
      if(string.IsNullOrEmpty(this.Url))
      {
        this.HasError = true;
        this.ErrorMessage = "URL is not set";
        return;
      }

      if (this.PostbackTypeID == null)
      {
        this.HasError = true;
        this.ErrorMessage = "PostbackTypeID is not set";
        return;
      }
      if (this.PostbackID == null)
      {
        this.HasError = true;
        this.ErrorMessage = "PostbackID is not set";
        return;
      }
      if (this.AffiliateID == null)
      {
        this.HasError = true;
        this.ErrorMessage = "PostbackTypeID is not set";
        return;
      }

      this.Affiliate = Adlite.Data.Affiliate.CreateManager().Load(this.AffiliateID);
      this.PostbackType = Adlite.Data.PostbackType.CreateManager().Load(this.PostbackTypeID);
    }
  }
}