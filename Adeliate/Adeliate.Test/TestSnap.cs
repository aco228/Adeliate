using Direct.Core.Snapshot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Direct.Core;

namespace Adeliate.Test
{
  public class TestSnap : SnapshotObjectBase<TestSnap>
  {
    private string _test = "BLABLA";
    private int _edit = 228;
    private DateTime _datetime = DateTime.Now;
    private bool _hasValue = false;
    private decimal _decimal = 26;
    
    public string Test { get { return this._test; } set { this._test = value; } }
    public int Edit { get { return this._edit; } set { this._edit = value; } }
    public DateTime DateTime { get { return this._datetime; } set { this._datetime = value; } }
    public bool HasValue { get { return this._hasValue; } set { this._hasValue = value; } }
    public decimal Decimal { get { return this._decimal; } set { this._decimal = value; } }
    
    public TestSnap(DirectDatabaseBase database, int reference, string tableName, string context) : base(database, reference, tableName, context)
    {
    }

    public override void Validate()
    {
      throw new NotImplementedException();
    }
  }
}
