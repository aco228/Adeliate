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
  public class PriceTable : TableBase<Price>
  {
    public static string GetColumnNames()
    {
      return TableBase<Price>.GetColumnNames(string.Empty, PriceTable.Columns.Item);
    }

    public static string GetColumnNames(string tablePrefix)
    {
      return TableBase<Price>.GetColumnNames(tablePrefix, PriceTable.Columns.Item);
    }

    public class Columns
    {
		
			public static readonly ColumnDescription PriceID = new ColumnDescription("PriceID", 0, typeof(int));
			public static readonly ColumnDescription CurrencyID = new ColumnDescription("CurrencyID", 1, typeof(int));
			public static readonly ColumnDescription Value = new ColumnDescription("Value", 2, typeof(double));
			public static readonly ColumnDescription Updated = new ColumnDescription("Updated", 3, typeof(DateTime));
			public static readonly ColumnDescription Created = new ColumnDescription("Created", 4, typeof(DateTime));
			

			public static readonly ColumnDescription[] Item = new ColumnDescription[] 
			{
				PriceID,
				CurrencyID,
				Value,
				Updated,
				Created
			};
    }

    public override int ColumnCount
    {
      get { return Columns.Item.Length; }
    }

    public PriceTable(SqlQuery query) : base(query) { }
    public PriceTable(SqlQuery query, ColumnSelectMode selectMode,
                           params ColumnDescription[] columns)
      : base(query, selectMode, columns) { }

    public ColumnDescription this[int index] { get { return Columns.Item[index]; } }

		public int PriceID { get { return this.Reader.GetInt32(this.GetIndex(Columns.PriceID)); } }
		public int CurrencyID { get { return this.Reader.GetInt32(this.GetIndex(Columns.CurrencyID)); } }
		public decimal Value { get { return this.Reader.GetDecimal(this.GetIndex(Columns.Value)); } }
		public DateTime Updated { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Updated)); } }
		public DateTime Created { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Created)); } }
		

	  public Price CreateInstance()  
		{ 
			if (!this.HasData) return null;
			return new Price(this.PriceID,new Currency(this.CurrencyID), this.Value,this.Updated,this.Created); 
		}
		public Price CreateInstance(Currency currency)  
		{ 
			if (!this.HasData) return null;
			return new Price(this.PriceID,currency ?? new Currency(this.CurrencyID), this.Value,this.Updated,this.Created); 
		}
		

  }
}

