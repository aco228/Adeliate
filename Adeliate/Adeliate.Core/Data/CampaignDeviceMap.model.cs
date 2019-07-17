using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;
using Adlite.Data;

 

namespace Adlite.Data 
{
  public partial interface ICampaignDeviceMapManager : IDataManager<CampaignDeviceMap> 
  {
	

  }

  public partial class CampaignDeviceMap : AdliteObject<ICampaignDeviceMapManager> 
  {
		private Campaign _campaign;
		private CampaignDevice _campaignDevice;
		

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

		public CampaignDevice CampaignDevice  { get { return this._campaignDevice; } set { this._campaignDevice = value; } }
		

    public override bool IsInsertable { get { return true;} }

    public override bool IsDeletable { get { return true;} }

    public override bool IsUpdatable { get { return true;} }

    public CampaignDeviceMap()
    { 
    }

    public CampaignDeviceMap(int id): base(id, DateTime.Now, DateTime.Now, true)
    {
    }

    public CampaignDeviceMap(int id,  Campaign campaign, CampaignDevice campaignDevice, DateTime updated, DateTime created): base(id, updated, created, false)
    {
			this._campaign = campaign;
			this._campaignDevice = campaignDevice;
			
    }

    public override object Clone(bool deepClone)
    {
      return new CampaignDeviceMap(-1,  this.Campaign, this.CampaignDevice, DateTime.Now, DateTime.Now);
    }
  }
}

