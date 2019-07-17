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
  public class PostbackTable : TableBase<Postback>
  {
    public static string GetColumnNames()
    {
      return TableBase<Postback>.GetColumnNames(string.Empty, PostbackTable.Columns.Item);
    }

    public static string GetColumnNames(string tablePrefix)
    {
      return TableBase<Postback>.GetColumnNames(tablePrefix, PostbackTable.Columns.Item);
    }

    public class Columns
    {
		
			public static readonly ColumnDescription PostbackID = new ColumnDescription("PostbackID", 0, typeof(int));
			public static readonly ColumnDescription PostbackTypeID = new ColumnDescription("PostbackTypeID", 1, typeof(int));
			public static readonly ColumnDescription AffiliateID = new ColumnDescription("AffiliateID", 2, typeof(int));
			public static readonly ColumnDescription Url = new ColumnDescription("Url", 3, typeof(string));
			public static readonly ColumnDescription IsActive = new ColumnDescription("IsActive", 4, typeof(bool));
			public static readonly ColumnDescription Updated = new ColumnDescription("Updated", 5, typeof(DateTime));
			public static readonly ColumnDescription Created = new ColumnDescription("Created", 6, typeof(DateTime));
			

			public static readonly ColumnDescription[] Item = new ColumnDescription[] 
			{
				PostbackID,
				PostbackTypeID,
				AffiliateID,
				Url,
				IsActive,
				Updated,
				Created
			};
    }

    public override int ColumnCount
    {
      get { return Columns.Item.Length; }
    }

    public PostbackTable(SqlQuery query) : base(query) { }
    public PostbackTable(SqlQuery query, ColumnSelectMode selectMode,
                           params ColumnDescription[] columns)
      : base(query, selectMode, columns) { }

    public ColumnDescription this[int index] { get { return Columns.Item[index]; } }

		public int PostbackID { get { return this.Reader.GetInt32(this.GetIndex(Columns.PostbackID)); } }
		public int PostbackTypeID { get { return this.Reader.GetInt32(this.GetIndex(Columns.PostbackTypeID)); } }
		public int AffiliateID { get { return this.Reader.GetInt32(this.GetIndex(Columns.AffiliateID)); } }
		public string Url { get { return this.Reader.GetString(this.GetIndex(Columns.Url)); } }
		public bool IsActive { get { return this.Reader.GetBoolean(this.GetIndex(Columns.IsActive)); } }
		public DateTime Updated { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Updated)); } }
		public DateTime Created { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Created)); } }
		

	  public Postback CreateInstance()  
		{ 
			if (!this.HasData) return null;
			return new Postback(this.PostbackID,new PostbackType(this.PostbackTypeID), new Affiliate(this.AffiliateID), this.Url,this.IsActive,this.Updated,this.Created); 
		}
		public Postback CreateInstance(Affiliate affiliate)  
		{ 
			if (!this.HasData) return null;
			return new Postback(this.PostbackID,new PostbackType(this.PostbackTypeID), affiliate ?? new Affiliate(this.AffiliateID), this.Url,this.IsActive,this.Updated,this.Created); 
		}
		public Postback CreateInstance(PostbackType postbackType)  
		{ 
			if (!this.HasData) return null;
			return new Postback(this.PostbackID,postbackType ?? new PostbackType(this.PostbackTypeID), new Affiliate(this.AffiliateID), this.Url,this.IsActive,this.Updated,this.Created); 
		}
		public Postback CreateInstance(PostbackType postbackType, Affiliate affiliate)  
		{ 
			if (!this.HasData) return null;
			return new Postback(this.PostbackID,postbackType ?? new PostbackType(this.PostbackTypeID), affiliate ?? new Affiliate(this.AffiliateID), this.Url,this.IsActive,this.Updated,this.Created); 
		}
		

  }
}

