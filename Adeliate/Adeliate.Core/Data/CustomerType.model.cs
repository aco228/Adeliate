using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;
using Adlite.Data;

 

namespace Adlite.Data 
{
  public partial interface ICustomerTypeManager : IDataManager<CustomerType> 
  {
	

  }

  public partial class CustomerType : AdliteObject<ICustomerTypeManager> 
  {
		private string _name = String.Empty;
		private string _typeName = String.Empty;
		

		public string Name{ get {return this._name; } set { this._name = value;} }
		public string TypeName{ get {return this._typeName; } set { this._typeName = value;} }
		

    public override bool IsInsertable { get { return true;} }

    public override bool IsDeletable { get { return false;} }

    public override bool IsUpdatable { get { return true;} }

    public CustomerType()
    { 
    }

    public CustomerType(int id): base(id, DateTime.Now, DateTime.Now, true)
    {
    }

    public CustomerType(int id,  string name, string typeName, DateTime updated, DateTime created): base(id, updated, created, false)
    {
			this._name = name;
			this._typeName = typeName;
			
    }

    public override object Clone(bool deepClone)
    {
      return new CustomerType(-1, this.Name,this.TypeName, DateTime.Now, DateTime.Now);
    }
  }
}

