using Adeliate.Web.Cache;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Code.Cache
{
  public class MobileOperatorCache : CacheObject
  {
    public static string MobileOperatorCacheKey = "mobileoperatorcache";

    private List<MobileOperator> _countries = null;

    public List<MobileOperator> MobileOperators { get { return this._countries; } }

    public MobileOperatorCache() : base(360)
    {
      this._countries = MobileOperator.CreateManager().Load();
    }

    public override string ConstructKey()
    {
      return Cache.MobileOperatorCache.MobileOperatorCacheKey;
    }
    
    public static MobileOperatorCache Request()
    {
      return PortalApplication.CacheManager.Request<MobileOperatorCache>(MobileOperatorCache.MobileOperatorCacheKey);
    }


  }
}