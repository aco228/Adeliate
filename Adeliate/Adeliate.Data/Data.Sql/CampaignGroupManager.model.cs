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
  [DataManager(typeof(CampaignGroup))] 
  public partial class CampaignGroupManager : Adlite.Data.Sql.SqlManagerBase<CampaignGroup>, ICampaignGroupManager
  {
    public override DatabaseType Type
    {
      get { return DatabaseType.Adlite; }
    }

    protected override CampaignGroup LoadInternal(ISqlConnectionInfo connection, int id)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "SELECT " + 
							CampaignGroupTable.GetColumnNames("[cg]") + 
							(this.Depth > 0 ? "," + CampaignGroupTable.GetColumnNames("[cg_cg]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignGroupTable.GetColumnNames("[cg_cg_cg]") : string.Empty) + 
					" FROM [core].[CampaignGroup] AS [cg] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[CampaignGroup] AS [cg_cg] ON [cg].[FallbackGroupID] = [cg_cg].[CampaignGroupID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[CampaignGroup] AS [cg_cg_cg] ON [cg_cg].[FallbackGroupID] = [cg_cg_cg].[CampaignGroupID] ";
				sqlCmdText += "WHERE [cg].[CampaignGroupID] = @CampaignGroupID;";

        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@CampaignGroupID", id);
        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cg", "loadinternal", "notfound"), "CampaignGroup could not be loaded by id as it was not found.", sqlCmdText, this, connection, id);
          if (this.Logger.IsWarnEnabled)
            this.Logger.Warn(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				CampaignGroupTable cgTable = new CampaignGroupTable(query);
				CampaignGroupTable cg_cgTable = (this.Depth > 0) ? new CampaignGroupTable(query) : null;
				CampaignGroupTable cg_cg_cgTable = (this.Depth > 1) ? new CampaignGroupTable(query) : null;

        
				CampaignGroup cg_cg_cgObject = (this.Depth > 1) ? cg_cg_cgTable.CreateInstance() : null;
				CampaignGroup cg_cgObject = (this.Depth > 0) ? cg_cgTable.CreateInstance(cg_cg_cgObject) : null;
				CampaignGroup cgObject = cgTable.CreateInstance(cg_cgObject);
				sqlReader.Close();

				return cgObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cg", "loadinternal", "exception"), "CampaignGroup could not be loaded by id. See exception for details.", sqlCmdText, ex, this, connection, id);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "CampaignGroup", "Exception while loading CampaignGroup object from database. See inner exception for details.", ex);
      }
    }

    public CampaignGroup Load(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							CampaignGroupTable.GetColumnNames("[cg]") + 
							(this.Depth > 0 ? "," + CampaignGroupTable.GetColumnNames("[cg_cg]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignGroupTable.GetColumnNames("[cg_cg_cg]") : string.Empty) +  
					" FROM [core].[CampaignGroup] AS [cg] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[CampaignGroup] AS [cg_cg] ON [cg].[FallbackGroupID] = [cg_cg].[CampaignGroupID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[CampaignGroup] AS [cg_cg_cg] ON [cg_cg].[FallbackGroupID] = [cg_cg_cg].[CampaignGroupID] ";
				

        parameters.Top = 1;
        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cg", "customload", "notfound"), "CampaignGroup could not be loaded using custom logic as it was not found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				CampaignGroupTable cgTable = new CampaignGroupTable(query);
				CampaignGroupTable cg_cgTable = (this.Depth > 0) ? new CampaignGroupTable(query) : null;
				CampaignGroupTable cg_cg_cgTable = (this.Depth > 1) ? new CampaignGroupTable(query) : null;

        
				CampaignGroup cg_cg_cgObject = (this.Depth > 1) ? cg_cg_cgTable.CreateInstance() : null;
				CampaignGroup cg_cgObject = (this.Depth > 0) ? cg_cgTable.CreateInstance(cg_cg_cgObject) : null;
				CampaignGroup cgObject = cgTable.CreateInstance(cg_cgObject);
				sqlReader.Close();

				return cgObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cg", "customload", "exception"), "CampaignGroup could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "CampaignGroup", "Exception while loading (custom/single) CampaignGroup object from database. See inner exception for details.", ex);
      }
    }

    public List<CampaignGroup> LoadMany(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							CampaignGroupTable.GetColumnNames("[cg]") + 
							(this.Depth > 0 ? "," + CampaignGroupTable.GetColumnNames("[cg_cg]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignGroupTable.GetColumnNames("[cg_cg_cg]") : string.Empty) +  
					" FROM [core].[CampaignGroup] AS [cg] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[CampaignGroup] AS [cg_cg] ON [cg].[FallbackGroupID] = [cg_cg].[CampaignGroupID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[CampaignGroup] AS [cg_cg_cg] ON [cg_cg].[FallbackGroupID] = [cg_cg_cg].[CampaignGroupID] ";
				

        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cg", "customloadmany", "notfound"), "CampaignGroup list could not be loaded using custom logic as no items were found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return new List<CampaignGroup>();
        }

        SqlQuery query = new SqlQuery(sqlReader);

				CampaignGroupTable cgTable = new CampaignGroupTable(query);
				CampaignGroupTable cg_cgTable = (this.Depth > 0) ? new CampaignGroupTable(query) : null;
				CampaignGroupTable cg_cg_cgTable = (this.Depth > 1) ? new CampaignGroupTable(query) : null;

        List<CampaignGroup> result = new List<CampaignGroup>();
        do
        {
          
					CampaignGroup cg_cg_cgObject = (this.Depth > 1) ? cg_cg_cgTable.CreateInstance() : null;
					CampaignGroup cg_cgObject = (this.Depth > 0) ? cg_cgTable.CreateInstance(cg_cg_cgObject) : null;
					CampaignGroup cgObject = (this.Depth > -1) ? cgTable.CreateInstance(cg_cgObject) : null;
					result.Add(cgObject);

        } while (sqlReader.Read());
        sqlReader.Close();

        return result;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cg", "customloadmany", "exception"), "CampaignGroup list could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "CampaignGroup", "Exception while loading (custom/many) CampaignGroup object from database. See inner exception for details.", ex);
      }
    }
    
    public override int? Insert(ISqlConnectionInfo connection, CampaignGroup data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "INSERT INTO [core].[CampaignGroup] ([FallbackGroupID],[Key],[Name],[Description]) VALUES(@FallbackGroupID,@Key,@Name,@Description); SELECT SCOPE_IDENTITY();";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@FallbackGroupID", data.FallbackGroup == null ? DBNull.Value : (object)data.FallbackGroup.ID);
				sqlCmd.Parameters.AddWithValue("@Key", !string.IsNullOrEmpty(data.Key) ? (object)data.Key : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@Name", data.Name).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@Description", data.Description).SqlDbType = SqlDbType.NVarChar;
				
        object idObj = sqlCmd.ExecuteScalar();
        if (idObj == null || DBNull.Value.Equals(idObj))
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cg", "insert", "noprimarykey"), "CampaignGroup could not be inserted or inserted primary key was not returned. Are you missing SELECT SCOPE_IDENTITY();?", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Insert, "CampaignGroup", "Exception while inserting CampaignGroup object in database.");
        }
        return (int)((decimal)idObj);
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cg", "insert", "exception"), "CampaignGroup could not be inserted. See exception for details.", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Insert, "CampaignGroup", "Exception while inserting CampaignGroup object in database. See inner exception for details.", ex);
      }
    }

    public override bool Update(ISqlConnectionInfo connection, CampaignGroup data)
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
        sqlCmdText = "UPDATE [core].[CampaignGroup] SET " +
												"[FallbackGroupID] = @FallbackGroupID, " + 
												"[Key] = @Key, " + 
												"[Name] = @Name, " + 
												"[Description] = @Description, " + 
												"[Updated] = GETDATE() " + 
											"WHERE [CampaignGroupID] = @CampaignGroupID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@FallbackGroupID", data.FallbackGroup == null ? DBNull.Value : (object)data.FallbackGroup.ID);
				sqlCmd.Parameters.AddWithValue("@Key", !string.IsNullOrEmpty(data.Key) ? (object)data.Key : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@Name", data.Name).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@Description", data.Description).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@Updated", data.Updated).SqlDbType = SqlDbType.DateTime2;
				sqlCmd.Parameters.AddWithValue("@CampaignGroupID", data.ID);

        int rowCount = sqlCmd.ExecuteNonQuery();
        if (rowCount < 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cg", "update", "norecord"), "CampaignGroup could not be updated as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "CampaignGroup", "Exception while updating CampaignGroup object in database. No record found for this id.");
        }
        else if (rowCount > 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cg", "update", "morerecords"), "CampaignGroup was updated but there was more than one record affected.", sqlCmdText, this, connection, data);
          if (this.Logger.IsFatalEnabled)
            this.Logger.Fatal(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "CampaignGroup", "Exception while updating CampaignGroup object in database. More than one record found for this statement (update statement where clause broken?!).");
        }
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cg", "update", "exception"), "CampaignGroup could not be updated. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Update, "CampaignGroup", "Exception while updating CampaignGroup object in database. See inner exception for details.", ex);
      }
    }

    public override bool Delete(ISqlConnectionInfo connection, CampaignGroup data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText =  "DELETE FROM [core].[CampaignGroup] WHERE CampaignGroupID = @CampaignGroupID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@CampaignGroupID", data.ID);

        int success = sqlCmd.ExecuteNonQuery();

        if (success == -1)        
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cg", "delete", "norecord"), "CampaignGroup could not be deleted as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Delete, "CampaignGroup", "Exception while deleting CampaignGroup object from database. No such record found.");
        }
        
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cg", "delete", "exception"), "CampaignGroup could not be deleted. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Delete, "CampaignGroup", "Exception while deleting CampaignGroup object from database. See inner exception for details.", ex);
      }
    }
  }
}

