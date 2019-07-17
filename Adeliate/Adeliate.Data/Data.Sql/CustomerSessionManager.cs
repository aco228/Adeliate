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
  public partial class CustomerSessionManager : ICustomerSessionManager
  {


    public CustomerSession Load(Guid sessionGuid)
    {
      using (SqlConnectionInfo connection = new SqlConnectionInfo(this.Type))
        return this.Load(connection, sessionGuid);
    }

    public CustomerSession Load(IConnectionInfo connection, Guid sessionGuid)
    {
      ISqlConnectionInfo sqlConnection = connection as ISqlConnectionInfo;
      if (sqlConnection != null)
        return this.Load(sqlConnection, sessionGuid);
      using (sqlConnection = new SqlConnectionInfo(connection, this.Type))
        return this.Load(sqlConnection, sessionGuid);
    }

    public CustomerSession Load(ISqlConnectionInfo connection, Guid sessionGuid)
    {
      SqlQueryParameters parameters = new SqlQueryParameters();
      parameters.Where = "[cs].CustomerSessionGuid=@csguid";
      parameters.Arguments.Add("csguid", sessionGuid);
      return this.Load(connection, parameters);
    }

  }
}

