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
  public class CampaignGroupTable : TableBase<CampaignGroup>
  {
    public static string GetColumnNames()
    {
      return TableBase<CampaignGroup>.GetColumnNames(string.Empty, CampaignGroupTable.Columns.Item);
    }

    public static string GetColumnNames(string tablePrefix)
    {
      return TableBase<CampaignGroup>.GetColumnNames(tablePrefix, CampaignGroupTable.Columns.Item);
    }

    public class Columns
    {
		
			public static readonly ColumnDescription CampaignGroupID = new ColumnDescription("CampaignGroupID", 0, typeof(int));
			public static readonly ColumnDescription FallbackGroupID = new ColumnDescription("FallbackGroupID", 1, typeof(int));
			public static readonly ColumnDescription Key = new ColumnDescription("Key", 2, typeof(string));
			public static readonly ColumnDescription Name = new ColumnDescription("Name", 3, typeof(string));
			public static readonly ColumnDescription Description = new ColumnDescription("Description", 4, typeof(string));
			public static readonly ColumnDescription Updated = new ColumnDescription("Updated", 5, typeof(DateTime));
			public static readonly ColumnDescription Created = new ColumnDescription("Created", 6, typeof(DateTime));
			

			public static readonly ColumnDescription[] Item = new ColumnDescription[] 
			{
				CampaignGroupID,
				FallbackGroupID,
				Key,
				Name,
				Description,
				Updated,
				Created
			};
    }

    public override int ColumnCount
    {
      get { return Columns.Item.Length; }
    }

    public CampaignGroupTable(SqlQuery query) : base(query) { }
    public CampaignGroupTable(SqlQuery query, ColumnSelectMode selectMode,
                           params ColumnDescription[] columns)
      : base(query, selectMode, columns) { }

    public ColumnDescription this[int index] { get { return Columns.Item[index]; } }

		public int CampaignGroupID { get { return this.Reader.GetInt32(this.GetIndex(Columns.CampaignGroupID)); } }
		
		public int? FallbackGroupID 
		{
			get
			{
				int index = this.GetIndex(Columns.FallbackGroupID);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetInt32(index);
			}
		}

		
		public string Key 
		{
			get
			{
				int index = this.GetIndex(Columns.Key);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetString(index);
			}
		}

		public string Name { get { return this.Reader.GetString(this.GetIndex(Columns.Name)); } }
		public string Description { get { return this.Reader.GetString(this.GetIndex(Columns.Description)); } }
		public DateTime Updated { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Updated)); } }
		public DateTime Created { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Created)); } }
		

	  public CampaignGroup CreateInstance()  
		{ 
			if (!this.HasData) return null;
			return new CampaignGroup(this.CampaignGroupID,(FallbackGroupID.HasValue ? new CampaignGroup(this.FallbackGroupID.Value) : null), this.Key,this.Name,this.Description,this.Updated,this.Created); 
		}
		public CampaignGroup CreateInstance(CampaignGroup fallbackGroup)  
		{ 
			if (!this.HasData) return null;
			return new CampaignGroup(this.CampaignGroupID,fallbackGroup ?? (this.FallbackGroupID.HasValue ? new CampaignGroup(this.FallbackGroupID.Value) : null), this.Key,this.Name,this.Description,this.Updated,this.Created); 
		}
		

  }
}

