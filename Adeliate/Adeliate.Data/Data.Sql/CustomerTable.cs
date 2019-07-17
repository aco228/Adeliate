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
  public class CustomerTable : TableBase<Customer>
  {
    public static string GetColumnNames()
    {
      return TableBase<Customer>.GetColumnNames(string.Empty, CustomerTable.Columns.Item);
    }

    public static string GetColumnNames(string tablePrefix)
    {
      return TableBase<Customer>.GetColumnNames(tablePrefix, CustomerTable.Columns.Item);
    }

    public class Columns
    {
		
			public static readonly ColumnDescription CustomerID = new ColumnDescription("CustomerID", 0, typeof(int));
			public static readonly ColumnDescription AffiliateID = new ColumnDescription("AffiliateID", 1, typeof(int));
			public static readonly ColumnDescription CustomerTypeID = new ColumnDescription("CustomerTypeID", 2, typeof(int));
			public static readonly ColumnDescription CustomerStatusID = new ColumnDescription("CustomerStatusID", 3, typeof(int));
			public static readonly ColumnDescription Username = new ColumnDescription("Username", 4, typeof(string));
			public static readonly ColumnDescription Password = new ColumnDescription("Password", 5, typeof(string));
			public static readonly ColumnDescription IsActive = new ColumnDescription("IsActive", 6, typeof(bool));
			public static readonly ColumnDescription Updated = new ColumnDescription("Updated", 7, typeof(DateTime));
			public static readonly ColumnDescription Created = new ColumnDescription("Created", 8, typeof(DateTime));
			

			public static readonly ColumnDescription[] Item = new ColumnDescription[] 
			{
				CustomerID,
				AffiliateID,
				CustomerTypeID,
				CustomerStatusID,
				Username,
				Password,
				IsActive,
				Updated,
				Created
			};
    }

    public override int ColumnCount
    {
      get { return Columns.Item.Length; }
    }

    public CustomerTable(SqlQuery query) : base(query) { }
    public CustomerTable(SqlQuery query, ColumnSelectMode selectMode,
                           params ColumnDescription[] columns)
      : base(query, selectMode, columns) { }

    public ColumnDescription this[int index] { get { return Columns.Item[index]; } }

		public int CustomerID { get { return this.Reader.GetInt32(this.GetIndex(Columns.CustomerID)); } }
		public int AffiliateID { get { return this.Reader.GetInt32(this.GetIndex(Columns.AffiliateID)); } }
		public int CustomerTypeID { get { return this.Reader.GetInt32(this.GetIndex(Columns.CustomerTypeID)); } }
		public int CustomerStatusID { get { return this.Reader.GetInt32(this.GetIndex(Columns.CustomerStatusID)); } }
		public string Username { get { return this.Reader.GetString(this.GetIndex(Columns.Username)); } }
		public string Password { get { return this.Reader.GetString(this.GetIndex(Columns.Password)); } }
		public bool IsActive { get { return this.Reader.GetBoolean(this.GetIndex(Columns.IsActive)); } }
		public DateTime Updated { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Updated)); } }
		public DateTime Created { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Created)); } }
		

	  public Customer CreateInstance()  
		{ 
			if (!this.HasData) return null;
			return new Customer(this.CustomerID,new Affiliate(this.AffiliateID), new CustomerType(this.CustomerTypeID), (CustomerStatus)this.CustomerStatusID,this.Username,this.Password,this.IsActive,this.Updated,this.Created); 
		}
		public Customer CreateInstance(Affiliate affiliate)  
		{ 
			if (!this.HasData) return null;
			return new Customer(this.CustomerID,affiliate ?? new Affiliate(this.AffiliateID), new CustomerType(this.CustomerTypeID), (CustomerStatus)this.CustomerStatusID,this.Username,this.Password,this.IsActive,this.Updated,this.Created); 
		}
		public Customer CreateInstance(CustomerType customerType)  
		{ 
			if (!this.HasData) return null;
			return new Customer(this.CustomerID,new Affiliate(this.AffiliateID), customerType ?? new CustomerType(this.CustomerTypeID), (CustomerStatus)this.CustomerStatusID,this.Username,this.Password,this.IsActive,this.Updated,this.Created); 
		}
		public Customer CreateInstance(Affiliate affiliate, CustomerType customerType)  
		{ 
			if (!this.HasData) return null;
			return new Customer(this.CustomerID,affiliate ?? new Affiliate(this.AffiliateID), customerType ?? new CustomerType(this.CustomerTypeID), (CustomerStatus)this.CustomerStatusID,this.Username,this.Password,this.IsActive,this.Updated,this.Created); 
		}
		

  }
}

