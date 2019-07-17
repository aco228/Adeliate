using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;
using Adlite.Data;

 

namespace Adlite.Data 
{
  public partial interface IClickInformationManager : IDataManager<ClickInformation> 
  {
	

  }

  public partial class ClickInformation : AdliteObject<IClickInformationManager> 
  {
		private Click _click;
		private Campaign _campaign;
		private Country _country;
		private MobileOperator _mobileOperator;
		private string _mobileOperatorName = String.Empty;
		private Price _price;
		private string _entranceUrl = String.Empty;
		private string _redirectUrl = String.Empty;
		private string _referrer = String.Empty;
		private string _userAgent = String.Empty;
		private string _iPAddress = String.Empty;
		private bool? _isMobile = false;
		private int? _screenPixelsHeight = -1;
		private int? _screenPixelsWidth = -1;
		private string _hardwareVendor = String.Empty;
		private string _hardwareModel = String.Empty;
		private string _platformVendor = String.Empty;
		private DynamicPlatform _dynamicPlatform;
		private string _platformName = String.Empty;
		private string _platformVersion = String.Empty;
		private string _browserVendor = String.Empty;
		private DynamicBrowser _dynamicBrowser;
		private string _browserName = String.Empty;
		private string _browserVersion = String.Empty;
		private string _countryCode = String.Empty;
		private string _region = String.Empty;
		private string _city = String.Empty;
		private string _iSPName = String.Empty;
		private string _domainName = String.Empty;
		private string _mobileBrand = String.Empty;
		private string _usageType = String.Empty;
		private string _note = String.Empty;
		

		public Click Click 
		{
			get
			{
				if (this._click != null &&
						this._click.IsEmpty)
					if (this.ConnectionContext != null)
						this._click = Click.CreateManager().LazyLoad(this.ConnectionContext, this._click.ID) as Click;
					else
						this._click = Click.CreateManager().LazyLoad(this._click.ID) as Click;
				return this._click;
			}
			set { _click = value; }
		}

		public Campaign Campaign 
		{
			get
			{
				if (this._campaign != null &&
						this._campaign.IsEmpty)
					if (this.ConnectionContext != null)
						this._campaign = Campaign.CreateManager().LazyLoad(this.ConnectionContext, this._campaign.ID) as Campaign;
					else
						this._campaign = Campaign.CreateManager().LazyLoad(this._campaign.ID) as Campaign;
				return this._campaign;
			}
			set { _campaign = value; }
		}

		public Country Country 
		{
			get
			{
				if (this._country != null &&
						this._country.IsEmpty)
					if (this.ConnectionContext != null)
						this._country = Country.CreateManager().LazyLoad(this.ConnectionContext, this._country.ID) as Country;
					else
						this._country = Country.CreateManager().LazyLoad(this._country.ID) as Country;
				return this._country;
			}
			set { _country = value; }
		}

		public MobileOperator MobileOperator 
		{
			get
			{
				if (this._mobileOperator != null &&
						this._mobileOperator.IsEmpty)
					if (this.ConnectionContext != null)
						this._mobileOperator = MobileOperator.CreateManager().LazyLoad(this.ConnectionContext, this._mobileOperator.ID) as MobileOperator;
					else
						this._mobileOperator = MobileOperator.CreateManager().LazyLoad(this._mobileOperator.ID) as MobileOperator;
				return this._mobileOperator;
			}
			set { _mobileOperator = value; }
		}

		public string MobileOperatorName{ get {return this._mobileOperatorName; } set { this._mobileOperatorName = value;} }
		public Price Price 
		{
			get
			{
				if (this._price != null &&
						this._price.IsEmpty)
					if (this.ConnectionContext != null)
						this._price = Price.CreateManager().LazyLoad(this.ConnectionContext, this._price.ID) as Price;
					else
						this._price = Price.CreateManager().LazyLoad(this._price.ID) as Price;
				return this._price;
			}
			set { _price = value; }
		}

		public string EntranceUrl{ get {return this._entranceUrl; } set { this._entranceUrl = value;} }
		public string RedirectUrl{ get {return this._redirectUrl; } set { this._redirectUrl = value;} }
		public string Referrer{ get {return this._referrer; } set { this._referrer = value;} }
		public string UserAgent{ get {return this._userAgent; } set { this._userAgent = value;} }
		public string IPAddress{ get {return this._iPAddress; } set { this._iPAddress = value;} }
		public bool? IsMobile {get {return this._isMobile; } set { this._isMobile = value;} }
		public int? ScreenPixelsHeight{ get {return this._screenPixelsHeight; } set { this._screenPixelsHeight = value;} }
		public int? ScreenPixelsWidth{ get {return this._screenPixelsWidth; } set { this._screenPixelsWidth = value;} }
		public string HardwareVendor{ get {return this._hardwareVendor; } set { this._hardwareVendor = value;} }
		public string HardwareModel{ get {return this._hardwareModel; } set { this._hardwareModel = value;} }
		public string PlatformVendor{ get {return this._platformVendor; } set { this._platformVendor = value;} }
		public DynamicPlatform DynamicPlatform 
		{
			get
			{
				if (this._dynamicPlatform != null &&
						this._dynamicPlatform.IsEmpty)
					if (this.ConnectionContext != null)
						this._dynamicPlatform = DynamicPlatform.CreateManager().LazyLoad(this.ConnectionContext, this._dynamicPlatform.ID) as DynamicPlatform;
					else
						this._dynamicPlatform = DynamicPlatform.CreateManager().LazyLoad(this._dynamicPlatform.ID) as DynamicPlatform;
				return this._dynamicPlatform;
			}
			set { _dynamicPlatform = value; }
		}

