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
  [DataManager(typeof(Click))] 
  public partial class ClickManager : Adlite.Data.Sql.SqlManagerBase<Click>, IClickManager
  {
    public override DatabaseType Type
    {
      get { return DatabaseType.Adlite; }
    }

    protected override Click LoadInternal(ISqlConnectionInfo connection, int id)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "SELECT " + 
							ClickTable.GetColumnNames("[c]") + 
							(this.Depth > 0 ? "," + OfferTable.GetColumnNames("[c_o]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignTable.GetColumnNames("[c_o_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTable.GetColumnNames("[c_o_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + PriceTable.GetColumnNames("[c_o_p]") : string.Empty) + 
							(this.Depth > 0 ? "," + AffiliateTable.GetColumnNames("[c_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTypeTable.GetColumnNames("[c_a_at]") : string.Empty) + 
					" FROM [core].[Click] AS [c] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Offer] AS [c_o] ON [c].[OfferID] = [c_o].[OfferID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Campaign] AS [c_o_c] ON [c_o].[CampaignID] = [c_o_c].[CampaignID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Affiliate] AS [c_o_a] ON [c_o].[AffiliateID] = [c_o_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Price] AS [c_o_p] ON [c_o].[PriceID] = [c_o_p].[PriceID] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Affiliate] AS [c_a] ON [c].[AffiliateID] = [c_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[AffiliateType] AS [c_a_at] ON [c_a].[AffiliateTypeID] = [c_a_at].[AffiliateTypeID] ";
				sqlCmdText += "WHERE [c].[ClickID] = @ClickID;";

        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@ClickID", id);
        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "loadinternal", "notfound"), "Click could not be loaded by id as it was not found.", sqlCmdText, this, connection, id);
          if (this.Logger.IsWarnEnabled)
            this.Logger.Warn(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				ClickTable cTable = new ClickTable(query);
				OfferTable c_oTable = (this.Depth > 0) ? new OfferTable(query) : null;
				CampaignTable c_o_cTable = (this.Depth > 1) ? new CampaignTable(query) : null;
				AffiliateTable c_o_aTable = (this.Depth > 1) ? new AffiliateTable(query) : null;
				PriceTable c_o_pTable = (this.Depth > 1) ? new PriceTable(query) : null;
				AffiliateTable c_aTable = (this.Depth > 0) ? new AffiliateTable(query) : null;
				AffiliateTypeTable c_a_atTable = (this.Depth > 1) ? new AffiliateTypeTable(query) : null;

        
				Campaign c_o_cObject = (this.Depth > 1) ? c_o_cTable.CreateInstance() : null;
				Affiliate c_o_aObject = (this.Depth > 1) ? c_o_aTable.CreateInstance() : null;
				Price c_o_pObject = (this.Depth > 1) ? c_o_pTable.CreateInstance() : null;
				Offer c_oObject = (this.Depth > 0) ? c_oTable.CreateInstance(c_o_cObject, c_o_aObject, c_o_pObject) : null;
				AffiliateType c_a_atObject = (this.Depth > 1) ? c_a_atTable.CreateInstance() : null;
				Affiliate c_aObject = (this.Depth > 0) ? c_aTable.CreateInstance(c_a_atObject) : null;
				Click cObject = cTable.CreateInstance(c_oObject, c_aObject);
				sqlReader.Close();

				return cObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "loadinternal", "exception"), "Click could not be loaded by id. See exception for details.", sqlCmdText, ex, this, connection, id);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "Click", "Exception while loading Click object from database. See inner exception for details.", ex);
      }
    }

    public Click Load(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							ClickTable.GetColumnNames("[c]") + 
							(this.Depth > 0 ? "," + OfferTable.GetColumnNames("[c_o]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignTable.GetColumnNames("[c_o_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTable.GetColumnNames("[c_o_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + PriceTable.GetColumnNames("[c_o_p]") : string.Empty) + 
							(this.Depth > 0 ? "," + AffiliateTable.GetColumnNames("[c_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTypeTable.GetColumnNames("[c_a_at]") : string.Empty) +  
					" FROM [core].[Click] AS [c] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Offer] AS [c_o] ON [c].[OfferID] = [c_o].[OfferID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Campaign] AS [c_o_c] ON [c_o].[CampaignID] = [c_o_c].[CampaignID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Affiliate] AS [c_o_a] ON [c_o].[AffiliateID] = [c_o_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Price] AS [c_o_p] ON [c_o].[PriceID] = [c_o_p].[PriceID] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Affiliate] AS [c_a] ON [c].[AffiliateID] = [c_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[AffiliateType] AS [c_a_at] ON [c_a].[AffiliateTypeID] = [c_a_at].[AffiliateTypeID] ";
				

        parameters.Top = 1;
        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "customload", "notfound"), "Click could not be loaded using custom logic as it was not found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				ClickTable cTable = new ClickTable(query);
				OfferTable c_oTable = (this.Depth > 0) ? new OfferTable(query) : null;
				CampaignTable c_o_cTable = (this.Depth > 1) ? new CampaignTable(query) : null;
				AffiliateTable c_o_aTable = (this.Depth > 1) ? new AffiliateTable(query) : null;
				PriceTable c_o_pTable = (this.Depth > 1) ? new PriceTable(query) : null;
				AffiliateTable c_aTable = (this.Depth > 0) ? new AffiliateTable(query) : null;
				AffiliateTypeTable c_a_atTable = (this.Depth > 1) ? new AffiliateTypeTable(query) : null;

        
				Campaign c_o_cObject = (this.Depth > 1) ? c_o_cTable.CreateInstance() : null;
				Affiliate c_o_aObject = (this.Depth > 1) ? c_o_aTable.CreateInstance() : null;
				Price c_o_pObject = (this.Depth > 1) ? c_o_pTable.CreateInstance() : null;
				Offer c_oObject = (this.Depth > 0) ? c_oTable.CreateInstance(c_o_cObject, c_o_aObject, c_o_pObject) : null;
				AffiliateType c_a_atObject = (this.Depth > 1) ? c_a_atTable.CreateInstance() : null;
				Affiliate c_aObject = (this.Depth > 0) ? c_aTable.CreateInstance(c_a_atObject) : null;
				Click cObject = cTable.CreateInstance(c_oObject, c_aObject);
				sqlReader.Close();

				return cObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "customload", "exception"), "Click could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "Click", "Exception while loading (custom/single) Click object from database. See inner exception for details.", ex);
      }
    }

    public List<Click> LoadMany(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							ClickTable.GetColumnNames("[c]") + 
							(this.Depth > 0 ? "," + OfferTable.GetColumnNames("[c_o]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignTable.GetColumnNames("[c_o_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTable.GetColumnNames("[c_o_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + PriceTable.GetColumnNames("[c_o_p]") : string.Empty) + 
							(this.Depth > 0 ? "," + AffiliateTable.GetColumnNames("[c_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTypeTable.GetColumnNames("[c_a_at]") : string.Empty) +  
					" FROM [core].[Click] AS [c] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Offer] AS [c_o] ON [c].[OfferID] = [c_o].[OfferID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Campaign] AS [c_o_c] ON [c_o].[CampaignID] = [c_o_c].[CampaignID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Affiliate] AS [c_o_a] ON [c_o].[AffiliateID] = [c_o_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Price] AS [c_o_p] ON [c_o].[PriceID] = [c_o_p].[PriceID] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Affiliate] AS [c_a] ON [c].[AffiliateID] = [c_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[AffiliateType] AS [c_a_at] ON [c_a].[AffiliateTypeID] = [c_a_at].[AffiliateTypeID] ";
				

        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "customloadmany", "notfound"), "Click list could not be loaded using custom logic as no items were found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return new List<Click>();
        }

        SqlQuery query = new SqlQuery(sqlReader);

				ClickTable cTable = new ClickTable(query);
				OfferTable c_oTable = (this.Depth > 0) ? new OfferTable(query) : null;
				CampaignTable c_o_cTable = (this.Depth > 1) ? new CampaignTable(query) : null;
				AffiliateTable c_o_aTable = (this.Depth > 1) ? new AffiliateTable(query) : null;
				PriceTable c_o_pTable = (this.Depth > 1) ? new PriceTable(query) : null;
				AffiliateTable c_aTable = (this.Depth > 0) ? new AffiliateTable(query) : null;
				AffiliateTypeTable c_a_atTable = (this.Depth > 1) ? new AffiliateTypeTable(query) : null;

        List<Click> result = new List<Click>();
        do
        {
          
					Campaign c_o_cObject = (this.Depth > 1) ? c_o_cTable.CreateInstance() : null;
					Affiliate c_o_aObject = (this.Depth > 1) ? c_o_aTable.CreateInstance() : null;
					Price c_o_pObject = (this.Depth > 1) ? c_o_pTable.CreateInstance() : null;
					Offer c_oObject = (this.Depth > 0) ? c_oTable.CreateInstance(c_o_cObject, c_o_aObject, c_o_pObject) : null;
					AffiliateType c_a_atObject = (this.Depth > 1) ? c_a_atTable.CreateInstance() : null;
					Affiliate c_aObject = (this.Depth > 0) ? c_aTable.CreateInstance(c_a_atObject) : null;
					Click cObject = (this.Depth > -1) ? cTable.CreateInstance(c_oObject, c_aObject) : null;
					result.Add(cObject);

        } while (sqlReader.Read());
        sqlReader.Close();

        return result;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "customloadmany", "exception"), "Click list could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "Click", "Exception while loading (custom/many) Click object from database. See inner exception for details.", ex);
      }
    }
    
    public override int? Insert(ISqlConnectionInfo connection, Click data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "INSERT INTO [core].[Click] ([SubID],[OfferID],[AffiliateID],[HasTransaction]) VALUES(@SubID,@OfferID,@AffiliateID,@HasTransaction); SELECT SCOPE_IDENTITY();";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@SubID", data.SubID).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@OfferID", data.Offer == null ? DBNull.Value : (object)data.Offer.ID);
				sqlCmd.Parameters.AddWithValue("@AffiliateID", data.Affiliate == null ? DBNull.Value : (object)data.Affiliate.ID);
				sqlCmd.Parameters.AddWithValue("@HasTransaction", data.HasTransaction).SqlDbType = SqlDbType.Bit;
				
        object idObj = sqlCmd.ExecuteScalar();
        if (idObj == null || DBNull.Value.Equals(idObj))
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "insert", "noprimarykey"), "Click could not be inserted or inserted primary key was not returned. Are you missing SELECT SCOPE_IDENTITY();?", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Insert, "Click", "Exception while inserting Click object in database.");
        }
        return (int)((decimal)idObj);
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "insert", "exception"), "Click could not be inserted. See exception for details.", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Insert, "Click", "Exception while inserting Click object in database. See inner exception for details.", ex);
      }
    }

    public override bool Update(ISqlConnectionInfo connection, Click data)
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
        sqlCmdText = "UPDATE [core].[Click] SET " +
												"[SubID] = @SubID, " + 
												"[OfferID] = @OfferID, " + 
												"[AffiliateID] = @AffiliateID, " + 
												"[HasTransaction] = @HasTransaction, " + 
												"[Updated] = GETDATE() " + 
											"WHERE [ClickID] = @ClickID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@SubID", data.SubID).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@OfferID", data.Offer == null ? DBNull.Value : (object)data.Offer.ID);
				sqlCmd.Parameters.AddWithValue("@AffiliateID", data.Affiliate == null ? DBNull.Value : (object)data.Affiliate.ID);
				sqlCmd.Parameters.AddWithValue("@HasTransaction", data.HasTransaction).SqlDbType = SqlDbType.Bit;
				sqlCmd.Parameters.AddWithValue("@Updated", data.Updated).SqlDbType = SqlDbType.DateTime2;
				sqlCmd.Parameters.AddWithValue("@ClickID", data.ID);

        int rowCount = sqlCmd.ExecuteNonQuery();
        if (rowCount < 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "update", "norecord"), "Click could not be updated as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "Click", "Exception while updating Click object in database. No record found for this id.");
        }
        else if (rowCount > 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "update", "morerecords"), "Click was updated but there was more than one record affected.", sqlCmdText, this, connection, data);
          if (this.Logger.IsFatalEnabled)
            this.Logger.Fatal(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "Click", "Exception while updating Click object in database. More than one record found for this statement (update statement where clause broken?!).");
        }
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "update", "exception"), "Click could not be updated. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Update, "Click", "Exception while updating Click object in database. See inner exception for details.", ex);
      }
    }

    public override bool Delete(ISqlConnectionInfo connection, Click data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText =  "DELETE FROM [core].[Click] WHERE ClickID = @ClickID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@ClickID", data.ID);

        int success = sqlCmd.ExecuteNonQuery();

        if (success == -1)        
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "delete", "norecord"), "Click could not be deleted as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Delete, "Click", "Exception while deleting Click object from database. No such record found.");
        }
        
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "delete", "exception"), "Click could not be deleted. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Delete, "Click", "Exception while deleting Click object from database. See inner exception for details.", ex);
      }
    }
  }
}

