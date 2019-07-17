using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;
using Adlite.Data;

 

namespace Adlite.Data 
{
  public partial interface ITransactionManager : IDataManager<Transaction> 
  {
	

  }

  public partial class Transaction : AdliteObject<ITransactionManager> 
  {
		private int _clickID = -1;
		private Price _price;
		private int _offerID = -1;
		private int _campaignID = -1;
		private int _affiliateID = -1;
		private decimal _priceInEuros = 0;
		

		public int ClickID{ get {return this._clickID; } set { this._clickID = value;} }
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

		public int OfferID{ get {return this._offerID; } set { this._offerID = value;} }
		public int CampaignID{ get {return this._campaignID; } set { this._campaignID = value;} }
		public int AffiliateID{ get {return this._affiliateID; } set { this._affiliateID = value;} }
		public decimal PriceInEuros { get { return this._priceInEuros; } set { this._priceInEuros = value;} }
		

    public override bool IsInsertable { get { return true;} }

    public override bool IsDeletable { get { return false;} }

    public override bool IsUpdatable { get { return true;} }

    public Transaction()
    { 
    }

    public Transaction(int id): base(id, DateTime.Now, DateTime.Now, true)
    {
    }

    public Transaction(int id,  int clickID, Price price, int offerID, int campaignID, int affiliateID, decimal priceInEuros, DateTime updated, DateTime created): base(id, updated, created, false)
    {
			this._clickID = clickID;
			this._price = price;
			this._offerID = offerID;
			this._campaignID = campaignID;
			this._affiliateID = affiliateID;
			this._priceInEuros = priceInEuros;
			
    }

    public override object Clone(bool deepClone)
    {
      return new Transaction(-1, this.ClickID, this.Price,this.OfferID,this.CampaignID,this.AffiliateID,this.PriceInEuros, DateTime.Now, DateTime.Now);
    }
  }
}

