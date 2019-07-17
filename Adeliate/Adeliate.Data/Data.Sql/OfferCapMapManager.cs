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
  public partial class OfferCapMapManager : IOfferCapMapManager
  {

    public OfferCapMap Load(Offer offer)
    {
      using (SqlConnectionInfo connection = new SqlConnectionInfo(this.Type))
        return this.Load(connection, offer);
    }

    public OfferCapMap Load(IConnectionInfo connection, Offer offer)
    {
      ISqlConnectionInfo sqlConnection = connection as ISqlConnectionInfo;
      if (sqlConnection != null)
        return this.Load(sqlConnection, offer);
      using (sqlConnection = new SqlConnectionInfo(connection, this.Type))
        return this.Load(sqlConnection, offer);
    }

    public OfferCapMap Load(ISqlConnectionInfo connection, Offer offer)
    {
      SqlQueryParameters parameters = new SqlQueryParameters();

      DateTime referenceTime = DateTime.Now.Date;
      parameters.Where = "[ocm].OfferID=@OfferID AND [ocm].Created>@Created";
      parameters.Arguments.Add("OfferID", offer.ID);
      parameters.Arguments.Add("Created", referenceTime);
      return this.Load(connection, parameters);
    }


  }
}

