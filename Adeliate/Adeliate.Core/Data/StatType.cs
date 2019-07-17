using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adlite.Data 
{
  public enum StatType
  {
    HOUR = 1,
		DAY = 2,
		WEEK = 3,
		MONTH = 4
  }

  public enum AdliteDayOfTheWeek
  {
    Monday = 0,
    Tuesday = 1,
    Wednesday = 2,
    Thursday = 3,
    Friday = 4,
    Saturday = 5,
    Sunday = 6,
  }
  
  public class StatTypeManager
  {
    public static AdliteDayOfTheWeek GetDayOfTheWeek(DayOfWeek day)
    {
      switch (day.ToString())
      {
        case "Monday": return AdliteDayOfTheWeek.Monday;
        case "Tuesday": return AdliteDayOfTheWeek.Tuesday;
        case "Wednesday": return AdliteDayOfTheWeek.Wednesday;
        case "Thursday": return AdliteDayOfTheWeek.Thursday;
        case "Friday": return AdliteDayOfTheWeek.Friday;
        case "Saturday": return AdliteDayOfTheWeek.Saturday;
        default:
        case "Sunday": return AdliteDayOfTheWeek.Sunday;
      }

    }

    public static DateTime GetDate(StatType type)
    {
      DateTime result;
      switch (type)
      {
        case StatType.HOUR:
          result = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);
          break;
        case StatType.DAY:
          result = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
          break;
        case StatType.WEEK:
          result = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0).AddDays((int)GetDayOfTheWeek(DateTime.Now.DayOfWeek) * -1);
          break;
        default:
        case StatType.MONTH:
          result = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
          break;
      }

      return result;
    }
  }


}

