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
  [DataManager(typeof(CustomerImageMap))] 
  public partial class CustomerImageMapManager : Adlite.Data.Sql.SqlManagerBase<CustomerImageMap>, ICustomerImageMapManager
  {
    public override DatabaseType Type
    {
      get { return DatabaseType.Adlite; }
    }

    protected override CustomerImageMap LoadInternal(ISqlConnectionInfo connection, int id)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "SELECT " + 
							CustomerImageMapTable.GetColumnNames("[cim]") + 
							(this.Depth > 0 ? "," + CustomerTable.GetColumnNames("[cim_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTable.GetColumnNames("[cim_c_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + CustomerTypeTable.GetColumnNames("[cim_c_ct]") : string.Empty) + 
							(this.Depth > 0 ? "," + ImageTable.GetColumnNames("[cim_i]") : string.Empty) + 
					" FROM [core].[CustomerImageMap] AS [cim] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Customer] AS [cim_c] ON [cim].[CustomerID] = [cim_c].[CustomerID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Affiliate] AS [cim_c_a] ON [cim_c].[AffiliateID] = [cim_c_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[CustomerType] AS [cim_c_ct] ON [cim_c].[CustomerTypeID] = [cim_c_ct].[CustomerTypeID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Image] AS [cim_i] ON [cim].[ImageID] = [cim_i].[ImageID] ";
				sqlCmdText += "WHERE [cim].[CustomerImageMapID] = @CustomerImageMapID;";

        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@CustomerImageMapID", id);
        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cim", "loadinternal", "notfound"), "CustomerImageMap could not be loaded by id as it was not found.", sqlCmdText, this, connection, id);
          if (this.Logger.IsWarnEnabled)
            this.Logger.Warn(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				CustomerImageMapTable cimTable = new CustomerImageMapTable(query);
				CustomerTable cim_cTable = (this.Depth > 0) ? new CustomerTable(query) : null;
				AffiliateTable cim_c_aTable = (this.Depth > 1) ? new AffiliateTable(query) : null;
				CustomerTypeTable cim_c_ctTable = (this.Depth > 1) ? new CustomerTypeTable(query) : null;
				ImageTable cim_iTable = (this.Depth > 0) ? new ImageTable(query) : null;

        
				Affiliate cim_c_aObject = (this.Depth > 1) ? cim_c_aTable.CreateInstance() : null;
				CustomerType cim_c_ctObject = (this.Depth > 1) ? cim_c_ctTable.CreateInstance() : null;
				Customer cim_cObject = (this.Depth > 0) ? cim_cTable.CreateInstance(cim_c_aObject, cim_c_ctObject) : null;
				Image cim_iObject = (this.Depth > 0) ? cim_iTable.CreateInstance() : null;
				CustomerImageMap cimObject = cimTable.CreateInstance(cim_cObject, cim_iObject);
				sqlReader.Close();

				return cimObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cim", "loadinternal", "exception"), "CustomerImageMap could not be loaded by id. See exception for details.", sqlCmdText, ex, this, connection, id);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "CustomerImageMap", "Exception while loading CustomerImageMap object from database. See inner exception for details.", ex);
      }
    }

    public CustomerImageMap Load(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							CustomerImageMapTable.GetColumnNames("[cim]") + 
							(this.Depth > 0 ? "," + CustomerTable.GetColumnNames("[cim_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTable.GetColumnNames("[cim_c_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + CustomerTypeTable.GetColumnNames("[cim_c_ct]") : string.Empty) + 
							(this.Depth > 0 ? "," + ImageTable.GetColumnNames("[cim_i]") : string.Empty) +  
					" FROM [core].[CustomerImageMap] AS [cim] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Customer] AS [cim_c] ON [cim].[CustomerID] = [cim_c].[CustomerID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Affiliate] AS [cim_c_a] ON [cim_c].[AffiliateID] = [cim_c_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[CustomerType] AS [cim_c_ct] ON [cim_c].[CustomerTypeID] = [cim_c_ct].[CustomerTypeID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Image] AS [cim_i] ON [cim].[ImageID] = [cim_i].[ImageID] ";
				

        parameters.Top = 1;
        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cim", "customload", "notfound"), "CustomerImageMap could not be loaded using custom logic as it was not found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				CustomerImageMapTable cimTable = new CustomerImageMapTable(query);
				CustomerTable cim_cTable = (this.Depth > 0) ? new CustomerTable(query) : null;
				AffiliateTable cim_c_aTable = (this.Depth > 1) ? new AffiliateTable(query) : null;
				CustomerTypeTable cim_c_ctTable = (this.Depth > 1) ? new CustomerTypeTable(query) : null;
				ImageTable cim_iTable = (this.Depth > 0) ? new ImageTable(query) : null;

        
				Affiliate cim_c_aObject = (this.Depth > 1) ? cim_c_aTable.CreateInstance() : null;
				CustomerType cim_c_ctObject = (this.Depth > 1) ? cim_c_ctTable.CreateInstance() : null;
				Customer cim_cObject = (this.Depth > 0) ? cim_cTable.CreateInstance(cim_c_aObject, cim_c_ctObject) : null;
				Image cim_iObject = (this.Depth > 0) ? cim_iTable.CreateInstance() : null;
				CustomerImageMap cimObject = cimTable.CreateInstance(cim_cObject, cim_iObject);
				sqlReader.Close();

				return cimObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cim", "customload", "exception"), "CustomerImageMap could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "CustomerImageMap", "Exception while loading (custom/single) CustomerImageMap object from database. See inner exception for details.", ex);
      }
    }

    public List<CustomerImageMap> LoadMany(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							CustomerImageMapTable.GetColumnNames("[cim]") + 
							(this.Depth > 0 ? "," + CustomerTable.GetColumnNames("[cim_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTable.GetColumnNames("[cim_c_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + CustomerTypeTable.GetColumnNames("[cim_c_ct]") : string.Empty) + 
							(this.Depth > 0 ? "," + ImageTable.GetColumnNames("[cim_i]") : string.Empty) +  
					" FROM [core].[CustomerImageMap] AS [cim] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Customer] AS [cim_c] ON [cim].[CustomerID] = [cim_c].[CustomerID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Affiliate] AS [cim_c_a] ON [cim_c].[AffiliateID] = [cim_c_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[CustomerType] AS [cim_c_ct] ON [cim_c].[CustomerTypeID] = [cim_c_ct].[CustomerTypeID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Image] AS [cim_i] ON [cim].[ImageID] = [cim_i].[ImageID] ";
				

        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cim", "customloadmany", "notfound"), "CustomerImageMap list could not be loaded using custom logic as no items were found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return new List<CustomerImageMap>();
        }

        SqlQuery query = new SqlQuery(sqlReader);

				CustomerImageMapTable cimTable = new CustomerImageMapTable(query);
				CustomerTable cim_cTable = (this.Depth > 0) ? new CustomerTable(query) : null;
				AffiliateTable cim_c_aTable = (this.Depth > 1) ? new AffiliateTable(query) : null;
				CustomerTypeTable cim_c_ctTable = (this.Depth > 1) ? new CustomerTypeTable(query) : null;
				ImageTable cim_iTable = (this.Depth > 0) ? new ImageTable(query) : null;

        List<CustomerImageMap> result = new List<CustomerImageMap>();
        do
        {
          
					Affiliate cim_c_aObject = (this.Depth > 1) ? cim_c_aTable.CreateInstance() : null;
					CustomerType cim_c_ctObject = (this.Depth > 1) ? cim_c_ctTable.CreateInstance() : null;
					Customer cim_cObject = (this.Depth > 0) ? cim_cTable.CreateInstance(cim_c_aObject, cim_c_ctObject) : null;
					Image cim_iObject = (this.Depth > 0) ? cim_iTable.CreateInstance() : null;
					CustomerImageMap cimObject = (this.Depth > -1) ? cimTable.CreateInstance(cim_cObject, cim_iObject) : null;
					result.Add(cimObject);

        } while (sqlReader.Read());
        sqlReader.Close();

        return result;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cim", "customloadmany", "exception"), "CustomerImageMap list could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "CustomerImageMap", "Exception while loading (custom/many) CustomerImageMap object from database. See inner exception for details.", ex);
      }
    }
    
    public override int? Insert(ISqlConnectionInfo connection, CustomerImageMap data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "INSERT INTO [core].[CustomerImageMap] ([CustomerID],[ImageID]) VALUES(@CustomerID,@ImageID); SELECT SCOPE_IDENTITY();";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@CustomerID", data.Customer.ID);
				sqlCmd.Parameters.AddWithValue("@ImageID", data.Image.ID);
				
        object idObj = sqlCmd.ExecuteScalar();
        if (idObj == null || DBNull.Value.Equals(idObj))
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cim", "insert", "noprimarykey"), "CustomerImageMap could not be inserted or inserted primary key was not returned. Are you missing SELECT SCOPE_IDENTITY();?", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Insert, "CustomerImageMap", "Exception while inserting CustomerImageMap object in database.");
        }
        return (int)((decimal)idObj);
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cim", "insert", "exception"), "CustomerImageMap could not be inserted. See exception for details.", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Insert, "CustomerImageMap", "Exception while inserting CustomerImageMap object in database. See inner exception for details.", ex);
      }
    }

    public override bool Update(ISqlConnectionInfo connection, CustomerImageMap data)
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
        sqlCmdText = "UPDATE [core].[CustomerImageMap] SET " +
												"[CustomerID] = @CustomerID, " + 
												"[ImageID] = @ImageID, " + 
												"[Updated] = GETDATE() " + 
											"WHERE [CustomerImageMapID] = @CustomerImageMapID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@CustomerID", data.Customer.ID);
				sqlCmd.Parameters.AddWithValue("@ImageID", data.Image.ID);
				sqlCmd.Parameters.AddWithValue("@Updated", data.Updated).SqlDbType = SqlDbType.DateTime2;
				sqlCmd.Parameters.AddWithValue("@CustomerImageMapID", data.ID);

        int rowCount = sqlCmd.ExecuteNonQuery();
        if (rowCount < 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cim", "update", "norecord"), "CustomerImageMap could not be updated as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "CustomerImageMap", "Exception while updating CustomerImageMap object in database. No record found for this id.");
        }
        else if (rowCount > 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cim", "update", "morerecords"), "CustomerImageMap was updated but there was more than one record affected.", sqlCmdText, this, connection, data);
          if (this.Logger.IsFatalEnabled)
            this.Logger.Fatal(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "CustomerImageMap", "Exception while updating CustomerImageMap object in database. More than one record found for this statement (update statement where clause broken?!).");
        }
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cim", "update", "exception"), "CustomerImageMap could not be updated. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Update, "CustomerImageMap", "Exception while updating CustomerImageMap object in database. See inner exception for details.", ex);
      }
    }

    public override bool Delete(ISqlConnectionInfo connection, CustomerImageMap data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText =  "DELETE FROM [core].[CustomerImageMap] WHERE CustomerImageMapID = @CustomerImageMapID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@CustomerImageMapID", data.ID);

        int success = sqlCmd.ExecuteNonQuery();

        if (success == -1)        
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cim", "delete", "norecord"), "CustomerImageMap could not be deleted as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Delete, "CustomerImageMap", "Exception while deleting CustomerImageMap object from database. No such record found.");
        }
        
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cim", "delete", "exception"), "CustomerImageMap could not be deleted. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Delete, "CustomerImageMap", "Exception while deleting CustomerImageMap object from database. See inner exception for details.", ex);
      }
    }
  }
}

