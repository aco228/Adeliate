using Senti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 

namespace Adlite.Data 
{
  public partial interface ICampaignMobileOperatorMapManager 
  {

    List<CampaignMobileOperatorMap> Load(Campaign campaign);
    List<CampaignMobileOperatorMap> Load(IConnectionInfo connection, Campaign campaign);


    CampaignMobileOperatorMap Load(MobileOperator mobileOperator, Campaign campaign);
    CampaignMobileOperatorMap Load(IConnectionInfo connection, MobileOperator mobileOperator, Campaign campaign);


  }

  public partial class CampaignMobileOperatorMap
  {
  }
}

