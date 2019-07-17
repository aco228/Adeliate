using Adeliate.Core;
using Adeliate.Core.Implementation.Affiliate.Outputs;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adeliate
{
  public abstract class AdliteAffiliate
  {
    private Affiliate _affiliate = null;

    public Affiliate AffiliateData { get { return this._affiliate; } }

    public AdliteAffiliate(Affiliate affiliate)
    {
      this._affiliate = affiliate;
    }
    
    public virtual BeforeClickOutput BeforeClick(AdliteClick click) { return new BeforeClickOutput(); }
    public virtual OnClickOutput OnClick(AdliteClick click) { return new OnClickOutput(); }
    
  }
}
