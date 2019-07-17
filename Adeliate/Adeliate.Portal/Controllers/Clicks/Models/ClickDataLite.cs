using Adlite.Direct.Data;
using Direct.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Controllers.Clicks.Models
{
  public class ClickDataLite
  {

    public string CountryCode = "";
    public string ClickID = "";
    public string SubID = "";
    public string Offer = "";
    public string MobileOperatorName = "";
    public string IPAddress = "";
    public string Platform = "";
    public string Browser = "";
    public string Revenue = "";
    public string TransactonCreated = "";
    public string ClickCreated = "";

    public string ClassName { get { return !string.IsNullOrEmpty(this.TransactonCreated) ? "trTransaction" : ""; } }
    public string RevenueString { get { return string.IsNullOrEmpty(this.Revenue) ? "" : this.Revenue + "€"; } }
        
    public static List<ClickDataLite> Load(InputModel input)
    {
      AdliteDirect db = AdliteDirect.Instance;

      string _offerSql = "";
      if (!string.IsNullOrEmpty(input.Offers))
        _offerSql = string.Format(" AND c.OfferID IN ({0})", input.Offers);

      string _countrySql = "";
      if (!string.IsNullOrEmpty(input.Countries))
        _countrySql = string.Format(" AND ci.CountryID IN ({0})", input.Countries);

      string _transactions = "";
      if (input.OnlyWithTransaction.Equals("1"))
        _transactions = string.Format(" AND t.TransactionID IS NOT NULL ");

      string _clickID = "";
      if (!string.IsNullOrEmpty(input.ClickID))
        _clickID = string.Format(" AND c.ClickID={0} ", input.ClickID);

      string _subID = "";
      if (!string.IsNullOrEmpty(input.SubID))
        _subID = string.Format(" AND c.SubID='{0}' ", input.SubID);

      string _ip = "";
      if (!string.IsNullOrEmpty(input.IPAddress))
        _ip = string.Format(" AND ci.IPAddress='{0}' ", input.IPAddress);

      string _browsers = "";
      if (!string.IsNullOrEmpty(input.Browsers))
        _browsers = string.Format(" AND ci.DynamicBrowserID IN ({0})", input.Browsers);

      string _platforms = "";
      if (!string.IsNullOrEmpty(input.Platforms))
        _platforms = string.Format(" AND ci.DynamicPlatformID IN ({0})", input.Platforms);


      string query = string.Format(@"
        SELECT 
          TOP {0}
	        c.ClickID,
	        c.SubID,
	        ci.CountryCode,
	        camp.Name AS OfferName,
	        ci.MobileOperatorName,
	        ci.IPAddress,
	        ci.PlatformName,
	        ci.BrowserName,
	        t.PriceInEuros,
	        t.Created AS TransactionCreated,
	        c.Created AS ClickCreated
        FROM Adlite.core.ClickInformation AS ci
        LEFT OUTER JOIN Adlite.core.Click AS c ON ci.ClickID=c.ClickID
        LEFT OUTER JOIN Adlite.core.Campaign AS camp ON ci.CampaignID=camp.CampaignID
        LEFT OUTER JOIN Adlite.core.[Transaction] AS t ON c.ClickID=t.ClickID
        WHERE 
	        c.AffiliateID={1}
          [ip] [clickID] [subID]
          [offers]
          [countries]
          [browsers] [platforms]
          [transaction]
        ORDER BY c.ClickID DESC", input.Limit, AdliteContext.Current.Customer.Affilite.ID)
        .Replace("[offers]", _offerSql)
        .Replace("[countries]", _countrySql)
        .Replace("[transaction]", _transactions)
        .Replace("[ip]", _ip)
        .Replace("[clickID]", _clickID)
        .Replace("[subID]", _subID)
        .Replace("[browsers]", _browsers)
        .Replace("[platforms]", _platforms);
      
      DirectContainer dc = db.LoadContainer(query);
      if (dc == null || !dc.HasValue)
        return new List<ClickDataLite>();

      List<ClickDataLite> result = new List<ClickDataLite>();
      foreach(DirectContainerRow row in dc.Rows)
      {
        ClickDataLite entry = new ClickDataLite();
        entry.ClickID = row.GetString("ClickID");
        entry.SubID = row.GetString("SubID");
        entry.CountryCode = row.GetString("CountryCode");
        entry.Offer = row.GetString("OfferName");
        entry.MobileOperatorName = row.GetString("MobileOperatorName");
        entry.IPAddress = row.GetString("IPAddress");
        entry.Platform = row.GetString("PlatformName");
        entry.Browser = row.GetString("BrowserName");
        entry.Revenue = row.GetString("PriceInEuros");
        entry.TransactonCreated = row.GetString("TransactionCreated");
        entry.ClickCreated= row.GetString("ClickCreated");
        result.Add(entry);
      }

      return result;
    }

  }
}