using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti;
using Senti.Data;
using Senti.Data.Sql;

using Adlite.Data;


namespace Adlite.Data.Sql
{
  public class ClickInformationTable : TableBase<ClickInformation>
  {
    public static string GetColumnNames()
    {
      return TableBase<ClickInformation>.GetColumnNames(string.Empty, ClickInformationTable.Columns.Item);
    }

    public static string GetColumnNames(string tablePrefix)
    {
      return TableBase<ClickInformation>.GetColumnNames(tablePrefix, ClickInformationTable.Columns.Item);
    }

    public class Columns
    {
		
			public static readonly ColumnDescription ClickInformationID = new ColumnDescription("ClickInformationID", 0, typeof(int));
			public static readonly ColumnDescription ClickID = new ColumnDescription("ClickID", 1, typeof(int));
			public static readonly ColumnDescription CampaignID = new ColumnDescription("CampaignID", 2, typeof(int));
			public static readonly ColumnDescription CountryID = new ColumnDescription("CountryID", 3, typeof(int));
			public static readonly ColumnDescription MobileOperatorID = new ColumnDescription("MobileOperatorID", 4, typeof(int));
			public static readonly ColumnDescription MobileOperatorName = new ColumnDescription("MobileOperatorName", 5, typeof(string));
			public static readonly ColumnDescription PriceID = new ColumnDescription("PriceID", 6, typeof(int));
			public static readonly ColumnDescription EntranceUrl = new ColumnDescription("EntranceUrl", 7, typeof(string));
			public static readonly ColumnDescription RedirectUrl = new ColumnDescription("RedirectUrl", 8, typeof(string));
			public static readonly ColumnDescription Referrer = new ColumnDescription("Referrer", 9, typeof(string));
			public static readonly ColumnDescription UserAgent = new ColumnDescription("UserAgent", 10, typeof(string));
			public static readonly ColumnDescription IPAddress = new ColumnDescription("IPAddress", 11, typeof(string));
			public static readonly ColumnDescription IsMobile = new ColumnDescription("IsMobile", 12, typeof(bool));
			public static readonly ColumnDescription ScreenPixelsHeight = new ColumnDescription("ScreenPixelsHeight", 13, typeof(int));
			public static readonly ColumnDescription ScreenPixelsWidth = new ColumnDescription("ScreenPixelsWidth", 14, typeof(int));
			public static readonly ColumnDescription HardwareVendor = new ColumnDescription("HardwareVendor", 15, typeof(string));
			public static readonly ColumnDescription HardwareModel = new ColumnDescription("HardwareModel", 16, typeof(string));
			public static readonly ColumnDescription PlatformVendor = new ColumnDescription("PlatformVendor", 17, typeof(string));
			public static readonly ColumnDescription DynamicPlatformID = new ColumnDescription("DynamicPlatformID", 18, typeof(int));
			public static readonly ColumnDescription PlatformName = new ColumnDescription("PlatformName", 19, typeof(string));
			public static readonly ColumnDescription PlatformVersion = new ColumnDescription("PlatformVersion", 20, typeof(string));
			public static readonly ColumnDescription BrowserVendor = new ColumnDescription("BrowserVendor", 21, typeof(string));
			public static readonly ColumnDescription DynamicBrowserID = new ColumnDescription("DynamicBrowserID", 22, typeof(int));
			public static readonly ColumnDescription BrowserName = new ColumnDescription("BrowserName", 23, typeof(string));
			public static readonly ColumnDescription BrowserVersion = new ColumnDescription("BrowserVersion", 24, typeof(string));
			public static readonly ColumnDescription CountryCode = new ColumnDescription("CountryCode", 25, typeof(string));
			public static readonly ColumnDescription Region = new ColumnDescription("Region", 26, typeof(string));
			public static readonly ColumnDescription City = new ColumnDescription("City", 27, typeof(string));
			public static readonly ColumnDescription ISPName = new ColumnDescription("ISPName", 28, typeof(string));
			public static readonly ColumnDescription DomainName = new ColumnDescription("DomainName", 29, typeof(string));
			public static readonly ColumnDescription MobileBrand = new ColumnDescription("MobileBrand", 30, typeof(string));
			public static readonly ColumnDescription UsageType = new ColumnDescription("UsageType", 31, typeof(string));
			public static readonly ColumnDescription Note = new ColumnDescription("Note", 32, typeof(string));
			public static readonly ColumnDescription Updated = new ColumnDescription("Updated", 33, typeof(DateTime));
			public static readonly ColumnDescription Created = new ColumnDescription("Created", 34, typeof(DateTime));
			

