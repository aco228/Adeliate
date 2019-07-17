using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;
using Adlite.Data;

 

namespace Adlite.Data 
{
  public partial interface IAffiliateNotificationManager : IDataManager<AffiliateNotification> 
  {
	

  }

  public partial class AffiliateNotification : AdliteObject<IAffiliateNotificationManager> 
  {
		private Affiliate _affiliate;
		private Campaign _campaign;
		private string _title = String.Empty;
		private string _text = String.Empty;
		

		public Affiliate Affiliate 
		{
			get
			{
				if (this._affiliate != null &&
						this._affiliate.IsEmpty)
					if (this.ConnectionContext != null)
						this._affiliate = Affiliate.CreateManager().LazyLoad(this.ConnectionContext, this._affiliate.ID) as Affiliate;
					else
						this._affiliate = Affiliate.CreateManager().LazyLoad(this._affiliate.ID) as Affiliate;
				return this._affiliate;
			}
			set { _affiliate = value; }
		}

		public Campaign Campaign 
		{
			get
			{
				if (this._campaign != null &&
						this._campaign.IsEmpty)
					if (this.ConnectionContext != null)
						this._campaign = Campaign.CreateManager().LazyLoad(this.ConnectionContext, this._campaign.ID) as Campaign;
					else
						this._campaign = Campaign.CreateManager().LazyLoad(this._campaign.ID) as Campaign;
				return this._campaign;
			}
			set { _campaign = value; }
		}

		public string Title{ get {return this._title; } set { this._title = value;} }
		public string Text{ get {return this._text; } set { this._text = value;} }
		

    public override bool IsInsertable { get { return true;} }

    public override bool IsDeletable { get { return false;} }

    public override bool IsUpdatable { get { return true;} }

    public AffiliateNotification()
    { 
    }

    public AffiliateNotification(int id): base(id, DateTime.Now, DateTime.Now, true)
    {
    }

    public AffiliateNotification(int id,  Affiliate affiliate, Campaign campaign, string title, string text, DateTime updated, DateTime created): base(id, updated, created, false)
    {
			this._affiliate = affiliate;
			this._campaign = campaign;
			this._title = title;
			this._text = text;
			
    }

    public override object Clone(bool deepClone)
    {
      return new AffiliateNotification(-1,  this.Affiliate, this.Campaign,this.Title,this.Text, DateTime.Now, DateTime.Now);
    }
  }
}

