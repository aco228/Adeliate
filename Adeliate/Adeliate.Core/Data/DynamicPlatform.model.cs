using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;
using Adlite.Data;

 

namespace Adlite.Data 
{
  public partial interface IDynamicPlatformManager : IDataManager<DynamicPlatform> 
  {
	

  }

  public partial class DynamicPlatform : AdliteObject<IDynamicPlatformManager> 
  {
		private string _name = String.Empty;
		

		public string Name{ get {return this._name; } set { this._name = value;} }
		

    public override bool IsInsertable { get { return true;} }

    public override bool IsDeletable { get { return false;} }

    public override bool IsUpdatable { get { return true;} }

    public DynamicPlatform()
    { 
    }

    public DynamicPlatform(int id): base(id, DateTime.Now, DateTime.Now, true)
    {
    }

    public DynamicPlatform(int id,  string name, DateTime updated, DateTime created): base(id, updated, created, false)
    {
			this._name = name;
			
    }

    public override object Clone(bool deepClone)
    {
      return new DynamicPlatform(-1, this.Name, DateTime.Now, DateTime.Now);
    }
  }
}

