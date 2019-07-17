using Adeliate.Web.Click.Code.Cache;
using Adeliate.Web.Click.Code.IP;
using Adlite.Data;
using Adlite.Direct.Data;
using Adlite.Direct.Data.Lite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Web.Click.Code
{
  public class IPHelper
  {

    public static bool CheckIfIPIsInTheRange(Offer offer, string ip)
    {
      if (string.IsNullOrEmpty(offer.Campaign.IPRanges))
        return true;

      string[] ranges = offer.Campaign.IPRanges.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
      foreach (string range in ranges)
        if (IPRangesHelper.IsInTheRange(range, ip))
          return true;

      return false;
    }
    
    public static CountryIPMapLite LoadCountryByIP(string ip)
    {
      AdliteDirect db = AdliteDirect.Instance;
      string countryMapQuery = string.Format(@"SELECT CountryID FROM Adlite.core.[IPCountryMap] 
        WHERE FromAddress<={0} AND ToAddress>={0}", CountryIPMapLite.IPAddressToLong(ip));

      int? countryId = db.LoadInt(countryMapQuery);
      if (!countryId.HasValue)
        return null;
      
      CountryCache cache = CountryCache.Request(countryId.Value);
      if (cache == null)
        return null;

      CountryIPMapLite lite = new CountryIPMapLite();
      lite.CountryID = cache.CountryData.ID;
      lite.CountryIDString = cache.CountryData.ID.ToString();
      lite.CountryCode = cache.CountryData.TwoLetterIsoCode;

      return lite;
    }
    

  }
}