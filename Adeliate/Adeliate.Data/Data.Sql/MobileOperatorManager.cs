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
  public partial class MobileOperatorManager : IMobileOperatorManager
  {

    public List<MobileOperator> Load()
    {
      using (SqlConnectionInfo connection = new SqlConnectionInfo(this.Type))
        return this.Load(connection);
    }

    public List<MobileOperator> Load(IConnectionInfo connection)
    {
      ISqlConnectionInfo sqlConnection = connection as ISqlConnectionInfo;
      if (sqlConnection != null)
        return this.Load(sqlConnection);
      using (sqlConnection = new SqlConnectionInfo(connection, this.Type))
        return this.Load(sqlConnection);
    }

    public List<MobileOperator> Load(ISqlConnectionInfo connection)
    {
      SqlQueryParameters parameters = new SqlQueryParameters();
      return this.LoadMany(connection, parameters);
    }


    public List<MobileOperator> Load(Campaign campaign)
    {
      using (SqlConnectionInfo connection = new SqlConnectionInfo(this.Type))
        return this.Load(connection, campaign);
    }

    public List<MobileOperator> Load(IConnectionInfo connection, Campaign campaign)
    {
      ISqlConnectionInfo sqlConnection = connection as ISqlConnectionInfo;
      if (sqlConnection != null)
        return this.Load(sqlConnection, campaign);
      using (sqlConnection = new SqlConnectionInfo(connection, this.Type))
        return this.Load(sqlConnection, campaign);
    }

    public List<MobileOperator> Load(ISqlConnectionInfo connection, Campaign campaign)
    {
      SqlQueryParameters parameters = new SqlQueryParameters();
      parameters.Where = "[mo].CountryID = @CountryID";
      //parameters.OrderBy = "abbr.PrimaryKeyColumnID DESC";
      parameters.Arguments.Add("CountryID", campaign.Country.ID);
      //return this.Load(connection, parameters);
      return this.LoadMany(connection, parameters);
    }

  }
}

