using Adlite.Data;
using Adlite.Direct.Data;
using Direct.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Code.Stats.CurrentStatManager
{
  public class CurrentStatManager
  {

    public static ClickInformations GetCurrentClickInformationsAffiliate(int affiliateID, StatType stat)
    {
      AdliteDirect database = AdliteDirect.Instance;

      string query = string.Format(@"
        SELECT Clicks, Transactions FROM Adlite.log.StatInfo WHERE StatTypeID={0} AND AffiliateID={1} AND Created >= '{2}';",
        (int)stat,
        affiliateID,
        database.Date(StatTypeManager.GetDate(stat)));

      DirectContainer dc = database.LoadContainer(query);
      ClickInformations result = new ClickInformations();
      if (dc == null || !dc.HasValue)
        return result;

      result.Clicks = dc.GetInt("Clicks").Value;
      result.Transactions = dc.GetInt("Transactions").Value;

      return result;
    }

    public static decimal GetRevenue(int affiliateID, StatType stat)
    {
      AdliteDirect database = AdliteDirect.Instance;

      string query = string.Format(@"
        SELECT p.Value, c.Abbreviation FROM Adlite.core.[Transaction] AS t
        LEFT OUTER JOIN Adlite.core.Price AS p ON t.PriceID=p.PriceID
        LEFT OUTER JOIN Adlite.core.Currency AS c ON p.CurrencyID=c.CurrencyID
        WHERE t.AffiliateID={0} AND t.Created >= '{1}';",
        affiliateID,
        database.Date(StatTypeManager.GetDate(stat)));

      DirectContainer dc = database.LoadContainer(query);
      if (dc == null || !dc.HasValue)
        return 0;

      decimal result = 0;
      foreach(DirectContainerRow row in dc.Rows)
      {
        decimal? value = row.GetDecimal("Value");
        if (!value.HasValue)
          continue;

        result += PortalApplication.CurrencyManager.ConvertToEur(row.GetString("Abbreviation"), value.Value);
      }

      return result;
    }

    public static ClickInformations GetCurrentClickInformationsOffer(int offerID, StatType stat)
    {
      AdliteDirect database = AdliteDirect.Instance;

      string query = string.Format(@"
        SELECT Clicks, Transactions FROM Adlite.log.StatInfo WHERE StatTypeID={0} AND OfferID={1} AND Created >= '{2}';",
        (int)stat,
        offerID,
        database.Date(StatTypeManager.GetDate(stat)));

      DirectContainer dc = database.LoadContainer(query);
      ClickInformations result = new ClickInformations();
      if (dc == null || !dc.HasValue)
        return result;

      result.Clicks = dc.GetInt("Clicks").Value;
      result.Transactions = dc.GetInt("Transactions").Value;

      return result;
    }

  }
}