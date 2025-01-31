using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti;
using Senti.Data;
using Senti.Data.Sql;
using Adeliate.Core.Data;

namespace Adlite.Data.Sql
{
  public partial class CountryManager : ICountryManager
  {
    public List<Country> Load()
    {
      using (SqlConnectionInfo connection = new SqlConnectionInfo(this.Type))
        return this.Load(connection);
    }

    public List<Country> Load(IConnectionInfo connection)
    {
      ISqlConnectionInfo sqlConnection = connection as ISqlConnectionInfo;
      if (sqlConnection != null)
        return this.Load(sqlConnection);
      using (sqlConnection = new SqlConnectionInfo(connection, this.Type))
        return this.Load(sqlConnection);
    }

    public List<Country> Load(ISqlConnectionInfo connection)
    {
      SqlQueryParameters parameters = new SqlQueryParameters();
      return this.LoadMany(connection, parameters);
    }
    public Country Load(string value, CountryIdentifier identifier)
    {
      using (SqlConnectionInfo connection = new SqlConnectionInfo(this.Type))
        return this.Load(connection, value, identifier);
    }

    public Country Load(IConnectionInfo connection, string value, CountryIdentifier identifier)
    {
      ISqlConnectionInfo sqlConnection = connection as ISqlConnectionInfo;
      if (sqlConnection != null)
        return this.Load(sqlConnection, value, identifier);
      using (sqlConnection = new SqlConnectionInfo(connection, this.Type))
        return this.Load(sqlConnection, value, identifier);
    }

    public Country Load(ISqlConnectionInfo connection, string value, CountryIdentifier identifier)
    {
      SqlQueryParameters parameters = new SqlQueryParameters();
      string parameter = string.Empty;
      switch (identifier)
      {
        case CountryIdentifier.GlobalName:
          parameter = "GlobalName";
          break;
        case CountryIdentifier.LocalName:
          parameter = "LocalName";
          break;
        case CountryIdentifier.CountryCode:
          parameter = "CountryCode";
          break;
        case CountryIdentifier.CultureCode:
          parameter = "CultureCode";
          break;
        case CountryIdentifier.TwoLetterIsoCode:
          parameter = "TwoLetterIsoCode";
          break;
        default:
          parameter = "GlobalName";
          break;
      }

      parameters.Where = string.Format("[c].{0} = @{0}", parameter);
      parameters.Arguments.Add(parameter, value);
      return this.Load(connection, parameters);
    }
  }
}


