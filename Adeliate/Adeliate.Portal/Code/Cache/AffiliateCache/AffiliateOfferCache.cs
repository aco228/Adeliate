using Adeliate.Web.Stats;
using Adlite.Direct.Data;
using Direct.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Code.Cache
{
  public class AffiliateOfferCache
  {
    private int _offerID = -1;
    private int _campaignID = -1;
    private int _currencyID = -1;
    private int _countryID = -1;
    private int _cap = -1;
    private int _clicks = -1;
    private int _transactions = -1;
    private int _contentType = -1;
    private decimal _price;
    private string _symbol;
    private string _name = string.Empty;
    private string _countryCode = string.Empty;
    private AffiliateOfferCacheClicks _clickInformations = null;

    public int OfferID { get { return this._offerID; } set { this._offerID = value; } }
    public int CampaignID { get { return this._campaignID; } set { this._campaignID = value; } }
    public int CurrencyID { get { return this._currencyID; } set { this._currencyID = value; } }
    public int CountryID { get { return this._countryID; } set { this._countryID = value; } }
    public int ContentTypeID { get { return this._contentType; } set { this._contentType = value; } }
    public decimal Price { get { return this._price; } set { this._price = value; } }
    public string Symbol { get { return this._symbol; } set { this._symbol = value; } }
    public string Name { get { return this._name; } set { this._name = value; } }
    public string CountryCode { get { return this._countryCode; } set { this._countryCode = value; } }

    public int Cap { get { return this._cap; } set { this._cap = value; } }

    public int Clicks
    {
      get
      {
        if (this._clickInformations != null)
          return this._clickInformations.Clicks;
        this._clickInformations = new AffiliateOfferCacheClicks(this);
        this._clickInformations.Check();
        return this._clickInformations.Clicks;
      }
      set
      {
        if (this._clickInformations == null)
          this._clickInformations = new AffiliateOfferCacheClicks(this);
        else
          this._clickInformations.Clicks++;
      }
    }

    public int Transactions
    {
      get
      {
        if (this._clickInformations != null)
          return this._clickInformations.Transactions;
        this._clickInformations = new AffiliateOfferCacheClicks(this);
        return this._clickInformations.Transactions;
      }
      set
      {
        if (this._clickInformations == null)
          this._clickInformations = new AffiliateOfferCacheClicks(this);
        else
          this._clickInformations.Transactions++;
      }
    }
    
    public static List<AffiliateOfferCache> Load(Adlite.Data.Affiliate affiliate)
    {
      AdliteDirect database = AdliteDirect.Instance;
      string query = string.Format(
        @"SELECT 
              o.OfferID, c.CampaignID, c.Name, c.Description, c.CampaignContentTypeID,
              o.CapValue AS OfferCap,  c.CapValue AS CampaignCap, 
              oPrice.Value AS OfferPriceValue, oCurrency.Symbol AS OfferSymbol, oCurrency.CurrencyID AS OfferCurrencyID,
              cPrice.Value AS CampaignPriceValue, cCurrency.Symbol AS CampaignSymbol, cCurrency.CurrencyID AS CampaignCurrencyID,
              country.CountryID, country.TwoLetterIsoCode AS countryCode
          FROM Adlite.core.Offer AS o
          LEFT OUTER JOIN Adlite.core.Campaign AS c ON o.CampaignID=c.CampaignID
          LEFT OUTER JOIN Adlite.core.Price AS oPrice ON o.PriceID=oPrice.PriceID
          LEFT OUTER JOIN Adlite.core.Currency AS oCurrency ON oPrice.CurrencyID=oCurrency.CurrencyID
          LEFT OUTER JOIN Adlite.core.Price AS cPrice ON c.PriceID=cPrice.PriceID
          LEFT OUTER JOIN Adlite.core.Currency AS cCurrency ON cCurrency.CurrencyID=cPrice.CurrencyID
          LEFT OUTER JOIN Adlite.core.Country AS country ON c.CountryID=country.CountryID
          WHERE o.AffiliateID={0} AND o.IsActive=1
          ORDER BY OfferID DESC", affiliate.ID);
      DirectContainer dc = database.LoadContainer(query);

      if (dc == null || !dc.HasValue)
        return null;

      List<AffiliateOfferCache> result = new List<AffiliateOfferCache>();

      foreach(DirectContainerRow row in dc.Rows)
      {
        AffiliateOfferCache entry = new AffiliateOfferCache();
        entry.OfferID = row.GetInt("OfferID").Value;
        entry.CampaignID = row.GetInt("CampaignID").Value;
        entry.Name = row.GetString("Name");
        entry.CountryCode = row.GetString("countryCode");
        entry.CountryID = row.GetInt("CountryID").Value;
        entry.ContentTypeID = row.GetInt("CampaignContentTypeID").Value;

        int? offerCap = row.GetInt("OfferCap");
        if (offerCap.HasValue)
          entry.Cap = offerCap.Value;
        else
          entry.Cap = row.GetInt("CampaignCap").Value;

        decimal? priceValue = row.GetDecimal("OfferPriceValue");
        if(priceValue.HasValue)
        {
          entry.Price = priceValue.Value;
          entry.Symbol = row.GetString("OfferSymbol");
          entry.CurrencyID = row.GetInt("OfferCurrencyID").Value;
        }
        else
        {
          entry.Price = row.GetDecimal("CampaignPriceValue").Value;
          entry.Symbol = row.GetString("CampaignSymbol");
          entry.CurrencyID = row.GetInt("CampaignCurrencyID").Value;
        }
        
        
        result.Add(entry);
      }

      return result;
    }
  }
}