using Adeliate.Web;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Administration.Input
{
  public class DeletePostbackInputModel : InputModelBase
  {
    public int PostbackID { get; set; }
    public Postback Postback { get; set; }
    public DeletePostbackInputModel() : base(false)
    {
    }

    public override void Validation()
    {
      if(this.PostbackID == null)
      {
        this.HasError = true;
        this.ErrorMessage = "PostbackID isn ot set!";
        return;
      }
      Postback = Postback.CreateManager().Load(PostbackID);
      if(Postback == null)
      {
        this.HasError = true;
        this.ErrorMessage = "Cannot load postback with this id!";
        return;
      }

    }
  }
}