			public static readonly ColumnDescription[] Item = new ColumnDescription[] 
			{
				ClickInformationID,
				ClickID,
				CampaignID,
				CountryID,
				MobileOperatorID,
				MobileOperatorName,
				PriceID,
				EntranceUrl,
				RedirectUrl,
				Referrer,
				UserAgent,
				IPAddress,
				IsMobile,
				ScreenPixelsHeight,
				ScreenPixelsWidth,
				HardwareVendor,
				HardwareModel,
				PlatformVendor,
				DynamicPlatformID,
				PlatformName,
				PlatformVersion,
				BrowserVendor,
				DynamicBrowserID,
				BrowserName,
				BrowserVersion,
				CountryCode,
				Region,
				City,
				ISPName,
				DomainName,
				MobileBrand,
				UsageType,
				Note,
				Updated,
				Created
			};
    }

    public override int ColumnCount
    {
      get { return Columns.Item.Length; }
    }

    public ClickInformationTable(SqlQuery query) : base(query) { }
    public ClickInformationTable(SqlQuery query, ColumnSelectMode selectMode,
                           params ColumnDescription[] columns)
      : base(query, selectMode, columns) { }

    public ColumnDescription this[int index] { get { return Columns.Item[index]; } }

		public int ClickInformationID { get { return this.Reader.GetInt32(this.GetIndex(Columns.ClickInformationID)); } }
		public int ClickID { get { return this.Reader.GetInt32(this.GetIndex(Columns.ClickID)); } }
		
		public int? CampaignID 
		{
			get
			{
				int index = this.GetIndex(Columns.CampaignID);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetInt32(index);
			}
		}

		
		public int? CountryID 
		{
			get
			{
				int index = this.GetIndex(Columns.CountryID);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetInt32(index);
			}
		}

		
		public int? MobileOperatorID 
		{
			get
			{
				int index = this.GetIndex(Columns.MobileOperatorID);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetInt32(index);
			}
		}

		
		public string MobileOperatorName 
		{
			get
			{
				int index = this.GetIndex(Columns.MobileOperatorName);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetString(index);
			}
		}

		
		public int? PriceID 
		{
			get
			{
				int index = this.GetIndex(Columns.PriceID);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetInt32(index);
			}
		}

		public string EntranceUrl { get { return this.Reader.GetString(this.GetIndex(Columns.EntranceUrl)); } }
		public string RedirectUrl { get { return this.Reader.GetString(this.GetIndex(Columns.RedirectUrl)); } }
		public string Referrer { get { return this.Reader.GetString(this.GetIndex(Columns.Referrer)); } }
		public string UserAgent { get { return this.Reader.GetString(this.GetIndex(Columns.UserAgent)); } }
		public string IPAddress { get { return this.Reader.GetString(this.GetIndex(Columns.IPAddress)); } }
		
