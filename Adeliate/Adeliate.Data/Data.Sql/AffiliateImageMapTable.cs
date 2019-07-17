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
  public class AffiliateImageMapTable : TableBase<AffiliateImageMap>
  {
    public static string GetColumnNames()
    {
      return TableBase<AffiliateImageMap>.GetColumnNames(string.Empty, AffiliateImageMapTable.Columns.Item);
    }

    public static string GetColumnNames(string tablePrefix)
    {
      return TableBase<AffiliateImageMap>.GetColumnNames(tablePrefix, AffiliateImageMapTable.Columns.Item);
    }

    public class Columns
    {
		
			public static readonly ColumnDescription AffiliateImageMapID = new ColumnDescription("AffiliateImageMapID", 0, typeof(int));
			public static readonly ColumnDescription AffiliateID = new ColumnDescription("AffiliateID", 1, typeof(int));
			public static readonly ColumnDescription ImageID = new ColumnDescription("ImageID", 2, typeof(int));
			public static readonly ColumnDescription Updated = new ColumnDescription("Updated", 3, typeof(DateTime));
			public static readonly ColumnDescription Created = new ColumnDescription("Created", 4, typeof(DateTime));
			

			public static readonly ColumnDescription[] Item = new ColumnDescription[] 
			{
				AffiliateImageMapID,
				AffiliateID,
				ImageID,
				Updated,
				Created
			};
    }

    public override int ColumnCount
    {
      get { return Columns.Item.Length; }
    }

    public AffiliateImageMapTable(SqlQuery query) : base(query) { }
    public AffiliateImageMapTable(SqlQuery query, ColumnSelectMode selectMode,
                           params ColumnDescription[] columns)
      : base(query, selectMode, columns) { }

    public ColumnDescription this[int index] { get { return Columns.Item[index]; } }

		public int AffiliateImageMapID { get { return this.Reader.GetInt32(this.GetIndex(Columns.AffiliateImageMapID)); } }
		public int AffiliateID { get { return this.Reader.GetInt32(this.GetIndex(Columns.AffiliateID)); } }
		public int ImageID { get { return this.Reader.GetInt32(this.GetIndex(Columns.ImageID)); } }
		public DateTime Updated { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Updated)); } }
		public DateTime Created { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Created)); } }
		

	  public AffiliateImageMap CreateInstance()  
		{ 
			if (!this.HasData) return null;
			return new AffiliateImageMap(this.AffiliateImageMapID,new Affiliate(this.AffiliateID), new Image(this.ImageID), this.Updated,this.Created); 
		}
		public AffiliateImageMap CreateInstance(Affiliate affiliate)  
		{ 
			if (!this.HasData) return null;
			return new AffiliateImageMap(this.AffiliateImageMapID,affiliate ?? new Affiliate(this.AffiliateID), new Image(this.ImageID), this.Updated,this.Created); 
		}
		public AffiliateImageMap CreateInstance(Image image)  
		{ 
			if (!this.HasData) return null;
			return new AffiliateImageMap(this.AffiliateImageMapID,new Affiliate(this.AffiliateID), image ?? new Image(this.ImageID), this.Updated,this.Created); 
		}
		public AffiliateImageMap CreateInstance(Affiliate affiliate, Image image)  
		{ 
			if (!this.HasData) return null;
			return new AffiliateImageMap(this.AffiliateImageMapID,affiliate ?? new Affiliate(this.AffiliateID), image ?? new Image(this.ImageID), this.Updated,this.Created); 
		}
		

  }
}

