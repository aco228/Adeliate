using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti;
using Senti.Data;
using Senti.Data.Sql;
using Adlite.Data;

namespace Adlite.Data.Sql
{
  public partial class AffiliateManager : IAffiliateManager
  {
    public Affiliate Load(string name)
    {
      using (SqlConnectionInfo connection = new SqlConnectionInfo(this.Type))
        return this.Load(connection, name);
    }

    public Affiliate Load(IConnectionInfo connection, string name)
    {
      ISqlConnectionInfo sqlConnection = connection as ISqlConnectionInfo;
      if (sqlConnection != null)
        return this.Load(sqlConnection, name);
      using (sqlConnection = new SqlConnectionInfo(connection, this.Type))
        return this.Load(sqlConnection, name);
    }

    public Affiliate Load(ISqlConnectionInfo connection, string name)
    {
      SqlQueryParameters parameters = new SqlQueryParameters();
      parameters.Where = "[a].Name = @Name";
      parameters.Arguments.Add("Name", name);
      return this.Load(connection, parameters);

    }
    public List<Affiliate> Load()
    {
      using (SqlConnectionInfo connection = new SqlConnectionInfo(this.Type))
        return this.Load(connection);
    }

    public List<Affiliate> Load(IConnectionInfo connection)
    {
      ISqlConnectionInfo sqlConnection = connection as ISqlConnectionInfo;
      if (sqlConnection != null)
        return this.Load(sqlConnection);
      using (sqlConnection = new SqlConnectionInfo(connection, this.Type))
        return this.Load(sqlConnection);
    }

    public List<Affiliate> Load(ISqlConnectionInfo connection)
    {
      SqlQueryParameters parameters = new SqlQueryParameters();
      //parameters.Where = "a = b OR a = @MyParam";
      //parameters.OrderBy = "abbr.PrimaryKeyColumnID DESC";
      //parameters.Arguments.Add("MyParam", "MyValue");
      //return this.Load(connection, parameters);
      return this.LoadMany(connection, parameters);
    }
  }
}

