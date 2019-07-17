using System.Web;
using System.Web.Mvc;

namespace Adeliate.Web.Click
{
  public class FilterConfig
  {
    public static void RegisterGlobalFilters(GlobalFilterCollection filters)
    {
      filters.Add(new HandleErrorAttribute());
    }
  }
}
