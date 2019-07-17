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
  public class DynamicBrowserTable : TableBase<DynamicBrowser>
  {
    public static string GetColumnNames()
    {
      return TableBase<DynamicBrowser>.GetColumnNames(string.Empty, DynamicBrowserTable.Columns.Item);
    }

    public static string GetColumnNames(string tablePrefix)
    {
      return TableBase<DynamicBrowser>.GetColumnNames(tablePrefix, DynamicBrowserTable.Columns.Item);
    }

    public class Columns
    {
		
			public static readonly ColumnDescription DynamicBrowserID = new ColumnDescription("DynamicBrowserID", 0, typeof(int));
			public static readonly ColumnDescription Name = new ColumnDescription("Name", 1, typeof(string));
			public static readonly ColumnDescription Updated = new ColumnDescription("Updated", 2, typeof(DateTime));
			public static readonly ColumnDescription Created = new ColumnDescription("Created", 3, typeof(DateTime));
			

			public static readonly ColumnDescription[] Item = new ColumnDescription[] 
			{
				DynamicBrowserID,
				Name,
				Updated,
				Created
			};
    }

    public override int ColumnCount
    {
      get { return Columns.Item.Length; }
    }

    public DynamicBrowserTable(SqlQuery query) : base(query) { }
    public DynamicBrowserTable(SqlQuery query, ColumnSelectMode selectMode,
                           params ColumnDescription[] columns)
      : base(query, selectMode, columns) { }

    public ColumnDescription this[int index] { get { return Columns.Item[index]; } }

		public int DynamicBrowserID { get { return this.Reader.GetInt32(this.GetIndex(Columns.DynamicBrowserID)); } }
		public string Name { get { return this.Reader.GetString(this.GetIndex(Columns.Name)); } }
		public DateTime Updated { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Updated)); } }
		public DateTime Created { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Created)); } }
		

	  public DynamicBrowser CreateInstance()  
		{ 
			if (!this.HasData) return null;
			return new DynamicBrowser(this.DynamicBrowserID,this.Name,this.Updated,this.Created); 
		}
		

  }
}

