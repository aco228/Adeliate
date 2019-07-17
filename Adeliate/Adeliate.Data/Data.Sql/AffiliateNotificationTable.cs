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
  public class AffiliateNotificationTable : TableBase<AffiliateNotification>
  {
    public static string GetColumnNames()
    {
      return TableBase<AffiliateNotification>.GetColumnNames(string.Empty, AffiliateNotificationTable.Columns.Item);
    }

    public static string GetColumnNames(string tablePrefix)
    {
      return TableBase<AffiliateNotification>.GetColumnNames(tablePrefix, AffiliateNotificationTable.Columns.Item);
    }

    public class Columns
    {
		
			public static readonly ColumnDescription AffiliateNotificationID = new ColumnDescription("AffiliateNotificationID", 0, typeof(int));
			public static readonly ColumnDescription AffiliateID = new ColumnDescription("AffiliateID", 1, typeof(int));
			public static readonly ColumnDescription CampaignID = new ColumnDescription("CampaignID", 2, typeof(int));
			public static readonly ColumnDescription Title = new ColumnDescription("Title", 3, typeof(string));
			public static readonly ColumnDescription Text = new ColumnDescription("Text", 4, typeof(string));
			public static readonly ColumnDescription Updated = new ColumnDescription("Updated", 5, typeof(DateTime));
			public static readonly ColumnDescription Created = new ColumnDescription("Created", 6, typeof(DateTime));
			

			public static readonly ColumnDescription[] Item = new ColumnDescription[] 
			{
				AffiliateNotificationID,
				AffiliateID,
				CampaignID,
				Title,
				Text,
				Updated,
				Created
			};
    }

    public override int ColumnCount
    {
      get { return Columns.Item.Length; }
    }

    public AffiliateNotificationTable(SqlQuery query) : base(query) { }
    public AffiliateNotificationTable(SqlQuery query, ColumnSelectMode selectMode,
                           params ColumnDescription[] columns)
      : base(query, selectMode, columns) { }

    public ColumnDescription this[int index] { get { return Columns.Item[index]; } }

		public int AffiliateNotificationID { get { return this.Reader.GetInt32(this.GetIndex(Columns.AffiliateNotificationID)); } }
		public int AffiliateID { get { return this.Reader.GetInt32(this.GetIndex(Columns.AffiliateID)); } }
		
		public int? CampaignID 
		{
			get
			{
				int index = this.GetIndex(Columns.CampaignID);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetInt32(index);
			}
		}

		public string Title { get { return this.Reader.GetString(this.GetIndex(Columns.Title)); } }
		public string Text { get { return this.Reader.GetString(this.GetIndex(Columns.Text)); } }
		public DateTime Updated { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Updated)); } }
		public DateTime Created { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Created)); } }
		

	  public AffiliateNotification CreateInstance()  
		{ 
			if (!this.HasData) return null;
			return new AffiliateNotification(this.AffiliateNotificationID,new Affiliate(this.AffiliateID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), this.Title,this.Text,this.Updated,this.Created); 
		}
		public AffiliateNotification CreateInstance(Affiliate affiliate)  
		{ 
			if (!this.HasData) return null;
			return new AffiliateNotification(this.AffiliateNotificationID,affiliate ?? new Affiliate(this.AffiliateID), (CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), this.Title,this.Text,this.Updated,this.Created); 
		}
		public AffiliateNotification CreateInstance(Campaign campaign)  
		{ 
			if (!this.HasData) return null;
			return new AffiliateNotification(this.AffiliateNotificationID,new Affiliate(this.AffiliateID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), this.Title,this.Text,this.Updated,this.Created); 
		}
		public AffiliateNotification CreateInstance(Affiliate affiliate, Campaign campaign)  
		{ 
			if (!this.HasData) return null;
			return new AffiliateNotification(this.AffiliateNotificationID,affiliate ?? new Affiliate(this.AffiliateID), campaign ?? (this.CampaignID.HasValue ? new Campaign(this.CampaignID.Value) : null), this.Title,this.Text,this.Updated,this.Created); 
		}
		

  }
}

