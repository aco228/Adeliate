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
  public class AffiliateTable : TableBase<Affiliate>
  {
    public static string GetColumnNames()
    {
      return TableBase<Affiliate>.GetColumnNames(string.Empty, AffiliateTable.Columns.Item);
    }

    public static string GetColumnNames(string tablePrefix)
    {
      return TableBase<Affiliate>.GetColumnNames(tablePrefix, AffiliateTable.Columns.Item);
    }

    public class Columns
    {
		
			public static readonly ColumnDescription AffiliateID = new ColumnDescription("AffiliateID", 0, typeof(int));
			public static readonly ColumnDescription AffiliateTypeID = new ColumnDescription("AffiliateTypeID", 1, typeof(int));
			public static readonly ColumnDescription Name = new ColumnDescription("Name", 2, typeof(string));
			public static readonly ColumnDescription Description = new ColumnDescription("Description", 3, typeof(string));
			public static readonly ColumnDescription SubidName = new ColumnDescription("SubidName", 4, typeof(string));
			public static readonly ColumnDescription Contact = new ColumnDescription("Contact", 5, typeof(string));
			public static readonly ColumnDescription WebsiteUrl = new ColumnDescription("WebsiteUrl", 6, typeof(string));
			public static readonly ColumnDescription IsActive = new ColumnDescription("IsActive", 7, typeof(bool));
			public static readonly ColumnDescription Updated = new ColumnDescription("Updated", 8, typeof(DateTime));
			public static readonly ColumnDescription Created = new ColumnDescription("Created", 9, typeof(DateTime));
			

			public static readonly ColumnDescription[] Item = new ColumnDescription[] 
			{
				AffiliateID,
				AffiliateTypeID,
				Name,
				Description,
				SubidName,
				Contact,
				WebsiteUrl,
				IsActive,
				Updated,
				Created
			};
    }

    public override int ColumnCount
    {
      get { return Columns.Item.Length; }
    }

    public AffiliateTable(SqlQuery query) : base(query) { }
    public AffiliateTable(SqlQuery query, ColumnSelectMode selectMode,
                           params ColumnDescription[] columns)
      : base(query, selectMode, columns) { }

    public ColumnDescription this[int index] { get { return Columns.Item[index]; } }

		public int AffiliateID { get { return this.Reader.GetInt32(this.GetIndex(Columns.AffiliateID)); } }
		public int AffiliateTypeID { get { return this.Reader.GetInt32(this.GetIndex(Columns.AffiliateTypeID)); } }
		public string Name { get { return this.Reader.GetString(this.GetIndex(Columns.Name)); } }
		
		public string Description 
		{
			get
			{
				int index = this.GetIndex(Columns.Description);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetString(index);
			}
		}

		
		public string SubidName 
		{
			get
			{
				int index = this.GetIndex(Columns.SubidName);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetString(index);
			}
		}

		public string Contact { get { return this.Reader.GetString(this.GetIndex(Columns.Contact)); } }
		public string WebsiteUrl { get { return this.Reader.GetString(this.GetIndex(Columns.WebsiteUrl)); } }
		public bool IsActive { get { return this.Reader.GetBoolean(this.GetIndex(Columns.IsActive)); } }
		public DateTime Updated { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Updated)); } }
		public DateTime Created { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Created)); } }
		

	  public Affiliate CreateInstance()  
		{ 
			if (!this.HasData) return null;
			return new Affiliate(this.AffiliateID,new AffiliateType(this.AffiliateTypeID), this.Name,this.Description,this.SubidName,this.Contact,this.WebsiteUrl,this.IsActive,this.Updated,this.Created); 
		}
		public Affiliate CreateInstance(AffiliateType affiliateType)  
		{ 
			if (!this.HasData) return null;
			return new Affiliate(this.AffiliateID,affiliateType ?? new AffiliateType(this.AffiliateTypeID), this.Name,this.Description,this.SubidName,this.Contact,this.WebsiteUrl,this.IsActive,this.Updated,this.Created); 
		}
		

  }
}

