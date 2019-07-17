using Adeliate.Web.Cache;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Code.Cache
{
  public class CountryCache : CacheObject
  {
    public static string CountryCacheKey = "countrycache";

    private List<Country> _countries = null;

    public List<Country> Countries { get { return this._countries; } }

    public CountryCache() : base(360)
    {
      this._countries = Country.CreateManager().Load();
    }

    public override string ConstructKey()
    {
      return CountryCache.CountryCacheKey;
    }

    public static CountryCache Request()
    {
      return PortalApplication.CacheManager.Request<CountryCache>(CountryCache.CountryCacheKey);
    }

    protected override void OnPing()
    {
    }
  }
}