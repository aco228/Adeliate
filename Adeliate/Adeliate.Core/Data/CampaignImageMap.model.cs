using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;
using Adlite.Data;

 

namespace Adlite.Data 
{
  public partial interface ICampaignImageMapManager : IDataManager<CampaignImageMap> 
  {
	

  }

  public partial class CampaignImageMap : AdliteObject<ICampaignImageMapManager> 
  {
		private Campaign _campaign;
		private Image _image;
		private bool _isDefault = false;
		

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

		public Image Image 
		{
			get
			{
				if (this._image != null &&
						this._image.IsEmpty)
					if (this.ConnectionContext != null)
						this._image = Image.CreateManager().LazyLoad(this.ConnectionContext, this._image.ID) as Image;
					else
						this._image = Image.CreateManager().LazyLoad(this._image.ID) as Image;
				return this._image;
			}
			set { _image = value; }
		}

		public bool IsDefault {get {return this._isDefault; } set { this._isDefault = value;} }
		

    public override bool IsInsertable { get { return true;} }

    public override bool IsDeletable { get { return true;} }

    public override bool IsUpdatable { get { return true;} }

    public CampaignImageMap()
    { 
    }

    public CampaignImageMap(int id): base(id, DateTime.Now, DateTime.Now, true)
    {
    }

    public CampaignImageMap(int id,  Campaign campaign, Image image, bool isDefault, DateTime updated, DateTime created): base(id, updated, created, false)
    {
			this._campaign = campaign;
			this._image = image;
			this._isDefault = isDefault;
			
    }

    public override object Clone(bool deepClone)
    {
      return new CampaignImageMap(-1,  this.Campaign, this.Image,this.IsDefault, DateTime.Now, DateTime.Now);
    }
  }
}

