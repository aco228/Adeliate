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
  [DataManager(typeof(AffiliateImageMap))] 
  public partial class AffiliateImageMapManager : Adlite.Data.Sql.SqlManagerBase<AffiliateImageMap>, IAffiliateImageMapManager
  {
    public override DatabaseType Type
    {
      get { return DatabaseType.Adlite; }
    }

    protected override AffiliateImageMap LoadInternal(ISqlConnectionInfo connection, int id)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "SELECT " + 
							AffiliateImageMapTable.GetColumnNames("[aim]") + 
							(this.Depth > 0 ? "," + AffiliateTable.GetColumnNames("[aim_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTypeTable.GetColumnNames("[aim_a_at]") : string.Empty) + 
							(this.Depth > 0 ? "," + ImageTable.GetColumnNames("[aim_i]") : string.Empty) + 
					" FROM [core].[AffiliateImageMap] AS [aim] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Affiliate] AS [aim_a] ON [aim].[AffiliateID] = [aim_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[AffiliateType] AS [aim_a_at] ON [aim_a].[AffiliateTypeID] = [aim_a_at].[AffiliateTypeID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Image] AS [aim_i] ON [aim].[ImageID] = [aim_i].[ImageID] ";
				sqlCmdText += "WHERE [aim].[AffiliateImageMapID] = @AffiliateImageMapID;";

        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@AffiliateImageMapID", id);
        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("aim", "loadinternal", "notfound"), "AffiliateImageMap could not be loaded by id as it was not found.", sqlCmdText, this, connection, id);
          if (this.Logger.IsWarnEnabled)
            this.Logger.Warn(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				AffiliateImageMapTable aimTable = new AffiliateImageMapTable(query);
				AffiliateTable aim_aTable = (this.Depth > 0) ? new AffiliateTable(query) : null;
				AffiliateTypeTable aim_a_atTable = (this.Depth > 1) ? new AffiliateTypeTable(query) : null;
				ImageTable aim_iTable = (this.Depth > 0) ? new ImageTable(query) : null;

        
				AffiliateType aim_a_atObject = (this.Depth > 1) ? aim_a_atTable.CreateInstance() : null;
				Affiliate aim_aObject = (this.Depth > 0) ? aim_aTable.CreateInstance(aim_a_atObject) : null;
				Image aim_iObject = (this.Depth > 0) ? aim_iTable.CreateInstance() : null;
				AffiliateImageMap aimObject = aimTable.CreateInstance(aim_aObject, aim_iObject);
				sqlReader.Close();

				return aimObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("aim", "loadinternal", "exception"), "AffiliateImageMap could not be loaded by id. See exception for details.", sqlCmdText, ex, this, connection, id);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "AffiliateImageMap", "Exception while loading AffiliateImageMap object from database. See inner exception for details.", ex);
      }
    }

    public AffiliateImageMap Load(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							AffiliateImageMapTable.GetColumnNames("[aim]") + 
							(this.Depth > 0 ? "," + AffiliateTable.GetColumnNames("[aim_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTypeTable.GetColumnNames("[aim_a_at]") : string.Empty) + 
							(this.Depth > 0 ? "," + ImageTable.GetColumnNames("[aim_i]") : string.Empty) +  
					" FROM [core].[AffiliateImageMap] AS [aim] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Affiliate] AS [aim_a] ON [aim].[AffiliateID] = [aim_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[AffiliateType] AS [aim_a_at] ON [aim_a].[AffiliateTypeID] = [aim_a_at].[AffiliateTypeID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Image] AS [aim_i] ON [aim].[ImageID] = [aim_i].[ImageID] ";
				

        parameters.Top = 1;
        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("aim", "customload", "notfound"), "AffiliateImageMap could not be loaded using custom logic as it was not found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				AffiliateImageMapTable aimTable = new AffiliateImageMapTable(query);
				AffiliateTable aim_aTable = (this.Depth > 0) ? new AffiliateTable(query) : null;
				AffiliateTypeTable aim_a_atTable = (this.Depth > 1) ? new AffiliateTypeTable(query) : null;
				ImageTable aim_iTable = (this.Depth > 0) ? new ImageTable(query) : null;

        
				AffiliateType aim_a_atObject = (this.Depth > 1) ? aim_a_atTable.CreateInstance() : null;
				Affiliate aim_aObject = (this.Depth > 0) ? aim_aTable.CreateInstance(aim_a_atObject) : null;
				Image aim_iObject = (this.Depth > 0) ? aim_iTable.CreateInstance() : null;
				AffiliateImageMap aimObject = aimTable.CreateInstance(aim_aObject, aim_iObject);
				sqlReader.Close();

				return aimObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("aim", "customload", "exception"), "AffiliateImageMap could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "AffiliateImageMap", "Exception while loading (custom/single) AffiliateImageMap object from database. See inner exception for details.", ex);
      }
    }

    public List<AffiliateImageMap> LoadMany(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							AffiliateImageMapTable.GetColumnNames("[aim]") + 
							(this.Depth > 0 ? "," + AffiliateTable.GetColumnNames("[aim_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTypeTable.GetColumnNames("[aim_a_at]") : string.Empty) + 
							(this.Depth > 0 ? "," + ImageTable.GetColumnNames("[aim_i]") : string.Empty) +  
					" FROM [core].[AffiliateImageMap] AS [aim] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Affiliate] AS [aim_a] ON [aim].[AffiliateID] = [aim_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[AffiliateType] AS [aim_a_at] ON [aim_a].[AffiliateTypeID] = [aim_a_at].[AffiliateTypeID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Image] AS [aim_i] ON [aim].[ImageID] = [aim_i].[ImageID] ";
				

        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("aim", "customloadmany", "notfound"), "AffiliateImageMap list could not be loaded using custom logic as no items were found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return new List<AffiliateImageMap>();
        }

        SqlQuery query = new SqlQuery(sqlReader);

				AffiliateImageMapTable aimTable = new AffiliateImageMapTable(query);
				AffiliateTable aim_aTable = (this.Depth > 0) ? new AffiliateTable(query) : null;
				AffiliateTypeTable aim_a_atTable = (this.Depth > 1) ? new AffiliateTypeTable(query) : null;
				ImageTable aim_iTable = (this.Depth > 0) ? new ImageTable(query) : null;

        List<AffiliateImageMap> result = new List<AffiliateImageMap>();
        do
        {
          
					AffiliateType aim_a_atObject = (this.Depth > 1) ? aim_a_atTable.CreateInstance() : null;
					Affiliate aim_aObject = (this.Depth > 0) ? aim_aTable.CreateInstance(aim_a_atObject) : null;
					Image aim_iObject = (this.Depth > 0) ? aim_iTable.CreateInstance() : null;
					AffiliateImageMap aimObject = (this.Depth > -1) ? aimTable.CreateInstance(aim_aObject, aim_iObject) : null;
					result.Add(aimObject);

        } while (sqlReader.Read());
        sqlReader.Close();

        return result;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("aim", "customloadmany", "exception"), "AffiliateImageMap list could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "AffiliateImageMap", "Exception while loading (custom/many) AffiliateImageMap object from database. See inner exception for details.", ex);
      }
    }
    
    public override int? Insert(ISqlConnectionInfo connection, AffiliateImageMap data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "INSERT INTO [core].[AffiliateImageMap] ([AffiliateID],[ImageID]) VALUES(@AffiliateID,@ImageID); SELECT SCOPE_IDENTITY();";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@AffiliateID", data.Affiliate.ID);
				sqlCmd.Parameters.AddWithValue("@ImageID", data.Image.ID);
				
        object idObj = sqlCmd.ExecuteScalar();
        if (idObj == null || DBNull.Value.Equals(idObj))
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("aim", "insert", "noprimarykey"), "AffiliateImageMap could not be inserted or inserted primary key was not returned. Are you missing SELECT SCOPE_IDENTITY();?", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Insert, "AffiliateImageMap", "Exception while inserting AffiliateImageMap object in database.");
        }
        return (int)((decimal)idObj);
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("aim", "insert", "exception"), "AffiliateImageMap could not be inserted. See exception for details.", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Insert, "AffiliateImageMap", "Exception while inserting AffiliateImageMap object in database. See inner exception for details.", ex);
      }
    }

    public override bool Update(ISqlConnectionInfo connection, AffiliateImageMap data)
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
        sqlCmdText = "UPDATE [core].[AffiliateImageMap] SET " +
												"[AffiliateID] = @AffiliateID, " + 
												"[ImageID] = @ImageID, " + 
												"[Updated] = GETDATE() " + 
											"WHERE [AffiliateImageMapID] = @AffiliateImageMapID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@AffiliateID", data.Affiliate.ID);
				sqlCmd.Parameters.AddWithValue("@ImageID", data.Image.ID);
				sqlCmd.Parameters.AddWithValue("@Updated", data.Updated).SqlDbType = SqlDbType.DateTime2;
				sqlCmd.Parameters.AddWithValue("@AffiliateImageMapID", data.ID);

        int rowCount = sqlCmd.ExecuteNonQuery();
        if (rowCount < 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("aim", "update", "norecord"), "AffiliateImageMap could not be updated as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "AffiliateImageMap", "Exception while updating AffiliateImageMap object in database. No record found for this id.");
        }
        else if (rowCount > 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("aim", "update", "morerecords"), "AffiliateImageMap was updated but there was more than one record affected.", sqlCmdText, this, connection, data);
          if (this.Logger.IsFatalEnabled)
            this.Logger.Fatal(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "AffiliateImageMap", "Exception while updating AffiliateImageMap object in database. More than one record found for this statement (update statement where clause broken?!).");
        }
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("aim", "update", "exception"), "AffiliateImageMap could not be updated. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Update, "AffiliateImageMap", "Exception while updating AffiliateImageMap object in database. See inner exception for details.", ex);
      }
    }

    public override bool Delete(ISqlConnectionInfo connection, AffiliateImageMap data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText =  "DELETE FROM [core].[AffiliateImageMap] WHERE AffiliateImageMapID = @AffiliateImageMapID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@AffiliateImageMapID", data.ID);

        int success = sqlCmd.ExecuteNonQuery();

        if (success == -1)        
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("aim", "delete", "norecord"), "AffiliateImageMap could not be deleted as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Delete, "AffiliateImageMap", "Exception while deleting AffiliateImageMap object from database. No such record found.");
        }
        
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("aim", "delete", "exception"), "AffiliateImageMap could not be deleted. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Delete, "AffiliateImageMap", "Exception while deleting AffiliateImageMap object from database. See inner exception for details.", ex);
      }
    }
  }
}

