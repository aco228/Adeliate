using Adeliate.Web.Cache;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Code.Cache
{
  public partial class AffiliateCache : CacheObject
  {
    private static int HoldingTime = 90; // in minutes

    public override string ConstructKey() { return AffiliateCache.ConstructKey(this._affiliate); }
    public static string ConstructKey(Affiliate affiliate) { return string.Format(AffiliateCache.CacheKey, affiliate.ID); }
    public static string ConstructKey(int affiliate) { return string.Format(AffiliateCache.CacheKey, affiliate.ToString()); }

    public static AffiliateCache Request(int affiliateID)
    {
      Affiliate affiliate = Adlite.Data.Affiliate.CreateManager().Load(affiliateID);
      if (affiliate == null)
        return null;
      return AffiliateCache.Request(affiliate, false);
    }

    public static AffiliateCache Request(Affiliate affiliate, bool createNewIfCacheDoNotExists = true)
    {
      AffiliateCache cache = PortalApplication.CacheManager.Request<AffiliateCache>(AffiliateCache.ConstructKey(affiliate));
      if (cache != null)
        return cache;

      if (!createNewIfCacheDoNotExists)
        return null;

      cache = new AffiliateCache(affiliate);
      PortalApplication.CacheManager.Add(cache);
      return cache;
    }

  }
}