using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti;
using Senti.Diagnostics.Log;
using Senti.Data;
using Senti.Data.Sql;

using Adlite.Data;
using Adlite.Data.Sql;



namespace Adlite.Data.Sql
{
  [DataManager(typeof(CustomerSession))] 
  public partial class CustomerSessionManager : Adlite.Data.Sql.SqlManagerBase<CustomerSession>, ICustomerSessionManager
  {
    public override DatabaseType Type
    {
      get { return DatabaseType.Adlite; }
    }

    protected override CustomerSession LoadInternal(ISqlConnectionInfo connection, int id)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "SELECT " + 
							CustomerSessionTable.GetColumnNames("[cs]") + 
							(this.Depth > 0 ? "," + CustomerTable.GetColumnNames("[cs_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTable.GetColumnNames("[cs_c_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + CustomerTypeTable.GetColumnNames("[cs_c_ct]") : string.Empty) + 
					" FROM [core].[CustomerSession] AS [cs] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Customer] AS [cs_c] ON [cs].[CustomerID] = [cs_c].[CustomerID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Affiliate] AS [cs_c_a] ON [cs_c].[AffiliateID] = [cs_c_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[CustomerType] AS [cs_c_ct] ON [cs_c].[CustomerTypeID] = [cs_c_ct].[CustomerTypeID] ";
				sqlCmdText += "WHERE [cs].[CustomerSessionID] = @CustomerSessionID;";

        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@CustomerSessionID", id);
        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cs", "loadinternal", "notfound"), "CustomerSession could not be loaded by id as it was not found.", sqlCmdText, this, connection, id);
          if (this.Logger.IsWarnEnabled)
            this.Logger.Warn(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				CustomerSessionTable csTable = new CustomerSessionTable(query);
				CustomerTable cs_cTable = (this.Depth > 0) ? new CustomerTable(query) : null;
				AffiliateTable cs_c_aTable = (this.Depth > 1) ? new AffiliateTable(query) : null;
				CustomerTypeTable cs_c_ctTable = (this.Depth > 1) ? new CustomerTypeTable(query) : null;

        
				Affiliate cs_c_aObject = (this.Depth > 1) ? cs_c_aTable.CreateInstance() : null;
				CustomerType cs_c_ctObject = (this.Depth > 1) ? cs_c_ctTable.CreateInstance() : null;
				Customer cs_cObject = (this.Depth > 0) ? cs_cTable.CreateInstance(cs_c_aObject, cs_c_ctObject) : null;
				CustomerSession csObject = csTable.CreateInstance(cs_cObject);
				sqlReader.Close();

				return csObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cs", "loadinternal", "exception"), "CustomerSession could not be loaded by id. See exception for details.", sqlCmdText, ex, this, connection, id);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "CustomerSession", "Exception while loading CustomerSession object from database. See inner exception for details.", ex);
      }
    }

    public CustomerSession Load(ISqlConnectionInfo connection, SqlQueryParameters parameters)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (parameters == null)
        throw new ArgumentNullException("parameters");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "SELECT {0} " + 
							CustomerSessionTable.GetColumnNames("[cs]") + 
							(this.Depth > 0 ? "," + CustomerTable.GetColumnNames("[cs_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTable.GetColumnNames("[cs_c_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + CustomerTypeTable.GetColumnNames("[cs_c_ct]") : string.Empty) +  
					" FROM [core].[CustomerSession] AS [cs] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Customer] AS [cs_c] ON [cs].[CustomerID] = [cs_c].[CustomerID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Affiliate] AS [cs_c_a] ON [cs_c].[AffiliateID] = [cs_c_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[CustomerType] AS [cs_c_ct] ON [cs_c].[CustomerTypeID] = [cs_c_ct].[CustomerTypeID] ";
				

        parameters.Top = 1;
        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cs", "customload", "notfound"), "CustomerSession could not be loaded using custom logic as it was not found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				CustomerSessionTable csTable = new CustomerSessionTable(query);
				CustomerTable cs_cTable = (this.Depth > 0) ? new CustomerTable(query) : null;
				AffiliateTable cs_c_aTable = (this.Depth > 1) ? new AffiliateTable(query) : null;
				CustomerTypeTable cs_c_ctTable = (this.Depth > 1) ? new CustomerTypeTable(query) : null;

        
				Affiliate cs_c_aObject = (this.Depth > 1) ? cs_c_aTable.CreateInstance() : null;
				CustomerType cs_c_ctObject = (this.Depth > 1) ? cs_c_ctTable.CreateInstance() : null;
				Customer cs_cObject = (this.Depth > 0) ? cs_cTable.CreateInstance(cs_c_aObject, cs_c_ctObject) : null;
				CustomerSession csObject = csTable.CreateInstance(cs_cObject);
				sqlReader.Close();

				return csObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cs", "customload", "exception"), "CustomerSession could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "CustomerSession", "Exception while loading (custom/single) CustomerSession object from database. See inner exception for details.", ex);
      }
    }

