using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;
using Adlite.Data;

 

namespace Adlite.Data 
{
  public partial interface ICurrencyManager : IDataManager<Currency> 
  {
	

  }

  public partial class Currency : AdliteObject<ICurrencyManager> 
  {
		private string _name = String.Empty;
		private string _abbreviation = String.Empty;
		private string _symbol = String.Empty;
		

		public string Name{ get {return this._name; } set { this._name = value;} }
		public string Abbreviation{ get {return this._abbreviation; } set { this._abbreviation = value;} }
		public string Symbol{ get {return this._symbol; } set { this._symbol = value;} }
		

    public override bool IsInsertable { get { return true;} }

    public override bool IsDeletable { get { return false;} }

    public override bool IsUpdatable { get { return true;} }

    public Currency()
    { 
    }

    public Currency(int id): base(id, DateTime.Now, DateTime.Now, true)
    {
    }

    public Currency(int id,  string name, string abbreviation, string symbol, DateTime updated, DateTime created): base(id, updated, created, false)
    {
			this._name = name;
			this._abbreviation = abbreviation;
			this._symbol = symbol;
			
    }

    public override object Clone(bool deepClone)
    {
      return new Currency(-1, this.Name,this.Abbreviation,this.Symbol, DateTime.Now, DateTime.Now);
    }
  }
}

