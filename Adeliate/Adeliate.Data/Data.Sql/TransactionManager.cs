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
  public partial class TransactionManager : ITransactionManager
  {

    public List<Transaction> Load(Click click)
    {
      using (SqlConnectionInfo connection = new SqlConnectionInfo(this.Type))
        return this.Load(connection, click);
    }

    public List<Transaction> Load(IConnectionInfo connection, Click click)
    {
      ISqlConnectionInfo sqlConnection = connection as ISqlConnectionInfo;
      if (sqlConnection != null)
        return this.Load(sqlConnection, click);
      using (sqlConnection = new SqlConnectionInfo(connection, this.Type))
        return this.Load(sqlConnection, click);
    }

    public List<Transaction> Load(ISqlConnectionInfo connection, Click click)
    {
      SqlQueryParameters parameters = new SqlQueryParameters();
      parameters.Where = "[t].ClickID=@ClickID";
      parameters.Arguments.Add("ClickID", click.ID);
      return this.LoadMany(connection, parameters);
    }

  }
}

