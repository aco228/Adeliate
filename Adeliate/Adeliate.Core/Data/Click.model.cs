using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;
using Adlite.Data;

 

namespace Adlite.Data 
{
  public partial interface IClickManager : IDataManager<Click> 
  {
	

  }

  public partial class Click : AdliteObject<IClickManager> 
  {
		private string _subID = String.Empty;
		private Offer _offer;
		private Affiliate _affiliate;
		private bool _hasTransaction = false;
		

		public string SubID{ get {return this._subID; } set { this._subID = value;} }
		public Offer Offer 
		{
			get
			{
				if (this._offer != null &&
						this._offer.IsEmpty)
					if (this.ConnectionContext != null)
						this._offer = Offer.CreateManager().LazyLoad(this.ConnectionContext, this._offer.ID) as Offer;
					else
						this._offer = Offer.CreateManager().LazyLoad(this._offer.ID) as Offer;
				return this._offer;
			}
			set { _offer = value; }
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

		public bool HasTransaction {get {return this._hasTransaction; } set { this._hasTransaction = value;} }
		

    public override bool IsInsertable { get { return true;} }

    public override bool IsDeletable { get { return false;} }

    public override bool IsUpdatable { get { return true;} }

    public Click()
    { 
    }

    public Click(int id): base(id, DateTime.Now, DateTime.Now, true)
    {
    }

    public Click(int id,  string subID, Offer offer, Affiliate affiliate, bool hasTransaction, DateTime updated, DateTime created): base(id, updated, created, false)
    {
			this._subID = subID;
			this._offer = offer;
			this._affiliate = affiliate;
			this._hasTransaction = hasTransaction;
			
    }

    public override object Clone(bool deepClone)
    {
      return new Click(-1, this.SubID, this.Offer, this.Affiliate,this.HasTransaction, DateTime.Now, DateTime.Now);
    }
  }
}

