using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Login
{
  public class LoginModel : AdliteModelBase
  {
    public string RedirectUrl { get; set; }
    public string PredifinedUsername { get; set; }
  }
}