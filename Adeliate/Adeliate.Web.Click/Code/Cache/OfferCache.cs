using Adeliate.Web.Cache;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Web.Click.Code.Cache
{
  public class OfferCache : CacheObject
  {
    private DateTime _lastInteraction;
    private Offer _offer = null;
    
    public Offer Offer { get { return this._offer; } }

    public OfferCache(Offer offer) : base(999)
    {
      this._lastInteraction = DateTime.Now;
      this._offer = offer;
    }

    public void Reload()
    {
      this._offer = Adlite.Data.Offer.CreateManager().Load(this._offer.ID);
    }

    public override string ConstructKey()
    {
      return this._offer.Key;
    }

    protected override void OnPing()
    {  
    }

    public void OnClick()
    {
    }
    
  }
}