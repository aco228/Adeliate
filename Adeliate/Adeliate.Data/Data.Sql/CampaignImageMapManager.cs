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
  public partial class CampaignImageMapManager : ICampaignImageMapManager
  {

    public List<CampaignImageMap> Load(Campaign campaign)
    {
      using (SqlConnectionInfo connection = new SqlConnectionInfo(this.Type))
        return this.Load(connection, campaign);
    }

    public List<CampaignImageMap> Load(IConnectionInfo connection, Campaign campaign)
    {
      ISqlConnectionInfo sqlConnection = connection as ISqlConnectionInfo;
      if (sqlConnection != null)
        return this.Load(sqlConnection, campaign);
      using (sqlConnection = new SqlConnectionInfo(connection, this.Type))
        return this.Load(sqlConnection, campaign);
    }

    public List<CampaignImageMap> Load(ISqlConnectionInfo connection, Campaign campaign)
    {
      SqlQueryParameters parameters = new SqlQueryParameters();
      parameters.Where = "[cim].CampaignID = @CampaignID";
      //parameters.OrderBy = "abbr.PrimaryKeyColumnID DESC";
      parameters.Arguments.Add("CampaignID", campaign.ID);
      //return this.Load(connection, parameters);
      return this.LoadMany(connection, parameters);
    }

  }
}

