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
  public partial class CampaignManager : ICampaignManager
  {

    public List<Campaign> Load(CampaignGroup group)
    {
      using (SqlConnectionInfo connection = new SqlConnectionInfo(this.Type))
        return this.Load(connection, group);
    }

    public List<Campaign> Load(IConnectionInfo connection, CampaignGroup group)
    {
      ISqlConnectionInfo sqlConnection = connection as ISqlConnectionInfo;
      if (sqlConnection != null)
        return this.Load(sqlConnection, group);
      using (sqlConnection = new SqlConnectionInfo(connection, this.Type))
        return this.Load(sqlConnection, group);
    }

    public List<Campaign> Load(ISqlConnectionInfo connection, CampaignGroup group)
    {
      SqlQueryParameters parameters = new SqlQueryParameters();
      parameters.Where = "[c].CampaignGroupID=@CampaignGroupID";
      parameters.Arguments.Add("CampaignGroupID", group.ID);
      return this.LoadMany(connection, parameters);
    }
  }
}

