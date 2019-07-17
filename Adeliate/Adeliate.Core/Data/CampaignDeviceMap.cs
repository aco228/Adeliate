using Senti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 

namespace Adlite.Data 
{
  public partial interface ICampaignDeviceMapManager 
  {

    List<CampaignDeviceMap> Load(Campaign campaign);
    List<CampaignDeviceMap> Load(IConnectionInfo connection, Campaign campaign);


    CampaignDeviceMap Load(CampaignDevice device, Campaign campaign);
    CampaignDeviceMap Load(IConnectionInfo connection, CampaignDevice device, Campaign campaign);


  }

  public partial class CampaignDeviceMap
  {
  }
}

