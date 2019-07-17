using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adeliate.Web.Hubs
{
  public abstract class AdliteHubUserIdProvider : IUserIdProvider
  {
    public abstract string GetUserId(IRequest request);
  }
}