		public bool? IsMobile 
		{
			get
			{
				int index = this.GetIndex(Columns.IsMobile);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetBoolean(index);
			}
		}

		
		public int? ScreenPixelsHeight 
		{
			get
			{
				int index = this.GetIndex(Columns.ScreenPixelsHeight);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetInt32(index);
			}
		}

		
		public int? ScreenPixelsWidth 
		{
			get
			{
				int index = this.GetIndex(Columns.ScreenPixelsWidth);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetInt32(index);
			}
		}

		
		public string HardwareVendor 
		{
			get
			{
				int index = this.GetIndex(Columns.HardwareVendor);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetString(index);
			}
		}

		
		public string HardwareModel 
		{
			get
			{
				int index = this.GetIndex(Columns.HardwareModel);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetString(index);
			}
		}

		
		public string PlatformVendor 
		{
			get
			{
				int index = this.GetIndex(Columns.PlatformVendor);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetString(index);
			}
		}

		
		public int? DynamicPlatformID 
		{
			get
			{
				int index = this.GetIndex(Columns.DynamicPlatformID);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetInt32(index);
			}
		}

		
		public string PlatformName 
		{
			get
			{
				int index = this.GetIndex(Columns.PlatformName);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetString(index);
			}
		}

		
		public string PlatformVersion 
		{
			get
			{
				int index = this.GetIndex(Columns.PlatformVersion);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetString(index);
			}
		}

		
		public string BrowserVendor 
		{
			get
			{
				int index = this.GetIndex(Columns.BrowserVendor);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetString(index);
			}
		}

		
		public int? DynamicBrowserID 
		{
			get
			{
				int index = this.GetIndex(Columns.DynamicBrowserID);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetInt32(index);
			}
		}

		
		public string BrowserName 
		{
			get
			{
				int index = this.GetIndex(Columns.BrowserName);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetString(index);
			}
		}

		
		public string BrowserVersion 
		{
			get
			{
				int index = this.GetIndex(Columns.BrowserVersion);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetString(index);
			}
		}

		
		public string CountryCode 
		{
			get
			{
				int index = this.GetIndex(Columns.CountryCode);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetString(index);
			}
		}

		
		public string Region 
		{
			get
			{
				int index = this.GetIndex(Columns.Region);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetString(index);
			}
		}

		
		public string City 
		{
			get
			{
				int index = this.GetIndex(Columns.City);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetString(index);
			}
		}

		
		public string ISPName 
		{
			get
			{
				int index = this.GetIndex(Columns.ISPName);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetString(index);
			}
		}

		
		public string DomainName 
		{
			get
			{
				int index = this.GetIndex(Columns.DomainName);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetString(index);
			}
		}

		
		public string MobileBrand 
		{
			get
			{
				int index = this.GetIndex(Columns.MobileBrand);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetString(index);
			}
		}

		
		public string UsageType 
		{
			get
			{
				int index = this.GetIndex(Columns.UsageType);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetString(index);
			}
		}

		
		public string Note 
		{
			get
			{
				int index = this.GetIndex(Columns.Note);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetString(index);
			}
		}