		public string PlatformName{ get {return this._platformName; } set { this._platformName = value;} }
		public string PlatformVersion{ get {return this._platformVersion; } set { this._platformVersion = value;} }
		public string BrowserVendor{ get {return this._browserVendor; } set { this._browserVendor = value;} }
		public DynamicBrowser DynamicBrowser 
		{
			get
			{
				if (this._dynamicBrowser != null &&
						this._dynamicBrowser.IsEmpty)
					if (this.ConnectionContext != null)
						this._dynamicBrowser = DynamicBrowser.CreateManager().LazyLoad(this.ConnectionContext, this._dynamicBrowser.ID) as DynamicBrowser;
					else
						this._dynamicBrowser = DynamicBrowser.CreateManager().LazyLoad(this._dynamicBrowser.ID) as DynamicBrowser;
				return this._dynamicBrowser;
			}
			set { _dynamicBrowser = value; }
		}

		public string BrowserName{ get {return this._browserName; } set { this._browserName = value;} }
		public string BrowserVersion{ get {return this._browserVersion; } set { this._browserVersion = value;} }
		public string CountryCode{ get {return this._countryCode; } set { this._countryCode = value;} }
		public string Region{ get {return this._region; } set { this._region = value;} }
		public string City{ get {return this._city; } set { this._city = value;} }
		public string ISPName{ get {return this._iSPName; } set { this._iSPName = value;} }
		public string DomainName{ get {return this._domainName; } set { this._domainName = value;} }
		public string MobileBrand{ get {return this._mobileBrand; } set { this._mobileBrand = value;} }
		public string UsageType{ get {return this._usageType; } set { this._usageType = value;} }
		public string Note{ get {return this._note; } set { this._note = value;} }
		

    public override bool IsInsertable { get { return true;} }

    public override bool IsDeletable { get { return false;} }

    public override bool IsUpdatable { get { return true;} }

    public ClickInformation()
    { 
    }

    public ClickInformation(int id): base(id, DateTime.Now, DateTime.Now, true)
    {
    }

    public ClickInformation(int id,  Click click, Campaign campaign, Country country, MobileOperator mobileOperator, string mobileOperatorName, Price price, string entranceUrl, string redirectUrl, string referrer, string userAgent, string iPAddress, bool? isMobile, int? screenPixelsHeight, int? screenPixelsWidth, string hardwareVendor, string hardwareModel, string platformVendor, DynamicPlatform dynamicPlatform, string platformName, string platformVersion, string browserVendor, DynamicBrowser dynamicBrowser, string browserName, string browserVersion, string countryCode, string region, string city, string iSPName, string domainName, string mobileBrand, string usageType, string note, DateTime updated, DateTime created): base(id, updated, created, false)
    {
			this._click = click;
			this._campaign = campaign;
			this._country = country;
			this._mobileOperator = mobileOperator;
			this._mobileOperatorName = mobileOperatorName;
			this._price = price;
			this._entranceUrl = entranceUrl;
			this._redirectUrl = redirectUrl;
			this._referrer = referrer;
			this._userAgent = userAgent;
			this._iPAddress = iPAddress;
			this._isMobile = isMobile;
			this._screenPixelsHeight = screenPixelsHeight;
			this._screenPixelsWidth = screenPixelsWidth;
			this._hardwareVendor = hardwareVendor;
			this._hardwareModel = hardwareModel;
			this._platformVendor = platformVendor;
			this._dynamicPlatform = dynamicPlatform;
			this._platformName = platformName;
			this._platformVersion = platformVersion;
			this._browserVendor = browserVendor;
			this._dynamicBrowser = dynamicBrowser;
			this._browserName = browserName;
			this._browserVersion = browserVersion;
			this._countryCode = countryCode;
			this._region = region;
			this._city = city;
			this._iSPName = iSPName;
			this._domainName = domainName;
			this._mobileBrand = mobileBrand;
			this._usageType = usageType;
			this._note = note;
			
    }

    public override object Clone(bool deepClone)
    {
      return new ClickInformation(-1,  this.Click, this.Campaign, this.Country, this.MobileOperator,this.MobileOperatorName, this.Price,this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor, this.DynamicPlatform,this.PlatformName,this.PlatformVersion,this.BrowserVendor, this.DynamicBrowser,this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note, DateTime.Now, DateTime.Now);
    }
  }
}

