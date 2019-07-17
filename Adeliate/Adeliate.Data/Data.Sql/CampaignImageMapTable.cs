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
  public class CampaignImageMapTable : TableBase<CampaignImageMap>
  {
    public static string GetColumnNames()
    {
      return TableBase<CampaignImageMap>.GetColumnNames(string.Empty, CampaignImageMapTable.Columns.Item);
    }

    public static string GetColumnNames(string tablePrefix)
    {
      return TableBase<CampaignImageMap>.GetColumnNames(tablePrefix, CampaignImageMapTable.Columns.Item);
    }

    public class Columns
    {
		
			public static readonly ColumnDescription CampaignImageMapID = new ColumnDescription("CampaignImageMapID", 0, typeof(int));
			public static readonly ColumnDescription CampaignID = new ColumnDescription("CampaignID", 1, typeof(int));
			public static readonly ColumnDescription ImageID = new ColumnDescription("ImageID", 2, typeof(int));
			public static readonly ColumnDescription IsDefault = new ColumnDescription("IsDefault", 3, typeof(bool));
			public static readonly ColumnDescription Updated = new ColumnDescription("Updated", 4, typeof(DateTime));
			public static readonly ColumnDescription Created = new ColumnDescription("Created", 5, typeof(DateTime));
			

			public static readonly ColumnDescription[] Item = new ColumnDescription[] 
			{
				CampaignImageMapID,
				CampaignID,
				ImageID,
				IsDefault,
				Updated,
				Created
			};
    }

    public override int ColumnCount
    {
      get { return Columns.Item.Length; }
    }

    public CampaignImageMapTable(SqlQuery query) : base(query) { }
    public CampaignImageMapTable(SqlQuery query, ColumnSelectMode selectMode,
                           params ColumnDescription[] columns)
      : base(query, selectMode, columns) { }

    public ColumnDescription this[int index] { get { return Columns.Item[index]; } }

		public int CampaignImageMapID { get { return this.Reader.GetInt32(this.GetIndex(Columns.CampaignImageMapID)); } }
		public int CampaignID { get { return this.Reader.GetInt32(this.GetIndex(Columns.CampaignID)); } }
		public int ImageID { get { return this.Reader.GetInt32(this.GetIndex(Columns.ImageID)); } }
		public bool IsDefault { get { return this.Reader.GetBoolean(this.GetIndex(Columns.IsDefault)); } }
		public DateTime Updated { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Updated)); } }
		public DateTime Created { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Created)); } }
		

	  public CampaignImageMap CreateInstance()  
		{ 
			if (!this.HasData) return null;
			return new CampaignImageMap(this.CampaignImageMapID,new Campaign(this.CampaignID), new Image(this.ImageID), this.IsDefault,this.Updated,this.Created); 
		}
		public CampaignImageMap CreateInstance(Campaign campaign)  
		{ 
			if (!this.HasData) return null;
			return new CampaignImageMap(this.CampaignImageMapID,campaign ?? new Campaign(this.CampaignID), new Image(this.ImageID), this.IsDefault,this.Updated,this.Created); 
		}
		public CampaignImageMap CreateInstance(Image image)  
		{ 
			if (!this.HasData) return null;
			return new CampaignImageMap(this.CampaignImageMapID,new Campaign(this.CampaignID), image ?? new Image(this.ImageID), this.IsDefault,this.Updated,this.Created); 
		}
		public CampaignImageMap CreateInstance(Campaign campaign, Image image)  
		{ 
			if (!this.HasData) return null;
			return new CampaignImageMap(this.CampaignImageMapID,campaign ?? new Campaign(this.CampaignID), image ?? new Image(this.ImageID), this.IsDefault,this.Updated,this.Created); 
		}
		

  }
}

