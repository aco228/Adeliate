using Adeliate.Portal.Models.Offers;
using Adlite.Direct.Data;
using Direct.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Code.Lite
{
  public class OfferLoadLite
  {

    public string OfferID;
    public string CampaignID;
    public string Name;
    public string CampaignContentType;
    public string Description;
    public string CountryCode;
    public string CampaignPrice;
    public string CampaignPriceSymbol;
    public string OfferPrice;
    public string OfferPriceSymbol;
    public string OfferCap;
    public string CampaignCap;
    public List<string> CampaignFlows = new List<string>();
    public List<string> CampaignDevices = new List<string>();
    public List<string> MobileOperators = new List<string>();

    public string Price
    {
      get
      {
        if(!string.IsNullOrEmpty(this.OfferPrice))
          return string.Format("{0}{1}", this.OfferPrice, this.OfferPriceSymbol);
        return string.Format("{0}{1}", this.CampaignPrice, this.CampaignPriceSymbol);
      }
    }

    public string Cap
    {
      get
      {
        if (!string.IsNullOrEmpty(this.OfferCap))
          return this.OfferCap;
        return this.CampaignCap;
      }
    }
    
    public static List<OfferLoadLite> Load(OffersLoadInputModel input)
    {
      string _keyword = string.Empty;
      if (!string.IsNullOrEmpty(input.Keyword))
        _keyword = string.Format("AND (c.Name LIKE '%{0}%' OR c.Description LIKE '%{0}%')", input.Keyword);

      string _country = string.Empty;
      if (input.CountryID != -1)
        _country = string.Format("AND c.CountryID={0}", input.CountryID.ToString());

      string _whereID = string.Empty;
      if (input.LastID > 0)
        _whereID = string.Format(" AND OfferID<{0} ", input.LastID);
      
      string _mobileOperator = string.Empty;
      if(input.MobileOperatorID != -1)
        _mobileOperator = "INNER JOIN Adlite.core.CampaignMobileOperatorMap AS campaignMNO ON campaignMNO.CampaignID=c.CampaignID AND campaignMNO.MobileOperatorID=" + input.MobileOperatorID;

      string _device = string.Empty;
      if (input.Device != -1)
        _device = "INNER JOIN Adlite.core.CampaignDeviceMap AS cdevice ON cdevice.CampaignID=c.CampaignID AND cdevice.CampaignDeviceID=" + input.Device;

      string _flow = string.Empty;
      if (input.Flow != -1)
        _flow = "INNER JOIN Adlite.core.CampaignFlowMap AS flowMap ON flowMap.CampaignID=c.CampaignID AND flowMap.CampaignFlowID=" + input.Flow;
      
      string _contentType = string.Empty;
      if (input.ContentType != -1)
        _contentType = "AND c.CampaignContentTypeID=" + input.ContentType;

      string query = @"
        SELECT TOP [Top]
	        o.OfferID, c.CampaignID, c.Name, c.Description,
	        country.CountryCode, c.CampaignContentTypeID,
	        campaignPrice.Value AS 'CampaignPrice', campaignCurrency.Symbol  AS 'CampaignPriceSymbol',
	        offerPrice.Value AS 'OfferPrice', offerCurrency.Symbol AS 'OfferPriceSymbol',
	        o.CapValue AS 'OfferCap', c.CapValue AS 'CampaignCap'
        FROM Adlite.core.Offer AS o
        LEFT OUTER JOIN Adlite.core.Campaign AS c ON o.CampaignID=c.CampaignID
        LEFT OUTER JOIN Adlite.core.Price AS campaignPrice ON c.PriceID=campaignPrice.PriceID
        LEFT OUTER JOIN Adlite.core.Price AS offerPrice ON o.PriceID=offerPrice.PriceID
        LEFT OUTER JOIN Adlite.core.Currency AS offerCurrency ON offerPrice.CurrencyID=offerCurrency.CurrencyID
        LEFT OUTER JOIN Adlite.core.Currency AS campaignCurrency ON campaignPrice.CurrencyID=campaignCurrency.CurrencyID
        LEFT OUTER JOIN Adlite.core.Country country ON c.CountryID=country.CountryID
        [mobileOperator]
        [device]
        [flow]
        WHERE o.AffiliateID=[affiliateID]
          [keyword]
          [country]
          [contentType]
          [whereID]
          AND o.IsActive=1
        ORDER BY o.OfferID DESC"
      .Replace("[mobileOperator]", _mobileOperator)
      .Replace("[device]", _device)
      .Replace("[flow]", _flow)
      .Replace("[keyword]", _keyword)
      .Replace("[country]", _country)
      .Replace("[contentType]", _contentType)
      .Replace("[Top]", input.Top.ToString())
      .Replace("[whereID]", _whereID)
      .Replace("[affiliateID]", AdliteContext.Current.Customer.CustomerData.Affiliate.ID.ToString());

      DirectContainer dc = AdliteDirect.Instance.LoadContainer(query);
      if (dc == null || !dc.HasValue)
        return null;

      AdliteDirect database = AdliteDirect.Instance;
      List<OfferLoadLite> result = new List<OfferLoadLite>();

      foreach(DirectContainerRow row in dc.Rows)
      {
        OfferLoadLite element = new OfferLoadLite();
        element.OfferID = row.GetString("OfferID");
        element.CampaignID = row.GetString("CampaignID");
        element.Name = row.GetString("Name");
        element.CampaignContentType = row.GetString("CampaignContentTypeID");
        element.Description = row.GetString("Description");
        element.CountryCode = row.GetString("CountryCode");
        element.CampaignPrice = row.GetString("CampaignPrice");
        element.CampaignPriceSymbol = row.GetString("CampaignPriceSymbol");
        element.OfferPrice = row.GetString("OfferPrice");
        element.OfferPriceSymbol = row.GetString("OfferPriceSymbol");
        element.OfferCap = row.GetString("OfferCap");
        element.CampaignCap = row.GetString("CampaignCap");

        element.MobileOperators = database.LoadArrayString(
          string.Format(@"SELECT mo.Name FROM Adlite.core.MobileOperator AS mo
            LEFT OUTER JOIN Adlite.core.CampaignMobileOperatorMap AS map ON mo.MobileOperatorID=map.MobileOperatorID
            WHERE map.CampaignID={0}", element.CampaignID));

        element.CampaignFlows = database.LoadArrayString(
          string.Format(@"SELECT f.Name FROM Adlite.core.CampaignFlow AS f
            LEFT OUTER JOIN Adlite.core.CampaignFlowMap AS map ON map.CampaignFlowID=f.CampaignFlowID
            WHERE map.CampaignID={0}", element.CampaignID));

        element.CampaignDevices = database.LoadArrayString(
          string.Format(@"SELECT d.Name FROM Adlite.core.CampaignDevice AS d
            LEFT OUTER JOIN Adlite.core.CampaignDeviceMap AS map ON d.CampaignDeviceID=map.CampaignDeviceID
            WHERE map.CampaignID={0}", element.CampaignID));

        result.Add(element);
      }
      
      return result;
    }

  }
}