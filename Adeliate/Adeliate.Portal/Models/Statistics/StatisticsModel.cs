using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Statistics
{
  public class StatisticsModel : AdliteModelBase
  {
    public int CampaignID { get; set; }
    public int AffiliateID { get; set; }
    public StatisticsFilterModel FilterModel { get; set; }
    public StatisticsLoadModel LoadModel { get; set; }

    public StatisticsModel()
    {
      FilterModel = new StatisticsFilterModel();
      //LoadModel = new StatisticsLoadModel();
    }
  }


}