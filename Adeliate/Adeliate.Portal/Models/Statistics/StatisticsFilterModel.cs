using Adeliate.Portal.Code.Cache;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Statistics
{
  public class StatisticsFilterModel : AdliteModelBase
  {

    public List<Country> Countries = null;
    public List<MobileOperator> MobileOperators = null;
    public List<DynamicBrowser> DynamicBrowsers { get; set; }
    public List<DynamicPlatform> DynamicPlatforms { get; set; }

    public StatisticsFilterModel()
    {
      this.Countries = CountryCache.Request().Countries;
      this.MobileOperators = MobileOperatorCache.Request().MobileOperators;

      DynamicBrowsers = DynamicBrowser.CreateManager().Load();
      if (DynamicBrowsers == null)
        DynamicBrowsers = new List<DynamicBrowser>();

      DynamicPlatforms = DynamicPlatform.CreateManager().Load();
      if (DynamicPlatforms == null)
        DynamicPlatforms = new List<DynamicPlatform>();



    }
  }
}