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
  public class CampaignDeviceMapTable : TableBase<CampaignDeviceMap>
  {
    public static string GetColumnNames()
    {
      return TableBase<CampaignDeviceMap>.GetColumnNames(string.Empty, CampaignDeviceMapTable.Columns.Item);
    }

    public static string GetColumnNames(string tablePrefix)
    {
      return TableBase<CampaignDeviceMap>.GetColumnNames(tablePrefix, CampaignDeviceMapTable.Columns.Item);
    }

    public class Columns
    {
		
			public static readonly ColumnDescription CampaignDeviceMapID = new ColumnDescription("CampaignDeviceMapID", 0, typeof(int));
			public static readonly ColumnDescription CampaignID = new ColumnDescription("CampaignID", 1, typeof(int));
			public static readonly ColumnDescription CampaignDeviceID = new ColumnDescription("CampaignDeviceID", 2, typeof(int));
			public static readonly ColumnDescription Updated = new ColumnDescription("Updated", 3, typeof(DateTime));
			public static readonly ColumnDescription Created = new ColumnDescription("Created", 4, typeof(DateTime));
			

			public static readonly ColumnDescription[] Item = new ColumnDescription[] 
			{
				CampaignDeviceMapID,
				CampaignID,
				CampaignDeviceID,
				Updated,
				Created
			};
    }

    public override int ColumnCount
    {
      get { return Columns.Item.Length; }
    }

    public CampaignDeviceMapTable(SqlQuery query) : base(query) { }
    public CampaignDeviceMapTable(SqlQuery query, ColumnSelectMode selectMode,
                           params ColumnDescription[] columns)
      : base(query, selectMode, columns) { }

    public ColumnDescription this[int index] { get { return Columns.Item[index]; } }

		public int CampaignDeviceMapID { get { return this.Reader.GetInt32(this.GetIndex(Columns.CampaignDeviceMapID)); } }
		public int CampaignID { get { return this.Reader.GetInt32(this.GetIndex(Columns.CampaignID)); } }
		public int CampaignDeviceID { get { return this.Reader.GetInt32(this.GetIndex(Columns.CampaignDeviceID)); } }
		public DateTime Updated { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Updated)); } }
		public DateTime Created { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Created)); } }
		

	  public CampaignDeviceMap CreateInstance()  
		{ 
			if (!this.HasData) return null;
			return new CampaignDeviceMap(this.CampaignDeviceMapID,new Campaign(this.CampaignID), (CampaignDevice)this.CampaignDeviceID,this.Updated,this.Created); 
		}
		public CampaignDeviceMap CreateInstance(Campaign campaign)  
		{ 
			if (!this.HasData) return null;
			return new CampaignDeviceMap(this.CampaignDeviceMapID,campaign ?? new Campaign(this.CampaignID), (CampaignDevice)this.CampaignDeviceID,this.Updated,this.Created); 
		}
		

  }
}