    public List<CustomerSession> LoadMany(ISqlConnectionInfo connection, SqlQueryParameters parameters)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (parameters == null)
        throw new ArgumentNullException("parameters");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "SELECT {0} " + 
							CustomerSessionTable.GetColumnNames("[cs]") + 
							(this.Depth > 0 ? "," + CustomerTable.GetColumnNames("[cs_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTable.GetColumnNames("[cs_c_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + CustomerTypeTable.GetColumnNames("[cs_c_ct]") : string.Empty) +  
					" FROM [core].[CustomerSession] AS [cs] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Customer] AS [cs_c] ON [cs].[CustomerID] = [cs_c].[CustomerID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Affiliate] AS [cs_c_a] ON [cs_c].[AffiliateID] = [cs_c_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[CustomerType] AS [cs_c_ct] ON [cs_c].[CustomerTypeID] = [cs_c_ct].[CustomerTypeID] ";
				

        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cs", "customloadmany", "notfound"), "CustomerSession list could not be loaded using custom logic as no items were found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return new List<CustomerSession>();
        }

        SqlQuery query = new SqlQuery(sqlReader);

				CustomerSessionTable csTable = new CustomerSessionTable(query);
				CustomerTable cs_cTable = (this.Depth > 0) ? new CustomerTable(query) : null;
				AffiliateTable cs_c_aTable = (this.Depth > 1) ? new AffiliateTable(query) : null;
				CustomerTypeTable cs_c_ctTable = (this.Depth > 1) ? new CustomerTypeTable(query) : null;

        List<CustomerSession> result = new List<CustomerSession>();
        do
        {
          
					Affiliate cs_c_aObject = (this.Depth > 1) ? cs_c_aTable.CreateInstance() : null;
					CustomerType cs_c_ctObject = (this.Depth > 1) ? cs_c_ctTable.CreateInstance() : null;
					Customer cs_cObject = (this.Depth > 0) ? cs_cTable.CreateInstance(cs_c_aObject, cs_c_ctObject) : null;
					CustomerSession csObject = (this.Depth > -1) ? csTable.CreateInstance(cs_cObject) : null;
					result.Add(csObject);

        } while (sqlReader.Read());
        sqlReader.Close();

        return result;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cs", "customloadmany", "exception"), "CustomerSession list could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "CustomerSession", "Exception while loading (custom/many) CustomerSession object from database. See inner exception for details.", ex);
      }
    }
    
    public override int? Insert(ISqlConnectionInfo connection, CustomerSession data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "INSERT INTO [core].[CustomerSession] ([CustomerSessionGuid],[CustomerID],[IPAddress],[UserAgent],[ValidUntil]) VALUES(@CustomerSessionGuid,@CustomerID,@IPAddress,@UserAgent,@ValidUntil); SELECT SCOPE_IDENTITY();";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@CustomerSessionGuid", data.Guid);
				sqlCmd.Parameters.AddWithValue("@CustomerID", data.Customer == null ? DBNull.Value : (object)data.Customer.ID);
				sqlCmd.Parameters.AddWithValue("@IPAddress", data.IPAddress).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@UserAgent", data.UserAgent).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@ValidUntil", data.ValidUntil).SqlDbType = SqlDbType.DateTime2;
				
        object idObj = sqlCmd.ExecuteScalar();
        if (idObj == null || DBNull.Value.Equals(idObj))
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cs", "insert", "noprimarykey"), "CustomerSession could not be inserted or inserted primary key was not returned. Are you missing SELECT SCOPE_IDENTITY();?", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Insert, "CustomerSession", "Exception while inserting CustomerSession object in database.");
        }
        return (int)((decimal)idObj);
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cs", "insert", "exception"), "CustomerSession could not be inserted. See exception for details.", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Insert, "CustomerSession", "Exception while inserting CustomerSession object in database. See inner exception for details.", ex);
      }
    }

    public override bool Update(ISqlConnectionInfo connection, CustomerSession data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        data.Updated = DateTime.Now;
        sqlCmdText = "UPDATE [core].[CustomerSession] SET " +
												"[CustomerSessionGuid] = @CustomerSessionGuid, " + 
												"[CustomerID] = @CustomerID, " + 
												"[IPAddress] = @IPAddress, " + 
												"[UserAgent] = @UserAgent, " + 
												"[ValidUntil] = @ValidUntil, " + 
												"[Updated] = GETDATE() " + 
											"WHERE [CustomerSessionID] = @CustomerSessionID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@CustomerSessionGuid", data.Guid);
				sqlCmd.Parameters.AddWithValue("@CustomerID", data.Customer == null ? DBNull.Value : (object)data.Customer.ID);
				sqlCmd.Parameters.AddWithValue("@IPAddress", data.IPAddress).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@UserAgent", data.UserAgent).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@ValidUntil", data.ValidUntil).SqlDbType = SqlDbType.DateTime2;
				sqlCmd.Parameters.AddWithValue("@Updated", data.Updated).SqlDbType = SqlDbType.DateTime2;
				sqlCmd.Parameters.AddWithValue("@CustomerSessionID", data.ID);

        int rowCount = sqlCmd.ExecuteNonQuery();
        if (rowCount < 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cs", "update", "norecord"), "CustomerSession could not be updated as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "CustomerSession", "Exception while updating CustomerSession object in database. No record found for this id.");
        }
        else if (rowCount > 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cs", "update", "morerecords"), "CustomerSession was updated but there was more than one record affected.", sqlCmdText, this, connection, data);
          if (this.Logger.IsFatalEnabled)
            this.Logger.Fatal(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "CustomerSession", "Exception while updating CustomerSession object in database. More than one record found for this statement (update statement where clause broken?!).");
        }
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cs", "update", "exception"), "CustomerSession could not be updated. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Update, "CustomerSession", "Exception while updating CustomerSession object in database. See inner exception for details.", ex);
      }
    }

    public override bool Delete(ISqlConnectionInfo connection, CustomerSession data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText =  "DELETE FROM [core].[CustomerSession] WHERE CustomerSessionID = @CustomerSessionID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@CustomerSessionID", data.ID);

        int success = sqlCmd.ExecuteNonQuery();

        if (success == -1)        
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cs", "delete", "norecord"), "CustomerSession could not be deleted as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Delete, "CustomerSession", "Exception while deleting CustomerSession object from database. No such record found.");
        }
        
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cs", "delete", "exception"), "CustomerSession could not be deleted. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Delete, "CustomerSession", "Exception while deleting CustomerSession object from database. See inner exception for details.", ex);
      }
    }
  }
}

