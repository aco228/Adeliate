using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;
using Adlite.Data;

 

namespace Adlite.Data 
{
  public partial interface ICampaignGroupManager : IDataManager<CampaignGroup> 
  {
	

  }

  public partial class CampaignGroup : AdliteObject<ICampaignGroupManager> 
  {
		private CampaignGroup _fallbackGroup;
		private string _key = String.Empty;
		private string _name = String.Empty;
		private string _description = String.Empty;
		

		public CampaignGroup FallbackGroup 
		{
			get
			{
				if (this._fallbackGroup != null &&
						this._fallbackGroup.IsEmpty)
					if (this.ConnectionContext != null)
						this._fallbackGroup = CampaignGroup.CreateManager().LazyLoad(this.ConnectionContext, this._fallbackGroup.ID) as CampaignGroup;
					else
						this._fallbackGroup = CampaignGroup.CreateManager().LazyLoad(this._fallbackGroup.ID) as CampaignGroup;
				return this._fallbackGroup;
			}
			set { _fallbackGroup = value; }
		}

		public string Key{ get {return this._key; } set { this._key = value;} }
		public string Name{ get {return this._name; } set { this._name = value;} }
		public string Description{ get {return this._description; } set { this._description = value;} }
		

    public override bool IsInsertable { get { return true;} }

    public override bool IsDeletable { get { return false;} }

    public override bool IsUpdatable { get { return true;} }

    public CampaignGroup()
    { 
    }

    public CampaignGroup(int id): base(id, DateTime.Now, DateTime.Now, true)
    {
    }

    public CampaignGroup(int id,  CampaignGroup fallbackGroup, string key, string name, string description, DateTime updated, DateTime created): base(id, updated, created, false)
    {
			this._fallbackGroup = fallbackGroup;
			this._key = key;
			this._name = name;
			this._description = description;
			
    }

    public override object Clone(bool deepClone)
    {
      return new CampaignGroup(-1,  this.FallbackGroup,this.Key,this.Name,this.Description, DateTime.Now, DateTime.Now);
    }
  }
}

