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
  public class DynamicPlatformTable : TableBase<DynamicPlatform>
  {
    public static string GetColumnNames()
    {
      return TableBase<DynamicPlatform>.GetColumnNames(string.Empty, DynamicPlatformTable.Columns.Item);
    }

    public static string GetColumnNames(string tablePrefix)
    {
      return TableBase<DynamicPlatform>.GetColumnNames(tablePrefix, DynamicPlatformTable.Columns.Item);
    }

    public class Columns
    {
		
			public static readonly ColumnDescription DynamicPlatformID = new ColumnDescription("DynamicPlatformID", 0, typeof(int));
			public static readonly ColumnDescription Name = new ColumnDescription("Name", 1, typeof(string));
			public static readonly ColumnDescription Updated = new ColumnDescription("Updated", 2, typeof(DateTime));
			public static readonly ColumnDescription Created = new ColumnDescription("Created", 3, typeof(DateTime));
			

			public static readonly ColumnDescription[] Item = new ColumnDescription[] 
			{
				DynamicPlatformID,
				Name,
				Updated,
				Created
			};
    }

    public override int ColumnCount
    {
      get { return Columns.Item.Length; }
    }

    public DynamicPlatformTable(SqlQuery query) : base(query) { }
    public DynamicPlatformTable(SqlQuery query, ColumnSelectMode selectMode,
                           params ColumnDescription[] columns)
      : base(query, selectMode, columns) { }

    public ColumnDescription this[int index] { get { return Columns.Item[index]; } }

		public int DynamicPlatformID { get { return this.Reader.GetInt32(this.GetIndex(Columns.DynamicPlatformID)); } }
		public string Name { get { return this.Reader.GetString(this.GetIndex(Columns.Name)); } }
		public DateTime Updated { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Updated)); } }
		public DateTime Created { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Created)); } }
		

	  public DynamicPlatform CreateInstance()  
		{ 
			if (!this.HasData) return null;
			return new DynamicPlatform(this.DynamicPlatformID,this.Name,this.Updated,this.Created); 
		}
		

  }
}

