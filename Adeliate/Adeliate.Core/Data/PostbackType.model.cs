using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;
using Adlite.Data;

 

namespace Adlite.Data 
{
  public partial interface IPostbackTypeManager : IDataManager<PostbackType> 
  {
	

  }

  public partial class PostbackType : AdliteObject<IPostbackTypeManager> 
  {
		private string _name = String.Empty;
		private string _typeName = String.Empty;
		

		public string Name{ get {return this._name; } set { this._name = value;} }
		public string TypeName{ get {return this._typeName; } set { this._typeName = value;} }
		

    public override bool IsInsertable { get { return true;} }

    public override bool IsDeletable { get { return false;} }

    public override bool IsUpdatable { get { return true;} }

    public PostbackType()
    { 
    }

    public PostbackType(int id): base(id, DateTime.Now, DateTime.Now, true)
    {
    }

    public PostbackType(int id,  string name, string typeName, DateTime updated, DateTime created): base(id, updated, created, false)
    {
			this._name = name;
			this._typeName = typeName;
			
    }

    public override object Clone(bool deepClone)
    {
      return new PostbackType(-1, this.Name,this.TypeName, DateTime.Now, DateTime.Now);
    }
  }
}

