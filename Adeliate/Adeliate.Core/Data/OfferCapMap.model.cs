using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;
using Adlite.Data;

 

namespace Adlite.Data 
{
  public partial interface IOfferCapMapManager : IDataManager<OfferCapMap> 
  {
	

  }

  public partial class OfferCapMap : AdliteObject<IOfferCapMapManager> 
  {
		private Offer _offer;
		private int _transactions = -1;
		private bool _locked = false;
		

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

		public int Transactions{ get {return this._transactions; } set { this._transactions = value;} }
		public bool Locked {get {return this._locked; } set { this._locked = value;} }
		

    public override bool IsInsertable { get { return true;} }

    public override bool IsDeletable { get { return false;} }

    public override bool IsUpdatable { get { return true;} }

    public OfferCapMap()
    { 
    }

    public OfferCapMap(int id): base(id, DateTime.Now, DateTime.Now, true)
    {
    }

    public OfferCapMap(int id,  Offer offer, int transactions, bool locked, DateTime updated, DateTime created): base(id, updated, created, false)
    {
			this._offer = offer;
			this._transactions = transactions;
			this._locked = locked;
			
    }

    public override object Clone(bool deepClone)
    {
      return new OfferCapMap(-1,  this.Offer,this.Transactions,this.Locked, DateTime.Now, DateTime.Now);
    }
  }
}

