using Adlite.Direct.Data;
using Direct.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Statistics
{
  public class StatisticsLoadModel : AdliteModelBase
  {
    public List<StatisticRow> Rows { get; set; }

    public StatisticsLoadModel(StatisticsInputModel input)
    {
      string groupBy = input.GroupBy;
      string affiliateID = input.AffiliateID;
      string countries = "";
      string browsers = "";
      string platforms = "";
      string carriers = "";
      string dateFrom = "";
      string dateTo = "";

      switch (groupBy)
      {
        case "1":
          groupBy = "camp.Name";
          break;
        case "2":
          groupBy = "cy.GlobalName";
          break;
      }

      countries = input.Countries != "" ? string.Format("({0})", input.Countries) : "";
      browsers = input.Browsers != "" ? string.Format("({0})", input.Browsers) : "";
      platforms = input.Platforms != "" ? string.Format("({0})", input.Platforms) : "";
      carriers = input.MobileOperators != "" ? string.Format("({0})", input.MobileOperators) : "";
      dateFrom = input.DateFrom != "" ? input.DateFrom : "";
      dateTo = input.DateTo != "" ? input.DateTo : "";

      string _carriers = "";
      if (carriers != "")
        _carriers = string.Format("and ci.MobileOperatorID in {0}", carriers);

      string _countries = "";
      if (countries != "")
        _countries = string.Format("and ci.CountryID in {0}", countries);

      string _platforms = "";
      if (platforms != "")
        _platforms = string.Format("and ci.DynamicPlatformID in {0}", platforms);

      string _browsers = "";
      if (browsers != "")
        _browsers = string.Format("and ci.DynamicBrowserID in {0}", browsers);

      string _dateFrom = "";
      if (dateFrom != "")
        _dateFrom = string.Format("AND ci.Created >= '{0}'", dateFrom);

      string _dateTo = "";
      if (dateTo != "")
        _dateTo = string.Format("AND ci.Created <= '{0}'", dateTo);

      AdliteDirect ad = new AdliteDirect();

      string cr = "floor((cast(count(t.TransactionID) as decimal(10, 2)) / cast(count(c.ClickID) as decimal(10, 2))) *100)";
      string query = "";
      if (input.CampaignID == "-1") {
        query = @"  SELECT  [groupBy] as GroupBy, sum(t.PriceInEuros) as Payout , count(t.TransactionID) as Conversions, count(c.ClickID) as Clicks, [cr] as cr
                        FROM Adlite.core.Click AS c
                        LEFT OUTER JOIN Adlite.core.[Transaction] AS t ON c.ClickID=t.ClickID
                        LEFT OUTER JOIN Adlite.core.ClickInformation AS ci ON c.ClickID=ci.ClickID
                        LEFT OUTER JOIN Adlite.core.Country as cy ON cy.CountryID = ci.CountryID 
                        LEFT OUTER JOIN Adlite.core.Campaign AS camp ON ci.CampaignID=camp.CampaignID
                        where c.AffiliateID = [affiliateID]
                        [carrier]
                        [country]
                        [browser]
                        [platform]
                        [dateFrom]
                        [dateTo]
                        group by [groupBy]"
                        .Replace("[groupBy]", groupBy)
                        .Replace("[affiliateID]", affiliateID)
                        .Replace("[cr]", cr)
                        .Replace("[carrier]", _carriers)
                        .Replace("[browser]", _browsers)
                        .Replace("[country]", _countries)
                        .Replace("[platform]", _platforms)
                        .Replace("[dateFrom]", _dateFrom)
                        .Replace("[dateTo]", _dateTo);
      }
      else
      {
        query = @"  SELECT  [groupBy] as GroupBy, sum(t.PriceInEuros) as Payout , count(t.TransactionID) as Conversions, count(c.ClickID) as Clicks, [cr] as cr
                        FROM Adlite.core.Click AS c
                        LEFT OUTER JOIN Adlite.core.[Transaction] AS t ON c.ClickID=t.ClickID
                        LEFT OUTER JOIN Adlite.core.ClickInformation AS ci ON c.ClickID=ci.ClickID
                        LEFT OUTER JOIN Adlite.core.Country as cy ON cy.CountryID = ci.CountryID 
                        LEFT OUTER JOIN Adlite.core.Campaign AS camp ON ci.CampaignID=camp.CampaignID
                        where c.AffiliateID = [affiliateID]
                        AND ci.CampaignID = [CampaignID]
                        [carrier]
                        [country]
                        [browser]
                        [platform]
                        [dateFrom]
                        [dateTo]
                        group by [groupBy]"
                       .Replace("[groupBy]", groupBy)
                       .Replace("[affiliateID]", affiliateID)
                       .Replace("[cr]", cr)
                       .Replace("[carrier]", _carriers)
                       .Replace("[browser]", _browsers)
                       .Replace("[country]", _countries)
                       .Replace("[platform]", _platforms)
                       .Replace("[dateFrom]", _dateFrom)
                       .Replace("[dateTo]", _dateTo)
                       .Replace("[CampaignID]",input.CampaignID);
      }

      DirectContainer container = ad.LoadContainer(query);

      Rows = new List<StatisticRow>();

      foreach (DirectContainerRow row in container.Rows)
      {
        StatisticRow sr = new StatisticRow();
        sr.GroupBy = row.GetString("GroupBy");
        if (sr.GroupBy == "")
          sr.GroupBy = "Unknown";
        sr.Clicks = row.GetString("Clicks");
        sr.Payout = row.GetString("Payout")+ "€";
        if (sr.Payout == "€")
          sr.Payout = "0€";
        sr.Conversions = row.GetString("Conversions");
        sr.CR = row.GetString("cr")+"%";

        Rows.Add(sr);
      }

    }
  }

  public class StatisticRow
  {
    public string GroupBy { get; set; }
    public string Clicks { get; set; }
    public string Conversions { get; set; }
    public string CR { get; set; }
    public string Payout { get; set; }

  }

}