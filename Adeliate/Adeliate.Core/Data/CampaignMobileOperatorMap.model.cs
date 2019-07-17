using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;
using Adlite.Data;

 

namespace Adlite.Data 
{
  public partial interface ICampaignMobileOperatorMapManager : IDataManager<CampaignMobileOperatorMap> 
  {
	

  }

  public partial class CampaignMobileOperatorMap : AdliteObject<ICampaignMobileOperatorMapManager> 
  {
		private Campaign _campaign;
		private MobileOperator _mobileOperator;
		

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

		public MobileOperator MobileOperator 
		{
			get
			{
				if (this._mobileOperator != null &&
						this._mobileOperator.IsEmpty)
					if (this.ConnectionContext != null)
						this._mobileOperator = MobileOperator.CreateManager().LazyLoad(this.ConnectionContext, this._mobileOperator.ID) as MobileOperator;
					else
						this._mobileOperator = MobileOperator.CreateManager().LazyLoad(this._mobileOperator.ID) as MobileOperator;
				return this._mobileOperator;
			}
			set { _mobileOperator = value; }
		}

		

    public override bool IsInsertable { get { return true;} }

    public override bool IsDeletable { get { return true;} }

    public override bool IsUpdatable { get { return true;} }

    public CampaignMobileOperatorMap()
    { 
    }

    public CampaignMobileOperatorMap(int id): base(id, DateTime.Now, DateTime.Now, true)
    {
    }

    public CampaignMobileOperatorMap(int id,  Campaign campaign, MobileOperator mobileOperator, DateTime updated, DateTime created): base(id, updated, created, false)
    {
			this._campaign = campaign;
			this._mobileOperator = mobileOperator;
			
    }

    public override object Clone(bool deepClone)
    {
      return new CampaignMobileOperatorMap(-1,  this.Campaign, this.MobileOperator, DateTime.Now, DateTime.Now);
    }
  }
}

