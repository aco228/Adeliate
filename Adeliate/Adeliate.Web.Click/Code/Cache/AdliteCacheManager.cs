using Adeliate.Web.Cache;
using Adeliate.Web.Click.Code.Cache;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Web.Click
{
  public class AdliteCacheManager : CacheManager
  {
    
    public AdliteCacheManager Current { get { return AdliteClickApplication.CacheManager; } }

    public AdliteCacheManager() : base() { }

    public void Add(Offer offer)
    {
      OfferCache offerCache = this.Get(offer.Key);
      if (offerCache != null)
        return;

      offerCache = new OfferCache(offer);
      this.Add(offerCache);
    }
    
    public OfferCache Get(Offer offer)
    {
      return this.Get(offer.Key);
    }
    
    public OfferCache Get(string key)
    {
      if (!this.Objects.ContainsKey(key))
        return null;

      OfferCache obj = this.Objects[key] as OfferCache;
      if (obj == null)
        return null;

      obj.Requested();
      return obj;
    }

  }
}