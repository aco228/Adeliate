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
  public partial class StatInfoManager : IStatInfoManager
  {


    public StatInfo Load(StatType type, Offer offer)
    {
      using (SqlConnectionInfo connection = new SqlConnectionInfo(this.Type))
        return this.Load(connection, type, offer);
    }

    public StatInfo Load(IConnectionInfo connection, StatType type, Offer offer)
    {
      ISqlConnectionInfo sqlConnection = connection as ISqlConnectionInfo;
      if (sqlConnection != null)
        return this.Load(sqlConnection, type, offer);
      using (sqlConnection = new SqlConnectionInfo(connection, this.Type))
        return this.Load(sqlConnection, type, offer);
    }

    public StatInfo Load(ISqlConnectionInfo connection, StatType type, Offer offer)
    {
      SqlQueryParameters parameters = new SqlQueryParameters();
      DateTime dateCompare = StatTypeManager.GetDate(type);

      parameters.Where = "[si].OfferID=@OfferID AND [si].StatTypeID=@StatTypeID AND [si].Created>@Created";
      parameters.Arguments.Add("OfferID", offer.ID);
      parameters.Arguments.Add("StatTypeID", type);
      parameters.Arguments.Add("Created", dateCompare);
      return this.Load(connection, parameters);
    }

  }
}

