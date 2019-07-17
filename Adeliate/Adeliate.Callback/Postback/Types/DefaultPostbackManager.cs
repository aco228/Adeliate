using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Adlite.Data;

namespace Adeliate.Callback.Postback.Types
{
  public class DefaultPostbackManager : AdlitePostbackBase
  {
    public DefaultPostbackManager(Adlite.Data.Postback postback) : base(postback)
    {
    }

  }
}