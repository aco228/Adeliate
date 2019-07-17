using Senti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 

namespace Adlite.Data 
{
  public partial interface ICampaignGroupManager 
  {

    List<CampaignGroup> Load();
    List<CampaignGroup> Load(IConnectionInfo connection );

   
  }

  public partial class CampaignGroup
  {
  }
}

