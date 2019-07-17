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
  public partial class CampaignDeviceMapManager : ICampaignDeviceMapManager
  {
    public List<CampaignDeviceMap> Load(Campaign campaign)
    {
      using (SqlConnectionInfo connection = new SqlConnectionInfo(this.Type))
        return this.Load(connection, campaign);
    }

    public List<CampaignDeviceMap> Load(IConnectionInfo connection, Campaign campaign)
    {
      ISqlConnectionInfo sqlConnection = connection as ISqlConnectionInfo;
      if (sqlConnection != null)
        return this.Load(sqlConnection, campaign);
      using (sqlConnection = new SqlConnectionInfo(connection, this.Type))
        return this.Load(sqlConnection, campaign);
    }

    public List<CampaignDeviceMap> Load(ISqlConnectionInfo connection, Campaign campaign)
    {
      SqlQueryParameters parameters = new SqlQueryParameters();
      parameters.Where = "[cdm].CampaignID = @CampaignID";
      //parameters.OrderBy = "abbr.PrimaryKeyColumnID DESC";
      parameters.Arguments.Add("CampaignID", campaign.ID);
      //return this.Load(connection, parameters);
      return this.LoadMany(connection, parameters);
    }
    public CampaignDeviceMap Load(CampaignDevice device, Campaign campaign)
    {
      using (SqlConnectionInfo connection = new SqlConnectionInfo(this.Type))
        return this.Load(connection, device, campaign);
    }

    public CampaignDeviceMap Load(IConnectionInfo connection, CampaignDevice device, Campaign campaign)
    {
      ISqlConnectionInfo sqlConnection = connection as ISqlConnectionInfo;
      if (sqlConnection != null)
        return this.Load(sqlConnection, device, campaign);
      using (sqlConnection = new SqlConnectionInfo(connection, this.Type))
        return this.Load(sqlConnection, device, campaign);
    }

    public CampaignDeviceMap Load(ISqlConnectionInfo connection, CampaignDevice device, Campaign campaign)
    {
      SqlQueryParameters parameters = new SqlQueryParameters();
      parameters.Where = "[cdm].CampaignDeviceID = @device AND [cdm].CampaignID = @campaignID";
      //parameters.OrderBy = "abbr.PrimaryKeyColumnID DESC";
      parameters.Arguments.Add("device", device);
      parameters.Arguments.Add("CampaignID", campaign.ID);
      return this.Load(connection, parameters);
      //return this.LoadMany(connection, parameters);
    }

  }
}

