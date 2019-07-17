using Adeliate.Portal.Code.Hubs;
using Adeliate.Web.Cache;
using Adeliate.Web.Stats;
using Adlite.Data;
using Adlite.Direct.Data;
using Direct.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Code.Cache
{
  public partial class AffiliateCache : CacheObject
  {

    private Adlite.Data.Affiliate _affiliate = null;
    private AdliteAffiliate _adliteAffiliate = null;
    private List<AffiliateOfferCache> _offers = null;
    private List<AffiliateNotificationsCache> _notifications = null;
    private List<AffiliateTransactionsCache> _transactionsNotification = null;
    private static string CacheKey = "affiliate_{0}";
    private int _clicks = 0;
    private int _transactions = 0;
    private decimal _revenue = 0;

    public Adlite.Data.Affiliate Affiliate { get { return this._affiliate; } }
    public int Clicks { get { return this._clicks; } set { this._clicks = value; } }
    public int Transactions { get { return this._transactions; } set { this._transactions = value; } }
    public List<AffiliateOfferCache> Offers { get { return this._offers; } }
    public int OfferCount { get { return this._offers == null ? 0 : this._offers.Count; } }
    public decimal Revenue { get { return this._revenue; } }
    public string RevenueString { get { return string.Format("{0}€", this._revenue); } }
    public List<AffiliateNotificationsCache> Notifications { get { return this._notifications; } }
    public List<AffiliateTransactionsCache> TransactionsNotification { get { return this._transactionsNotification; } }
    
    public AffiliateCache(Adlite.Data.Affiliate affiliate) : base(HoldingTime)
    {
      this._affiliate = affiliate;
      this._adliteAffiliate = this._affiliate.Instantiate();
      this.Load();
    }

    public AffiliateCache(int expireValueInMinutes) : base(expireValueInMinutes)
    {
    }

    public void Load()
    {
      ClickInformations clickInformations = PortalApplication.StatManager.GetCurrentClickInformationsAffiliate(this._affiliate.ID, StatType.MONTH);
      this._clicks = clickInformations.Clicks;
      this._transactions = clickInformations.Transactions;
      this._revenue = PortalApplication.StatManager.GetRevenue(this._affiliate.ID, StatType.MONTH);
      this._notifications = AffiliateNotificationsCache.Load(this);
      this._transactionsNotification = AffiliateTransactionsCache.Load(this);

      this.UpdateOffers();
    }

    public void UpdateOffers()
    {
      this._offers = AffiliateOfferCache.Load(this._affiliate);
      if (this._offers == null)
        this._offers = new List<AffiliateOfferCache>();
    }
    
    public void NewClick(Adlite.Data.Click click)
    {
      AffiliateOfferCache offer = (from o in this._offers where o.OfferID == click.Offer.ID select o).FirstOrDefault();
      if (offer != null)
        offer.Clicks++;

      this._clicks++;
    }

    public void NewTransaction(Adlite.Data.Click click)
    {
      int? priceID = AdliteDirect.Instance.LoadInt(string.Format("SELECT PriceID FROM Adlite.core.[Transaction] WHERE ClickID={0} ORDER BY TransactionID DESC ", click.ID));
      if(priceID.HasValue)
      {
        Price price = Price.CreateManager().Load(priceID.Value);
        if(price != null)
        {
          this._revenue += PortalApplication.CurrencyManager.ConvertToEur(price);
          ClickHub.Current.UpdateRevenue(this.RevenueString, this._affiliate.ID);
        }
      }

      AffiliateOfferCache offer = (from o in this._offers where o.OfferID == click.Offer.ID select o).FirstOrDefault();
      if (offer != null)
        offer.Transactions++;

      if (this._transactionsNotification.Count == AffiliateTransactionsCache.LIMIT)
        this._transactionsNotification.RemoveAt(this._transactionsNotification.Count - 1);

      this._transactionsNotification.Add(new AffiliateTransactionsCache()
      {
        CampaignID = click.Offer.Campaign.ID,
        Name = click.Offer.Campaign.Name,
        Created = DateTime.Now.ToString()
      });
      
      this._transactions++;
    }

    public void AddNotification(Adlite.Data.AffiliateNotification notification)
    {
      if (this._notifications == null)
        return;

      AffiliateNotificationsCache cache = (from c in this._notifications where c.ID == notification.ID select c).FirstOrDefault();
      if (cache != null)
        return;

      if(this._notifications.Count == AffiliateNotificationsCache.LIMIT)
        this._notifications.RemoveAt(this._notifications.Count - 1);

      this._notifications.Insert(0, new AffiliateNotificationsCache()
      {
        ID = notification.ID,
        CampaignIDValue = (notification.Campaign != null ? notification.Campaign.ID : (int?)null),
        Title = notification.Title,
        Text = notification.Text
      });

    }

    public List<AffiliateOfferCache> GetOffers(AffiliateOfferSearchFilter filter)
    {
      if (this._offers == null)
        this.UpdateOffers();
    
      List<AffiliateOfferCache> result = this._offers;

      if (this._offers == null || this._offers.Count == 0)
        return new List<AffiliateOfferCache>();

      if (filter.SkipOnesWithoutTransactions)
        result = (from c in this._offers where c.Transactions > 0 select c).ToList();

      if (filter.Skip > this._offers.Count)
        return new List<AffiliateOfferCache>();

      if (filter.Take > this._offers.Count)
        filter.Take = this._offers.Count;

      switch (filter.Type)
      {
        case AffiliateOfferSearchFilter.DescendingType.DescendingByClicks:
          result = result.OrderByDescending(x => x.Clicks).ToList();
          break;
        case AffiliateOfferSearchFilter.DescendingType.DescendingByTransactions:
          result = result.OrderByDescending(x => x.Transactions).ToList();
          break;
        case AffiliateOfferSearchFilter.DescendingType.DescendingByCreated:
          result = result.OrderByDescending(x => x.OfferID).ToList(); 
          break;
        default:
          break;
      }

      result = result.Skip(filter.Skip).Take(filter.Take).ToList();
      
      return result;
    }
    
  }
}