using Adeliate.Portal.Code.Cache;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Clicks
{
  public class ClicksIndexModel : AdliteModelBase
  {
    public int OfferID { get; set; }
    public List<Country> Countries = null;
    public List<DynamicPlatform> Platforms = null;
    public List<DynamicBrowser> Browsers = null;

    public ClicksIndexModel()
    {
      this.Countries = CountryCache.Request().Countries;
      if (this.Countries == null)
        this.Countries = new List<Country>();

      this.Platforms = DynamicPlatform.CreateManager().Load();
      if (this.Platforms == null)
        this.Platforms = new List<DynamicPlatform>();

      this.Browsers = DynamicBrowser.CreateManager().Load();
      if (this.Browsers == null)
        this.Browsers = new List<DynamicBrowser>();

      OfferID = 0;
    }

  }
}