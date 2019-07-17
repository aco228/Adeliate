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
  [DataManager(typeof(Customer))] 
  public partial class CustomerManager : Adlite.Data.Sql.SqlManagerBase<Customer>, ICustomerManager
  {
    public override DatabaseType Type
    {
      get { return DatabaseType.Adlite; }
    }

    protected override Customer LoadInternal(ISqlConnectionInfo connection, int id)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "SELECT " + 
							CustomerTable.GetColumnNames("[c]") + 
							(this.Depth > 0 ? "," + AffiliateTable.GetColumnNames("[c_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTypeTable.GetColumnNames("[c_a_at]") : string.Empty) + 
							(this.Depth > 0 ? "," + CustomerTypeTable.GetColumnNames("[c_ct]") : string.Empty) + 
					" FROM [core].[Customer] AS [c] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Affiliate] AS [c_a] ON [c].[AffiliateID] = [c_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[AffiliateType] AS [c_a_at] ON [c_a].[AffiliateTypeID] = [c_a_at].[AffiliateTypeID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[CustomerType] AS [c_ct] ON [c].[CustomerTypeID] = [c_ct].[CustomerTypeID] ";
				sqlCmdText += "WHERE [c].[CustomerID] = @CustomerID;";

        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@CustomerID", id);
        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "loadinternal", "notfound"), "Customer could not be loaded by id as it was not found.", sqlCmdText, this, connection, id);
          if (this.Logger.IsWarnEnabled)
            this.Logger.Warn(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				CustomerTable cTable = new CustomerTable(query);
				AffiliateTable c_aTable = (this.Depth > 0) ? new AffiliateTable(query) : null;
				AffiliateTypeTable c_a_atTable = (this.Depth > 1) ? new AffiliateTypeTable(query) : null;
				CustomerTypeTable c_ctTable = (this.Depth > 0) ? new CustomerTypeTable(query) : null;

        
				AffiliateType c_a_atObject = (this.Depth > 1) ? c_a_atTable.CreateInstance() : null;
				Affiliate c_aObject = (this.Depth > 0) ? c_aTable.CreateInstance(c_a_atObject) : null;
				CustomerType c_ctObject = (this.Depth > 0) ? c_ctTable.CreateInstance() : null;
				Customer cObject = cTable.CreateInstance(c_aObject, c_ctObject);
				sqlReader.Close();

				return cObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "loadinternal", "exception"), "Customer could not be loaded by id. See exception for details.", sqlCmdText, ex, this, connection, id);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "Customer", "Exception while loading Customer object from database. See inner exception for details.", ex);
      }
    }

    public Customer Load(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							CustomerTable.GetColumnNames("[c]") + 
							(this.Depth > 0 ? "," + AffiliateTable.GetColumnNames("[c_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTypeTable.GetColumnNames("[c_a_at]") : string.Empty) + 
							(this.Depth > 0 ? "," + CustomerTypeTable.GetColumnNames("[c_ct]") : string.Empty) +  
					" FROM [core].[Customer] AS [c] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Affiliate] AS [c_a] ON [c].[AffiliateID] = [c_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[AffiliateType] AS [c_a_at] ON [c_a].[AffiliateTypeID] = [c_a_at].[AffiliateTypeID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[CustomerType] AS [c_ct] ON [c].[CustomerTypeID] = [c_ct].[CustomerTypeID] ";
				

        parameters.Top = 1;
        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "customload", "notfound"), "Customer could not be loaded using custom logic as it was not found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				CustomerTable cTable = new CustomerTable(query);
				AffiliateTable c_aTable = (this.Depth > 0) ? new AffiliateTable(query) : null;
				AffiliateTypeTable c_a_atTable = (this.Depth > 1) ? new AffiliateTypeTable(query) : null;
				CustomerTypeTable c_ctTable = (this.Depth > 0) ? new CustomerTypeTable(query) : null;

        
				AffiliateType c_a_atObject = (this.Depth > 1) ? c_a_atTable.CreateInstance() : null;
				Affiliate c_aObject = (this.Depth > 0) ? c_aTable.CreateInstance(c_a_atObject) : null;
				CustomerType c_ctObject = (this.Depth > 0) ? c_ctTable.CreateInstance() : null;
				Customer cObject = cTable.CreateInstance(c_aObject, c_ctObject);
				sqlReader.Close();

				return cObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "customload", "exception"), "Customer could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "Customer", "Exception while loading (custom/single) Customer object from database. See inner exception for details.", ex);
      }
    }

    public List<Customer> LoadMany(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							CustomerTable.GetColumnNames("[c]") + 
							(this.Depth > 0 ? "," + AffiliateTable.GetColumnNames("[c_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTypeTable.GetColumnNames("[c_a_at]") : string.Empty) + 
							(this.Depth > 0 ? "," + CustomerTypeTable.GetColumnNames("[c_ct]") : string.Empty) +  
					" FROM [core].[Customer] AS [c] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Affiliate] AS [c_a] ON [c].[AffiliateID] = [c_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[AffiliateType] AS [c_a_at] ON [c_a].[AffiliateTypeID] = [c_a_at].[AffiliateTypeID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[CustomerType] AS [c_ct] ON [c].[CustomerTypeID] = [c_ct].[CustomerTypeID] ";
				

        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "customloadmany", "notfound"), "Customer list could not be loaded using custom logic as no items were found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return new List<Customer>();
        }

        SqlQuery query = new SqlQuery(sqlReader);

				CustomerTable cTable = new CustomerTable(query);
				AffiliateTable c_aTable = (this.Depth > 0) ? new AffiliateTable(query) : null;
				AffiliateTypeTable c_a_atTable = (this.Depth > 1) ? new AffiliateTypeTable(query) : null;
				CustomerTypeTable c_ctTable = (this.Depth > 0) ? new CustomerTypeTable(query) : null;

        List<Customer> result = new List<Customer>();
        do
        {
          
					AffiliateType c_a_atObject = (this.Depth > 1) ? c_a_atTable.CreateInstance() : null;
					Affiliate c_aObject = (this.Depth > 0) ? c_aTable.CreateInstance(c_a_atObject) : null;
					CustomerType c_ctObject = (this.Depth > 0) ? c_ctTable.CreateInstance() : null;
					Customer cObject = (this.Depth > -1) ? cTable.CreateInstance(c_aObject, c_ctObject) : null;
					result.Add(cObject);

        } while (sqlReader.Read());
        sqlReader.Close();

        return result;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "customloadmany", "exception"), "Customer list could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "Customer", "Exception while loading (custom/many) Customer object from database. See inner exception for details.", ex);
      }
    }
    
    public override int? Insert(ISqlConnectionInfo connection, Customer data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "INSERT INTO [core].[Customer] ([AffiliateID],[CustomerTypeID],[CustomerStatusID],[Username],[Password],[IsActive]) VALUES(@AffiliateID,@CustomerTypeID,@CustomerStatusID,@Username,@Password,@IsActive); SELECT SCOPE_IDENTITY();";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@AffiliateID", data.Affiliate.ID);
				sqlCmd.Parameters.AddWithValue("@CustomerTypeID", data.CustomerType.ID);
				sqlCmd.Parameters.AddWithValue("@CustomerStatusID", (int)data.CustomerStatus);
				sqlCmd.Parameters.AddWithValue("@Username", data.Username).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@Password", data.Password).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@IsActive", data.IsActive).SqlDbType = SqlDbType.Bit;
				
        object idObj = sqlCmd.ExecuteScalar();
        if (idObj == null || DBNull.Value.Equals(idObj))
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "insert", "noprimarykey"), "Customer could not be inserted or inserted primary key was not returned. Are you missing SELECT SCOPE_IDENTITY();?", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Insert, "Customer", "Exception while inserting Customer object in database.");
        }
        return (int)((decimal)idObj);
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "insert", "exception"), "Customer could not be inserted. See exception for details.", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Insert, "Customer", "Exception while inserting Customer object in database. See inner exception for details.", ex);
      }
    }

    public override bool Update(ISqlConnectionInfo connection, Customer data)
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
        sqlCmdText = "UPDATE [core].[Customer] SET " +
												"[AffiliateID] = @AffiliateID, " + 
												"[CustomerTypeID] = @CustomerTypeID, " + 
												"[CustomerStatusID] = @CustomerStatusID, " + 
												"[Username] = @Username, " + 
												"[Password] = @Password, " + 
												"[IsActive] = @IsActive, " + 
												"[Updated] = GETDATE() " + 
											"WHERE [CustomerID] = @CustomerID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@AffiliateID", data.Affiliate.ID);
				sqlCmd.Parameters.AddWithValue("@CustomerTypeID", data.CustomerType.ID);
				sqlCmd.Parameters.AddWithValue("@CustomerStatusID", (int)data.CustomerStatus);
				sqlCmd.Parameters.AddWithValue("@Username", data.Username).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@Password", data.Password).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@IsActive", data.IsActive).SqlDbType = SqlDbType.Bit;
				sqlCmd.Parameters.AddWithValue("@Updated", data.Updated).SqlDbType = SqlDbType.DateTime2;
				sqlCmd.Parameters.AddWithValue("@CustomerID", data.ID);

        int rowCount = sqlCmd.ExecuteNonQuery();
        if (rowCount < 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "update", "norecord"), "Customer could not be updated as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "Customer", "Exception while updating Customer object in database. No record found for this id.");
        }
        else if (rowCount > 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "update", "morerecords"), "Customer was updated but there was more than one record affected.", sqlCmdText, this, connection, data);
          if (this.Logger.IsFatalEnabled)
            this.Logger.Fatal(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "Customer", "Exception while updating Customer object in database. More than one record found for this statement (update statement where clause broken?!).");
        }
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "update", "exception"), "Customer could not be updated. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Update, "Customer", "Exception while updating Customer object in database. See inner exception for details.", ex);
      }
    }

    public override bool Delete(ISqlConnectionInfo connection, Customer data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText =  "DELETE FROM [core].[Customer] WHERE CustomerID = @CustomerID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@CustomerID", data.ID);

        int success = sqlCmd.ExecuteNonQuery();

        if (success == -1)        
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "delete", "norecord"), "Customer could not be deleted as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Delete, "Customer", "Exception while deleting Customer object from database. No such record found.");
        }
        
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "delete", "exception"), "Customer could not be deleted. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Delete, "Customer", "Exception while deleting Customer object from database. See inner exception for details.", ex);
      }
    }
  }
}

