using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;
using Adlite.Data;

 

namespace Adlite.Data 
{
  public partial interface ICountryManager : IDataManager<Country> 
  {
	

  }

  public partial class Country : AdliteObject<ICountryManager> 
  {
		private Language _language;
		private Currency _currency;
		private string _globalName = String.Empty;
		private string _localName = String.Empty;
		private string _countryCode = String.Empty;
		private string _cultureCode = String.Empty;
		private string _twoLetterIsoCode = String.Empty;
		private int? _lCID = -1;
		private int? _callingCode = -1;
		

		public Language Language 
		{
			get
			{
				if (this._language != null &&
						this._language.IsEmpty)
					if (this.ConnectionContext != null)
						this._language = Language.CreateManager().LazyLoad(this.ConnectionContext, this._language.ID) as Language;
					else
						this._language = Language.CreateManager().LazyLoad(this._language.ID) as Language;
				return this._language;
			}
			set { _language = value; }
		}

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

		public string GlobalName{ get {return this._globalName; } set { this._globalName = value;} }
		public string LocalName{ get {return this._localName; } set { this._localName = value;} }
		public string CountryCode{ get {return this._countryCode; } set { this._countryCode = value;} }
		public string CultureCode{ get {return this._cultureCode; } set { this._cultureCode = value;} }
		public string TwoLetterIsoCode{ get {return this._twoLetterIsoCode; } set { this._twoLetterIsoCode = value;} }
		public int? LCID{ get {return this._lCID; } set { this._lCID = value;} }
		public int? CallingCode{ get {return this._callingCode; } set { this._callingCode = value;} }
		

    public override bool IsInsertable { get { return true;} }

    public override bool IsDeletable { get { return false;} }

    public override bool IsUpdatable { get { return true;} }

    public Country()
    { 
    }

    public Country(int id): base(id, DateTime.Now, DateTime.Now, true)
    {
    }

    public Country(int id,  Language language, Currency currency, string globalName, string localName, string countryCode, string cultureCode, string twoLetterIsoCode, int? lCID, int? callingCode, DateTime updated, DateTime created): base(id, updated, created, false)
    {
			this._language = language;
			this._currency = currency;
			this._globalName = globalName;
			this._localName = localName;
			this._countryCode = countryCode;
			this._cultureCode = cultureCode;
			this._twoLetterIsoCode = twoLetterIsoCode;
			this._lCID = lCID;
			this._callingCode = callingCode;
			
    }

    public override object Clone(bool deepClone)
    {
      return new Country(-1,  this.Language, this.Currency,this.GlobalName,this.LocalName,this.CountryCode,this.CultureCode,this.TwoLetterIsoCode,this.LCID,this.CallingCode, DateTime.Now, DateTime.Now);
    }
  }
}

