using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;
using Adlite.Data;

 

namespace Adlite.Data 
{
  public partial interface ICampaignFlowMapManager : IDataManager<CampaignFlowMap> 
  {
	

  }

  public partial class CampaignFlowMap : AdliteObject<ICampaignFlowMapManager> 
  {
		private Campaign _campaign;
		private CampaignFlow _campaignFlow;
		

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

		public CampaignFlow CampaignFlow  { get { return this._campaignFlow; } set { this._campaignFlow = value; } }
		

    public override bool IsInsertable { get { return true;} }

    public override bool IsDeletable { get { return true;} }

    public override bool IsUpdatable { get { return true;} }

    public CampaignFlowMap()
    { 
    }

    public CampaignFlowMap(int id): base(id, DateTime.Now, DateTime.Now, true)
    {
    }

    public CampaignFlowMap(int id,  Campaign campaign, CampaignFlow campaignFlow, DateTime updated, DateTime created): base(id, updated, created, false)
    {
			this._campaign = campaign;
			this._campaignFlow = campaignFlow;
			
    }

    public override object Clone(bool deepClone)
    {
      return new CampaignFlowMap(-1,  this.Campaign, this.CampaignFlow, DateTime.Now, DateTime.Now);
    }
  }
}

