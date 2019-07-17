using Direct.Core.DatabaseTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adlite.Direct.Data
{
  public class AdliteDirect : DirectDatabaseMsSql
  {

    public AdliteDirect()
      : base("Adlite", "core")
    {
      string connectionString = System.Configuration.ConfigurationManager.
        ConnectionStrings[this.DatabaseName].ConnectionString; 
      this.SetConnectionString(connectionString);
    }

    public static AdliteDirect Instance
    {
      get
      {
        return new AdliteDirect();
      }
    }

  }
}
