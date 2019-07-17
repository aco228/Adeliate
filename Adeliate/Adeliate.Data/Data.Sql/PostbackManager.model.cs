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
  [DataManager(typeof(Postback))] 
  public partial class PostbackManager : Adlite.Data.Sql.SqlManagerBase<Postback>, IPostbackManager
  {
    public override DatabaseType Type
    {
      get { return DatabaseType.Adlite; }
    }

    protected override Postback LoadInternal(ISqlConnectionInfo connection, int id)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "SELECT " + 
							PostbackTable.GetColumnNames("[p]") + 
							(this.Depth > 0 ? "," + PostbackTypeTable.GetColumnNames("[p_pt]") : string.Empty) + 
							(this.Depth > 0 ? "," + AffiliateTable.GetColumnNames("[p_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTypeTable.GetColumnNames("[p_a_at]") : string.Empty) + 
					" FROM [core].[Postback] AS [p] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[PostbackType] AS [p_pt] ON [p].[PostbackTypeID] = [p_pt].[PostbackTypeID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Affiliate] AS [p_a] ON [p].[AffiliateID] = [p_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[AffiliateType] AS [p_a_at] ON [p_a].[AffiliateTypeID] = [p_a_at].[AffiliateTypeID] ";
				sqlCmdText += "WHERE [p].[PostbackID] = @PostbackID;";

        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@PostbackID", id);
        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("p", "loadinternal", "notfound"), "Postback could not be loaded by id as it was not found.", sqlCmdText, this, connection, id);
          if (this.Logger.IsWarnEnabled)
            this.Logger.Warn(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				PostbackTable pTable = new PostbackTable(query);
				PostbackTypeTable p_ptTable = (this.Depth > 0) ? new PostbackTypeTable(query) : null;
				AffiliateTable p_aTable = (this.Depth > 0) ? new AffiliateTable(query) : null;
				AffiliateTypeTable p_a_atTable = (this.Depth > 1) ? new AffiliateTypeTable(query) : null;

        
				PostbackType p_ptObject = (this.Depth > 0) ? p_ptTable.CreateInstance() : null;
				AffiliateType p_a_atObject = (this.Depth > 1) ? p_a_atTable.CreateInstance() : null;
				Affiliate p_aObject = (this.Depth > 0) ? p_aTable.CreateInstance(p_a_atObject) : null;
				Postback pObject = pTable.CreateInstance(p_ptObject, p_aObject);
				sqlReader.Close();

				return pObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("p", "loadinternal", "exception"), "Postback could not be loaded by id. See exception for details.", sqlCmdText, ex, this, connection, id);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "Postback", "Exception while loading Postback object from database. See inner exception for details.", ex);
      }
    }

    public Postback Load(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							PostbackTable.GetColumnNames("[p]") + 
							(this.Depth > 0 ? "," + PostbackTypeTable.GetColumnNames("[p_pt]") : string.Empty) + 
							(this.Depth > 0 ? "," + AffiliateTable.GetColumnNames("[p_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTypeTable.GetColumnNames("[p_a_at]") : string.Empty) +  
					" FROM [core].[Postback] AS [p] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[PostbackType] AS [p_pt] ON [p].[PostbackTypeID] = [p_pt].[PostbackTypeID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Affiliate] AS [p_a] ON [p].[AffiliateID] = [p_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[AffiliateType] AS [p_a_at] ON [p_a].[AffiliateTypeID] = [p_a_at].[AffiliateTypeID] ";
				

        parameters.Top = 1;
        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("p", "customload", "notfound"), "Postback could not be loaded using custom logic as it was not found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				PostbackTable pTable = new PostbackTable(query);
				PostbackTypeTable p_ptTable = (this.Depth > 0) ? new PostbackTypeTable(query) : null;
				AffiliateTable p_aTable = (this.Depth > 0) ? new AffiliateTable(query) : null;
				AffiliateTypeTable p_a_atTable = (this.Depth > 1) ? new AffiliateTypeTable(query) : null;

        
				PostbackType p_ptObject = (this.Depth > 0) ? p_ptTable.CreateInstance() : null;
				AffiliateType p_a_atObject = (this.Depth > 1) ? p_a_atTable.CreateInstance() : null;
				Affiliate p_aObject = (this.Depth > 0) ? p_aTable.CreateInstance(p_a_atObject) : null;
				Postback pObject = pTable.CreateInstance(p_ptObject, p_aObject);
				sqlReader.Close();

				return pObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("p", "customload", "exception"), "Postback could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "Postback", "Exception while loading (custom/single) Postback object from database. See inner exception for details.", ex);
      }
    }

    public List<Postback> LoadMany(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							PostbackTable.GetColumnNames("[p]") + 
							(this.Depth > 0 ? "," + PostbackTypeTable.GetColumnNames("[p_pt]") : string.Empty) + 
							(this.Depth > 0 ? "," + AffiliateTable.GetColumnNames("[p_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTypeTable.GetColumnNames("[p_a_at]") : string.Empty) +  
					" FROM [core].[Postback] AS [p] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[PostbackType] AS [p_pt] ON [p].[PostbackTypeID] = [p_pt].[PostbackTypeID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Affiliate] AS [p_a] ON [p].[AffiliateID] = [p_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[AffiliateType] AS [p_a_at] ON [p_a].[AffiliateTypeID] = [p_a_at].[AffiliateTypeID] ";
				

        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("p", "customloadmany", "notfound"), "Postback list could not be loaded using custom logic as no items were found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return new List<Postback>();
        }

        SqlQuery query = new SqlQuery(sqlReader);

				PostbackTable pTable = new PostbackTable(query);
				PostbackTypeTable p_ptTable = (this.Depth > 0) ? new PostbackTypeTable(query) : null;
				AffiliateTable p_aTable = (this.Depth > 0) ? new AffiliateTable(query) : null;
				AffiliateTypeTable p_a_atTable = (this.Depth > 1) ? new AffiliateTypeTable(query) : null;

        List<Postback> result = new List<Postback>();
        do
        {
          
					PostbackType p_ptObject = (this.Depth > 0) ? p_ptTable.CreateInstance() : null;
					AffiliateType p_a_atObject = (this.Depth > 1) ? p_a_atTable.CreateInstance() : null;
					Affiliate p_aObject = (this.Depth > 0) ? p_aTable.CreateInstance(p_a_atObject) : null;
					Postback pObject = (this.Depth > -1) ? pTable.CreateInstance(p_ptObject, p_aObject) : null;
					result.Add(pObject);

        } while (sqlReader.Read());
        sqlReader.Close();

        return result;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("p", "customloadmany", "exception"), "Postback list could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "Postback", "Exception while loading (custom/many) Postback object from database. See inner exception for details.", ex);
      }
    }
    
    public override int? Insert(ISqlConnectionInfo connection, Postback data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "INSERT INTO [core].[Postback] ([PostbackTypeID],[AffiliateID],[Url],[IsActive]) VALUES(@PostbackTypeID,@AffiliateID,@Url,@IsActive); SELECT SCOPE_IDENTITY();";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@PostbackTypeID", data.PostbackType.ID);
				sqlCmd.Parameters.AddWithValue("@AffiliateID", data.Affiliate.ID);
				sqlCmd.Parameters.AddWithValue("@Url", data.Url).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@IsActive", data.IsActive).SqlDbType = SqlDbType.Bit;
				
        object idObj = sqlCmd.ExecuteScalar();
        if (idObj == null || DBNull.Value.Equals(idObj))
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("p", "insert", "noprimarykey"), "Postback could not be inserted or inserted primary key was not returned. Are you missing SELECT SCOPE_IDENTITY();?", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Insert, "Postback", "Exception while inserting Postback object in database.");
        }
        return (int)((decimal)idObj);
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("p", "insert", "exception"), "Postback could not be inserted. See exception for details.", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Insert, "Postback", "Exception while inserting Postback object in database. See inner exception for details.", ex);
      }
    }

    public override bool Update(ISqlConnectionInfo connection, Postback data)
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
        sqlCmdText = "UPDATE [core].[Postback] SET " +
												"[PostbackTypeID] = @PostbackTypeID, " + 
												"[AffiliateID] = @AffiliateID, " + 
												"[Url] = @Url, " + 
												"[IsActive] = @IsActive, " + 
												"[Updated] = GETDATE() " + 
											"WHERE [PostbackID] = @PostbackID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@PostbackTypeID", data.PostbackType.ID);
				sqlCmd.Parameters.AddWithValue("@AffiliateID", data.Affiliate.ID);
				sqlCmd.Parameters.AddWithValue("@Url", data.Url).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@IsActive", data.IsActive).SqlDbType = SqlDbType.Bit;
				sqlCmd.Parameters.AddWithValue("@Updated", data.Updated).SqlDbType = SqlDbType.DateTime2;
				sqlCmd.Parameters.AddWithValue("@PostbackID", data.ID);

        int rowCount = sqlCmd.ExecuteNonQuery();
        if (rowCount < 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("p", "update", "norecord"), "Postback could not be updated as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "Postback", "Exception while updating Postback object in database. No record found for this id.");
        }
        else if (rowCount > 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("p", "update", "morerecords"), "Postback was updated but there was more than one record affected.", sqlCmdText, this, connection, data);
          if (this.Logger.IsFatalEnabled)
            this.Logger.Fatal(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "Postback", "Exception while updating Postback object in database. More than one record found for this statement (update statement where clause broken?!).");
        }
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("p", "update", "exception"), "Postback could not be updated. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Update, "Postback", "Exception while updating Postback object in database. See inner exception for details.", ex);
      }
    }

    public override bool Delete(ISqlConnectionInfo connection, Postback data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText =  "DELETE FROM [core].[Postback] WHERE PostbackID = @PostbackID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@PostbackID", data.ID);

        int success = sqlCmd.ExecuteNonQuery();

        if (success == -1)        
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("p", "delete", "norecord"), "Postback could not be deleted as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Delete, "Postback", "Exception while deleting Postback object from database. No such record found.");
        }
        
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("p", "delete", "exception"), "Postback could not be deleted. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Delete, "Postback", "Exception while deleting Postback object from database. See inner exception for details.", ex);
      }
    }
  }
}

