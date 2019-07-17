using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;
using Adlite.Data;

 

namespace Adlite.Data 
{
  public partial interface IOfferManager : IDataManager<Offer> 
  {
	

  }

  public partial class Offer : AdliteObject<IOfferManager> 
  {
		private Campaign _campaign;
		private Affiliate _affiliate;
		private Price _price;
		private int? _capValue = -1;
		private string _key = String.Empty;
		private bool _isActive = false;
		

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

		public Price Price 
		{
			get
			{
				if (this._price != null &&
						this._price.IsEmpty)
					if (this.ConnectionContext != null)
						this._price = Price.CreateManager().LazyLoad(this.ConnectionContext, this._price.ID) as Price;
					else
						this._price = Price.CreateManager().LazyLoad(this._price.ID) as Price;
				return this._price;
			}
			set { _price = value; }
		}

		public int? CapValue{ get {return this._capValue; } set { this._capValue = value;} }
		public string Key{ get {return this._key; } set { this._key = value;} }
		public bool IsActive {get {return this._isActive; } set { this._isActive = value;} }
		

    public override bool IsInsertable { get { return true;} }

    public override bool IsDeletable { get { return false;} }

    public override bool IsUpdatable { get { return true;} }

    public Offer()
    { 
    }

    public Offer(int id): base(id, DateTime.Now, DateTime.Now, true)
    {
    }

    public Offer(int id,  Campaign campaign, Affiliate affiliate, Price price, int? capValue, string key, bool isActive, DateTime updated, DateTime created): base(id, updated, created, false)
    {
			this._campaign = campaign;
			this._affiliate = affiliate;
			this._price = price;
			this._capValue = capValue;
			this._key = key;
			this._isActive = isActive;
			
    }

    public override object Clone(bool deepClone)
    {
      return new Offer(-1,  this.Campaign, this.Affiliate, this.Price,this.CapValue,this.Key,this.IsActive, DateTime.Now, DateTime.Now);
    }
  }
}

