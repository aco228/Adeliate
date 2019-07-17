using Senti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 

namespace Adlite.Data 
{
  public partial interface ICampaignImageMapManager 
  {

    List<CampaignImageMap> Load(Campaign campaign);
    List<CampaignImageMap> Load(IConnectionInfo connection, Campaign campaign);

    

  }

  public partial class CampaignImageMap
  {
  }
}

