using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;
using Adlite.Data;

 

namespace Adlite.Data 
{
  public partial interface IMobileOperatorManager : IDataManager<MobileOperator> 
  {
	

  }

  public partial class MobileOperator : AdliteObject<IMobileOperatorManager> 
  {
		private int? _externalMobileOperatorID = -1;
		private Country _country;
		private string _name = String.Empty;
		

		public int? ExternalMobileOperatorID{ get {return this._externalMobileOperatorID; } set { this._externalMobileOperatorID = value;} }
		public Country Country 
		{
			get
			{
				if (this._country != null &&
						this._country.IsEmpty)
					if (this.ConnectionContext != null)
						this._country = Country.CreateManager().LazyLoad(this.ConnectionContext, this._country.ID) as Country;
					else
						this._country = Country.CreateManager().LazyLoad(this._country.ID) as Country;
				return this._country;
			}
			set { _country = value; }
		}

		public string Name{ get {return this._name; } set { this._name = value;} }
		

    public override bool IsInsertable { get { return true;} }

    public override bool IsDeletable { get { return false;} }

    public override bool IsUpdatable { get { return true;} }

    public MobileOperator()
    { 
    }

    public MobileOperator(int id): base(id, DateTime.Now, DateTime.Now, true)
    {
    }

    public MobileOperator(int id,  int? externalMobileOperatorID, Country country, string name, DateTime updated, DateTime created): base(id, updated, created, false)
    {
			this._externalMobileOperatorID = externalMobileOperatorID;
			this._country = country;
			this._name = name;
			
    }

    public override object Clone(bool deepClone)
    {
      return new MobileOperator(-1, this.ExternalMobileOperatorID, this.Country,this.Name, DateTime.Now, DateTime.Now);
    }
  }
}