		public DateTime Updated { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Updated)); } }
		public DateTime Created { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Created)); } }
		

	  public ClickInformation CreateInstance()  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Campaign campaign)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Country country)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(DynamicPlatform dynamicPlatform)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(MobileOperator mobileOperator)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Price price)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Campaign campaign)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Campaign campaign, Country country)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Campaign campaign, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Campaign campaign, DynamicPlatform dynamicPlatform)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Campaign campaign, MobileOperator mobileOperator)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Campaign campaign, Price price)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Country country)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, DynamicPlatform dynamicPlatform)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, MobileOperator mobileOperator)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Price price)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Country country, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Country country, DynamicPlatform dynamicPlatform)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Country country, MobileOperator mobileOperator)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Country country, Price price)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(DynamicPlatform dynamicPlatform, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(MobileOperator mobileOperator, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Price price, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(MobileOperator mobileOperator, DynamicPlatform dynamicPlatform)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Price price, DynamicPlatform dynamicPlatform)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(MobileOperator mobileOperator, Price price)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Campaign campaign, Country country)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Campaign campaign, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Campaign campaign, DynamicPlatform dynamicPlatform)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Campaign campaign, MobileOperator mobileOperator)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Campaign campaign, Price price)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Campaign campaign, Country country, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Campaign campaign, Country country, DynamicPlatform dynamicPlatform)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Campaign campaign, Country country, MobileOperator mobileOperator)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Campaign campaign, Country country, Price price)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Campaign campaign, DynamicPlatform dynamicPlatform, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Campaign campaign, MobileOperator mobileOperator, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Campaign campaign, Price price, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Campaign campaign, MobileOperator mobileOperator, DynamicPlatform dynamicPlatform)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Campaign campaign, Price price, DynamicPlatform dynamicPlatform)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Campaign campaign, MobileOperator mobileOperator, Price price)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Country country, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Country country, DynamicPlatform dynamicPlatform)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Country country, MobileOperator mobileOperator)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Country country, Price price)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, DynamicPlatform dynamicPlatform, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, MobileOperator mobileOperator, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Price price, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, MobileOperator mobileOperator, DynamicPlatform dynamicPlatform)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Price price, DynamicPlatform dynamicPlatform)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, MobileOperator mobileOperator, Price price)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Country country, DynamicPlatform dynamicPlatform, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Country country, MobileOperator mobileOperator, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Country country, Price price, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Country country, MobileOperator mobileOperator, DynamicPlatform dynamicPlatform)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Country country, Price price, DynamicPlatform dynamicPlatform)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Country country, MobileOperator mobileOperator, Price price)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(MobileOperator mobileOperator, DynamicPlatform dynamicPlatform, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Price price, DynamicPlatform dynamicPlatform, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(MobileOperator mobileOperator, Price price, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(MobileOperator mobileOperator, Price price, DynamicPlatform dynamicPlatform)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Campaign campaign, Country country, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Campaign campaign, Country country, DynamicPlatform dynamicPlatform)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Campaign campaign, Country country, MobileOperator mobileOperator)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Campaign campaign, Country country, Price price)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Campaign campaign, DynamicPlatform dynamicPlatform, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Campaign campaign, MobileOperator mobileOperator, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Campaign campaign, Price price, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Campaign campaign, MobileOperator mobileOperator, DynamicPlatform dynamicPlatform)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Campaign campaign, Price price, DynamicPlatform dynamicPlatform)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Campaign campaign, MobileOperator mobileOperator, Price price)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Campaign campaign, Country country, DynamicPlatform dynamicPlatform, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Campaign campaign, Country country, MobileOperator mobileOperator, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Campaign campaign, Country country, Price price, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Campaign campaign, Country country, MobileOperator mobileOperator, DynamicPlatform dynamicPlatform)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Campaign campaign, Country country, Price price, DynamicPlatform dynamicPlatform)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Campaign campaign, Country country, MobileOperator mobileOperator, Price price)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Campaign campaign, MobileOperator mobileOperator, DynamicPlatform dynamicPlatform, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Campaign campaign, Price price, DynamicPlatform dynamicPlatform, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Campaign campaign, MobileOperator mobileOperator, Price price, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Campaign campaign, MobileOperator mobileOperator, Price price, DynamicPlatform dynamicPlatform)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Country country, DynamicPlatform dynamicPlatform, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Country country, MobileOperator mobileOperator, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Country country, Price price, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Country country, MobileOperator mobileOperator, DynamicPlatform dynamicPlatform)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Country country, Price price, DynamicPlatform dynamicPlatform)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Country country, MobileOperator mobileOperator, Price price)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, MobileOperator mobileOperator, DynamicPlatform dynamicPlatform, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Price price, DynamicPlatform dynamicPlatform, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, MobileOperator mobileOperator, Price price, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, MobileOperator mobileOperator, Price price, DynamicPlatform dynamicPlatform)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Country country, MobileOperator mobileOperator, DynamicPlatform dynamicPlatform, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Country country, Price price, DynamicPlatform dynamicPlatform, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Country country, MobileOperator mobileOperator, Price price, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Country country, MobileOperator mobileOperator, Price price, DynamicPlatform dynamicPlatform)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(MobileOperator mobileOperator, Price price, DynamicPlatform dynamicPlatform, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Campaign campaign, Country country, DynamicPlatform dynamicPlatform, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Campaign campaign, Country country, MobileOperator mobileOperator, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Campaign campaign, Country country, Price price, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Campaign campaign, Country country, MobileOperator mobileOperator, DynamicPlatform dynamicPlatform)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Campaign campaign, Country country, Price price, DynamicPlatform dynamicPlatform)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Campaign campaign, Country country, MobileOperator mobileOperator, Price price)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Campaign campaign, MobileOperator mobileOperator, DynamicPlatform dynamicPlatform, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Campaign campaign, Price price, DynamicPlatform dynamicPlatform, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Campaign campaign, MobileOperator mobileOperator, Price price, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Campaign campaign, MobileOperator mobileOperator, Price price, DynamicPlatform dynamicPlatform)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Campaign campaign, Country country, MobileOperator mobileOperator, DynamicPlatform dynamicPlatform, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Campaign campaign, Country country, Price price, DynamicPlatform dynamicPlatform, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Campaign campaign, Country country, MobileOperator mobileOperator, Price price, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Campaign campaign, Country country, MobileOperator mobileOperator, Price price, DynamicPlatform dynamicPlatform)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Campaign campaign, MobileOperator mobileOperator, Price price, DynamicPlatform dynamicPlatform, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Country country, MobileOperator mobileOperator, DynamicPlatform dynamicPlatform, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Country country, Price price, DynamicPlatform dynamicPlatform, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Country country, MobileOperator mobileOperator, Price price, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Country country, MobileOperator mobileOperator, Price price, DynamicPlatform dynamicPlatform)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, MobileOperator mobileOperator, Price price, DynamicPlatform dynamicPlatform, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Country country, MobileOperator mobileOperator, Price price, DynamicPlatform dynamicPlatform, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Campaign campaign, Country country, MobileOperator mobileOperator, DynamicPlatform dynamicPlatform, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,(PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Campaign campaign, Country country, Price price, DynamicPlatform dynamicPlatform, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), (MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Campaign campaign, Country country, MobileOperator mobileOperator, Price price, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,(DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Campaign campaign, Country country, MobileOperator mobileOperator, Price price, DynamicPlatform dynamicPlatform)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,(DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Campaign campaign, MobileOperator mobileOperator, Price price, DynamicPlatform dynamicPlatform, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), (CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Campaign campaign, Country country, MobileOperator mobileOperator, Price price, DynamicPlatform dynamicPlatform, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Country country, MobileOperator mobileOperator, Price price, DynamicPlatform dynamicPlatform, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		public ClickInformation CreateInstance(Click click, Campaign campaign, Country country, MobileOperator mobileOperator, Price price, DynamicPlatform dynamicPlatform, DynamicBrowser dynamicBrowser)  
		{ 
			if (!this.HasData) return null;
			return new ClickInformation(this.ClickInformationID,click ?? new Click(this.ClickID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), country ?? (this.CountryID.HasValue ? new Country(this.CountryID.Value) : null), mobileOperator ?? (this.MobileOperatorID.HasValue ? new MobileOperator(this.MobileOperatorID.Value) : null), this.MobileOperatorName,price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.EntranceUrl,this.RedirectUrl,this.Referrer,this.UserAgent,this.IPAddress,this.IsMobile,this.ScreenPixelsHeight,this.ScreenPixelsWidth,this.HardwareVendor,this.HardwareModel,this.PlatformVendor,dynamicPlatform ?? (this.DynamicPlatformID.HasValue ? new DynamicPlatform(this.DynamicPlatformID.Value) : null), this.PlatformName,this.PlatformVersion,this.BrowserVendor,dynamicBrowser ?? (this.DynamicBrowserID.HasValue ? new DynamicBrowser(this.DynamicBrowserID.Value) : null), this.BrowserName,this.BrowserVersion,this.CountryCode,this.Region,this.City,this.ISPName,this.DomainName,this.MobileBrand,this.UsageType,this.Note,this.Updated,this.Created); 
		}
		

  }
}

