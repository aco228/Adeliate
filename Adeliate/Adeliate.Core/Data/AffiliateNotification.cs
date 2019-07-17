using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 

namespace Adlite.Data 
{
  public partial interface IAffiliateNotificationManager 
  {
  }

  public partial class AffiliateNotification
  {

    public int CampaignIDValue { get { return this._campaign == null ? -1 : this._campaign.ID; } }
  }
}

