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
  public partial class CampaignFlowMapManager : ICampaignFlowMapManager
  {
    public CampaignFlowMap Load(CampaignFlow campaignFlow,Campaign campaign)
    {
      using (SqlConnectionInfo connection = new SqlConnectionInfo(this.Type))
        return this.Load(connection, campaignFlow, campaign);
    }

    public CampaignFlowMap Load(IConnectionInfo connection, CampaignFlow campaignFlow, Campaign campaign)
    {
      ISqlConnectionInfo sqlConnection = connection as ISqlConnectionInfo;
      if (sqlConnection != null)
        return this.Load(sqlConnection, campaignFlow, campaign);
      using (sqlConnection = new SqlConnectionInfo(connection, this.Type))
        return this.Load(sqlConnection, campaignFlow, campaign);
    }

    public CampaignFlowMap Load(ISqlConnectionInfo connection, CampaignFlow campaignFlow, Campaign campaign)
    {
      SqlQueryParameters parameters = new SqlQueryParameters();
      parameters.Where = "[cfm].CampaignFlowID = @campaignFlow AND [cfm].CampaignID = @campaignID";
      //parameters.OrderBy = "abbr.PrimaryKeyColumnID DESC";
      parameters.Arguments.Add("campaignFlow", campaignFlow);
      parameters.Arguments.Add("campaignID", campaign.ID);
      return this.Load(connection, parameters);
      //return this.LoadMany(connection, parameters);
    }

    public List<CampaignFlowMap> Load(Campaign campaign)
    {
      using (SqlConnectionInfo connection = new SqlConnectionInfo(this.Type))
        return this.Load(connection, campaign);
    }

    public List<CampaignFlowMap> Load(IConnectionInfo connection, Campaign campaign)
    {
      ISqlConnectionInfo sqlConnection = connection as ISqlConnectionInfo;
      if (sqlConnection != null)
        return this.Load(sqlConnection, campaign);
      using (sqlConnection = new SqlConnectionInfo(connection, this.Type))
        return this.Load(sqlConnection, campaign);
    }

    public List<CampaignFlowMap> Load(ISqlConnectionInfo connection, Campaign campaign)
    {
      SqlQueryParameters parameters = new SqlQueryParameters();
      parameters.Where = "[cfm].CampaignID = @CampaignID";
      //parameters.OrderBy = "abbr.PrimaryKeyColumnID DESC";
      parameters.Arguments.Add("CampaignID", campaign.ID);
      //return this.Load(connection, parameters);
      return this.LoadMany(connection, parameters);
    }
  }
}

