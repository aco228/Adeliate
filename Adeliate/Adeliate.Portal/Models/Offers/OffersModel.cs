using Adeliate.Portal.Code.Cache;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Offers
{
  public class OffersModel : AdliteModelBase
  {
    public List<Country> Countries = null;
    public List<MobileOperator> MobileOperators = null;

    public OffersModel()
    {
      this.Countries = CountryCache.Request().Countries;
      this.MobileOperators = MobileOperatorCache.Request().MobileOperators;
    }

  }
}