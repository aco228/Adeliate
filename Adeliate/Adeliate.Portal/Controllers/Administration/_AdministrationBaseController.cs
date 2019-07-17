using Adeliate.Portal.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adeliate.Portal.Controllers.Administration
{
  [LoginFilter(Order = 1)]
  [AdministrationFilter(Order = 2)]
  public class AdministrationBaseController : AdliteController
  {
  }
}