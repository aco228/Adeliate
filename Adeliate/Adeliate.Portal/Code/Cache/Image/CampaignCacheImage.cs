using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Adlite.Data;
using Adlite.Direct.Data;

namespace Adeliate.Portal.Code.Cache
{
  public class CampaignCacheImage : ImageCache
  {
    private Campaign _campaign = null;
    private static string ConstructPattern = "campaignimage_{0}";

    public CampaignCacheImage(Campaign campaign, Image image) : base(image, 10)
    {
      this._campaign = campaign;
    }

    public override string ConstructKey()
    {
      return string.Format(ConstructPattern, this._campaign.ID);
    }

    public static string ConstructKey(Campaign campaign)
    {
      return string.Format(ConstructPattern, campaign.ID);
    }

    public static string ConstructKey(int id)
    {
      return string.Format(ConstructPattern, id);
    }

    public static string ConstructKey(Image image)
    {
      int? campaignID = AdliteDirect.Instance.LoadInt(string.Format(@"
        SELECT c.CampaignID FROM Adlite.core.Campaign AS c
        LEFT OUTER JOIN Adlite.core.CampaignImageMap AS map ON map.CampaignID=c.CampaignID
        WHERE map.ImageID={0}", image.ID));

      if (!campaignID.HasValue)
        return string.Empty;

      return string.Format(ConstructPattern, campaignID.Value);
    }
    
    public static CampaignCacheImage Request(Campaign campaign)
    {
      if (campaign == null)
        return null;

      CampaignCacheImage cache = PortalApplication.CacheManager.Request<CampaignCacheImage>(CampaignCacheImage.ConstructKey(campaign));
      if (cache != null)
        return cache;

      int? imageID = AdliteDirect.Instance.LoadInt(string.Format(@"
        SELECT map.ImageID FROM Adlite.core.Campaign AS c
        LEFT OUTER JOIN Adlite.core.CampaignImageMap AS map ON map.CampaignID=c.CampaignID
        WHERE c.CampaignID={0} AND map.IsDefault=1", campaign.ID));

      if (!imageID.HasValue)
        return null;

      Adlite.Data.Image image = Adlite.Data.Image.CreateManager().Load(imageID.Value);
      if (image == null)
        return null;

      cache = new CampaignCacheImage(campaign, image);
      PortalApplication.CacheManager.Add(cache);
      return cache;
    }
  }
}