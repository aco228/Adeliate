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
  public class TransactionTable : TableBase<Transaction>
  {
    public static string GetColumnNames()
    {
      return TableBase<Transaction>.GetColumnNames(string.Empty, TransactionTable.Columns.Item);
    }

    public static string GetColumnNames(string tablePrefix)
    {
      return TableBase<Transaction>.GetColumnNames(tablePrefix, TransactionTable.Columns.Item);
    }

    public class Columns
    {
		
			public static readonly ColumnDescription TransactionID = new ColumnDescription("TransactionID", 0, typeof(int));
			public static readonly ColumnDescription ClickID = new ColumnDescription("ClickID", 1, typeof(int));
			public static readonly ColumnDescription PriceID = new ColumnDescription("PriceID", 2, typeof(int));
			public static readonly ColumnDescription OfferID = new ColumnDescription("OfferID", 3, typeof(int));
			public static readonly ColumnDescription CampaignID = new ColumnDescription("CampaignID", 4, typeof(int));
			public static readonly ColumnDescription AffiliateID = new ColumnDescription("AffiliateID", 5, typeof(int));
			public static readonly ColumnDescription PriceInEuros = new ColumnDescription("PriceInEuros", 6, typeof(double));
			public static readonly ColumnDescription Updated = new ColumnDescription("Updated", 7, typeof(DateTime));
			public static readonly ColumnDescription Created = new ColumnDescription("Created", 8, typeof(DateTime));
			

			public static readonly ColumnDescription[] Item = new ColumnDescription[] 
			{
				TransactionID,
				ClickID,
				PriceID,
				OfferID,
				CampaignID,
				AffiliateID,
				PriceInEuros,
				Updated,
				Created
			};
    }

    public override int ColumnCount
    {
      get { return Columns.Item.Length; }
    }

    public TransactionTable(SqlQuery query) : base(query) { }
    public TransactionTable(SqlQuery query, ColumnSelectMode selectMode,
                           params ColumnDescription[] columns)
      : base(query, selectMode, columns) { }

    public ColumnDescription this[int index] { get { return Columns.Item[index]; } }

		public int TransactionID { get { return this.Reader.GetInt32(this.GetIndex(Columns.TransactionID)); } }
		public int ClickID { get { return this.Reader.GetInt32(this.GetIndex(Columns.ClickID)); } }
		public int PriceID { get { return this.Reader.GetInt32(this.GetIndex(Columns.PriceID)); } }
		public int OfferID { get { return this.Reader.GetInt32(this.GetIndex(Columns.OfferID)); } }
		public int CampaignID { get { return this.Reader.GetInt32(this.GetIndex(Columns.CampaignID)); } }
		public int AffiliateID { get { return this.Reader.GetInt32(this.GetIndex(Columns.AffiliateID)); } }
		public decimal PriceInEuros { get { return this.Reader.GetDecimal(this.GetIndex(Columns.PriceInEuros)); } }
		public DateTime Updated { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Updated)); } }
		public DateTime Created { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Created)); } }
		

	  public Transaction CreateInstance()  
		{ 
			if (!this.HasData) return null;
			return new Transaction(this.TransactionID,this.ClickID,new Price(this.PriceID), this.OfferID,this.CampaignID,this.AffiliateID,this.PriceInEuros,this.Updated,this.Created); 
		}
		public Transaction CreateInstance(Price price)  
		{ 
			if (!this.HasData) return null;
			return new Transaction(this.TransactionID,this.ClickID,price ?? new Price(this.PriceID), this.OfferID,this.CampaignID,this.AffiliateID,this.PriceInEuros,this.Updated,this.Created); 
		}
		

  }
}

