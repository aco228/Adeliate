using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Adlite.Data;
using Direct.Core;
using Adlite.Direct.Data;

namespace Adeliate.Portal.Code.Cache
{
  public class CustomerCacheImage : ImageCache
  {
    private Customer _customer = null;
    private static string ConstructKeyPattern = "customerimage_{0}";
    
    public CustomerCacheImage(Customer customer, Image image) : base(image, 30)
    {
      this._customer = customer;
    }

    public override string ConstructKey()
    {
      return string.Format(ConstructKeyPattern, this._customer.ID);
    }

    public static string ConstructKey(Customer customer)
    {
      return string.Format(ConstructKeyPattern, customer.ID);
    }

    public static string ConstructKey(Image image)
    {
      int? customerID = AdliteDirect.Instance.LoadInt(string.Format(@"
        SELECT c.CustomerID FROM Adlite.core.Customer AS c
        LEFT OUTER JOIN Adlite.core.CustomerImageMap AS map ON c.CustomerID=map.CustomerID
        WHERE map.ImageID={0}", image.ID));

      if (!customerID.HasValue)
        return string.Empty;

      return string.Format(ConstructKeyPattern, customerID.Value);
    }

    public static string ConstructKey(int id)
    {
      return string.Format(ConstructKeyPattern, id);
    }

    public static CustomerCacheImage Request(Customer customer)
    {
      if (customer == null)
        return null;
      
      CustomerCacheImage cache = PortalApplication.CacheManager.Request<CustomerCacheImage>(CustomerCacheImage.ConstructKey(customer));
      if (cache != null)
        return cache;
      
      int? imageID = AdliteDirect.Instance.LoadInt(string.Format(@"
        SELECT map.ImageID FROM Adlite.core.Customer AS c
        LEFT OUTER JOIN Adlite.core.CustomerImageMap AS map ON c.CustomerID=map.CustomerID
        WHERE c.CustomerID={0}", customer.ID));

      if (!imageID.HasValue)
        return null;

      Adlite.Data.Image image = Adlite.Data.Image.CreateManager().Load(imageID.Value);
      if (image == null)
        return null;

      cache = new CustomerCacheImage(customer, image);
      PortalApplication.CacheManager.Add(cache);
      return cache;
    }

  }
}