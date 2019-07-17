using Adeliate.Web.Cache;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Code.Cache
{
  public class ImageCache : CacheObject
  {
    private Image _image = null;

    public Image Image { get { return this._image; } set { this._image = value; } }

    public ImageCache(Image image, int expireValueInMinutes) : base(expireValueInMinutes)
    {
      this._image = image;
    }

    public override string ConstructKey()
    {
      return ImageCache.ConstructKey(this._image);
    }

    public static string ConstructKey(Image image)
    {
      return string.Format("image_{0}", image.ID);
    }
     
    public static string ConstructKey(int id)
    {
      return string.Format("image_{0}", id);
    }

    public static ImageCache Request(int id)
    {
      ImageCache cache = PortalApplication.CacheManager.Request<ImageCache>(ImageCache.ConstructKey(id));
      if (cache != null)
        return cache;

      Adlite.Data.Image image = Adlite.Data.Image.CreateManager().Load(id);
      if (image == null)
        return null;

      cache = new ImageCache(image, 3);
      PortalApplication.CacheManager.Add(cache);
      return cache;
    }

    protected override void OnPing()
    {
    }
  }
}