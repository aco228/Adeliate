using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;

namespace Adlite.Data
{
  public abstract class AdliteObject<DataManagerInterfaceType> 
    : DataObject<DataManagerInterfaceType>, IAdliteObject
    where DataManagerInterfaceType : class, IDataManager
  {

    public new static DataManagerInterfaceType CreateManager()
    {
      return DataObject<DataManagerInterfaceType>.CreateManager() as DataManagerInterfaceType;
    }

    public AdliteObject()
    {
    }

    public AdliteObject(int id, DateTime updated, DateTime created, bool isEmpty)
      : base(id, updated, created, isEmpty)
    {
    }
  }

  public abstract class AdliteObject : DataObject, IAdliteObject
  {
    public AdliteObject()
    {
    }

    public AdliteObject(int id, DateTime updated, DateTime created, bool isEmpty)
      : base(id, updated, created, isEmpty)
    {
    }
  }
}

