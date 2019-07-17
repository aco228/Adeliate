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
  public partial class CampaignMobileOperatorMapManager : ICampaignMobileOperatorMapManager
  {
    public List<CampaignMobileOperatorMap> Load(Campaign campaign)
    {
      using (SqlConnectionInfo connection = new SqlConnectionInfo(this.Type))
        return this.Load(connection, campaign);
    }

    public List<CampaignMobileOperatorMap> Load(IConnectionInfo connection, Campaign campaign)
    {
      ISqlConnectionInfo sqlConnection = connection as ISqlConnectionInfo;
      if (sqlConnection != null)
        return this.Load(sqlConnection, campaign);
      using (sqlConnection = new SqlConnectionInfo(connection, this.Type))
        return this.Load(sqlConnection, campaign);
    }

    public List<CampaignMobileOperatorMap> Load(ISqlConnectionInfo connection, Campaign campaign)
    {
      SqlQueryParameters parameters = new SqlQueryParameters();
      parameters.Where = "[cmom].CampaignID = @CampaignID";
      //parameters.OrderBy = "abbr.PrimaryKeyColumnID DESC";
      parameters.Arguments.Add("CampaignID", campaign.ID);
      //return this.Load(connection, parameters);
      return this.LoadMany(connection, parameters);
    }



    public CampaignMobileOperatorMap Load(MobileOperator mobileOperator, Campaign campaign)
    {
      using (SqlConnectionInfo connection = new SqlConnectionInfo(this.Type))
        return this.Load(connection, mobileOperator, campaign);
    }

    public CampaignMobileOperatorMap Load(IConnectionInfo connection, MobileOperator mobileOperator, Campaign campaign)
    {
      ISqlConnectionInfo sqlConnection = connection as ISqlConnectionInfo;
      if (sqlConnection != null)
        return this.Load(sqlConnection, mobileOperator, campaign);
      using (sqlConnection = new SqlConnectionInfo(connection, this.Type))
        return this.Load(sqlConnection, mobileOperator, campaign);
    }

    public CampaignMobileOperatorMap Load(ISqlConnectionInfo connection, MobileOperator mobileOperator, Campaign campaign)
    {
      SqlQueryParameters parameters = new SqlQueryParameters();
      parameters.Where = "[cmom].MobileOperatorID = @MobileOperatorID AND [cmom].CampaignID = @CampaignID";
      //parameters.OrderBy = "abbr.PrimaryKeyColumnID DESC";
      parameters.Arguments.Add("MobileOperatorID", mobileOperator.ID);
      parameters.Arguments.Add("CampaignID", campaign.ID);
      return this.Load(connection, parameters);
      //return this.LoadMany(connection, parameters);
    }


  }
}

