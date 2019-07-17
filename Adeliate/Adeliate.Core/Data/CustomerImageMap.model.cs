using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;
using Adlite.Data;

 

namespace Adlite.Data 
{
  public partial interface ICustomerImageMapManager : IDataManager<CustomerImageMap> 
  {
	

  }

  public partial class CustomerImageMap : AdliteObject<ICustomerImageMapManager> 
  {
		private Customer _customer;
		private Image _image;
		

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

		public Image Image 
		{
			get
			{
				if (this._image != null &&
						this._image.IsEmpty)
					if (this.ConnectionContext != null)
						this._image = Image.CreateManager().LazyLoad(this.ConnectionContext, this._image.ID) as Image;
					else
						this._image = Image.CreateManager().LazyLoad(this._image.ID) as Image;
				return this._image;
			}
			set { _image = value; }
		}

		

    public override bool IsInsertable { get { return true;} }

    public override bool IsDeletable { get { return true;} }

    public override bool IsUpdatable { get { return true;} }

    public CustomerImageMap()
    { 
    }

    public CustomerImageMap(int id): base(id, DateTime.Now, DateTime.Now, true)
    {
    }

    public CustomerImageMap(int id,  Customer customer, Image image, DateTime updated, DateTime created): base(id, updated, created, false)
    {
			this._customer = customer;
			this._image = image;
			
    }

    public override object Clone(bool deepClone)
    {
      return new CustomerImageMap(-1,  this.Customer, this.Image, DateTime.Now, DateTime.Now);
    }
  }
}

