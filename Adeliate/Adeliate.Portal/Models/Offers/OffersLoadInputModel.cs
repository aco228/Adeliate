using Adeliate.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Offers
{
  public class OffersLoadInputModel : InputModelBase
  {

    public string Keyword { get; set; }
    public int Top { get; set; }
    public int LastID { get; set; }
    public int CountryID { get; set; }
    public int MobileOperatorID { get; set; }
    public int Device { get; set; }
    public int Flow { get; set; }
    public int ContentType { get; set; }

    public OffersLoadInputModel() : base(true)
    {
    }

    public override void Validation()
    {
    }
  }
}