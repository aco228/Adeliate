using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Code.Cache
{
  public class AffiliateOfferCacheClicks
  {

    private int _clicks;
    private int _transactions;
    private DateTime _created;
    private AffiliateOfferCache _offer;

    public int Clicks
    {
      get
      {
        this.Check();
        return this._clicks;
      }
      set
      {
        if (!this.Check())
          this._clicks++;
      }
    }

    public int Transactions
    {
      get
      {
        this.Check();
        return this._transactions;
      }
      set
      {
        if (!this.Check())
          this._transactions++;
      }
    }

    public AffiliateOfferCacheClicks(AffiliateOfferCache offer)
    {
      this._offer = offer;
      this.Load();
    }

    public bool Check()
    {
      if ((DateTime.Now - this._created).Days > 0)
      {
        this.Load();
        return true;
      }
      return false;
    }

    private void Load()
    {
      var clickInformations = PortalApplication.StatManager.GetCurrentClickInformationsOffer(this._offer.OfferID, Adlite.Data.StatType.DAY);
      this._clicks = clickInformations.Clicks;
      this._transactions = clickInformations.Transactions;
      this._created = DateTime.Now;
    }
    
  }
}