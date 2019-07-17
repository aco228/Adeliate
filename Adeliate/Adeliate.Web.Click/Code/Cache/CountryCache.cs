using Adeliate.Web.Cache;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Web.Click.Code.Cache
{
  public class CountryCache : CacheObject
  {
    private Country _country = null;

    public Country CountryData { get { return this._country; } }

    public CountryCache(Country country) : base(999)
    {
      this._country = country;
    }

    public override string ConstructKey() { return CountryCache.ConstructKey(this._country); }
    public static string ConstructKey(Country country) { return string.Format("country_{0}", country.ID); }
    public static string ConstructKey(int id) { return string.Format("country_{0}", id); }

    public static CountryCache Request(int countryID)
    {
      CountryCache cache = AdliteClickApplication.CacheManager.Request<CountryCache>(CountryCache.ConstructKey(countryID));
      if (cache != null)
        return cache;

      Country country = Country.CreateManager().Load(countryID);
      if (country == null)
        return null;
      return CountryCache.Request(country);
    }

    public static CountryCache Request (Country country)
    {
      CountryCache cache = AdliteClickApplication.CacheManager.Request<CountryCache>(CountryCache.ConstructKey(country));
      if (cache != null)
        return cache;

      cache = new CountryCache(country);
      AdliteClickApplication.CacheManager.Add(cache);
      return cache;
    }

  }
}