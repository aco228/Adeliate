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
  [DataManager(typeof(Affiliate))] 
  public partial class AffiliateManager : Adlite.Data.Sql.SqlManagerBase<Affiliate>, IAffiliateManager
  {
    public override DatabaseType Type
    {
      get { return DatabaseType.Adlite; }
    }

    protected override Affiliate LoadInternal(ISqlConnectionInfo connection, int id)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "SELECT " + 
							AffiliateTable.GetColumnNames("[a]") + 
							(this.Depth > 0 ? "," + AffiliateTypeTable.GetColumnNames("[a_at]") : string.Empty) + 
					" FROM [core].[Affiliate] AS [a] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[AffiliateType] AS [a_at] ON [a].[AffiliateTypeID] = [a_at].[AffiliateTypeID] ";
				sqlCmdText += "WHERE [a].[AffiliateID] = @AffiliateID;";

        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@AffiliateID", id);
        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("a", "loadinternal", "notfound"), "Affiliate could not be loaded by id as it was not found.", sqlCmdText, this, connection, id);
          if (this.Logger.IsWarnEnabled)
            this.Logger.Warn(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				AffiliateTable aTable = new AffiliateTable(query);
				AffiliateTypeTable a_atTable = (this.Depth > 0) ? new AffiliateTypeTable(query) : null;

        
				AffiliateType a_atObject = (this.Depth > 0) ? a_atTable.CreateInstance() : null;
				Affiliate aObject = aTable.CreateInstance(a_atObject);
				sqlReader.Close();

				return aObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("a", "loadinternal", "exception"), "Affiliate could not be loaded by id. See exception for details.", sqlCmdText, ex, this, connection, id);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "Affiliate", "Exception while loading Affiliate object from database. See inner exception for details.", ex);
      }
    }

    public Affiliate Load(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							AffiliateTable.GetColumnNames("[a]") + 
							(this.Depth > 0 ? "," + AffiliateTypeTable.GetColumnNames("[a_at]") : string.Empty) +  
					" FROM [core].[Affiliate] AS [a] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[AffiliateType] AS [a_at] ON [a].[AffiliateTypeID] = [a_at].[AffiliateTypeID] ";
				

        parameters.Top = 1;
        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("a", "customload", "notfound"), "Affiliate could not be loaded using custom logic as it was not found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				AffiliateTable aTable = new AffiliateTable(query);
				AffiliateTypeTable a_atTable = (this.Depth > 0) ? new AffiliateTypeTable(query) : null;

        
				AffiliateType a_atObject = (this.Depth > 0) ? a_atTable.CreateInstance() : null;
				Affiliate aObject = aTable.CreateInstance(a_atObject);
				sqlReader.Close();

				return aObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("a", "customload", "exception"), "Affiliate could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "Affiliate", "Exception while loading (custom/single) Affiliate object from database. See inner exception for details.", ex);
      }
    }

    public List<Affiliate> LoadMany(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							AffiliateTable.GetColumnNames("[a]") + 
							(this.Depth > 0 ? "," + AffiliateTypeTable.GetColumnNames("[a_at]") : string.Empty) +  
					" FROM [core].[Affiliate] AS [a] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[AffiliateType] AS [a_at] ON [a].[AffiliateTypeID] = [a_at].[AffiliateTypeID] ";
				

        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("a", "customloadmany", "notfound"), "Affiliate list could not be loaded using custom logic as no items were found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return new List<Affiliate>();
        }

        SqlQuery query = new SqlQuery(sqlReader);

				AffiliateTable aTable = new AffiliateTable(query);
				AffiliateTypeTable a_atTable = (this.Depth > 0) ? new AffiliateTypeTable(query) : null;

        List<Affiliate> result = new List<Affiliate>();
        do
        {
          
					AffiliateType a_atObject = (this.Depth > 0) ? a_atTable.CreateInstance() : null;
					Affiliate aObject = (this.Depth > -1) ? aTable.CreateInstance(a_atObject) : null;
					result.Add(aObject);

        } while (sqlReader.Read());
        sqlReader.Close();

        return result;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("a", "customloadmany", "exception"), "Affiliate list could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "Affiliate", "Exception while loading (custom/many) Affiliate object from database. See inner exception for details.", ex);
      }
    }
    
    public override int? Insert(ISqlConnectionInfo connection, Affiliate data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "INSERT INTO [core].[Affiliate] ([AffiliateTypeID],[Name],[Description],[SubidName],[Contact],[WebsiteUrl],[IsActive]) VALUES(@AffiliateTypeID,@Name,@Description,@SubidName,@Contact,@WebsiteUrl,@IsActive); SELECT SCOPE_IDENTITY();";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@AffiliateTypeID", data.AffiliateType.ID);
				sqlCmd.Parameters.AddWithValue("@Name", data.Name).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@Description", !string.IsNullOrEmpty(data.Description) ? (object)data.Description : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@SubidName", !string.IsNullOrEmpty(data.SubidName) ? (object)data.SubidName : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@Contact", data.Contact).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@WebsiteUrl", data.WebsiteUrl).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@IsActive", data.IsActive).SqlDbType = SqlDbType.Bit;
				
        object idObj = sqlCmd.ExecuteScalar();
        if (idObj == null || DBNull.Value.Equals(idObj))
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("a", "insert", "noprimarykey"), "Affiliate could not be inserted or inserted primary key was not returned. Are you missing SELECT SCOPE_IDENTITY();?", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Insert, "Affiliate", "Exception while inserting Affiliate object in database.");
        }
        return (int)((decimal)idObj);
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("a", "insert", "exception"), "Affiliate could not be inserted. See exception for details.", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Insert, "Affiliate", "Exception while inserting Affiliate object in database. See inner exception for details.", ex);
      }
    }

    public override bool Update(ISqlConnectionInfo connection, Affiliate data)
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
        sqlCmdText = "UPDATE [core].[Affiliate] SET " +
												"[AffiliateTypeID] = @AffiliateTypeID, " + 
												"[Name] = @Name, " + 
												"[Description] = @Description, " + 
												"[SubidName] = @SubidName, " + 
												"[Contact] = @Contact, " + 
												"[WebsiteUrl] = @WebsiteUrl, " + 
												"[IsActive] = @IsActive, " + 
												"[Updated] = GETDATE() " + 
											"WHERE [AffiliateID] = @AffiliateID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@AffiliateTypeID", data.AffiliateType.ID);
				sqlCmd.Parameters.AddWithValue("@Name", data.Name).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@Description", !string.IsNullOrEmpty(data.Description) ? (object)data.Description : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@SubidName", !string.IsNullOrEmpty(data.SubidName) ? (object)data.SubidName : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@Contact", data.Contact).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@WebsiteUrl", data.WebsiteUrl).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@IsActive", data.IsActive).SqlDbType = SqlDbType.Bit;
				sqlCmd.Parameters.AddWithValue("@Updated", data.Updated).SqlDbType = SqlDbType.DateTime2;
				sqlCmd.Parameters.AddWithValue("@AffiliateID", data.ID);

        int rowCount = sqlCmd.ExecuteNonQuery();
        if (rowCount < 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("a", "update", "norecord"), "Affiliate could not be updated as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "Affiliate", "Exception while updating Affiliate object in database. No record found for this id.");
        }
        else if (rowCount > 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("a", "update", "morerecords"), "Affiliate was updated but there was more than one record affected.", sqlCmdText, this, connection, data);
          if (this.Logger.IsFatalEnabled)
            this.Logger.Fatal(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "Affiliate", "Exception while updating Affiliate object in database. More than one record found for this statement (update statement where clause broken?!).");
        }
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("a", "update", "exception"), "Affiliate could not be updated. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Update, "Affiliate", "Exception while updating Affiliate object in database. See inner exception for details.", ex);
      }
    }

    public override bool Delete(ISqlConnectionInfo connection, Affiliate data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText =  "DELETE FROM [core].[Affiliate] WHERE AffiliateID = @AffiliateID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@AffiliateID", data.ID);

        int success = sqlCmd.ExecuteNonQuery();

        if (success == -1)        
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("a", "delete", "norecord"), "Affiliate could not be deleted as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Delete, "Affiliate", "Exception while deleting Affiliate object from database. No such record found.");
        }
        
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("a", "delete", "exception"), "Affiliate could not be deleted. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Delete, "Affiliate", "Exception while deleting Affiliate object from database. See inner exception for details.", ex);
      }
    }
  }
}

