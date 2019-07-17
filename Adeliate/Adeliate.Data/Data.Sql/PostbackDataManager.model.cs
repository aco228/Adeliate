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
  [DataManager(typeof(PostbackData))] 
  public partial class PostbackDataManager : Adlite.Data.Sql.SqlManagerBase<PostbackData>, IPostbackDataManager
  {
    public override DatabaseType Type
    {
      get { return DatabaseType.Adlite; }
    }

    protected override PostbackData LoadInternal(ISqlConnectionInfo connection, int id)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "SELECT " + 
							PostbackDataTable.GetColumnNames("[pd]") + 
							(this.Depth > 0 ? "," + PostbackTable.GetColumnNames("[pd_p]") : string.Empty) + 
							(this.Depth > 1 ? "," + PostbackTypeTable.GetColumnNames("[pd_p_pt]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTable.GetColumnNames("[pd_p_a]") : string.Empty) + 
							(this.Depth > 0 ? "," + AffiliateTable.GetColumnNames("[pd_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTypeTable.GetColumnNames("[pd_a_at]") : string.Empty) + 
							(this.Depth > 0 ? "," + ClickTable.GetColumnNames("[pd_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + OfferTable.GetColumnNames("[pd_c_o]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTable.GetColumnNames("[pd_c_a]") : string.Empty) + 
							(this.Depth > 0 ? "," + PostbackRequestTable.GetColumnNames("[pd_pr]") : string.Empty) + 
					" FROM [core].[PostbackData] AS [pd] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Postback] AS [pd_p] ON [pd].[PostbackID] = [pd_p].[PostbackID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[PostbackType] AS [pd_p_pt] ON [pd_p].[PostbackTypeID] = [pd_p_pt].[PostbackTypeID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Affiliate] AS [pd_p_a] ON [pd_p].[AffiliateID] = [pd_p_a].[AffiliateID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Affiliate] AS [pd_a] ON [pd].[AffiliateID] = [pd_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[AffiliateType] AS [pd_a_at] ON [pd_a].[AffiliateTypeID] = [pd_a_at].[AffiliateTypeID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Click] AS [pd_c] ON [pd].[ClickID] = [pd_c].[ClickID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Offer] AS [pd_c_o] ON [pd_c].[OfferID] = [pd_c_o].[OfferID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Affiliate] AS [pd_c_a] ON [pd_c].[AffiliateID] = [pd_c_a].[AffiliateID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[PostbackRequest] AS [pd_pr] ON [pd].[PostbackRequestID] = [pd_pr].[PostbackRequestID] ";
				sqlCmdText += "WHERE [pd].[PostbackDataID] = @PostbackDataID;";

        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@PostbackDataID", id);
        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("pd", "loadinternal", "notfound"), "PostbackData could not be loaded by id as it was not found.", sqlCmdText, this, connection, id);
          if (this.Logger.IsWarnEnabled)
            this.Logger.Warn(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				PostbackDataTable pdTable = new PostbackDataTable(query);
				PostbackTable pd_pTable = (this.Depth > 0) ? new PostbackTable(query) : null;
				PostbackTypeTable pd_p_ptTable = (this.Depth > 1) ? new PostbackTypeTable(query) : null;
				AffiliateTable pd_p_aTable = (this.Depth > 1) ? new AffiliateTable(query) : null;
				AffiliateTable pd_aTable = (this.Depth > 0) ? new AffiliateTable(query) : null;
				AffiliateTypeTable pd_a_atTable = (this.Depth > 1) ? new AffiliateTypeTable(query) : null;
				ClickTable pd_cTable = (this.Depth > 0) ? new ClickTable(query) : null;
				OfferTable pd_c_oTable = (this.Depth > 1) ? new OfferTable(query) : null;
				AffiliateTable pd_c_aTable = (this.Depth > 1) ? new AffiliateTable(query) : null;
				PostbackRequestTable pd_prTable = (this.Depth > 0) ? new PostbackRequestTable(query) : null;

        
				PostbackType pd_p_ptObject = (this.Depth > 1) ? pd_p_ptTable.CreateInstance() : null;
				Affiliate pd_p_aObject = (this.Depth > 1) ? pd_p_aTable.CreateInstance() : null;
				Postback pd_pObject = (this.Depth > 0) ? pd_pTable.CreateInstance(pd_p_ptObject, pd_p_aObject) : null;
				AffiliateType pd_a_atObject = (this.Depth > 1) ? pd_a_atTable.CreateInstance() : null;
				Affiliate pd_aObject = (this.Depth > 0) ? pd_aTable.CreateInstance(pd_a_atObject) : null;
				Offer pd_c_oObject = (this.Depth > 1) ? pd_c_oTable.CreateInstance() : null;
				Affiliate pd_c_aObject = (this.Depth > 1) ? pd_c_aTable.CreateInstance() : null;
				Click pd_cObject = (this.Depth > 0) ? pd_cTable.CreateInstance(pd_c_oObject, pd_c_aObject) : null;
				PostbackRequest pd_prObject = (this.Depth > 0) ? pd_prTable.CreateInstance() : null;
				PostbackData pdObject = pdTable.CreateInstance(pd_pObject, pd_aObject, pd_cObject, pd_prObject);
				sqlReader.Close();

				return pdObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("pd", "loadinternal", "exception"), "PostbackData could not be loaded by id. See exception for details.", sqlCmdText, ex, this, connection, id);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "PostbackData", "Exception while loading PostbackData object from database. See inner exception for details.", ex);
      }
    }

    public PostbackData Load(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							PostbackDataTable.GetColumnNames("[pd]") + 
							(this.Depth > 0 ? "," + PostbackTable.GetColumnNames("[pd_p]") : string.Empty) + 
							(this.Depth > 1 ? "," + PostbackTypeTable.GetColumnNames("[pd_p_pt]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTable.GetColumnNames("[pd_p_a]") : string.Empty) + 
							(this.Depth > 0 ? "," + AffiliateTable.GetColumnNames("[pd_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTypeTable.GetColumnNames("[pd_a_at]") : string.Empty) + 
							(this.Depth > 0 ? "," + ClickTable.GetColumnNames("[pd_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + OfferTable.GetColumnNames("[pd_c_o]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTable.GetColumnNames("[pd_c_a]") : string.Empty) + 
							(this.Depth > 0 ? "," + PostbackRequestTable.GetColumnNames("[pd_pr]") : string.Empty) +  
					" FROM [core].[PostbackData] AS [pd] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Postback] AS [pd_p] ON [pd].[PostbackID] = [pd_p].[PostbackID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[PostbackType] AS [pd_p_pt] ON [pd_p].[PostbackTypeID] = [pd_p_pt].[PostbackTypeID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Affiliate] AS [pd_p_a] ON [pd_p].[AffiliateID] = [pd_p_a].[AffiliateID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Affiliate] AS [pd_a] ON [pd].[AffiliateID] = [pd_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[AffiliateType] AS [pd_a_at] ON [pd_a].[AffiliateTypeID] = [pd_a_at].[AffiliateTypeID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Click] AS [pd_c] ON [pd].[ClickID] = [pd_c].[ClickID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Offer] AS [pd_c_o] ON [pd_c].[OfferID] = [pd_c_o].[OfferID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Affiliate] AS [pd_c_a] ON [pd_c].[AffiliateID] = [pd_c_a].[AffiliateID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[PostbackRequest] AS [pd_pr] ON [pd].[PostbackRequestID] = [pd_pr].[PostbackRequestID] ";
				

        parameters.Top = 1;
        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("pd", "customload", "notfound"), "PostbackData could not be loaded using custom logic as it was not found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				PostbackDataTable pdTable = new PostbackDataTable(query);
				PostbackTable pd_pTable = (this.Depth > 0) ? new PostbackTable(query) : null;
				PostbackTypeTable pd_p_ptTable = (this.Depth > 1) ? new PostbackTypeTable(query) : null;
				AffiliateTable pd_p_aTable = (this.Depth > 1) ? new AffiliateTable(query) : null;
				AffiliateTable pd_aTable = (this.Depth > 0) ? new AffiliateTable(query) : null;
				AffiliateTypeTable pd_a_atTable = (this.Depth > 1) ? new AffiliateTypeTable(query) : null;
				ClickTable pd_cTable = (this.Depth > 0) ? new ClickTable(query) : null;
				OfferTable pd_c_oTable = (this.Depth > 1) ? new OfferTable(query) : null;
				AffiliateTable pd_c_aTable = (this.Depth > 1) ? new AffiliateTable(query) : null;
				PostbackRequestTable pd_prTable = (this.Depth > 0) ? new PostbackRequestTable(query) : null;

        
				PostbackType pd_p_ptObject = (this.Depth > 1) ? pd_p_ptTable.CreateInstance() : null;
				Affiliate pd_p_aObject = (this.Depth > 1) ? pd_p_aTable.CreateInstance() : null;
				Postback pd_pObject = (this.Depth > 0) ? pd_pTable.CreateInstance(pd_p_ptObject, pd_p_aObject) : null;
				AffiliateType pd_a_atObject = (this.Depth > 1) ? pd_a_atTable.CreateInstance() : null;
				Affiliate pd_aObject = (this.Depth > 0) ? pd_aTable.CreateInstance(pd_a_atObject) : null;
				Offer pd_c_oObject = (this.Depth > 1) ? pd_c_oTable.CreateInstance() : null;
				Affiliate pd_c_aObject = (this.Depth > 1) ? pd_c_aTable.CreateInstance() : null;
				Click pd_cObject = (this.Depth > 0) ? pd_cTable.CreateInstance(pd_c_oObject, pd_c_aObject) : null;
				PostbackRequest pd_prObject = (this.Depth > 0) ? pd_prTable.CreateInstance() : null;
				PostbackData pdObject = pdTable.CreateInstance(pd_pObject, pd_aObject, pd_cObject, pd_prObject);
				sqlReader.Close();

				return pdObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("pd", "customload", "exception"), "PostbackData could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "PostbackData", "Exception while loading (custom/single) PostbackData object from database. See inner exception for details.", ex);
      }
    }

    public List<PostbackData> LoadMany(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							PostbackDataTable.GetColumnNames("[pd]") + 
							(this.Depth > 0 ? "," + PostbackTable.GetColumnNames("[pd_p]") : string.Empty) + 
							(this.Depth > 1 ? "," + PostbackTypeTable.GetColumnNames("[pd_p_pt]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTable.GetColumnNames("[pd_p_a]") : string.Empty) + 
							(this.Depth > 0 ? "," + AffiliateTable.GetColumnNames("[pd_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTypeTable.GetColumnNames("[pd_a_at]") : string.Empty) + 
							(this.Depth > 0 ? "," + ClickTable.GetColumnNames("[pd_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + OfferTable.GetColumnNames("[pd_c_o]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTable.GetColumnNames("[pd_c_a]") : string.Empty) + 
							(this.Depth > 0 ? "," + PostbackRequestTable.GetColumnNames("[pd_pr]") : string.Empty) +  
					" FROM [core].[PostbackData] AS [pd] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Postback] AS [pd_p] ON [pd].[PostbackID] = [pd_p].[PostbackID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[PostbackType] AS [pd_p_pt] ON [pd_p].[PostbackTypeID] = [pd_p_pt].[PostbackTypeID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Affiliate] AS [pd_p_a] ON [pd_p].[AffiliateID] = [pd_p_a].[AffiliateID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Affiliate] AS [pd_a] ON [pd].[AffiliateID] = [pd_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[AffiliateType] AS [pd_a_at] ON [pd_a].[AffiliateTypeID] = [pd_a_at].[AffiliateTypeID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Click] AS [pd_c] ON [pd].[ClickID] = [pd_c].[ClickID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Offer] AS [pd_c_o] ON [pd_c].[OfferID] = [pd_c_o].[OfferID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Affiliate] AS [pd_c_a] ON [pd_c].[AffiliateID] = [pd_c_a].[AffiliateID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[PostbackRequest] AS [pd_pr] ON [pd].[PostbackRequestID] = [pd_pr].[PostbackRequestID] ";
				

        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("pd", "customloadmany", "notfound"), "PostbackData list could not be loaded using custom logic as no items were found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return new List<PostbackData>();
        }

        SqlQuery query = new SqlQuery(sqlReader);

				PostbackDataTable pdTable = new PostbackDataTable(query);
				PostbackTable pd_pTable = (this.Depth > 0) ? new PostbackTable(query) : null;
				PostbackTypeTable pd_p_ptTable = (this.Depth > 1) ? new PostbackTypeTable(query) : null;
				AffiliateTable pd_p_aTable = (this.Depth > 1) ? new AffiliateTable(query) : null;
				AffiliateTable pd_aTable = (this.Depth > 0) ? new AffiliateTable(query) : null;
				AffiliateTypeTable pd_a_atTable = (this.Depth > 1) ? new AffiliateTypeTable(query) : null;
				ClickTable pd_cTable = (this.Depth > 0) ? new ClickTable(query) : null;
				OfferTable pd_c_oTable = (this.Depth > 1) ? new OfferTable(query) : null;
				AffiliateTable pd_c_aTable = (this.Depth > 1) ? new AffiliateTable(query) : null;
				PostbackRequestTable pd_prTable = (this.Depth > 0) ? new PostbackRequestTable(query) : null;

        List<PostbackData> result = new List<PostbackData>();
        do
        {
          
					PostbackType pd_p_ptObject = (this.Depth > 1) ? pd_p_ptTable.CreateInstance() : null;
					Affiliate pd_p_aObject = (this.Depth > 1) ? pd_p_aTable.CreateInstance() : null;
					Postback pd_pObject = (this.Depth > 0) ? pd_pTable.CreateInstance(pd_p_ptObject, pd_p_aObject) : null;
					AffiliateType pd_a_atObject = (this.Depth > 1) ? pd_a_atTable.CreateInstance() : null;
					Affiliate pd_aObject = (this.Depth > 0) ? pd_aTable.CreateInstance(pd_a_atObject) : null;
					Offer pd_c_oObject = (this.Depth > 1) ? pd_c_oTable.CreateInstance() : null;
					Affiliate pd_c_aObject = (this.Depth > 1) ? pd_c_aTable.CreateInstance() : null;
					Click pd_cObject = (this.Depth > 0) ? pd_cTable.CreateInstance(pd_c_oObject, pd_c_aObject) : null;
					PostbackRequest pd_prObject = (this.Depth > 0) ? pd_prTable.CreateInstance() : null;
					PostbackData pdObject = (this.Depth > -1) ? pdTable.CreateInstance(pd_pObject, pd_aObject, pd_cObject, pd_prObject) : null;
					result.Add(pdObject);

        } while (sqlReader.Read());
        sqlReader.Close();

        return result;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("pd", "customloadmany", "exception"), "PostbackData list could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "PostbackData", "Exception while loading (custom/many) PostbackData object from database. See inner exception for details.", ex);
      }
    }
    
    public override int? Insert(ISqlConnectionInfo connection, PostbackData data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "INSERT INTO [core].[PostbackData] ([PostbackID],[AffiliateID],[ClickID],[PostbackRequestID],[EntranceUrl],[PostbackUrl]) VALUES(@PostbackID,@AffiliateID,@ClickID,@PostbackRequestID,@EntranceUrl,@PostbackUrl); SELECT SCOPE_IDENTITY();";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@PostbackID", data.Postback.ID);
				sqlCmd.Parameters.AddWithValue("@AffiliateID", data.Affiliate.ID);
				sqlCmd.Parameters.AddWithValue("@ClickID", data.Click.ID);
				sqlCmd.Parameters.AddWithValue("@PostbackRequestID", data.PostbackRequest.ID);
				sqlCmd.Parameters.AddWithValue("@EntranceUrl", data.EntranceUrl).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@PostbackUrl", data.PostbackUrl).SqlDbType = SqlDbType.NVarChar;
				
        object idObj = sqlCmd.ExecuteScalar();
        if (idObj == null || DBNull.Value.Equals(idObj))
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("pd", "insert", "noprimarykey"), "PostbackData could not be inserted or inserted primary key was not returned. Are you missing SELECT SCOPE_IDENTITY();?", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Insert, "PostbackData", "Exception while inserting PostbackData object in database.");
        }
        return (int)((decimal)idObj);
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("pd", "insert", "exception"), "PostbackData could not be inserted. See exception for details.", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Insert, "PostbackData", "Exception while inserting PostbackData object in database. See inner exception for details.", ex);
      }
    }

    public override bool Update(ISqlConnectionInfo connection, PostbackData data)
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
        sqlCmdText = "UPDATE [core].[PostbackData] SET " +
												"[PostbackID] = @PostbackID, " + 
												"[AffiliateID] = @AffiliateID, " + 
												"[ClickID] = @ClickID, " + 
												"[PostbackRequestID] = @PostbackRequestID, " + 
												"[EntranceUrl] = @EntranceUrl, " + 
												"[PostbackUrl] = @PostbackUrl, " + 
												"[Updated] = GETDATE() " + 
											"WHERE [PostbackDataID] = @PostbackDataID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@PostbackID", data.Postback.ID);
				sqlCmd.Parameters.AddWithValue("@AffiliateID", data.Affiliate.ID);
				sqlCmd.Parameters.AddWithValue("@ClickID", data.Click.ID);
				sqlCmd.Parameters.AddWithValue("@PostbackRequestID", data.PostbackRequest.ID);
				sqlCmd.Parameters.AddWithValue("@EntranceUrl", data.EntranceUrl).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@PostbackUrl", data.PostbackUrl).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@Updated", data.Updated).SqlDbType = SqlDbType.DateTime2;
				sqlCmd.Parameters.AddWithValue("@PostbackDataID", data.ID);

        int rowCount = sqlCmd.ExecuteNonQuery();
        if (rowCount < 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("pd", "update", "norecord"), "PostbackData could not be updated as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "PostbackData", "Exception while updating PostbackData object in database. No record found for this id.");
        }
        else if (rowCount > 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("pd", "update", "morerecords"), "PostbackData was updated but there was more than one record affected.", sqlCmdText, this, connection, data);
          if (this.Logger.IsFatalEnabled)
            this.Logger.Fatal(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "PostbackData", "Exception while updating PostbackData object in database. More than one record found for this statement (update statement where clause broken?!).");
        }
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("pd", "update", "exception"), "PostbackData could not be updated. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Update, "PostbackData", "Exception while updating PostbackData object in database. See inner exception for details.", ex);
      }
    }

    public override bool Delete(ISqlConnectionInfo connection, PostbackData data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText =  "DELETE FROM [core].[PostbackData] WHERE PostbackDataID = @PostbackDataID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@PostbackDataID", data.ID);

        int success = sqlCmd.ExecuteNonQuery();

        if (success == -1)        
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("pd", "delete", "norecord"), "PostbackData could not be deleted as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Delete, "PostbackData", "Exception while deleting PostbackData object from database. No such record found.");
        }
        
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("pd", "delete", "exception"), "PostbackData could not be deleted. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Delete, "PostbackData", "Exception while deleting PostbackData object from database. See inner exception for details.", ex);
      }
    }
  }
}

