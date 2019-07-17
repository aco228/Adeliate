using log4net.Layout.Pattern;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Adeliate.Web.Click.Code
{
  public class ClickIDLogConverter : PatternLayoutConverter
  {

    protected override void Convert(TextWriter writer, log4net.Core.LoggingEvent loggingEvent)
    {
      try
      {
        writer.Write(AdliteContext.Current.ClickID.ToString());
      }
      catch (Exception e)
      {
        writer.Write("0");
      }
    }

  }
}