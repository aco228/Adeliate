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
  public partial class PostbackManager : IPostbackManager
  {
    public List<Postback> Load()
    {
      using (SqlConnectionInfo connection = new SqlConnectionInfo(this.Type))
        return this.Load(connection);
    }

    public List<Postback> Load(IConnectionInfo connection)
    {
      ISqlConnectionInfo sqlConnection = connection as ISqlConnectionInfo;
      if (sqlConnection != null)
        return this.Load(sqlConnection);
      using (sqlConnection = new SqlConnectionInfo(connection, this.Type))
        return this.Load(sqlConnection);
    }

    public List<Postback> Load(ISqlConnectionInfo connection)
    {
      SqlQueryParameters parameters = new SqlQueryParameters();
      //parameters.Where = "a = b OR a = @MyParam";
      //parameters.OrderBy = "abbr.PrimaryKeyColumnID DESC";
      //parameters.Arguments.Add("MyParam", "MyValue");
      //return this.Load(connection, parameters);
      return this.LoadMany(connection, parameters);
    }
    public List<Postback> Load(Affiliate affiliate)
    {
      using (SqlConnectionInfo connection = new SqlConnectionInfo(this.Type))
        return this.Load(connection, affiliate);
    }

    public List<Postback> Load(IConnectionInfo connection, Affiliate affiliate)
    {
      ISqlConnectionInfo sqlConnection = connection as ISqlConnectionInfo;
      if (sqlConnection != null)
        return this.Load(sqlConnection, affiliate);
      using (sqlConnection = new SqlConnectionInfo(connection, this.Type))
        return this.Load(sqlConnection, affiliate);
    }

    public List<Postback> Load(ISqlConnectionInfo connection, Affiliate affiliate)
    {
      SqlQueryParameters parameters = new SqlQueryParameters();
      parameters.Where = "[p].AffiliateID=@AffiliateID";
      parameters.Arguments.Add("AffiliateID", affiliate.ID);
      return this.LoadMany(connection, parameters);
    }


  }
}

