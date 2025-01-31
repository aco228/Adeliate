using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;

namespace Adlite.Data
{
  /// <summary>
  /// This interface must be implemented by all data classes, which should be<br />
  /// stored/retrieved from some kind of data storage. It provides some basic methods/properties<br />
  /// to enable easy (de)serialization of these objects. This interface is inherited by<br />
  /// MobileMafiaObject. Data classes should rather derive from this class instead of implementing<br />
  /// this interface. If deriving is not possible, this interface has to be implemented.
  /// </summary>
  public interface IAdliteObject : IDataObject, ICloneable
  {
  }
}

