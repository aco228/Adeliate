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
  public class PostbackDataTable : TableBase<PostbackData>
  {
    public static string GetColumnNames()
    {
      return TableBase<PostbackData>.GetColumnNames(string.Empty, PostbackDataTable.Columns.Item);
    }

    public static string GetColumnNames(string tablePrefix)
    {
      return TableBase<PostbackData>.GetColumnNames(tablePrefix, PostbackDataTable.Columns.Item);
    }

    public class Columns
    {
		
			public static readonly ColumnDescription PostbackDataID = new ColumnDescription("PostbackDataID", 0, typeof(int));
			public static readonly ColumnDescription PostbackID = new ColumnDescription("PostbackID", 1, typeof(int));
			public static readonly ColumnDescription AffiliateID = new ColumnDescription("AffiliateID", 2, typeof(int));
			public static readonly ColumnDescription ClickID = new ColumnDescription("ClickID", 3, typeof(int));
			public static readonly ColumnDescription PostbackRequestID = new ColumnDescription("PostbackRequestID", 4, typeof(int));
			public static readonly ColumnDescription EntranceUrl = new ColumnDescription("EntranceUrl", 5, typeof(string));
			public static readonly ColumnDescription PostbackUrl = new ColumnDescription("PostbackUrl", 6, typeof(string));
			public static readonly ColumnDescription Updated = new ColumnDescription("Updated", 7, typeof(DateTime));
			public static readonly ColumnDescription Created = new ColumnDescription("Created", 8, typeof(DateTime));
			

			public static readonly ColumnDescription[] Item = new ColumnDescription[] 
			{
				PostbackDataID,
				PostbackID,
				AffiliateID,
				ClickID,
				PostbackRequestID,
				EntranceUrl,
				PostbackUrl,
				Updated,
				Created
			};
    }

    public override int ColumnCount
    {
      get { return Columns.Item.Length; }
    }

    public PostbackDataTable(SqlQuery query) : base(query) { }
    public PostbackDataTable(SqlQuery query, ColumnSelectMode selectMode,
                           params ColumnDescription[] columns)
      : base(query, selectMode, columns) { }

    public ColumnDescription this[int index] { get { return Columns.Item[index]; } }

		public int PostbackDataID { get { return this.Reader.GetInt32(this.GetIndex(Columns.PostbackDataID)); } }
		public int PostbackID { get { return this.Reader.GetInt32(this.GetIndex(Columns.PostbackID)); } }
		public int AffiliateID { get { return this.Reader.GetInt32(this.GetIndex(Columns.AffiliateID)); } }
		public int ClickID { get { return this.Reader.GetInt32(this.GetIndex(Columns.ClickID)); } }
		public int PostbackRequestID { get { return this.Reader.GetInt32(this.GetIndex(Columns.PostbackRequestID)); } }
		public string EntranceUrl { get { return this.Reader.GetString(this.GetIndex(Columns.EntranceUrl)); } }
		public string PostbackUrl { get { return this.Reader.GetString(this.GetIndex(Columns.PostbackUrl)); } }
		public DateTime Updated { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Updated)); } }
		public DateTime Created { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Created)); } }
		

	  public PostbackData CreateInstance()  
		{ 
			if (!this.HasData) return null;
			return new PostbackData(this.PostbackDataID,new Postback(this.PostbackID), new Affiliate(this.AffiliateID), new Click(this.ClickID), new PostbackRequest(this.PostbackRequestID), this.EntranceUrl,this.PostbackUrl,this.Updated,this.Created); 
		}
		public PostbackData CreateInstance(Affiliate affiliate)  
		{ 
			if (!this.HasData) return null;
			return new PostbackData(this.PostbackDataID,new Postback(this.PostbackID), affiliate ?? new Affiliate(this.AffiliateID), new Click(this.ClickID), new PostbackRequest(this.PostbackRequestID), this.EntranceUrl,this.PostbackUrl,this.Updated,this.Created); 
		}
		public PostbackData CreateInstance(Click click)  
		{ 
			if (!this.HasData) return null;
			return new PostbackData(this.PostbackDataID,new Postback(this.PostbackID), new Affiliate(this.AffiliateID), click ?? new Click(this.ClickID), new PostbackRequest(this.PostbackRequestID), this.EntranceUrl,this.PostbackUrl,this.Updated,this.Created); 
		}
		public PostbackData CreateInstance(Postback postback)  
		{ 
			if (!this.HasData) return null;
			return new PostbackData(this.PostbackDataID,postback ?? new Postback(this.PostbackID), new Affiliate(this.AffiliateID), new Click(this.ClickID), new PostbackRequest(this.PostbackRequestID), this.EntranceUrl,this.PostbackUrl,this.Updated,this.Created); 
		}
		public PostbackData CreateInstance(PostbackRequest postbackRequest)  
		{ 
			if (!this.HasData) return null;
			return new PostbackData(this.PostbackDataID,new Postback(this.PostbackID), new Affiliate(this.AffiliateID), new Click(this.ClickID), postbackRequest ?? new PostbackRequest(this.PostbackRequestID), this.EntranceUrl,this.PostbackUrl,this.Updated,this.Created); 
		}
		public PostbackData CreateInstance(Affiliate affiliate, Click click)  
		{ 
			if (!this.HasData) return null;
			return new PostbackData(this.PostbackDataID,new Postback(this.PostbackID), affiliate ?? new Affiliate(this.AffiliateID), click ?? new Click(this.ClickID), new PostbackRequest(this.PostbackRequestID), this.EntranceUrl,this.PostbackUrl,this.Updated,this.Created); 
		}
		public PostbackData CreateInstance(Postback postback, Affiliate affiliate)  
		{ 
			if (!this.HasData) return null;
			return new PostbackData(this.PostbackDataID,postback ?? new Postback(this.PostbackID), affiliate ?? new Affiliate(this.AffiliateID), new Click(this.ClickID), new PostbackRequest(this.PostbackRequestID), this.EntranceUrl,this.PostbackUrl,this.Updated,this.Created); 
		}
		public PostbackData CreateInstance(Affiliate affiliate, PostbackRequest postbackRequest)  
		{ 
			if (!this.HasData) return null;
			return new PostbackData(this.PostbackDataID,new Postback(this.PostbackID), affiliate ?? new Affiliate(this.AffiliateID), new Click(this.ClickID), postbackRequest ?? new PostbackRequest(this.PostbackRequestID), this.EntranceUrl,this.PostbackUrl,this.Updated,this.Created); 
		}
		public PostbackData CreateInstance(Postback postback, Click click)  
		{ 
			if (!this.HasData) return null;
			return new PostbackData(this.PostbackDataID,postback ?? new Postback(this.PostbackID), new Affiliate(this.AffiliateID), click ?? new Click(this.ClickID), new PostbackRequest(this.PostbackRequestID), this.EntranceUrl,this.PostbackUrl,this.Updated,this.Created); 
		}
		public PostbackData CreateInstance(Click click, PostbackRequest postbackRequest)  
		{ 
			if (!this.HasData) return null;
			return new PostbackData(this.PostbackDataID,new Postback(this.PostbackID), new Affiliate(this.AffiliateID), click ?? new Click(this.ClickID), postbackRequest ?? new PostbackRequest(this.PostbackRequestID), this.EntranceUrl,this.PostbackUrl,this.Updated,this.Created); 
		}
		public PostbackData CreateInstance(Postback postback, PostbackRequest postbackRequest)  
		{ 
			if (!this.HasData) return null;
			return new PostbackData(this.PostbackDataID,postback ?? new Postback(this.PostbackID), new Affiliate(this.AffiliateID), new Click(this.ClickID), postbackRequest ?? new PostbackRequest(this.PostbackRequestID), this.EntranceUrl,this.PostbackUrl,this.Updated,this.Created); 
		}
		public PostbackData CreateInstance(Postback postback, Affiliate affiliate, Click click)  
		{ 
			if (!this.HasData) return null;
			return new PostbackData(this.PostbackDataID,postback ?? new Postback(this.PostbackID), affiliate ?? new Affiliate(this.AffiliateID), click ?? new Click(this.ClickID), new PostbackRequest(this.PostbackRequestID), this.EntranceUrl,this.PostbackUrl,this.Updated,this.Created); 
		}
		public PostbackData CreateInstance(Affiliate affiliate, Click click, PostbackRequest postbackRequest)  
		{ 
			if (!this.HasData) return null;
			return new PostbackData(this.PostbackDataID,new Postback(this.PostbackID), affiliate ?? new Affiliate(this.AffiliateID), click ?? new Click(this.ClickID), postbackRequest ?? new PostbackRequest(this.PostbackRequestID), this.EntranceUrl,this.PostbackUrl,this.Updated,this.Created); 
		}
		public PostbackData CreateInstance(Postback postback, Affiliate affiliate, PostbackRequest postbackRequest)  
		{ 
			if (!this.HasData) return null;
			return new PostbackData(this.PostbackDataID,postback ?? new Postback(this.PostbackID), affiliate ?? new Affiliate(this.AffiliateID), new Click(this.ClickID), postbackRequest ?? new PostbackRequest(this.PostbackRequestID), this.EntranceUrl,this.PostbackUrl,this.Updated,this.Created); 
		}
		public PostbackData CreateInstance(Postback postback, Click click, PostbackRequest postbackRequest)  
		{ 
			if (!this.HasData) return null;
			return new PostbackData(this.PostbackDataID,postback ?? new Postback(this.PostbackID), new Affiliate(this.AffiliateID), click ?? new Click(this.ClickID), postbackRequest ?? new PostbackRequest(this.PostbackRequestID), this.EntranceUrl,this.PostbackUrl,this.Updated,this.Created); 
		}
		public PostbackData CreateInstance(Postback postback, Affiliate affiliate, Click click, PostbackRequest postbackRequest)  
		{ 
			if (!this.HasData) return null;
			return new PostbackData(this.PostbackDataID,postback ?? new Postback(this.PostbackID), affiliate ?? new Affiliate(this.AffiliateID), click ?? new Click(this.ClickID), postbackRequest ?? new PostbackRequest(this.PostbackRequestID), this.EntranceUrl,this.PostbackUrl,this.Updated,this.Created); 
		}
		

  }
}

