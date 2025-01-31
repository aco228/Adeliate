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
  public class CurrencyTable : TableBase<Currency>
  {
    public static string GetColumnNames()
    {
      return TableBase<Currency>.GetColumnNames(string.Empty, CurrencyTable.Columns.Item);
    }

    public static string GetColumnNames(string tablePrefix)
    {
      return TableBase<Currency>.GetColumnNames(tablePrefix, CurrencyTable.Columns.Item);
    }

    public class Columns
    {
		
			public static readonly ColumnDescription CurrencyID = new ColumnDescription("CurrencyID", 0, typeof(int));
			public static readonly ColumnDescription Name = new ColumnDescription("Name", 1, typeof(string));
			public static readonly ColumnDescription Abbreviation = new ColumnDescription("Abbreviation", 2, typeof(string));
			public static readonly ColumnDescription Symbol = new ColumnDescription("Symbol", 3, typeof(string));
			public static readonly ColumnDescription Updated = new ColumnDescription("Updated", 4, typeof(DateTime));
			public static readonly ColumnDescription Created = new ColumnDescription("Created", 5, typeof(DateTime));
			

			public static readonly ColumnDescription[] Item = new ColumnDescription[] 
			{
				CurrencyID,
				Name,
				Abbreviation,
				Symbol,
				Updated,
				Created
			};
    }

    public override int ColumnCount
    {
      get { return Columns.Item.Length; }
    }

    public CurrencyTable(SqlQuery query) : base(query) { }
    public CurrencyTable(SqlQuery query, ColumnSelectMode selectMode,
                           params ColumnDescription[] columns)
      : base(query, selectMode, columns) { }

    public ColumnDescription this[int index] { get { return Columns.Item[index]; } }

		public int CurrencyID { get { return this.Reader.GetInt32(this.GetIndex(Columns.CurrencyID)); } }
		public string Name { get { return this.Reader.GetString(this.GetIndex(Columns.Name)); } }
		public string Abbreviation { get { return this.Reader.GetString(this.GetIndex(Columns.Abbreviation)); } }
		public string Symbol { get { return this.Reader.GetString(this.GetIndex(Columns.Symbol)); } }
		public DateTime Updated { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Updated)); } }
		public DateTime Created { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Created)); } }
		

	  public Currency CreateInstance()  
		{ 
			if (!this.HasData) return null;
			return new Currency(this.CurrencyID,this.Name,this.Abbreviation,this.Symbol,this.Updated,this.Created); 
		}
		

  }
}

