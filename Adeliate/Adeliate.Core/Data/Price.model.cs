using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;
using Adlite.Data;

 

namespace Adlite.Data 
{
  public partial interface IPriceManager : IDataManager<Price> 
  {
	

  }

  public partial class Price : AdliteObject<IPriceManager> 
  {
		private Currency _currency;
		private decimal _value = 0;
		

		public Currency Currency 
		{
			get
			{
				if (this._currency != null &&
						this._currency.IsEmpty)
					if (this.ConnectionContext != null)
						this._currency = Currency.CreateManager().LazyLoad(this.ConnectionContext, this._currency.ID) as Currency;
					else
						this._currency = Currency.CreateManager().LazyLoad(this._currency.ID) as Currency;
				return this._currency;
			}
			set { _currency = value; }
		}

		public decimal Value { get { return this._value; } set { this._value = value;} }
		

    public override bool IsInsertable { get { return true;} }

    public override bool IsDeletable { get { return false;} }

    public override bool IsUpdatable { get { return true;} }

    public Price()
    { 
    }

    public Price(int id): base(id, DateTime.Now, DateTime.Now, true)
    {
    }

    public Price(int id,  Currency currency, decimal value, DateTime updated, DateTime created): base(id, updated, created, false)
    {
			this._currency = currency;
			this._value = value;
			
    }

    public override object Clone(bool deepClone)
    {
      return new Price(-1,  this.Currency,this.Value, DateTime.Now, DateTime.Now);
    }
  }
}

