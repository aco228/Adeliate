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
  public class CampaignMobileOperatorMapTable : TableBase<CampaignMobileOperatorMap>
  {
    public static string GetColumnNames()
    {
      return TableBase<CampaignMobileOperatorMap>.GetColumnNames(string.Empty, CampaignMobileOperatorMapTable.Columns.Item);
    }

    public static string GetColumnNames(string tablePrefix)
    {
      return TableBase<CampaignMobileOperatorMap>.GetColumnNames(tablePrefix, CampaignMobileOperatorMapTable.Columns.Item);
    }

    public class Columns
    {
		
			public static readonly ColumnDescription CampaignMobileOperatorMapID = new ColumnDescription("CampaignMobileOperatorMapID", 0, typeof(int));
			public static readonly ColumnDescription CampaignID = new ColumnDescription("CampaignID", 1, typeof(int));
			public static readonly ColumnDescription MobileOperatorID = new ColumnDescription("MobileOperatorID", 2, typeof(int));
			public static readonly ColumnDescription Updated = new ColumnDescription("Updated", 3, typeof(DateTime));
			public static readonly ColumnDescription Created = new ColumnDescription("Created", 4, typeof(DateTime));
			

			public static readonly ColumnDescription[] Item = new ColumnDescription[] 
			{
				CampaignMobileOperatorMapID,
				CampaignID,
				MobileOperatorID,
				Updated,
				Created
			};
    }

    public override int ColumnCount
    {
      get { return Columns.Item.Length; }
    }

    public CampaignMobileOperatorMapTable(SqlQuery query) : base(query) { }
    public CampaignMobileOperatorMapTable(SqlQuery query, ColumnSelectMode selectMode,
                           params ColumnDescription[] columns)
      : base(query, selectMode, columns) { }

    public ColumnDescription this[int index] { get { return Columns.Item[index]; } }

		public int CampaignMobileOperatorMapID { get { return this.Reader.GetInt32(this.GetIndex(Columns.CampaignMobileOperatorMapID)); } }
		public int CampaignID { get { return this.Reader.GetInt32(this.GetIndex(Columns.CampaignID)); } }
		public int MobileOperatorID { get { return this.Reader.GetInt32(this.GetIndex(Columns.MobileOperatorID)); } }
		public DateTime Updated { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Updated)); } }
		public DateTime Created { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Created)); } }
		

	  public CampaignMobileOperatorMap CreateInstance()  
		{ 
			if (!this.HasData) return null;
			return new CampaignMobileOperatorMap(this.CampaignMobileOperatorMapID,new Campaign(this.CampaignID), new MobileOperator(this.MobileOperatorID), this.Updated,this.Created); 
		}
		public CampaignMobileOperatorMap CreateInstance(Campaign campaign)  
		{ 
			if (!this.HasData) return null;
			return new CampaignMobileOperatorMap(this.CampaignMobileOperatorMapID,campaign ?? new Campaign(this.CampaignID), new MobileOperator(this.MobileOperatorID), this.Updated,this.Created); 
		}
		public CampaignMobileOperatorMap CreateInstance(MobileOperator mobileOperator)  
		{ 
			if (!this.HasData) return null;
			return new CampaignMobileOperatorMap(this.CampaignMobileOperatorMapID,new Campaign(this.CampaignID), mobileOperator ?? new MobileOperator(this.MobileOperatorID), this.Updated,this.Created); 
		}
		public CampaignMobileOperatorMap CreateInstance(Campaign campaign, MobileOperator mobileOperator)  
		{ 
			if (!this.HasData) return null;
			return new CampaignMobileOperatorMap(this.CampaignMobileOperatorMapID,campaign ?? new Campaign(this.CampaignID), mobileOperator ?? new MobileOperator(this.MobileOperatorID), this.Updated,this.Created); 
		}
		

  }
}

