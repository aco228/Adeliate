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
  public partial class CustomerImageMapManager : ICustomerImageMapManager
  {
    public CustomerImageMap Load(Customer customer)
    {
      using (SqlConnectionInfo connection = new SqlConnectionInfo(this.Type))
        return this.Load(connection, customer);
    }

    public CustomerImageMap Load(IConnectionInfo connection, Customer customer)
    {
      ISqlConnectionInfo sqlConnection = connection as ISqlConnectionInfo;
      if (sqlConnection != null)
        return this.Load(sqlConnection, customer);
      using (sqlConnection = new SqlConnectionInfo(connection, this.Type))
        return this.Load(sqlConnection, customer);
    }

    public CustomerImageMap Load(ISqlConnectionInfo connection, Customer customer)
    {
      SqlQueryParameters parameters = new SqlQueryParameters();
      parameters.Where = "[cim].CustomerID =  @CustomerID";
      //parameters.OrderBy = "abbr.PrimaryKeyColumnID DESC";
      parameters.Arguments.Add("CustomerID", customer.ID);
      return this.Load(connection, parameters);
      //return this.LoadMany(connection, parameters);
    }



  }
}

