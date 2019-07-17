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
  public class ClickTable : TableBase<Click>
  {
    public static string GetColumnNames()
    {
      return TableBase<Click>.GetColumnNames(string.Empty, ClickTable.Columns.Item);
    }

    public static string GetColumnNames(string tablePrefix)
    {
      return TableBase<Click>.GetColumnNames(tablePrefix, ClickTable.Columns.Item);
    }

    public class Columns
    {
		
			public static readonly ColumnDescription ClickID = new ColumnDescription("ClickID", 0, typeof(int));
			public static readonly ColumnDescription SubID = new ColumnDescription("SubID", 1, typeof(string));
			public static readonly ColumnDescription OfferID = new ColumnDescription("OfferID", 2, typeof(int));
			public static readonly ColumnDescription AffiliateID = new ColumnDescription("AffiliateID", 3, typeof(int));
			public static readonly ColumnDescription HasTransaction = new ColumnDescription("HasTransaction", 4, typeof(bool));
			public static readonly ColumnDescription Updated = new ColumnDescription("Updated", 5, typeof(DateTime));
			public static readonly ColumnDescription Created = new ColumnDescription("Created", 6, typeof(DateTime));
			

			public static readonly ColumnDescription[] Item = new ColumnDescription[] 
			{
				ClickID,
				SubID,
				OfferID,
				AffiliateID,
				HasTransaction,
				Updated,
				Created
			};
    }

    public override int ColumnCount
    {
      get { return Columns.Item.Length; }
    }

    public ClickTable(SqlQuery query) : base(query) { }
    public ClickTable(SqlQuery query, ColumnSelectMode selectMode,
                           params ColumnDescription[] columns)
      : base(query, selectMode, columns) { }

    public ColumnDescription this[int index] { get { return Columns.Item[index]; } }

		public int ClickID { get { return this.Reader.GetInt32(this.GetIndex(Columns.ClickID)); } }
		public string SubID { get { return this.Reader.GetString(this.GetIndex(Columns.SubID)); } }
		
		public int? OfferID 
		{
			get
			{
				int index = this.GetIndex(Columns.OfferID);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetInt32(index);
			}
		}

		
		public int? AffiliateID 
		{
			get
			{
				int index = this.GetIndex(Columns.AffiliateID);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetInt32(index);
			}
		}

		public bool HasTransaction { get { return this.Reader.GetBoolean(this.GetIndex(Columns.HasTransaction)); } }
		public DateTime Updated { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Updated)); } }
		public DateTime Created { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Created)); } }
		

	  public Click CreateInstance()  
		{ 
			if (!this.HasData) return null;
			return new Click(this.ClickID,this.SubID,(OfferID.HasValue ? new Offer(this.OfferID.Value) : null), (AffiliateID.HasValue ? new Affiliate(this.AffiliateID.Value) : null), this.HasTransaction,this.Updated,this.Created); 
		}
		public Click CreateInstance(Affiliate affiliate)  
		{ 
			if (!this.HasData) return null;
			return new Click(this.ClickID,this.SubID,(OfferID.HasValue ? new Offer(this.OfferID.Value) : null), affiliate ?? (this.AffiliateID.HasValue ? new Affiliate(this.AffiliateID.Value) : null), this.HasTransaction,this.Updated,this.Created); 
		}
		public Click CreateInstance(Offer offer)  
		{ 
			if (!this.HasData) return null;
			return new Click(this.ClickID,this.SubID,offer ?? (this.OfferID.HasValue ? new Offer(this.OfferID.Value) : null), (AffiliateID.HasValue ? new Affiliate(this.AffiliateID.Value) : null), this.HasTransaction,this.Updated,this.Created); 
		}
		public Click CreateInstance(Offer offer, Affiliate affiliate)  
		{ 
			if (!this.HasData) return null;
			return new Click(this.ClickID,this.SubID,offer ?? (this.OfferID.HasValue ? new Offer(this.OfferID.Value) : null), affiliate ?? (this.AffiliateID.HasValue ? new Affiliate(this.AffiliateID.Value) : null), this.HasTransaction,this.Updated,this.Created); 
		}
		

  }
}

