using Adeliate.Click.Code;
using Adeliate.Core.Data;
using Adeliate.Web.Click.Code.Cache;
using Adlite.Data;
using Adlite.Direct.Data;
using Adlite.Direct.Data.Lite;
using FiftyOne.Foundation.Mobile.Detection;
using log4net;
using MaxMind.GeoIP2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Adeliate.Web.Click.Code
{

  public class AdliteContext
  {

    #region #logging#
    private static ILog _log = null;

    protected static ILog Log
    {
      get
      {
        if (AdliteContext._log == null)
          AdliteContext._log = LogManager.GetLogger(typeof(AdliteContext));
        return AdliteContext._log;
      }
    }
    #endregion

    protected static readonly string AdliteHttpContextItemKey = "AdliteHttpContextItemKey";
    protected static readonly string AdliteLocalizationItemKey = "AdliteLocalizationItemKey";

    public static AdliteContext Current
    {
      get
      {
        HttpContext httpContext = HttpContext.Current;
        return AdliteContext.GetCurrent(httpContext);
      }
    }

    public static AdliteContext GetExisting()
    {
      HttpContext httpContext = HttpContext.Current;
      return httpContext.Items[AdliteHttpContextItemKey] as AdliteContext;
    }

    public static AdliteContext GetCurrent(HttpContext httpContext)
    {
      lock (httpContext.Request)
      {
        AdliteContext adliteContext = httpContext.Items[AdliteHttpContextItemKey] as AdliteContext;
        if (adliteContext != null)
          return adliteContext as AdliteContext;

        adliteContext = new AdliteContext(httpContext);
        httpContext.Items[AdliteHttpContextItemKey] = adliteContext;
        return adliteContext;
      }
    }

    public AdliteContext(HttpContext context)
    {
      this._httpContext = context;
      this.InitializeSession(context);
    }

    public string REDIRECT_URL = "";
    
    private bool _hasError = false;
    private string _errorMessage = string.Empty;
    private bool _asyncCollectMobileData = true;
    private bool _asyncCollectCountryData = true;
    private string _rawUrl = string.Empty;
    private bool _useFallback = false;
    private string _parameters = string.Empty;
    private HttpContext _httpContext = null;
    private AdliteDirect _database = null;
    private OfferLite _offer = null;
    private AdliteAffiliate _affiliate = null;
    private AdliteClick _click = null;
    private ClickInformation _clickInformation = null;

    public int ClickID { get { return this._click.ClickData.ID; } }
    public bool HasError { get { return this._hasError; } }
    public string ErrorMessage { get { return this._errorMessage; } set { this._errorMessage = value; } }
    public string RawUrl { get { return this._rawUrl; } }
    public bool AsyncCollectMobileData { get { return this._asyncCollectMobileData; } }
    public bool AsyncCollectCountryData { get { return this._asyncCollectCountryData; } }
    public HttpContext HttpContext { get { return this._httpContext; } }
    public AdliteDirect Database { get { return this._database; } }
    public OfferLite Offer { get { return this._offer; } }
    public AdliteAffiliate Affiliate { get { return this._affiliate; } }
    public AdliteClick Click { get { return this._click; } }
    public ClickInformation ClickInformation { get { return this._clickInformation; } set { this._clickInformation = value; } }

    protected virtual void InitializeSession(HttpContext httpContext)
    {
      this._database = AdliteDirect.Instance;
      this._rawUrl = httpContext.Request.RawUrl;
      this._httpContext = httpContext;

      this.PrepareClickData(httpContext);

      this.GetOffer();

      if(this._hasError)
        return;

      this._click.ClickData.Offer = this._offer.OfferData;
      this._affiliate = this._offer.AffiliateData.Instantiate();
      if (this._affiliate == null)
      {
        this._hasError = true;
        this._click.ClickData.Insert();
        this._errorMessage = "Affiliate IOC error";
        Log.Debug("Context: Affiliate IOC error");
        return;
      }
      this._click.ClickData.Affiliate = this._affiliate.AffiliateData;

      string subID = this._httpContext.Request[this._affiliate.AffiliateData.SubidName] != null && !string.IsNullOrEmpty(this._affiliate.AffiliateData.SubidName) ?
        this._httpContext.Request[this._affiliate.AffiliateData.SubidName].ToString() :
        string.Empty;
      this._click.ClickData.SubID = subID;

      this._clickInformation.Campaign = this._click.ClickData.Offer.Campaign;
      this._clickInformation.Price = (this._offer.OfferData.Price != null ? this._offer.OfferData.Price : this._offer.OfferData.Campaign.Price);

      this.GetAllIPInformations();
      this.GetAllMobileInformations();
      this.CheckFallback();

      // Check ip range
      if (!IPHelper.CheckIfIPIsInTheRange(this._offer.OfferData, this._clickInformation.IPAddress))
        this._useFallback = true;

      // Mobile phone restrictions
      if(this._offer.OfferData.Campaign.RestrictMobileTraffic &&
        this._clickInformation.IsMobile.HasValue && this._clickInformation.IsMobile.Value)
          this._useFallback = true;

      // Country restrictions
      if (this._offer.OfferData.Campaign.RestrictCountryTraffic &&
        this._clickInformation.Country != null && this._clickInformation.Country.ID != this._offer.OfferData.Campaign.Country.ID)
        this._useFallback = true;
    }

    // SUMMARY: Prepare click data for this click
    private void PrepareClickData(HttpContext httpContext)
    {
      this._click = new AdliteClick(
        new Adlite.Data.Click(-1, string.Empty, null, null, false, DateTime.Now, DateTime.Now),
        httpContext.Request.RawUrl,
        httpContext.Request.UrlReferrer != null ? httpContext.Request.UrlReferrer.ToString() : string.Empty,
        httpContext.Request.UserAgent,
        httpContext.Request.UserHostAddress);

      this._clickInformation = new ClickInformation(-1);
      this._clickInformation.Click = this._click.ClickData;
      this._clickInformation.EntranceUrl = httpContext.Request.RawUrl;
      this._clickInformation.Referrer = httpContext.Request.UrlReferrer != null ? httpContext.Request.UrlReferrer.ToString() : string.Empty;
      this._clickInformation.UserAgent = httpContext.Request.UserAgent;
      this._clickInformation.IPAddress = httpContext.Request.UserHostAddress;
    }

    // SUMMARY: Get offer by entrance Url
    private void GetOffer()
    {
      string[] split = this._rawUrl.Split('/');
      if (split.Length < 2)
      {
        //this._click.ClickData.Insert();
        this._hasError = true;
        this._errorMessage = "Split error";
        Log.Debug("Context: Split error");
        return;
      }

      string[] offerSplit = split[1].Split('?');
      if (offerSplit.Length > 1)
        this._parameters = offerSplit[offerSplit.Length - 1];
      string offerKey = offerSplit[0];

      OfferCache cache = AdliteClickApplication.CacheManager.Get(offerKey);
      if (cache != null)
      {
        this._offer = new OfferLite(cache.Offer);
        return;
      }

      this._offer = OfferLite.LoadOfferByKey(this._database, offerSplit[0]);
      if (this._offer == null)
      {
        //this._click.ClickData.Insert();
        this._errorMessage = "Offer could not be loaded by key " + offerSplit[0];
        Log.Debug("Context: Offer could not be loaded by key " + offerSplit[0]);
        this._hasError = true;
        return;
      }

      AdliteClickApplication.CacheManager.Add(this._offer.OfferData);
    }

    // SUMMARY: Get all mobile informations about this click
    private void GetAllMobileInformations()
    {
      if (WebProvider.ActiveProvider != null)
      {
        var match = WebProvider.ActiveProvider.Match(this._httpContext.Request.Headers);
        Device device = new Device(match);
        this._clickInformation.IsMobile = device.IsMobile;
        this._clickInformation.ScreenPixelsHeight = device.ScreenPixelsHeight;
        this._clickInformation.ScreenPixelsWidth = device.ScreenPixelsWidth;
        this._clickInformation.HardwareVendor = device.HardwareVendor;
        this._clickInformation.HardwareModel = device.HardwareModel;

        DynamicPlatform dynamicPlatform = DynamicPlatform.CreateManager().Load(device.PlatformName);
        if (dynamicPlatform == null)
        {
          dynamicPlatform = new DynamicPlatform(-1, device.PlatformName, DateTime.Now, DateTime.Now);
          dynamicPlatform.Insert();
        }

        this._clickInformation.DynamicPlatform = dynamicPlatform;
        this._clickInformation.PlatformVendor = device.PlatformVendor;
        this._clickInformation.PlatformName = device.PlatformName;
        this._clickInformation.PlatformVersion = device.PlatformVersion;

        DynamicBrowser dynamicBrowser = DynamicBrowser.CreateManager().Load(device.BrowserName);
        if (dynamicBrowser == null)
        {
          dynamicBrowser = new DynamicBrowser(-1, device.BrowserName, DateTime.Now, DateTime.Now);
          dynamicBrowser.Insert();
        }

        this._clickInformation.DynamicBrowser = dynamicBrowser;
        this._clickInformation.BrowserName = device.BrowserName;
        this._clickInformation.BrowserVendor = device.BrowserVendor;
        this._clickInformation.BrowserVersion = device.BrowserVersion;
      }
    }

    // SUMMARY: Check if this click should go to fallback
    private void CheckFallback()
    {
      if (!this._offer.OfferData.IsActive)
      {
        this._useFallback = true;
        return;
      }

      OfferCapMap capMap = OfferCapMap.CreateManager().Load(this._offer.OfferData);
      if (capMap == null)
        return;

      if (capMap.Locked)
      {
        this._useFallback = true;
        return;
      }

      if (capMap.Transactions >= this._offer.OfferData.CapValue)
      {
        this._useFallback = true;
        capMap.Locked = true;
        capMap.Update();
        AdliteHttpRequest.Request(string.Format("http://portal.adlite.local/callback/capreached?offer_id={0}", this._offer.ID));
        return;
      }
    }

    // SUMMARY: Prepare redirection url for this click
    public void PrepareRedirectUrl()
    {
      if (!this._useFallback)
        this.Click.RedirectUrl = this._offer.OfferData.Campaign.Link
          + (this._offer.OfferData.Campaign.Link.Contains('?') ? "&" : "?")
          + "click_id=" + this._click.ID
          + (!string.IsNullOrEmpty(this._parameters) ? "&" + this._parameters : string.Empty);
      else
        this.Click.RedirectUrl = this._offer.OfferData.Campaign.FallbackLink
          + (this._offer.OfferData.Campaign.FallbackLink.Contains('?') ? "&" : "?")
          + "click_id=" + this._click.ID
          + (!string.IsNullOrEmpty(this._parameters) ? "&" + this._parameters : string.Empty);
    }

    // SUMMARY: Get IP Informations
    private void GetAllIPInformations()
    {
      if (this._clickInformation.IPAddress == "::1")
        this._clickInformation.IPAddress = "62.4.59.218";

      CountryIPMapLite country = IPHelper.LoadCountryByIP(this._clickInformation.IPAddress);
      if (country == null)
        return;

      this._clickInformation.CountryCode = country.CountryCode;
      this._clickInformation.Region = country.Region;
      this._clickInformation.City = country.City;
      this._clickInformation.ISPName = country.ISP_Name;
      this._clickInformation.DomainName = country.DomainName;
      this._clickInformation.MobileBrand = country.MobileBrand;
      this._clickInformation.UsageType = country.UsageType;

      if (country.CountryID != -1)
        this._clickInformation.Country = Country.CreateManager().Load(country.CountryID);

      if (country.MobileOperatorID != -1)
      {
        MobileOperator mno = MobileOperator.CreateManager().Load(country.MobileOperatorID);
        if (mno != null)
        {
          this._clickInformation.MobileOperator = mno;
          this._clickInformation.MobileOperatorName = mno.Name;
        }
      }
    }

    public static void Insert(AdliteContext context)
    {
      if (context.ClickInformation.Click == null)
        return;

      if (context.Click.ClickData.ID == -1)
        context.Click.ClickData.Insert();

      context.ClickInformation.RedirectUrl = context.Click.RedirectUrl;
      context.ClickInformation.Note = context.ErrorMessage;
      context.ClickInformation.Insert();
      context.PrepareRedirectUrl();

      if (context.HasError)
        return;

      AdliteHttpResponse response = AdliteHttpRequest.Request(string.Format("http://portal.adeliate.me/callback/onclick?click_id={0}", context.Click.ID));
      if (!response.Successfull)
        Log.Error("Signal callback portal error. Response = " + response.Response + ", " + response.StatusCode.ToString());
    }
  }
}