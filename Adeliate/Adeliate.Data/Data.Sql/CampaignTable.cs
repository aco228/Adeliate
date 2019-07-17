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
  public class CampaignTable : TableBase<Campaign>
  {
    public static string GetColumnNames()
    {
      return TableBase<Campaign>.GetColumnNames(string.Empty, CampaignTable.Columns.Item);
    }

    public static string GetColumnNames(string tablePrefix)
    {
      return TableBase<Campaign>.GetColumnNames(tablePrefix, CampaignTable.Columns.Item);
    }

    public class Columns
    {
		
			public static readonly ColumnDescription CampaignID = new ColumnDescription("CampaignID", 0, typeof(int));
			public static readonly ColumnDescription CampaignGroupID = new ColumnDescription("CampaignGroupID", 1, typeof(int));
			public static readonly ColumnDescription CampaignContentTypeID = new ColumnDescription("CampaignContentTypeID", 2, typeof(int));
			public static readonly ColumnDescription CountryID = new ColumnDescription("CountryID", 3, typeof(int));
			public static readonly ColumnDescription PriceID = new ColumnDescription("PriceID", 4, typeof(int));
			public static readonly ColumnDescription Name = new ColumnDescription("Name", 5, typeof(string));
			public static readonly ColumnDescription CapValue = new ColumnDescription("CapValue", 6, typeof(int));
			public static readonly ColumnDescription Link = new ColumnDescription("Link", 7, typeof(string));
			public static readonly ColumnDescription FallbackLink = new ColumnDescription("FallbackLink", 8, typeof(string));
			public static readonly ColumnDescription Description = new ColumnDescription("Description", 9, typeof(string));
			public static readonly ColumnDescription Device = new ColumnDescription("Device", 10, typeof(string));
			public static readonly ColumnDescription Browser = new ColumnDescription("Browser", 11, typeof(string));
			public static readonly ColumnDescription IPRanges = new ColumnDescription("IPRanges", 12, typeof(string));
			public static readonly ColumnDescription RestrictCountryTraffic = new ColumnDescription("RestrictCountryTraffic", 13, typeof(bool));
			public static readonly ColumnDescription RestrictMobileTraffic = new ColumnDescription("RestrictMobileTraffic", 14, typeof(bool));
			public static readonly ColumnDescription Updated = new ColumnDescription("Updated", 15, typeof(DateTime));
			public static readonly ColumnDescription Created = new ColumnDescription("Created", 16, typeof(DateTime));
			

			public static readonly ColumnDescription[] Item = new ColumnDescription[] 
			{
				CampaignID,
				CampaignGroupID,
				CampaignContentTypeID,
				CountryID,
				PriceID,
				Name,
				CapValue,
				Link,
				FallbackLink,
				Description,
				Device,
				Browser,
				IPRanges,
				RestrictCountryTraffic,
				RestrictMobileTraffic,
				Updated,
				Created
			};
    }

    public override int ColumnCount
    {
      get { return Columns.Item.Length; }
    }

    public CampaignTable(SqlQuery query) : base(query) { }
    public CampaignTable(SqlQuery query, ColumnSelectMode selectMode,
                           params ColumnDescription[] columns)
      : base(query, selectMode, columns) { }

    public ColumnDescription this[int index] { get { return Columns.Item[index]; } }

		public int CampaignID { get { return this.Reader.GetInt32(this.GetIndex(Columns.CampaignID)); } }
		public int CampaignGroupID { get { return this.Reader.GetInt32(this.GetIndex(Columns.CampaignGroupID)); } }
		public int CampaignContentTypeID { get { return this.Reader.GetInt32(this.GetIndex(Columns.CampaignContentTypeID)); } }
		public int CountryID { get { return this.Reader.GetInt32(this.GetIndex(Columns.CountryID)); } }
		public int PriceID { get { return this.Reader.GetInt32(this.GetIndex(Columns.PriceID)); } }
		public string Name { get { return this.Reader.GetString(this.GetIndex(Columns.Name)); } }
		public int CapValue { get { return this.Reader.GetInt32(this.GetIndex(Columns.CapValue)); } }
		public string Link { get { return this.Reader.GetString(this.GetIndex(Columns.Link)); } }
		public string FallbackLink { get { return this.Reader.GetString(this.GetIndex(Columns.FallbackLink)); } }
		
		public string Description 
		{
			get
			{
				int index = this.GetIndex(Columns.Description);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetString(index);
			}
		}

		public string Device { get { return this.Reader.GetString(this.GetIndex(Columns.Device)); } }
		public string Browser { get { return this.Reader.GetString(this.GetIndex(Columns.Browser)); } }
		
		public string IPRanges 
		{
			get
			{
				int index = this.GetIndex(Columns.IPRanges);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetString(index);
			}
		}

		public bool RestrictCountryTraffic { get { return this.Reader.GetBoolean(this.GetIndex(Columns.RestrictCountryTraffic)); } }
		public bool RestrictMobileTraffic { get { return this.Reader.GetBoolean(this.GetIndex(Columns.RestrictMobileTraffic)); } }
		public DateTime Updated { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Updated)); } }
		public DateTime Created { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Created)); } }
		

	  public Campaign CreateInstance()  
		{ 
			if (!this.HasData) return null;
			return new Campaign(this.CampaignID,new CampaignGroup(this.CampaignGroupID), (CampaignContentType)this.CampaignContentTypeID,new Country(this.CountryID), new Price(this.PriceID), this.Name,this.CapValue,this.Link,this.FallbackLink,this.Description,this.Device,this.Browser,this.IPRanges,this.RestrictCountryTraffic,this.RestrictMobileTraffic,this.Updated,this.Created); 
		}
		public Campaign CreateInstance(CampaignGroup campaignGroup)  
		{ 
			if (!this.HasData) return null;
			return new Campaign(this.CampaignID,campaignGroup ?? new CampaignGroup(this.CampaignGroupID), (CampaignContentType)this.CampaignContentTypeID,new Country(this.CountryID), new Price(this.PriceID), this.Name,this.CapValue,this.Link,this.FallbackLink,this.Description,this.Device,this.Browser,this.IPRanges,this.RestrictCountryTraffic,this.RestrictMobileTraffic,this.Updated,this.Created); 
		}
		public Campaign CreateInstance(Country country)  
		{ 
			if (!this.HasData) return null;
			return new Campaign(this.CampaignID,new CampaignGroup(this.CampaignGroupID), (CampaignContentType)this.CampaignContentTypeID,country ?? new Country(this.CountryID), new Price(this.PriceID), this.Name,this.CapValue,this.Link,this.FallbackLink,this.Description,this.Device,this.Browser,this.IPRanges,this.RestrictCountryTraffic,this.RestrictMobileTraffic,this.Updated,this.Created); 
		}
		public Campaign CreateInstance(Price price)  
		{ 
			if (!this.HasData) return null;
			return new Campaign(this.CampaignID,new CampaignGroup(this.CampaignGroupID), (CampaignContentType)this.CampaignContentTypeID,new Country(this.CountryID), price ?? new Price(this.PriceID), this.Name,this.CapValue,this.Link,this.FallbackLink,this.Description,this.Device,this.Browser,this.IPRanges,this.RestrictCountryTraffic,this.RestrictMobileTraffic,this.Updated,this.Created); 
		}
		public Campaign CreateInstance(CampaignGroup campaignGroup, Country country)  
		{ 
			if (!this.HasData) return null;
			return new Campaign(this.CampaignID,campaignGroup ?? new CampaignGroup(this.CampaignGroupID), (CampaignContentType)this.CampaignContentTypeID,country ?? new Country(this.CountryID), new Price(this.PriceID), this.Name,this.CapValue,this.Link,this.FallbackLink,this.Description,this.Device,this.Browser,this.IPRanges,this.RestrictCountryTraffic,this.RestrictMobileTraffic,this.Updated,this.Created); 
		}
		public Campaign CreateInstance(CampaignGroup campaignGroup, Price price)  
		{ 
			if (!this.HasData) return null;
			return new Campaign(this.CampaignID,campaignGroup ?? new CampaignGroup(this.CampaignGroupID), (CampaignContentType)this.CampaignContentTypeID,new Country(this.CountryID), price ?? new Price(this.PriceID), this.Name,this.CapValue,this.Link,this.FallbackLink,this.Description,this.Device,this.Browser,this.IPRanges,this.RestrictCountryTraffic,this.RestrictMobileTraffic,this.Updated,this.Created); 
		}
		public Campaign CreateInstance(Country country, Price price)  
		{ 
			if (!this.HasData) return null;
			return new Campaign(this.CampaignID,new CampaignGroup(this.CampaignGroupID), (CampaignContentType)this.CampaignContentTypeID,country ?? new Country(this.CountryID), price ?? new Price(this.PriceID), this.Name,this.CapValue,this.Link,this.FallbackLink,this.Description,this.Device,this.Browser,this.IPRanges,this.RestrictCountryTraffic,this.RestrictMobileTraffic,this.Updated,this.Created); 
		}
		public Campaign CreateInstance(CampaignGroup campaignGroup, Country country, Price price)  
		{ 
			if (!this.HasData) return null;
			return new Campaign(this.CampaignID,campaignGroup ?? new CampaignGroup(this.CampaignGroupID), (CampaignContentType)this.CampaignContentTypeID,country ?? new Country(this.CountryID), price ?? new Price(this.PriceID), this.Name,this.CapValue,this.Link,this.FallbackLink,this.Description,this.Device,this.Browser,this.IPRanges,this.RestrictCountryTraffic,this.RestrictMobileTraffic,this.Updated,this.Created); 
		}
		

  }
}

