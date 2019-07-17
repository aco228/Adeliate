using Adlite.Data;
using Adlite.Direct.Data;
using Direct.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Web.Stats
{
  public class CurrentStatManager
  {
    private CurrencyManager _currencyManager = null;

    public CurrentStatManager(CurrencyManager currency)
    {
      this._currencyManager = currency;
    }

    public ClickInformations GetCurrentClickInformationsAffiliate(int affiliateID, StatType stat)
    {
      #region # old #

      //AdliteDirect database = AdliteDirect.Instance;

      //string query = string.Format(@"
      //  SELECT Clicks, Transactions FROM Adlite.log.StatInfo WHERE StatTypeID={0} AND AffiliateID={1} AND Created >= '{2}';",
      //  (int)stat,
      //  affiliateID,
      //  database.Date(StatTypeManager.GetDate(stat)));

      //DirectContainer dc = database.LoadContainer(query);
      //ClickInformations result = new ClickInformations();
      //if (dc == null || !dc.HasValue)
      //  return result;

      //result.Clicks = dc.GetInt("Clicks").Value;
      //result.Transactions = dc.GetInt("Transactions").Value;

      //return result;

      #endregion

      AdliteDirect database = AdliteDirect.Instance;

      string queryClicks = string.Format(@"
        SELECT COUNT(*) AS br FROM Adlite.core.Click WHERE AffiliateID={0} AND Created>='{1}';",
        affiliateID,
        database.Date(StatTypeManager.GetDate(stat)));

      string queryTransactions = string.Format(@"
        SELECT COUNT(*) AS br FROM Adlite.core.Click WHERE AffiliateID={0} AND HasTransaction=1 AND Created>='{1}';",
        affiliateID,
        database.Date(StatTypeManager.GetDate(stat)));

      DirectContainer dck = database.LoadContainer(queryClicks);
      DirectContainer dct = database.LoadContainer(queryTransactions);
      ClickInformations result = new ClickInformations();

      if (dck != null && dck.HasValue)
        result.Clicks = dck.GetInt("br").Value;
      
      if (dct != null && dct.HasValue)
        result.Transactions = dct.GetInt("br").Value;

      return result;
    }

    public decimal GetRevenue(int affiliateID, StatType stat)
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

        if(this._currencyManager != null)
          result += this._currencyManager.ConvertToEur(row.GetString("Abbreviation"), value.Value);

      }

      return result;
    }

    public ClickInformations GetCurrentClickInformationsOffer(int offerID, StatType stat)
    {
      #region # old #

      //AdliteDirect database = AdliteDirect.Instance;

      //string query = string.Format(@"
      //  SELECT Clicks, Transactions FROM Adlite.log.StatInfo WHERE StatTypeID={0} AND OfferID={1} AND Created >= '{2}';",
      //  (int)stat,
      //  offerID,
      //  database.Date(StatTypeManager.GetDate(stat)));

      //DirectContainer dc = database.LoadContainer(query);
      //ClickInformations result = new ClickInformations();
      //if (dc == null || !dc.HasValue)
      //  return result;

      //result.Clicks = dc.GetInt("Clicks").Value;
      //result.Transactions = dc.GetInt("Transactions").Value;

      //return result;

      #endregion

      AdliteDirect database = AdliteDirect.Instance;

      string queryClicks = string.Format(@"
        SELECT COUNT(*) AS br FROM Adlite.core.Click WHERE OfferID={0} AND Created>='{1}';",
        offerID,
        database.Date(StatTypeManager.GetDate(stat)));

      string queryTransactions = string.Format(@"
        SELECT COUNT(*) AS br FROM Adlite.core.Click WHERE OfferID={0} AND HasTransaction=1 AND Created>='{1}';",
        offerID,
        database.Date(StatTypeManager.GetDate(stat)));

      DirectContainer dck = database.LoadContainer(queryClicks);
      DirectContainer dct = database.LoadContainer(queryTransactions);
      ClickInformations result = new ClickInformations();

      if (dck != null && dck.HasValue)
        result.Clicks = dck.GetInt("br").Value;


      if (dct != null && dct.HasValue)
        result.Transactions = dct.GetInt("br").Value;
      
      return result;
    }

  }
}