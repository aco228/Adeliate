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
  public class StatInfoTable : TableBase<StatInfo>
  {
    public static string GetColumnNames()
    {
      return TableBase<StatInfo>.GetColumnNames(string.Empty, StatInfoTable.Columns.Item);
    }

    public static string GetColumnNames(string tablePrefix)
    {
      return TableBase<StatInfo>.GetColumnNames(tablePrefix, StatInfoTable.Columns.Item);
    }

    public class Columns
    {
		
			public static readonly ColumnDescription StatInfoID = new ColumnDescription("StatInfoID", 0, typeof(int));
			public static readonly ColumnDescription OfferID = new ColumnDescription("OfferID", 1, typeof(int));
			public static readonly ColumnDescription AffiliateID = new ColumnDescription("AffiliateID", 2, typeof(int));
			public static readonly ColumnDescription StatTypeID = new ColumnDescription("StatTypeID", 3, typeof(int));
			public static readonly ColumnDescription Clicks = new ColumnDescription("Clicks", 4, typeof(int));
			public static readonly ColumnDescription Transactions = new ColumnDescription("Transactions", 5, typeof(int));
			public static readonly ColumnDescription Revenue = new ColumnDescription("Revenue", 6, typeof(double));
			public static readonly ColumnDescription Updated = new ColumnDescription("Updated", 7, typeof(DateTime));
			public static readonly ColumnDescription Created = new ColumnDescription("Created", 8, typeof(DateTime));
			

			public static readonly ColumnDescription[] Item = new ColumnDescription[] 
			{
				StatInfoID,
				OfferID,
				AffiliateID,
				StatTypeID,
				Clicks,
				Transactions,
				Revenue,
				Updated,
				Created
			};
    }

    public override int ColumnCount
    {
      get { return Columns.Item.Length; }
    }

    public StatInfoTable(SqlQuery query) : base(query) { }
    public StatInfoTable(SqlQuery query, ColumnSelectMode selectMode,
                           params ColumnDescription[] columns)
      : base(query, selectMode, columns) { }

    public ColumnDescription this[int index] { get { return Columns.Item[index]; } }

		public int StatInfoID { get { return this.Reader.GetInt32(this.GetIndex(Columns.StatInfoID)); } }
		public int OfferID { get { return this.Reader.GetInt32(this.GetIndex(Columns.OfferID)); } }
		public int AffiliateID { get { return this.Reader.GetInt32(this.GetIndex(Columns.AffiliateID)); } }
		public int StatTypeID { get { return this.Reader.GetInt32(this.GetIndex(Columns.StatTypeID)); } }
		
		public int? Clicks 
		{
			get
			{
				int index = this.GetIndex(Columns.Clicks);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetInt32(index);
			}
		}

		
		public int? Transactions 
		{
			get
			{
				int index = this.GetIndex(Columns.Transactions);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetInt32(index);
			}
		}

		
		public decimal? Revenue 
		{
			get
			{
				int index = this.GetIndex(Columns.Revenue);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetDecimal(index);
			}
		}

		public DateTime Updated { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Updated)); } }
		public DateTime Created { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Created)); } }
		

	  public StatInfo CreateInstance()  
		{ 
			if (!this.HasData) return null;
			return new StatInfo(this.StatInfoID,this.OfferID,this.AffiliateID,(StatType)this.StatTypeID,this.Clicks,this.Transactions,this.Revenue,this.Updated,this.Created); 
		}
		

  }
}

