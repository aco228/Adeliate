using Senti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 

namespace Adlite.Data 
{
  public partial interface ICampaignManager 
  {

    List<Campaign> Load(CampaignGroup group);
    List<Campaign> Load(IConnectionInfo connection, CampaignGroup group);

  }

  public partial class Campaign
  {
  }
}

