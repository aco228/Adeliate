using Adlite.Data;
using Senti.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Callback.Code
{
  public class PostbackConverter
  {

    public static AdlitePostback Convert(Adlite.Data.Postback postback)
    {
      return TypeFactory.Instantiate<AdlitePostback, Adlite.Data.Postback>(postback.PostbackType.TypeName, postback);
    }

  }
}