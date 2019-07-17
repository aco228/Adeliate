using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adeliate
{
  public class AdliteGroup
  {
    private CampaignGroup _group = null;

    public CampaignGroup GroupData { get { return this._group; } }

    public AdliteGroup(CampaignGroup group)
    {
      this._group = group;
    }
  }
}
