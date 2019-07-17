using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;
using Adlite.Data;

 

namespace Adlite.Data 
{
  public partial interface IDynamicBrowserManager : IDataManager<DynamicBrowser> 
  {
	

  }

  public partial class DynamicBrowser : AdliteObject<IDynamicBrowserManager> 
  {
		private string _name = String.Empty;
		

		public string Name{ get {return this._name; } set { this._name = value;} }
		

    public override bool IsInsertable { get { return true;} }

    public override bool IsDeletable { get { return false;} }

    public override bool IsUpdatable { get { return true;} }

    public DynamicBrowser()
    { 
    }

    public DynamicBrowser(int id): base(id, DateTime.Now, DateTime.Now, true)
    {
    }

    public DynamicBrowser(int id,  string name, DateTime updated, DateTime created): base(id, updated, created, false)
    {
			this._name = name;
			
    }

    public override object Clone(bool deepClone)
    {
      return new DynamicBrowser(-1, this.Name, DateTime.Now, DateTime.Now);
    }
  }
}

