using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti;
using Senti.Data;
using Senti.Data.Sql;



namespace Adlite.Data.Sql
{
  public partial class CurrencyManager : ICurrencyManager
  {

    public List<Currency> Load()
    {
      using (SqlConnectionInfo connection = new SqlConnectionInfo(this.Type))
        return this.Load(connection);
    }

    public List<Currency> Load(IConnectionInfo connection)
    {
      ISqlConnectionInfo sqlConnection = connection as ISqlConnectionInfo;
      if (sqlConnection != null)
        return this.Load(sqlConnection);
      using (sqlConnection = new SqlConnectionInfo(connection, this.Type))
        return this.Load(sqlConnection);
    }

    public List<Currency> Load(ISqlConnectionInfo connection)
    {
      SqlQueryParameters parameters = new SqlQueryParameters();
      //parameters.Where = "[c] = b OR a = @MyParam";
      //parameters.OrderBy = "abbr.PrimaryKeyColumnID DESC";
      //parameters.Arguments.Add("MyParam", "MyValue");
      //return this.Load(connection, parameters);
      return this.LoadMany(connection, parameters);
    }

  }
}

