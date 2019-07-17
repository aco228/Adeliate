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
  public partial class OfferManager : IOfferManager
  {


    public Offer Load(string key)
    {
      using (SqlConnectionInfo connection = new SqlConnectionInfo(this.Type))
        return this.Load(connection, key);
    }

    public Offer Load(IConnectionInfo connection, string key)
    {
      ISqlConnectionInfo sqlConnection = connection as ISqlConnectionInfo;
      if (sqlConnection != null)
        return this.Load(sqlConnection, key);
      using (sqlConnection = new SqlConnectionInfo(connection, this.Type))
        return this.Load(sqlConnection, key);
    }

    public Offer Load(ISqlConnectionInfo connection, string key)
    {
      SqlQueryParameters parameters = new SqlQueryParameters();
      parameters.Where = string.Format("[o].[Key]='{0}'", key);
      return this.Load(connection, parameters);
    }

    public Offer Load(Campaign campign, Affiliate affiliate)
    {
      using (SqlConnectionInfo connection = new SqlConnectionInfo(this.Type))
        return this.Load(connection, campign, affiliate);
    }

    public Offer Load(IConnectionInfo connection, Campaign campign, Affiliate affiliate)
    {
      ISqlConnectionInfo sqlConnection = connection as ISqlConnectionInfo;
      if (sqlConnection != null)
        return this.Load(sqlConnection, campign, affiliate);
      using (sqlConnection = new SqlConnectionInfo(connection, this.Type))
        return this.Load(sqlConnection, campign, affiliate);
    }

    public Offer Load(ISqlConnectionInfo connection, Campaign campign, Affiliate affiliate)
    {
      SqlQueryParameters parameters = new SqlQueryParameters();
      parameters.Where = "[o].CampaignID = @CampignID AND [o].AffiliateID = @AffiliateID";
      //parameters.OrderBy = "abbr.PrimaryKeyColumnID DESC";
      parameters.Arguments.Add("CampignID", campign.ID);
      parameters.Arguments.Add("AffiliateID", affiliate.ID);
      return this.Load(connection, parameters);
      //return this.LoadMany(connection, parameters);
    }

    public List<Offer> Load(Affiliate affiliate)
    {
      using (SqlConnectionInfo connection = new SqlConnectionInfo(this.Type))
        return this.Load(connection, affiliate);
    }

    public List<Offer> Load(IConnectionInfo connection, Affiliate affiliate)
    {
      ISqlConnectionInfo sqlConnection = connection as ISqlConnectionInfo;
      if (sqlConnection != null)
        return this.Load(sqlConnection, affiliate);
      using (sqlConnection = new SqlConnectionInfo(connection, this.Type))
        return this.Load(sqlConnection, affiliate);
    }

    public List<Offer> Load(ISqlConnectionInfo connection, Affiliate affiliate)
    {
      SqlQueryParameters parameters = new SqlQueryParameters();
      parameters.Where = "[o].AffiliateID = @ID";
      //parameters.OrderBy = "abbr.PrimaryKeyColumnID DESC";
      parameters.Arguments.Add("ID", affiliate.ID);
     
      //return this.Load(connection, parameters);
      return this.LoadMany(connection, parameters);
    }

  }
}

