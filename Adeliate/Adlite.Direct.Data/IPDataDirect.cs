using Direct.Core.DatabaseTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adlite.Direct.Data
{
  public class IPDataDirect : DirectDatabaseMsSql
  {
    public IPDataDirect()
      : base("IPData", "core")
    {
      string connectionString = "Data Source=192.168.11.26;Initial Catalog=IPData;uid=sa;pwd=n7F2f9o41GH6Nid;Max Pool Size=1000";
      this.SetConnectionString(connectionString);
    }

    public static IPDataDirect Instance
    {
      get
      {
        return new IPDataDirect();
      }
    }

  }
}
