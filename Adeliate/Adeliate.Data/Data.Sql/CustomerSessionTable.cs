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
  public class CustomerSessionTable : TableBase<CustomerSession>
  {
    public static string GetColumnNames()
    {
      return TableBase<CustomerSession>.GetColumnNames(string.Empty, CustomerSessionTable.Columns.Item);
    }

    public static string GetColumnNames(string tablePrefix)
    {
      return TableBase<CustomerSession>.GetColumnNames(tablePrefix, CustomerSessionTable.Columns.Item);
    }

    public class Columns
    {
		
			public static readonly ColumnDescription CustomerSessionID = new ColumnDescription("CustomerSessionID", 0, typeof(int));
			public static readonly ColumnDescription CustomerSessionGuid = new ColumnDescription("CustomerSessionGuid", 1, typeof(Guid));
			public static readonly ColumnDescription CustomerID = new ColumnDescription("CustomerID", 2, typeof(int));
			public static readonly ColumnDescription IPAddress = new ColumnDescription("IPAddress", 3, typeof(string));
			public static readonly ColumnDescription UserAgent = new ColumnDescription("UserAgent", 4, typeof(string));
			public static readonly ColumnDescription ValidUntil = new ColumnDescription("ValidUntil", 5, typeof(DateTime));
			public static readonly ColumnDescription Updated = new ColumnDescription("Updated", 6, typeof(DateTime));
			public static readonly ColumnDescription Created = new ColumnDescription("Created", 7, typeof(DateTime));
			

			public static readonly ColumnDescription[] Item = new ColumnDescription[] 
			{
				CustomerSessionID,
				CustomerSessionGuid,
				CustomerID,
				IPAddress,
				UserAgent,
				ValidUntil,
				Updated,
				Created
			};
    }

    public override int ColumnCount
    {
      get { return Columns.Item.Length; }
    }

    public CustomerSessionTable(SqlQuery query) : base(query) { }
    public CustomerSessionTable(SqlQuery query, ColumnSelectMode selectMode,
                           params ColumnDescription[] columns)
      : base(query, selectMode, columns) { }

    public ColumnDescription this[int index] { get { return Columns.Item[index]; } }

		public int CustomerSessionID { get { return this.Reader.GetInt32(this.GetIndex(Columns.CustomerSessionID)); } }
		public Guid CustomerSessionGuid { get { return this.Reader.GetGuid(this.GetIndex(Columns.CustomerSessionGuid)); } }
		
		public int? CustomerID 
		{
			get
			{
				int index = this.GetIndex(Columns.CustomerID);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetInt32(index);
			}
		}

		public string IPAddress { get { return this.Reader.GetString(this.GetIndex(Columns.IPAddress)); } }
		public string UserAgent { get { return this.Reader.GetString(this.GetIndex(Columns.UserAgent)); } }
		public DateTime ValidUntil { get { return this.Reader.GetDateTime(this.GetIndex(Columns.ValidUntil)); } }
		public DateTime Updated { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Updated)); } }
		public DateTime Created { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Created)); } }
		

	  public CustomerSession CreateInstance()  
		{ 
			if (!this.HasData) return null;
			return new CustomerSession(this.CustomerSessionID,this.CustomerSessionGuid,(CustomerID.HasValue ? new Customer(this.CustomerID.Value) : null), this.IPAddress,this.UserAgent,this.ValidUntil,this.Updated,this.Created); 
		}
		public CustomerSession CreateInstance(Customer customer)  
		{ 
			if (!this.HasData) return null;
			return new CustomerSession(this.CustomerSessionID,this.CustomerSessionGuid,customer ?? (this.CustomerID.HasValue ? new Customer(this.CustomerID.Value) : null), this.IPAddress,this.UserAgent,this.ValidUntil,this.Updated,this.Created); 
		}
		

  }
}

