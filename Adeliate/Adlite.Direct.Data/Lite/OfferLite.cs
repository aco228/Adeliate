using Direct.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adlite.Direct.Data.Lite
{
  public class OfferLite
  {
    private int _id = -1;
    private int _campaignID = -1;
    private int _affiliateID = -1;
    private int _countryID = -1;
    private string _key = string.Empty;
    private int _defaultCap = -1;
    private int? _offerCap = -1;
    private string _link = string.Empty;
    private string _fallbackLink = string.Empty;
    private Adlite.Data.Offer _offerData = null;
    private Adlite.Data.Affiliate _affiliateData = null;
    
    public int ID { get { return this._id; } set { this._id = value; } }
    public int CountryID { get { return this._countryID; } set { this._countryID = value; } }
    public int CampaignID { get { return this._campaignID; } set { this._campaignID = value; } }
    public int AffiliateID { get { return this._affiliateID; } set { this._affiliateID = value; } }
    public string Key { get { return this._key; } set { this._key = value; } }
    public int DefaultCap { get { return this._defaultCap; } set { this._defaultCap = value; } }
    public int? OfferCap { get { return this._offerCap; } set { this._offerCap = value; } }
    public string Link { get { return this._link; } set { this._link = value; } }
    public string FallbakLink { get { return this._fallbackLink; } set { this._fallbackLink = value; } }
    public Adlite.Data.Offer OfferData { get { return this._offerData; } set { this._offerData = value; } }
    public Adlite.Data.Affiliate AffiliateData { get { return this._affiliateData; } set { this._affiliateData = value; } }


    public OfferLite() { }
    public OfferLite(Adlite.Data.Offer offer)
    {
      this._id = offer.ID;
      this._campaignID = offer.Campaign.ID;
      this._affiliateID = offer.Affiliate.ID;
      this._countryID = offer.Campaign.Country.ID;
      this._key = offer.Key;
      this._defaultCap = offer.Campaign.CapValue;
      this._offerCap = offer.CapValue;
      this._link = offer.Campaign.Link;
      this._fallbackLink = offer.Campaign.FallbackLink;
      this._offerData = offer;
      this._affiliateData = offer.Affiliate;
    }

    public static OfferLite LoadOfferByKey(AdliteDirect database, string key)
    {
      string sql = string.Format(@"
        SELECT 
	        o.OfferID, o.CampaignID, o.AffiliateID, o.CapValue, o.[Key], c.CountryID, c.CapValue AS DefaultCap, c.Link, c.FallbackLink
        FROM Adlite.core.Offer AS o
        LEFT OUTER JOIN Adlite.core.Campaign AS c ON o.CampaignID=c.CampaignID
        WHERE o.[Key]='{0}' AND o.IsActive=1", key);
      DirectContainer dc = database.LoadContainer(sql);

      if (!dc.HasValue || dc.RowsCount == 0)
        return null;

      OfferLite result = new OfferLite();
      result.ID = dc.GetInt("OfferID").Value;
      result.CountryID = dc.GetInt("CountryID").Value;
      result.AffiliateID = dc.GetInt("AffiliateID").Value;
      result.CampaignID = dc.GetInt("CampaignID").Value;
      result.OfferCap = dc.GetInt("CapValue");
      result.DefaultCap = dc.GetInt("DefaultCap").Value;
      result.Link = dc.GetString("Link");
      result.FallbakLink = dc.GetString("FallbackLink");
      result.OfferData = Adlite.Data.Offer.CreateManager().LazyLoad(result.ID);
      result.AffiliateData = Adlite.Data.Affiliate.CreateManager().Load(result.AffiliateID);


      return result;
    }
  }
}
