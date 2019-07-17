using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adeliate.Core.Implementation.Affiliate.Outputs
{
  public class BeforeClickOutput
  {
    private bool _shouldRedirect = true;
    private string _redirectUrl = string.Empty;

    public bool ShouldRedirect { get { return this._shouldRedirect; } }
    public bool DefaultRedirect { get { return string.IsNullOrEmpty(this._redirectUrl); } }
    public string RedirectUrl { get { return this._redirectUrl; } }

  }
}
