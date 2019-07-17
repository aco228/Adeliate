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
  public class CustomerImageMapTable : TableBase<CustomerImageMap>
  {
    public static string GetColumnNames()
    {
      return TableBase<CustomerImageMap>.GetColumnNames(string.Empty, CustomerImageMapTable.Columns.Item);
    }

    public static string GetColumnNames(string tablePrefix)
    {
      return TableBase<CustomerImageMap>.GetColumnNames(tablePrefix, CustomerImageMapTable.Columns.Item);
    }

    public class Columns
    {
		
			public static readonly ColumnDescription CustomerImageMapID = new ColumnDescription("CustomerImageMapID", 0, typeof(int));
			public static readonly ColumnDescription CustomerID = new ColumnDescription("CustomerID", 1, typeof(int));
			public static readonly ColumnDescription ImageID = new ColumnDescription("ImageID", 2, typeof(int));
			public static readonly ColumnDescription Updated = new ColumnDescription("Updated", 3, typeof(DateTime));
			public static readonly ColumnDescription Created = new ColumnDescription("Created", 4, typeof(DateTime));
			

			public static readonly ColumnDescription[] Item = new ColumnDescription[] 
			{
				CustomerImageMapID,
				CustomerID,
				ImageID,
				Updated,
				Created
			};
    }

    public override int ColumnCount
    {
      get { return Columns.Item.Length; }
    }

    public CustomerImageMapTable(SqlQuery query) : base(query) { }
    public CustomerImageMapTable(SqlQuery query, ColumnSelectMode selectMode,
                           params ColumnDescription[] columns)
      : base(query, selectMode, columns) { }

    public ColumnDescription this[int index] { get { return Columns.Item[index]; } }

		public int CustomerImageMapID { get { return this.Reader.GetInt32(this.GetIndex(Columns.CustomerImageMapID)); } }
		public int CustomerID { get { return this.Reader.GetInt32(this.GetIndex(Columns.CustomerID)); } }
		public int ImageID { get { return this.Reader.GetInt32(this.GetIndex(Columns.ImageID)); } }
		public DateTime Updated { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Updated)); } }
		public DateTime Created { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Created)); } }
		

	  public CustomerImageMap CreateInstance()  
		{ 
			if (!this.HasData) return null;
			return new CustomerImageMap(this.CustomerImageMapID,new Customer(this.CustomerID), new Image(this.ImageID), this.Updated,this.Created); 
		}
		public CustomerImageMap CreateInstance(Customer customer)  
		{ 
			if (!this.HasData) return null;
			return new CustomerImageMap(this.CustomerImageMapID,customer ?? new Customer(this.CustomerID), new Image(this.ImageID), this.Updated,this.Created); 
		}
		public CustomerImageMap CreateInstance(Image image)  
		{ 
			if (!this.HasData) return null;
			return new CustomerImageMap(this.CustomerImageMapID,new Customer(this.CustomerID), image ?? new Image(this.ImageID), this.Updated,this.Created); 
		}
		public CustomerImageMap CreateInstance(Customer customer, Image image)  
		{ 
			if (!this.HasData) return null;
			return new CustomerImageMap(this.CustomerImageMapID,customer ?? new Customer(this.CustomerID), image ?? new Image(this.ImageID), this.Updated,this.Created); 
		}
		

  }
}

