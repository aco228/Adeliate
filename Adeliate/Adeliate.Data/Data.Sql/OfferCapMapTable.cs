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
  public class OfferCapMapTable : TableBase<OfferCapMap>
  {
    public static string GetColumnNames()
    {
      return TableBase<OfferCapMap>.GetColumnNames(string.Empty, OfferCapMapTable.Columns.Item);
    }

    public static string GetColumnNames(string tablePrefix)
    {
      return TableBase<OfferCapMap>.GetColumnNames(tablePrefix, OfferCapMapTable.Columns.Item);
    }

    public class Columns
    {
		
			public static readonly ColumnDescription OfferCapMapID = new ColumnDescription("OfferCapMapID", 0, typeof(int));
			public static readonly ColumnDescription OfferID = new ColumnDescription("OfferID", 1, typeof(int));
			public static readonly ColumnDescription Transactions = new ColumnDescription("Transactions", 2, typeof(int));
			public static readonly ColumnDescription Locked = new ColumnDescription("Locked", 3, typeof(bool));
			public static readonly ColumnDescription Updated = new ColumnDescription("Updated", 4, typeof(DateTime));
			public static readonly ColumnDescription Created = new ColumnDescription("Created", 5, typeof(DateTime));
			

			public static readonly ColumnDescription[] Item = new ColumnDescription[] 
			{
				OfferCapMapID,
				OfferID,
				Transactions,
				Locked,
				Updated,
				Created
			};
    }

    public override int ColumnCount
    {
      get { return Columns.Item.Length; }
    }

    public OfferCapMapTable(SqlQuery query) : base(query) { }
    public OfferCapMapTable(SqlQuery query, ColumnSelectMode selectMode,
                           params ColumnDescription[] columns)
      : base(query, selectMode, columns) { }

    public ColumnDescription this[int index] { get { return Columns.Item[index]; } }

		public int OfferCapMapID { get { return this.Reader.GetInt32(this.GetIndex(Columns.OfferCapMapID)); } }
		public int OfferID { get { return this.Reader.GetInt32(this.GetIndex(Columns.OfferID)); } }
		public int Transactions { get { return this.Reader.GetInt32(this.GetIndex(Columns.Transactions)); } }
		public bool Locked { get { return this.Reader.GetBoolean(this.GetIndex(Columns.Locked)); } }
		public DateTime Updated { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Updated)); } }
		public DateTime Created { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Created)); } }
		

	  public OfferCapMap CreateInstance()  
		{ 
			if (!this.HasData) return null;
			return new OfferCapMap(this.OfferCapMapID,new Offer(this.OfferID), this.Transactions,this.Locked,this.Updated,this.Created); 
		}
		public OfferCapMap CreateInstance(Offer offer)  
		{ 
			if (!this.HasData) return null;
			return new OfferCapMap(this.OfferCapMapID,offer ?? new Offer(this.OfferID), this.Transactions,this.Locked,this.Updated,this.Created); 
		}
		

  }
}

