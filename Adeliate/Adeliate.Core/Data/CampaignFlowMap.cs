using Senti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 

namespace Adlite.Data 
{
  public partial interface ICampaignFlowMapManager 
  {

    CampaignFlowMap Load(CampaignFlow campaignFlow, Campaign campaign);
    CampaignFlowMap Load(IConnectionInfo connection, CampaignFlow campaignFlow, Campaign campaign);


    List<CampaignFlowMap> Load(Campaign campaign);
    List<CampaignFlowMap> Load(IConnectionInfo connection, Campaign campaign);


  }

  public partial class CampaignFlowMap
  {
  }
}

