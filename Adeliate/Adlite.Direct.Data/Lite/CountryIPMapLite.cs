using Adlite.Data;
using Direct.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Adlite.Direct.Data.Lite
{
  public class CountryIPMapLite
  {

    public string CountryCode;
    public string Region;
    public string City;
    public string Latitude;
    public string Longtitude;
    public string ISP_Name;
    public string DomainName;
    public string MCC;
    public string MNC;
    public string MobileBrand;
    public string UsageType;
    public string CountryIDString;
    public string MobileOperatorIDString;

    public int CountryID = -1;
    public int MobileOperatorID = -1;

    public static CountryIPMapLite LoadByIPWithMno(string ip)
    {
      AdliteDirect db = AdliteDirect.Instance;

      string countryMapQuery = string.Format(@"SELECT CountryID FROM Adlite.core.[IPCountryMap] 
        WHERE FromAddress<={0} AND ToAddress>={0}", CountryIPMapLite.IPAddressToLong(ip));

      int? countryId = db.LoadInt(countryMapQuery);
      if (!countryId.HasValue)
        return null;

      Country country = Country.CreateManager().Load(countryId.Value);
      if (country == null)
        return null;

      string query = string.Format(
        @"SELECT CountryCode, Region, City, Latitude, Longtitude, ISP_Name, DomainName, MCC, MNC, MobileBrand, UsageType, CountryID, MobileOperatorID
          FROM Adlite.core.IPCountryMobileOperatorMap
          WHERE FromAddress<={0} AND ToAddress>={0} AND CountryID={1}",
        CountryIPMapLite.IPAddressToLong(ip), country.ID);
      DirectContainer dc = db.LoadContainer(query);

      if (dc == null || !dc.HasValue)
        return null;

      CountryIPMapLite result = new CountryIPMapLite();
      result.CountryCode = dc.GetString("CountryCode");
      result.Region = dc.GetString("Region");
      result.City = dc.GetString("City");
      result.Latitude = dc.GetString("Latitude");
      result.Longtitude = dc.GetString("Longtitude");
      result.ISP_Name = dc.GetString("ISP_Name");
      result.DomainName = dc.GetString("DomainName");
      result.MCC = dc.GetString("MCC");
      result.MNC = dc.GetString("MNC");
      result.MobileBrand = dc.GetString("MobileBrand");
      result.UsageType = dc.GetString("UsageType");
      result.CountryIDString = dc.GetString("CountryID");
      result.MobileOperatorIDString = dc.GetString("MobileOperatorID");

      if (!string.IsNullOrEmpty(result.CountryIDString))
        int.TryParse(result.CountryIDString, out result.CountryID);

      if (!string.IsNullOrEmpty(result.MobileOperatorIDString))
        int.TryParse(result.MobileOperatorIDString, out result.MobileOperatorID);

      return result;
    }


    public static CountryIPMapLite LoadByIP(string ip)
    {
      AdliteDirect db = AdliteDirect.Instance;
      string countryMapQuery = string.Format(@"SELECT CountryID FROM Adlite.core.[IPCountryMap] 
        WHERE FromAddress<={0} AND ToAddress>={0}", CountryIPMapLite.IPAddressToLong(ip));

      int? countryId = db.LoadInt(countryMapQuery);
      if (!countryId.HasValue)
        return null;

      Country country = Country.CreateManager().Load(countryId.Value);
      return null;

    }

    public static long IPAddressToLong(string addressText)
    {
      IPAddress address = IPAddress.None;
      if (!IPAddress.TryParse(addressText, out address))
        throw new InvalidOperationException();
      byte[] addressBytes = address.GetAddressBytes();
      long ipnum = 0;
      for (int i = 0; i < 4; ++i)
      {
        long y = addressBytes[i];
        if (y < 0)
          y += 256;
        ipnum += y << ((3 - i) * 8);
      }
      return ipnum;
    }




  }
}
