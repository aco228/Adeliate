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
  public class OfferTable : TableBase<Offer>
  {
    public static string GetColumnNames()
    {
      return TableBase<Offer>.GetColumnNames(string.Empty, OfferTable.Columns.Item);
    }

    public static string GetColumnNames(string tablePrefix)
    {
      return TableBase<Offer>.GetColumnNames(tablePrefix, OfferTable.Columns.Item);
    }

    public class Columns
    {
		
			public static readonly ColumnDescription OfferID = new ColumnDescription("OfferID", 0, typeof(int));
			public static readonly ColumnDescription CampaignID = new ColumnDescription("CampaignID", 1, typeof(int));
			public static readonly ColumnDescription AffiliateID = new ColumnDescription("AffiliateID", 2, typeof(int));
			public static readonly ColumnDescription PriceID = new ColumnDescription("PriceID", 3, typeof(int));
			public static readonly ColumnDescription CapValue = new ColumnDescription("CapValue", 4, typeof(int));
			public static readonly ColumnDescription Key = new ColumnDescription("Key", 5, typeof(string));
			public static readonly ColumnDescription IsActive = new ColumnDescription("IsActive", 6, typeof(bool));
			public static readonly ColumnDescription Updated = new ColumnDescription("Updated", 7, typeof(DateTime));
			public static readonly ColumnDescription Created = new ColumnDescription("Created", 8, typeof(DateTime));
			

			public static readonly ColumnDescription[] Item = new ColumnDescription[] 
			{
				OfferID,
				CampaignID,
				AffiliateID,
				PriceID,
				CapValue,
				Key,
				IsActive,
				Updated,
				Created
			};
    }

    public override int ColumnCount
    {
      get { return Columns.Item.Length; }
    }

    public OfferTable(SqlQuery query) : base(query) { }
    public OfferTable(SqlQuery query, ColumnSelectMode selectMode,
                           params ColumnDescription[] columns)
      : base(query, selectMode, columns) { }

    public ColumnDescription this[int index] { get { return Columns.Item[index]; } }

		public int OfferID { get { return this.Reader.GetInt32(this.GetIndex(Columns.OfferID)); } }
		public int CampaignID { get { return this.Reader.GetInt32(this.GetIndex(Columns.CampaignID)); } }
		public int AffiliateID { get { return this.Reader.GetInt32(this.GetIndex(Columns.AffiliateID)); } }
		
		public int? PriceID 
		{
			get
			{
				int index = this.GetIndex(Columns.PriceID);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetInt32(index);
			}
		}

		
		public int? CapValue 
		{
			get
			{
				int index = this.GetIndex(Columns.CapValue);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetInt32(index);
			}
		}

		public string Key { get { return this.Reader.GetString(this.GetIndex(Columns.Key)); } }
		public bool IsActive { get { return this.Reader.GetBoolean(this.GetIndex(Columns.IsActive)); } }
		public DateTime Updated { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Updated)); } }
		public DateTime Created { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Created)); } }
		

	  public Offer CreateInstance()  
		{ 
			if (!this.HasData) return null;
			return new Offer(this.OfferID,new Campaign(this.CampaignID), new Affiliate(this.AffiliateID), (PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.CapValue,this.Key,this.IsActive,this.Updated,this.Created); 
		}
		public Offer CreateInstance(Affiliate affiliate)  
		{ 
			if (!this.HasData) return null;
			return new Offer(this.OfferID,new Campaign(this.CampaignID), affiliate ?? new Affiliate(this.AffiliateID), (PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.CapValue,this.Key,this.IsActive,this.Updated,this.Created); 
		}
		public Offer CreateInstance(Campaign campaign)  
		{ 
			if (!this.HasData) return null;
			return new Offer(this.OfferID,campaign ?? new Campaign(this.CampaignID), new Affiliate(this.AffiliateID), (PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.CapValue,this.Key,this.IsActive,this.Updated,this.Created); 
		}
		public Offer CreateInstance(Price price)  
		{ 
			if (!this.HasData) return null;
			return new Offer(this.OfferID,new Campaign(this.CampaignID), new Affiliate(this.AffiliateID), price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.CapValue,this.Key,this.IsActive,this.Updated,this.Created); 
		}
		public Offer CreateInstance(Campaign campaign, Affiliate affiliate)  
		{ 
			if (!this.HasData) return null;
			return new Offer(this.OfferID,campaign ?? new Campaign(this.CampaignID), affiliate ?? new Affiliate(this.AffiliateID), (PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.CapValue,this.Key,this.IsActive,this.Updated,this.Created); 
		}
		public Offer CreateInstance(Affiliate affiliate, Price price)  
		{ 
			if (!this.HasData) return null;
			return new Offer(this.OfferID,new Campaign(this.CampaignID), affiliate ?? new Affiliate(this.AffiliateID), price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.CapValue,this.Key,this.IsActive,this.Updated,this.Created); 
		}
		public Offer CreateInstance(Campaign campaign, Price price)  
		{ 
			if (!this.HasData) return null;
			return new Offer(this.OfferID,campaign ?? new Campaign(this.CampaignID), new Affiliate(this.AffiliateID), price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.CapValue,this.Key,this.IsActive,this.Updated,this.Created); 
		}
		public Offer CreateInstance(Campaign campaign, Affiliate affiliate, Price price)  
		{ 
			if (!this.HasData) return null;
			return new Offer(this.OfferID,campaign ?? new Campaign(this.CampaignID), affiliate ?? new Affiliate(this.AffiliateID), price ?? (this.PriceID.HasValue ? new Price(this.PriceID.Value) : null), this.CapValue,this.Key,this.IsActive,this.Updated,this.Created); 
		}
		

  }
}

