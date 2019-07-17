using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;
using Adlite.Data;

 

namespace Adlite.Data 
{
  public partial interface IMobileOperatorCodeManager : IDataManager<MobileOperatorCode> 
  {
	

  }

  public partial class MobileOperatorCode : AdliteObject<IMobileOperatorCodeManager> 
  {
		private MobileOperator _mobileOperator;
		private int _mCC = -1;
		private int _mNC = -1;
		private bool _isDefault = false;
		

		public MobileOperator MobileOperator 
		{
			get
			{
				if (this._mobileOperator != null &&
						this._mobileOperator.IsEmpty)
					if (this.ConnectionContext != null)
						this._mobileOperator = MobileOperator.CreateManager().LazyLoad(this.ConnectionContext, this._mobileOperator.ID) as MobileOperator;
					else
						this._mobileOperator = MobileOperator.CreateManager().LazyLoad(this._mobileOperator.ID) as MobileOperator;
				return this._mobileOperator;
			}
			set { _mobileOperator = value; }
		}

		public int MCC{ get {return this._mCC; } set { this._mCC = value;} }
		public int MNC{ get {return this._mNC; } set { this._mNC = value;} }
		public bool IsDefault {get {return this._isDefault; } set { this._isDefault = value;} }
		

    public override bool IsInsertable { get { return true;} }

    public override bool IsDeletable { get { return false;} }

    public override bool IsUpdatable { get { return true;} }

    public MobileOperatorCode()
    { 
    }

    public MobileOperatorCode(int id): base(id, DateTime.Now, DateTime.Now, true)
    {
    }

    public MobileOperatorCode(int id,  MobileOperator mobileOperator, int mCC, int mNC, bool isDefault, DateTime updated, DateTime created): base(id, updated, created, false)
    {
			this._mobileOperator = mobileOperator;
			this._mCC = mCC;
			this._mNC = mNC;
			this._isDefault = isDefault;
			
    }

    public override object Clone(bool deepClone)
    {
      return new MobileOperatorCode(-1,  this.MobileOperator,this.MCC,this.MNC,this.IsDefault, DateTime.Now, DateTime.Now);
    }
  }
}

