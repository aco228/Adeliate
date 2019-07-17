using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;
using Adlite.Data;

 

namespace Adlite.Data 
{
  public partial interface ICampaignManager : IDataManager<Campaign> 
  {
	

  }

  public partial class Campaign : AdliteObject<ICampaignManager> 
  {
		private CampaignGroup _campaignGroup;
		private CampaignContentType _campaignContentType;
		private Country _country;
		private Price _price;
		private string _name = String.Empty;
		private int _capValue = -1;
		private string _link = String.Empty;
		private string _fallbackLink = String.Empty;
		private string _description = String.Empty;
		private string _device = String.Empty;
		private string _browser = String.Empty;
		private string _iPRanges = String.Empty;
		private bool _restrictCountryTraffic = false;
		private bool _restrictMobileTraffic = false;
		

		public CampaignGroup CampaignGroup 
		{
			get
			{
				if (this._campaignGroup != null &&
						this._campaignGroup.IsEmpty)
					if (this.ConnectionContext != null)
						this._campaignGroup = CampaignGroup.CreateManager().LazyLoad(this.ConnectionContext, this._campaignGroup.ID) as CampaignGroup;
					else
						this._campaignGroup = CampaignGroup.CreateManager().LazyLoad(this._campaignGroup.ID) as CampaignGroup;
				return this._campaignGroup;
			}
			set { _campaignGroup = value; }
		}

		public CampaignContentType CampaignContentType  { get { return this._campaignContentType; } set { this._campaignContentType = value; } }
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

		public string Name{ get {return this._name; } set { this._name = value;} }
		public int CapValue{ get {return this._capValue; } set { this._capValue = value;} }
		public string Link{ get {return this._link; } set { this._link = value;} }
		public string FallbackLink{ get {return this._fallbackLink; } set { this._fallbackLink = value;} }
		public string Description{ get {return this._description; } set { this._description = value;} }
		public string Device{ get {return this._device; } set { this._device = value;} }
		public string Browser{ get {return this._browser; } set { this._browser = value;} }
		public string IPRanges{ get {return this._iPRanges; } set { this._iPRanges = value;} }
		public bool RestrictCountryTraffic {get {return this._restrictCountryTraffic; } set { this._restrictCountryTraffic = value;} }
		public bool RestrictMobileTraffic {get {return this._restrictMobileTraffic; } set { this._restrictMobileTraffic = value;} }
		

    public override bool IsInsertable { get { return true;} }

    public override bool IsDeletable { get { return false;} }

    public override bool IsUpdatable { get { return true;} }

    public Campaign()
    { 
    }

    public Campaign(int id): base(id, DateTime.Now, DateTime.Now, true)
    {
    }

    public Campaign(int id,  CampaignGroup campaignGroup, CampaignContentType campaignContentType, Country country, Price price, string name, int capValue, string link, string fallbackLink, string description, string device, string browser, string iPRanges, bool restrictCountryTraffic, bool restrictMobileTraffic, DateTime updated, DateTime created): base(id, updated, created, false)
    {
			this._campaignGroup = campaignGroup;
			this._campaignContentType = campaignContentType;
			this._country = country;
			this._price = price;
			this._name = name;
			this._capValue = capValue;
			this._link = link;
			this._fallbackLink = fallbackLink;
			this._description = description;
			this._device = device;
			this._browser = browser;
			this._iPRanges = iPRanges;
			this._restrictCountryTraffic = restrictCountryTraffic;
			this._restrictMobileTraffic = restrictMobileTraffic;
			
    }

    public override object Clone(bool deepClone)
    {
      return new Campaign(-1,  this.CampaignGroup, this.CampaignContentType, this.Country, this.Price,this.Name,this.CapValue,this.Link,this.FallbackLink,this.Description,this.Device,this.Browser,this.IPRanges,this.RestrictCountryTraffic,this.RestrictMobileTraffic, DateTime.Now, DateTime.Now);
    }
  }
}

