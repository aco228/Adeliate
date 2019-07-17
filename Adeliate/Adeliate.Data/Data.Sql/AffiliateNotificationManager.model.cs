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
  [DataManager(typeof(AffiliateNotification))] 
  public partial class AffiliateNotificationManager : Adlite.Data.Sql.SqlManagerBase<AffiliateNotification>, IAffiliateNotificationManager
  {
    public override DatabaseType Type
    {
      get { return DatabaseType.Adlite; }
    }

    protected override AffiliateNotification LoadInternal(ISqlConnectionInfo connection, int id)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "SELECT " + 
							AffiliateNotificationTable.GetColumnNames("[an]") + 
							(this.Depth > 0 ? "," + AffiliateTable.GetColumnNames("[an_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTypeTable.GetColumnNames("[an_a_at]") : string.Empty) + 
							(this.Depth > 0 ? "," + CampaignTable.GetColumnNames("[an_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignGroupTable.GetColumnNames("[an_c_cg]") : string.Empty) + 
							(this.Depth > 1 ? "," + CountryTable.GetColumnNames("[an_c_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + PriceTable.GetColumnNames("[an_c_p]") : string.Empty) + 
					" FROM [core].[AffiliateNotification] AS [an] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Affiliate] AS [an_a] ON [an].[AffiliateID] = [an_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[AffiliateType] AS [an_a_at] ON [an_a].[AffiliateTypeID] = [an_a_at].[AffiliateTypeID] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Campaign] AS [an_c] ON [an].[CampaignID] = [an_c].[CampaignID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[CampaignGroup] AS [an_c_cg] ON [an_c].[CampaignGroupID] = [an_c_cg].[CampaignGroupID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Country] AS [an_c_c] ON [an_c].[CountryID] = [an_c_c].[CountryID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Price] AS [an_c_p] ON [an_c].[PriceID] = [an_c_p].[PriceID] ";
				sqlCmdText += "WHERE [an].[AffiliateNotificationID] = @AffiliateNotificationID;";

        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@AffiliateNotificationID", id);
        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("an", "loadinternal", "notfound"), "AffiliateNotification could not be loaded by id as it was not found.", sqlCmdText, this, connection, id);
          if (this.Logger.IsWarnEnabled)
            this.Logger.Warn(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				AffiliateNotificationTable anTable = new AffiliateNotificationTable(query);
				AffiliateTable an_aTable = (this.Depth > 0) ? new AffiliateTable(query) : null;
				AffiliateTypeTable an_a_atTable = (this.Depth > 1) ? new AffiliateTypeTable(query) : null;
				CampaignTable an_cTable = (this.Depth > 0) ? new CampaignTable(query) : null;
				CampaignGroupTable an_c_cgTable = (this.Depth > 1) ? new CampaignGroupTable(query) : null;
				CountryTable an_c_cTable = (this.Depth > 1) ? new CountryTable(query) : null;
				PriceTable an_c_pTable = (this.Depth > 1) ? new PriceTable(query) : null;

        
				AffiliateType an_a_atObject = (this.Depth > 1) ? an_a_atTable.CreateInstance() : null;
				Affiliate an_aObject = (this.Depth > 0) ? an_aTable.CreateInstance(an_a_atObject) : null;
				CampaignGroup an_c_cgObject = (this.Depth > 1) ? an_c_cgTable.CreateInstance() : null;
				Country an_c_cObject = (this.Depth > 1) ? an_c_cTable.CreateInstance() : null;
				Price an_c_pObject = (this.Depth > 1) ? an_c_pTable.CreateInstance() : null;
				Campaign an_cObject = (this.Depth > 0) ? an_cTable.CreateInstance(an_c_cgObject, an_c_cObject, an_c_pObject) : null;
				AffiliateNotification anObject = anTable.CreateInstance(an_aObject, an_cObject);
				sqlReader.Close();

				return anObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("an", "loadinternal", "exception"), "AffiliateNotification could not be loaded by id. See exception for details.", sqlCmdText, ex, this, connection, id);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "AffiliateNotification", "Exception while loading AffiliateNotification object from database. See inner exception for details.", ex);
      }
    }

    public AffiliateNotification Load(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							AffiliateNotificationTable.GetColumnNames("[an]") + 
							(this.Depth > 0 ? "," + AffiliateTable.GetColumnNames("[an_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTypeTable.GetColumnNames("[an_a_at]") : string.Empty) + 
							(this.Depth > 0 ? "," + CampaignTable.GetColumnNames("[an_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignGroupTable.GetColumnNames("[an_c_cg]") : string.Empty) + 
							(this.Depth > 1 ? "," + CountryTable.GetColumnNames("[an_c_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + PriceTable.GetColumnNames("[an_c_p]") : string.Empty) +  
					" FROM [core].[AffiliateNotification] AS [an] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Affiliate] AS [an_a] ON [an].[AffiliateID] = [an_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[AffiliateType] AS [an_a_at] ON [an_a].[AffiliateTypeID] = [an_a_at].[AffiliateTypeID] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Campaign] AS [an_c] ON [an].[CampaignID] = [an_c].[CampaignID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[CampaignGroup] AS [an_c_cg] ON [an_c].[CampaignGroupID] = [an_c_cg].[CampaignGroupID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Country] AS [an_c_c] ON [an_c].[CountryID] = [an_c_c].[CountryID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Price] AS [an_c_p] ON [an_c].[PriceID] = [an_c_p].[PriceID] ";
				

        parameters.Top = 1;
        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("an", "customload", "notfound"), "AffiliateNotification could not be loaded using custom logic as it was not found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				AffiliateNotificationTable anTable = new AffiliateNotificationTable(query);
				AffiliateTable an_aTable = (this.Depth > 0) ? new AffiliateTable(query) : null;
				AffiliateTypeTable an_a_atTable = (this.Depth > 1) ? new AffiliateTypeTable(query) : null;
				CampaignTable an_cTable = (this.Depth > 0) ? new CampaignTable(query) : null;
				CampaignGroupTable an_c_cgTable = (this.Depth > 1) ? new CampaignGroupTable(query) : null;
				CountryTable an_c_cTable = (this.Depth > 1) ? new CountryTable(query) : null;
				PriceTable an_c_pTable = (this.Depth > 1) ? new PriceTable(query) : null;

        
				AffiliateType an_a_atObject = (this.Depth > 1) ? an_a_atTable.CreateInstance() : null;
				Affiliate an_aObject = (this.Depth > 0) ? an_aTable.CreateInstance(an_a_atObject) : null;
				CampaignGroup an_c_cgObject = (this.Depth > 1) ? an_c_cgTable.CreateInstance() : null;
				Country an_c_cObject = (this.Depth > 1) ? an_c_cTable.CreateInstance() : null;
				Price an_c_pObject = (this.Depth > 1) ? an_c_pTable.CreateInstance() : null;
				Campaign an_cObject = (this.Depth > 0) ? an_cTable.CreateInstance(an_c_cgObject, an_c_cObject, an_c_pObject) : null;
				AffiliateNotification anObject = anTable.CreateInstance(an_aObject, an_cObject);
				sqlReader.Close();

				return anObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("an", "customload", "exception"), "AffiliateNotification could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "AffiliateNotification", "Exception while loading (custom/single) AffiliateNotification object from database. See inner exception for details.", ex);
      }
    }

    public List<AffiliateNotification> LoadMany(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							AffiliateNotificationTable.GetColumnNames("[an]") + 
							(this.Depth > 0 ? "," + AffiliateTable.GetColumnNames("[an_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTypeTable.GetColumnNames("[an_a_at]") : string.Empty) + 
							(this.Depth > 0 ? "," + CampaignTable.GetColumnNames("[an_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignGroupTable.GetColumnNames("[an_c_cg]") : string.Empty) + 
							(this.Depth > 1 ? "," + CountryTable.GetColumnNames("[an_c_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + PriceTable.GetColumnNames("[an_c_p]") : string.Empty) +  
					" FROM [core].[AffiliateNotification] AS [an] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Affiliate] AS [an_a] ON [an].[AffiliateID] = [an_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[AffiliateType] AS [an_a_at] ON [an_a].[AffiliateTypeID] = [an_a_at].[AffiliateTypeID] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Campaign] AS [an_c] ON [an].[CampaignID] = [an_c].[CampaignID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[CampaignGroup] AS [an_c_cg] ON [an_c].[CampaignGroupID] = [an_c_cg].[CampaignGroupID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Country] AS [an_c_c] ON [an_c].[CountryID] = [an_c_c].[CountryID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Price] AS [an_c_p] ON [an_c].[PriceID] = [an_c_p].[PriceID] ";
				

        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("an", "customloadmany", "notfound"), "AffiliateNotification list could not be loaded using custom logic as no items were found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return new List<AffiliateNotification>();
        }

        SqlQuery query = new SqlQuery(sqlReader);

				AffiliateNotificationTable anTable = new AffiliateNotificationTable(query);
				AffiliateTable an_aTable = (this.Depth > 0) ? new AffiliateTable(query) : null;
				AffiliateTypeTable an_a_atTable = (this.Depth > 1) ? new AffiliateTypeTable(query) : null;
				CampaignTable an_cTable = (this.Depth > 0) ? new CampaignTable(query) : null;
				CampaignGroupTable an_c_cgTable = (this.Depth > 1) ? new CampaignGroupTable(query) : null;
				CountryTable an_c_cTable = (this.Depth > 1) ? new CountryTable(query) : null;
				PriceTable an_c_pTable = (this.Depth > 1) ? new PriceTable(query) : null;

        List<AffiliateNotification> result = new List<AffiliateNotification>();
        do
        {
          
					AffiliateType an_a_atObject = (this.Depth > 1) ? an_a_atTable.CreateInstance() : null;
					Affiliate an_aObject = (this.Depth > 0) ? an_aTable.CreateInstance(an_a_atObject) : null;
					CampaignGroup an_c_cgObject = (this.Depth > 1) ? an_c_cgTable.CreateInstance() : null;
					Country an_c_cObject = (this.Depth > 1) ? an_c_cTable.CreateInstance() : null;
					Price an_c_pObject = (this.Depth > 1) ? an_c_pTable.CreateInstance() : null;
					Campaign an_cObject = (this.Depth > 0) ? an_cTable.CreateInstance(an_c_cgObject, an_c_cObject, an_c_pObject) : null;
					AffiliateNotification anObject = (this.Depth > -1) ? anTable.CreateInstance(an_aObject, an_cObject) : null;
					result.Add(anObject);

        } while (sqlReader.Read());
        sqlReader.Close();

        return result;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("an", "customloadmany", "exception"), "AffiliateNotification list could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "AffiliateNotification", "Exception while loading (custom/many) AffiliateNotification object from database. See inner exception for details.", ex);
      }
    }
    
    public override int? Insert(ISqlConnectionInfo connection, AffiliateNotification data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "INSERT INTO [core].[AffiliateNotification] ([AffiliateID],[CampaignID],[Title],[Text]) VALUES(@AffiliateID,@CampaignID,@Title,@Text); SELECT SCOPE_IDENTITY();";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@AffiliateID", data.Affiliate.ID);
				sqlCmd.Parameters.AddWithValue("@CampaignID", data.Campaign == null ? DBNull.Value : (object)data.Campaign.ID);
				sqlCmd.Parameters.AddWithValue("@Title", data.Title).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@Text", data.Text).SqlDbType = SqlDbType.NText;
				
        object idObj = sqlCmd.ExecuteScalar();
        if (idObj == null || DBNull.Value.Equals(idObj))
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("an", "insert", "noprimarykey"), "AffiliateNotification could not be inserted or inserted primary key was not returned. Are you missing SELECT SCOPE_IDENTITY();?", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Insert, "AffiliateNotification", "Exception while inserting AffiliateNotification object in database.");
        }
        return (int)((decimal)idObj);
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("an", "insert", "exception"), "AffiliateNotification could not be inserted. See exception for details.", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Insert, "AffiliateNotification", "Exception while inserting AffiliateNotification object in database. See inner exception for details.", ex);
      }
    }

    public override bool Update(ISqlConnectionInfo connection, AffiliateNotification data)
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
        sqlCmdText = "UPDATE [core].[AffiliateNotification] SET " +
												"[AffiliateID] = @AffiliateID, " + 
												"[CampaignID] = @CampaignID, " + 
												"[Title] = @Title, " + 
												"[Text] = @Text, " + 
												"[Updated] = GETDATE() " + 
											"WHERE [AffiliateNotificationID] = @AffiliateNotificationID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@AffiliateID", data.Affiliate.ID);
				sqlCmd.Parameters.AddWithValue("@CampaignID", data.Campaign == null ? DBNull.Value : (object)data.Campaign.ID);
				sqlCmd.Parameters.AddWithValue("@Title", data.Title).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@Text", data.Text).SqlDbType = SqlDbType.NText;
				sqlCmd.Parameters.AddWithValue("@Updated", data.Updated).SqlDbType = SqlDbType.DateTime2;
				sqlCmd.Parameters.AddWithValue("@AffiliateNotificationID", data.ID);

        int rowCount = sqlCmd.ExecuteNonQuery();
        if (rowCount < 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("an", "update", "norecord"), "AffiliateNotification could not be updated as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "AffiliateNotification", "Exception while updating AffiliateNotification object in database. No record found for this id.");
        }
        else if (rowCount > 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("an", "update", "morerecords"), "AffiliateNotification was updated but there was more than one record affected.", sqlCmdText, this, connection, data);
          if (this.Logger.IsFatalEnabled)
            this.Logger.Fatal(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "AffiliateNotification", "Exception while updating AffiliateNotification object in database. More than one record found for this statement (update statement where clause broken?!).");
        }
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("an", "update", "exception"), "AffiliateNotification could not be updated. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Update, "AffiliateNotification", "Exception while updating AffiliateNotification object in database. See inner exception for details.", ex);
      }
    }

    public override bool Delete(ISqlConnectionInfo connection, AffiliateNotification data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText =  "DELETE FROM [core].[AffiliateNotification] WHERE AffiliateNotificationID = @AffiliateNotificationID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@AffiliateNotificationID", data.ID);

        int success = sqlCmd.ExecuteNonQuery();

        if (success == -1)        
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("an", "delete", "norecord"), "AffiliateNotification could not be deleted as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Delete, "AffiliateNotification", "Exception while deleting AffiliateNotification object from database. No such record found.");
        }
        
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("an", "delete", "exception"), "AffiliateNotification could not be deleted. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Delete, "AffiliateNotification", "Exception while deleting AffiliateNotification object from database. See inner exception for details.", ex);
      }
    }
  }
}

