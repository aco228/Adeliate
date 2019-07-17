using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;
using Adlite.Data;

 

namespace Adlite.Data 
{
  public partial interface ICustomerSessionManager : IDataManager<CustomerSession> 
  {
	

  }

  public partial class CustomerSession : AdliteObject<ICustomerSessionManager> 
  {
		private Guid _guid;
		private Customer _customer;
		private string _iPAddress = String.Empty;
		private string _userAgent = String.Empty;
		private DateTime _validUntil = DateTime.MinValue;
		

		public Guid Guid { get { return this._guid; } set { _guid = value; } }
		public Customer Customer 
		{
			get
			{
				if (this._customer != null &&
						this._customer.IsEmpty)
					if (this.ConnectionContext != null)
						this._customer = Customer.CreateManager().LazyLoad(this.ConnectionContext, this._customer.ID) as Customer;
					else
						this._customer = Customer.CreateManager().LazyLoad(this._customer.ID) as Customer;
				return this._customer;
			}
			set { _customer = value; }
		}

		public string IPAddress{ get {return this._iPAddress; } set { this._iPAddress = value;} }
		public string UserAgent{ get {return this._userAgent; } set { this._userAgent = value;} }
		public DateTime ValidUntil { get { return this._validUntil; } set { this._validUntil = value;} }
		

    public override bool IsInsertable { get { return true;} }

    public override bool IsDeletable { get { return false;} }

    public override bool IsUpdatable { get { return true;} }

    public CustomerSession()
    { 
    }

    public CustomerSession(int id): base(id, DateTime.Now, DateTime.Now, true)
    {
    }

    public CustomerSession(int id,  Guid guid, Customer customer, string iPAddress, string userAgent, DateTime validUntil, DateTime updated, DateTime created): base(id, updated, created, false)
    {
			this._guid = guid;
			this._customer = customer;
			this._iPAddress = iPAddress;
			this._userAgent = userAgent;
			this._validUntil = validUntil;
			
    }

    public override object Clone(bool deepClone)
    {
      return new CustomerSession(-1, this.Guid, this.Customer,this.IPAddress,this.UserAgent,this.ValidUntil, DateTime.Now, DateTime.Now);
    }
  }
}

