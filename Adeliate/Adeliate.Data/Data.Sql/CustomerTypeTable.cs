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
  public class CustomerTypeTable : TableBase<CustomerType>
  {
    public static string GetColumnNames()
    {
      return TableBase<CustomerType>.GetColumnNames(string.Empty, CustomerTypeTable.Columns.Item);
    }

    public static string GetColumnNames(string tablePrefix)
    {
      return TableBase<CustomerType>.GetColumnNames(tablePrefix, CustomerTypeTable.Columns.Item);
    }

    public class Columns
    {
		
			public static readonly ColumnDescription CustomerTypeID = new ColumnDescription("CustomerTypeID", 0, typeof(int));
			public static readonly ColumnDescription Name = new ColumnDescription("Name", 1, typeof(string));
			public static readonly ColumnDescription TypeName = new ColumnDescription("TypeName", 2, typeof(string));
			public static readonly ColumnDescription Updated = new ColumnDescription("Updated", 3, typeof(DateTime));
			public static readonly ColumnDescription Created = new ColumnDescription("Created", 4, typeof(DateTime));
			

			public static readonly ColumnDescription[] Item = new ColumnDescription[] 
			{
				CustomerTypeID,
				Name,
				TypeName,
				Updated,
				Created
			};
    }

    public override int ColumnCount
    {
      get { return Columns.Item.Length; }
    }

    public CustomerTypeTable(SqlQuery query) : base(query) { }
    public CustomerTypeTable(SqlQuery query, ColumnSelectMode selectMode,
                           params ColumnDescription[] columns)
      : base(query, selectMode, columns) { }

    public ColumnDescription this[int index] { get { return Columns.Item[index]; } }

		public int CustomerTypeID { get { return this.Reader.GetInt32(this.GetIndex(Columns.CustomerTypeID)); } }
		public string Name { get { return this.Reader.GetString(this.GetIndex(Columns.Name)); } }
		public string TypeName { get { return this.Reader.GetString(this.GetIndex(Columns.TypeName)); } }
		public DateTime Updated { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Updated)); } }
		public DateTime Created { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Created)); } }
		

	  public CustomerType CreateInstance()  
		{ 
			if (!this.HasData) return null;
			return new CustomerType(this.CustomerTypeID,this.Name,this.TypeName,this.Updated,this.Created); 
		}
		

  }
}

