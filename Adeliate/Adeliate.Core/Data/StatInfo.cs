using log4net;
using Senti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Adlite.Data 
{
  public partial interface IStatInfoManager 
  {

    StatInfo Load(StatType type, Offer offer);
    StatInfo Load(IConnectionInfo connection, StatType type, Offer offer);
  }

  public partial class StatInfo
  {

    #region #logging#
    private static ILog _log = null;

    protected static ILog Log
    {
      get
      {
        if (StatInfo._log == null)
          StatInfo._log = LogManager.GetLogger(typeof(StatInfo));
        return StatInfo._log;
      }
    }
    #endregion

    protected enum Type { Click, Transaction };

    public static void UpdateClick(Offer offer)
    {
      IStatInfoManager manager = StatInfo.CreateManager();
      StatInfo.Update(manager, StatType.HOUR, Type.Click, offer, null);
      StatInfo.Update(manager, StatType.WEEK, Type.Click, offer, null);
      StatInfo.Update(manager, StatType.DAY, Type.Click, offer, null);
      StatInfo.Update(manager, StatType.MONTH, Type.Click, offer, null);
    }

    public static void UpdateTransactions(Offer offer, decimal? addedRevenue = null)
    {
      IStatInfoManager manager = StatInfo.CreateManager();
      StatInfo.Update(manager, StatType.HOUR, Type.Transaction, offer, addedRevenue);
      StatInfo.Update(manager, StatType.WEEK, Type.Transaction, offer, addedRevenue);
      StatInfo.Update(manager, StatType.DAY, Type.Transaction, offer, addedRevenue);
      Update(manager, StatType.MONTH, Type.Transaction, offer, addedRevenue);
    }

    private static void Update(IStatInfoManager manager, StatType statType, Type type, Offer offer, decimal? addedRevenue)
    {
      StatInfo stat = manager.Load(statType, offer);

      if (stat == null)
        stat = new StatInfo(-1, offer.ID, offer.Affiliate.ID, statType, 0, 0, null, DateTime.Now, DateTime.Now);

      if (type == Type.Click)
        stat.Clicks++;
      else
        stat.Transactions++;

      if(addedRevenue.HasValue)
      {
        if (stat.Revenue.HasValue)
          stat.Revenue = stat.Revenue.Value + addedRevenue.Value;
        else
          stat.Revenue = addedRevenue;
      }
    
      stat.UpdateOrInsert();
      Log.Debug("Stat isert = " + stat.ID);
    }
    
    
  }
}

