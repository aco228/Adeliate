using Adeliate.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Statistics
{
  public class StatisticsInputModel : InputModelBase
  {
    public string AffiliateID { get; set; }
    public string CampaignID { get; set; }
    public string GroupBy { get; set; }
    public string Countries { get; set; }
    public string MobileOperators { get; set; }
    public string Browsers { get; set; }
    public string Platforms { get; set; }
    public string DateFrom { get; set; }
    public string DateTo { get; set; }



    public StatisticsInputModel() : base(true)
    {
    }

    public override void Validation()
    {
      
    }
  }
}