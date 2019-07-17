using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;
using Adlite.Data;

 

namespace Adlite.Data 
{
  public partial interface ICustomerManager : IDataManager<Customer> 
  {
	

  }

  public partial class Customer : AdliteObject<ICustomerManager> 
  {
		private Affiliate _affiliate;
		private CustomerType _customerType;
		private CustomerStatus _customerStatus;
		private string _username = String.Empty;
		private string _password = String.Empty;
		private bool _isActive = false;
		

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

		public CustomerType CustomerType 
		{
			get
			{
				if (this._customerType != null &&
						this._customerType.IsEmpty)
					if (this.ConnectionContext != null)
						this._customerType = CustomerType.CreateManager().LazyLoad(this.ConnectionContext, this._customerType.ID) as CustomerType;
					else
						this._customerType = CustomerType.CreateManager().LazyLoad(this._customerType.ID) as CustomerType;
				return this._customerType;
			}
			set { _customerType = value; }
		}

		public CustomerStatus CustomerStatus  { get { return this._customerStatus; } set { this._customerStatus = value; } }
		public string Username{ get {return this._username; } set { this._username = value;} }
		public string Password{ get {return this._password; } set { this._password = value;} }
		public bool IsActive {get {return this._isActive; } set { this._isActive = value;} }
		

    public override bool IsInsertable { get { return true;} }

    public override bool IsDeletable { get { return false;} }

    public override bool IsUpdatable { get { return true;} }

    public Customer()
    { 
    }

    public Customer(int id): base(id, DateTime.Now, DateTime.Now, true)
    {
    }

    public Customer(int id,  Affiliate affiliate, CustomerType customerType, CustomerStatus customerStatus, string username, string password, bool isActive, DateTime updated, DateTime created): base(id, updated, created, false)
    {
			this._affiliate = affiliate;
			this._customerType = customerType;
			this._customerStatus = customerStatus;
			this._username = username;
			this._password = password;
			this._isActive = isActive;
			
    }

    public override object Clone(bool deepClone)
    {
      return new Customer(-1,  this.Affiliate, this.CustomerType, this.CustomerStatus,this.Username,this.Password,this.IsActive, DateTime.Now, DateTime.Now);
    }
  }
}

