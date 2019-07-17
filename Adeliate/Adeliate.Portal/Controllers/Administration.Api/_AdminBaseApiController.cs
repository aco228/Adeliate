using Adeliate.Portal.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Controllers.Administration.Api
{
  [LoginFilter(Order = 1)]
  [AdministrationFilter(Order = 2)]
  public class AdminBaseApiController : AdliteApiController
  {
  }
}