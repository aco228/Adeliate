using FiftyOne.Foundation.Mobile.Detection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Click.Code
{
  public class Device
  {

    private const string SWITCH_HTML = "<a href=\"" +
        "https://51degrees.com/compare-data-options\">" +
        "Switch Data Set</a>";
    
    private readonly Match _match;
    
    public bool IsMobile
    {
      get
      {
        return _match["IsMobile"] != null ?
            _match["IsMobile"].ToBool() :
            false;
      }
    }
    public int ScreenPixelsHeight
    {
      get
      {
        return _match["ScreenPixelsHeight"] != null ?
            (int)_match["ScreenPixelsHeight"].ToDouble() :
            0;
      }
    }
    public int ScreenPixelsWidth
    {
      get
      {
        return _match["ScreenPixelsWidth"] != null ?
            (int)_match["ScreenPixelsWidth"].ToDouble() :
            0;
      }
    }
    
    public string HardwareVendor { get; private set; }
    public string HardwareModel { get; private set; }
    public string PlatformVendor { get; private set; }
    public string PlatformName { get; private set; }
    public string PlatformVersion { get; private set; }
    public string BrowserVendor { get; private set; }
    public string BrowserName { get; private set; }
    public string BrowserVersion { get; private set; }
    
    internal Device(Match match)
    {
      _match = match;
      foreach (var classProperty in this.GetType().GetProperties().Where(p =>
          p.PropertyType == typeof(string)))
      {
        var values = match[classProperty.Name];
        if (values != null && values.Count > 0)
        {
          classProperty.SetValue(this, values.ToString());
        }
        else
        {
          classProperty.SetValue(this, SWITCH_HTML);
        }
      }
    }
  }
}