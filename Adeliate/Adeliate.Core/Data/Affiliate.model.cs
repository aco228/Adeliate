using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;
using Adlite.Data;

 

namespace Adlite.Data 
{
  public partial interface IAffiliateManager : IDataManager<Affiliate> 
  {
	

  }

  public partial class Affiliate : AdliteObject<IAffiliateManager> 
  {
		private AffiliateType _affiliateType;
		private string _name = String.Empty;
		private string _description = String.Empty;
		private string _subidName = String.Empty;
		private string _contact = String.Empty;
		private string _websiteUrl = String.Empty;
		private bool _isActive = false;
		

		public AffiliateType AffiliateType 
		{
			get
			{
				if (this._affiliateType != null &&
						this._affiliateType.IsEmpty)
					if (this.ConnectionContext != null)
						this._affiliateType = AffiliateType.CreateManager().LazyLoad(this.ConnectionContext, this._affiliateType.ID) as AffiliateType;
					else
						this._affiliateType = AffiliateType.CreateManager().LazyLoad(this._affiliateType.ID) as AffiliateType;
				return this._affiliateType;
			}
			set { _affiliateType = value; }
		}

		public string Name{ get {return this._name; } set { this._name = value;} }
		public string Description{ get {return this._description; } set { this._description = value;} }
		public string SubidName{ get {return this._subidName; } set { this._subidName = value;} }
		public string Contact{ get {return this._contact; } set { this._contact = value;} }
		public string WebsiteUrl{ get {return this._websiteUrl; } set { this._websiteUrl = value;} }
		public bool IsActive {get {return this._isActive; } set { this._isActive = value;} }
		

    public override bool IsInsertable { get { return true;} }

    public override bool IsDeletable { get { return false;} }

    public override bool IsUpdatable { get { return true;} }

    public Affiliate()
    { 
    }

    public Affiliate(int id): base(id, DateTime.Now, DateTime.Now, true)
    {
    }

    public Affiliate(int id,  AffiliateType affiliateType, string name, string description, string subidName, string contact, string websiteUrl, bool isActive, DateTime updated, DateTime created): base(id, updated, created, false)
    {
			this._affiliateType = affiliateType;
			this._name = name;
			this._description = description;
			this._subidName = subidName;
			this._contact = contact;
			this._websiteUrl = websiteUrl;
			this._isActive = isActive;
			
    }

    public override object Clone(bool deepClone)
    {
      return new Affiliate(-1,  this.AffiliateType,this.Name,this.Description,this.SubidName,this.Contact,this.WebsiteUrl,this.IsActive, DateTime.Now, DateTime.Now);
    }
  }
}

