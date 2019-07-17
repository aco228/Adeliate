using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adeliate
{
  public static class AdliteDateTime
  {

    public static string AdliteDateTimeString(this DateTime dateTime)
    {
      return dateTime.ToString("yyyy-MM-dd HH:mm:ss.mmm");
    }

    public static string DateTimeToString(DateTime datetime)
    {
      return datetime.ToString("yyyy-MM-dd HH:mm:ss.mmm");
    }
    
    public static string DatePrint(string input)
    {
      DateTime dt;
      if (!DateTime.TryParse(input, out dt))
        return string.Empty;
      return AdliteDateTime.DatePrint(dt);
    }
    
    public static string DatePrint(DateTime dt)
    {
      if (dt.Day == DateTime.Now.Day && dt.Month == DateTime.Now.Month && dt.Year == DateTime.Now.Year)
        return string.Format("{0}:{1}.{2}", dt.Hour, dt.Minute, dt.Second);
      else if (dt.Year == DateTime.Now.Year)
        return string.Format("{0}/{1} {2}:{3}.{4}", dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
      else
        return string.Format("{0}/{1}/{2} {3}:{4}.{5}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);

    }

  }
}
