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
  public class CampaignFlowMapTable : TableBase<CampaignFlowMap>
  {
    public static string GetColumnNames()
    {
      return TableBase<CampaignFlowMap>.GetColumnNames(string.Empty, CampaignFlowMapTable.Columns.Item);
    }

    public static string GetColumnNames(string tablePrefix)
    {
      return TableBase<CampaignFlowMap>.GetColumnNames(tablePrefix, CampaignFlowMapTable.Columns.Item);
    }

    public class Columns
    {
		
			public static readonly ColumnDescription CampaignFlowMapID = new ColumnDescription("CampaignFlowMapID", 0, typeof(int));
			public static readonly ColumnDescription CampaignID = new ColumnDescription("CampaignID", 1, typeof(int));
			public static readonly ColumnDescription CampaignFlowID = new ColumnDescription("CampaignFlowID", 2, typeof(int));
			public static readonly ColumnDescription Updated = new ColumnDescription("Updated", 3, typeof(DateTime));
			public static readonly ColumnDescription Created = new ColumnDescription("Created", 4, typeof(DateTime));
			

			public static readonly ColumnDescription[] Item = new ColumnDescription[] 
			{
				CampaignFlowMapID,
				CampaignID,
				CampaignFlowID,
				Updated,
				Created
			};
    }

    public override int ColumnCount
    {
      get { return Columns.Item.Length; }
    }

    public CampaignFlowMapTable(SqlQuery query) : base(query) { }
    public CampaignFlowMapTable(SqlQuery query, ColumnSelectMode selectMode,
                           params ColumnDescription[] columns)
      : base(query, selectMode, columns) { }

    public ColumnDescription this[int index] { get { return Columns.Item[index]; } }

		public int CampaignFlowMapID { get { return this.Reader.GetInt32(this.GetIndex(Columns.CampaignFlowMapID)); } }
		public int CampaignID { get { return this.Reader.GetInt32(this.GetIndex(Columns.CampaignID)); } }
		public int CampaignFlowID { get { return this.Reader.GetInt32(this.GetIndex(Columns.CampaignFlowID)); } }
		public DateTime Updated { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Updated)); } }
		public DateTime Created { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Created)); } }
		

	  public CampaignFlowMap CreateInstance()  
		{ 
			if (!this.HasData) return null;
			return new CampaignFlowMap(this.CampaignFlowMapID,new Campaign(this.CampaignID), (CampaignFlow)this.CampaignFlowID,this.Updated,this.Created); 
		}
		public CampaignFlowMap CreateInstance(Campaign campaign)  
		{ 
			if (!this.HasData) return null;
			return new CampaignFlowMap(this.CampaignFlowMapID,campaign ?? new Campaign(this.CampaignID), (CampaignFlow)this.CampaignFlowID,this.Updated,this.Created); 
		}
		

  }
}

