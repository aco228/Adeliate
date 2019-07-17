using Adeliate.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Test
{
  public class TestInputModel : InputModelBase
  {

    public string Name { get; set; }
    public int SomeID { get; set; }
    public DateTime SomeDate { get; set; }
    public bool SomeBool { get; set; }

    public Adlite.Data.Offer Offer { get; set; }

    public TestInputModel() : base(false) { }

    public override void Validation()
    {
      if(string.IsNullOrEmpty(this.Name))
      {
        this.HasError = true;
        this.ErrorMessage = "Name is not set";
        return;
      }

      this.Offer = Adlite.Data.Offer.CreateManager().Load(this.SomeID);

    }
  }
}