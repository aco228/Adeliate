using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adeliate
{
  public class AdliteClick
  {
    private Adlite.Data.Click _click = null;
    private string _entranceUrl = string.Empty;
    private string _referrer = string.Empty;
    private string _userAgent = string.Empty;
    private string _ipAddress = string.Empty;
    private string _redirectUrl = string.Empty;

    public int ID { get { return this._click.ID; } }
    public Adlite.Data.Click ClickData { get { return this._click; } }
    public string EntranceUrl { get { return this._entranceUrl; } }
    public string Referrer { get { return this._referrer; } }
    public string UserAgent { get { return this._userAgent; } }
    public string IPAddress { get { return this._ipAddress; } }
    public string RedirectUrl { get { return this._redirectUrl; } set { this._redirectUrl = value; } }

    public AdliteClick(Adlite.Data.Click click)
    {
      this._click = click;
    }

    public AdliteClick(Adlite.Data.Click click, string entranceUrl, string referrer, string userAgent, string ipAddress)
    {
      this._click = click;
      this._entranceUrl = entranceUrl;
      this._referrer = referrer;
      this._userAgent = userAgent;
      this._ipAddress = ipAddress;
    }

  }
}
