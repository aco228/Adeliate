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
  [DataManager(typeof(OfferCapMap))] 
  public partial class OfferCapMapManager : Adlite.Data.Sql.SqlManagerBase<OfferCapMap>, IOfferCapMapManager
  {
    public override DatabaseType Type
    {
      get { return DatabaseType.Adlite; }
    }

    protected override OfferCapMap LoadInternal(ISqlConnectionInfo connection, int id)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "SELECT " + 
							OfferCapMapTable.GetColumnNames("[ocm]") + 
							(this.Depth > 0 ? "," + OfferTable.GetColumnNames("[ocm_o]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignTable.GetColumnNames("[ocm_o_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTable.GetColumnNames("[ocm_o_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + PriceTable.GetColumnNames("[ocm_o_p]") : string.Empty) + 
					" FROM [core].[OfferCapMap] AS [ocm] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Offer] AS [ocm_o] ON [ocm].[OfferID] = [ocm_o].[OfferID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Campaign] AS [ocm_o_c] ON [ocm_o].[CampaignID] = [ocm_o_c].[CampaignID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Affiliate] AS [ocm_o_a] ON [ocm_o].[AffiliateID] = [ocm_o_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Price] AS [ocm_o_p] ON [ocm_o].[PriceID] = [ocm_o_p].[PriceID] ";
				sqlCmdText += "WHERE [ocm].[OfferCapMapID] = @OfferCapMapID;";

        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@OfferCapMapID", id);
        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("ocm", "loadinternal", "notfound"), "OfferCapMap could not be loaded by id as it was not found.", sqlCmdText, this, connection, id);
          if (this.Logger.IsWarnEnabled)
            this.Logger.Warn(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				OfferCapMapTable ocmTable = new OfferCapMapTable(query);
				OfferTable ocm_oTable = (this.Depth > 0) ? new OfferTable(query) : null;
				CampaignTable ocm_o_cTable = (this.Depth > 1) ? new CampaignTable(query) : null;
				AffiliateTable ocm_o_aTable = (this.Depth > 1) ? new AffiliateTable(query) : null;
				PriceTable ocm_o_pTable = (this.Depth > 1) ? new PriceTable(query) : null;

        
				Campaign ocm_o_cObject = (this.Depth > 1) ? ocm_o_cTable.CreateInstance() : null;
				Affiliate ocm_o_aObject = (this.Depth > 1) ? ocm_o_aTable.CreateInstance() : null;
				Price ocm_o_pObject = (this.Depth > 1) ? ocm_o_pTable.CreateInstance() : null;
				Offer ocm_oObject = (this.Depth > 0) ? ocm_oTable.CreateInstance(ocm_o_cObject, ocm_o_aObject, ocm_o_pObject) : null;
				OfferCapMap ocmObject = ocmTable.CreateInstance(ocm_oObject);
				sqlReader.Close();

				return ocmObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("ocm", "loadinternal", "exception"), "OfferCapMap could not be loaded by id. See exception for details.", sqlCmdText, ex, this, connection, id);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "OfferCapMap", "Exception while loading OfferCapMap object from database. See inner exception for details.", ex);
      }
    }

    public OfferCapMap Load(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							OfferCapMapTable.GetColumnNames("[ocm]") + 
							(this.Depth > 0 ? "," + OfferTable.GetColumnNames("[ocm_o]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignTable.GetColumnNames("[ocm_o_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTable.GetColumnNames("[ocm_o_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + PriceTable.GetColumnNames("[ocm_o_p]") : string.Empty) +  
					" FROM [core].[OfferCapMap] AS [ocm] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Offer] AS [ocm_o] ON [ocm].[OfferID] = [ocm_o].[OfferID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Campaign] AS [ocm_o_c] ON [ocm_o].[CampaignID] = [ocm_o_c].[CampaignID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Affiliate] AS [ocm_o_a] ON [ocm_o].[AffiliateID] = [ocm_o_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Price] AS [ocm_o_p] ON [ocm_o].[PriceID] = [ocm_o_p].[PriceID] ";
				

        parameters.Top = 1;
        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("ocm", "customload", "notfound"), "OfferCapMap could not be loaded using custom logic as it was not found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				OfferCapMapTable ocmTable = new OfferCapMapTable(query);
				OfferTable ocm_oTable = (this.Depth > 0) ? new OfferTable(query) : null;
				CampaignTable ocm_o_cTable = (this.Depth > 1) ? new CampaignTable(query) : null;
				AffiliateTable ocm_o_aTable = (this.Depth > 1) ? new AffiliateTable(query) : null;
				PriceTable ocm_o_pTable = (this.Depth > 1) ? new PriceTable(query) : null;

        
				Campaign ocm_o_cObject = (this.Depth > 1) ? ocm_o_cTable.CreateInstance() : null;
				Affiliate ocm_o_aObject = (this.Depth > 1) ? ocm_o_aTable.CreateInstance() : null;
				Price ocm_o_pObject = (this.Depth > 1) ? ocm_o_pTable.CreateInstance() : null;
				Offer ocm_oObject = (this.Depth > 0) ? ocm_oTable.CreateInstance(ocm_o_cObject, ocm_o_aObject, ocm_o_pObject) : null;
				OfferCapMap ocmObject = ocmTable.CreateInstance(ocm_oObject);
				sqlReader.Close();

				return ocmObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("ocm", "customload", "exception"), "OfferCapMap could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "OfferCapMap", "Exception while loading (custom/single) OfferCapMap object from database. See inner exception for details.", ex);
      }
    }

    public List<OfferCapMap> LoadMany(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							OfferCapMapTable.GetColumnNames("[ocm]") + 
							(this.Depth > 0 ? "," + OfferTable.GetColumnNames("[ocm_o]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignTable.GetColumnNames("[ocm_o_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTable.GetColumnNames("[ocm_o_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + PriceTable.GetColumnNames("[ocm_o_p]") : string.Empty) +  
					" FROM [core].[OfferCapMap] AS [ocm] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Offer] AS [ocm_o] ON [ocm].[OfferID] = [ocm_o].[OfferID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Campaign] AS [ocm_o_c] ON [ocm_o].[CampaignID] = [ocm_o_c].[CampaignID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Affiliate] AS [ocm_o_a] ON [ocm_o].[AffiliateID] = [ocm_o_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Price] AS [ocm_o_p] ON [ocm_o].[PriceID] = [ocm_o_p].[PriceID] ";
				

        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("ocm", "customloadmany", "notfound"), "OfferCapMap list could not be loaded using custom logic as no items were found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return new List<OfferCapMap>();
        }

        SqlQuery query = new SqlQuery(sqlReader);

				OfferCapMapTable ocmTable = new OfferCapMapTable(query);
				OfferTable ocm_oTable = (this.Depth > 0) ? new OfferTable(query) : null;
				CampaignTable ocm_o_cTable = (this.Depth > 1) ? new CampaignTable(query) : null;
				AffiliateTable ocm_o_aTable = (this.Depth > 1) ? new AffiliateTable(query) : null;
				PriceTable ocm_o_pTable = (this.Depth > 1) ? new PriceTable(query) : null;

        List<OfferCapMap> result = new List<OfferCapMap>();
        do
        {
          
					Campaign ocm_o_cObject = (this.Depth > 1) ? ocm_o_cTable.CreateInstance() : null;
					Affiliate ocm_o_aObject = (this.Depth > 1) ? ocm_o_aTable.CreateInstance() : null;
					Price ocm_o_pObject = (this.Depth > 1) ? ocm_o_pTable.CreateInstance() : null;
					Offer ocm_oObject = (this.Depth > 0) ? ocm_oTable.CreateInstance(ocm_o_cObject, ocm_o_aObject, ocm_o_pObject) : null;
					OfferCapMap ocmObject = (this.Depth > -1) ? ocmTable.CreateInstance(ocm_oObject) : null;
					result.Add(ocmObject);

        } while (sqlReader.Read());
        sqlReader.Close();

        return result;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("ocm", "customloadmany", "exception"), "OfferCapMap list could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "OfferCapMap", "Exception while loading (custom/many) OfferCapMap object from database. See inner exception for details.", ex);
      }
    }
    
    public override int? Insert(ISqlConnectionInfo connection, OfferCapMap data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "INSERT INTO [core].[OfferCapMap] ([OfferID],[Transactions],[Locked]) VALUES(@OfferID,@Transactions,@Locked); SELECT SCOPE_IDENTITY();";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@OfferID", data.Offer.ID);
				sqlCmd.Parameters.AddWithValue("@Transactions", data.Transactions).SqlDbType = SqlDbType.Int;
				sqlCmd.Parameters.AddWithValue("@Locked", data.Locked).SqlDbType = SqlDbType.Bit;
				
        object idObj = sqlCmd.ExecuteScalar();
        if (idObj == null || DBNull.Value.Equals(idObj))
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("ocm", "insert", "noprimarykey"), "OfferCapMap could not be inserted or inserted primary key was not returned. Are you missing SELECT SCOPE_IDENTITY();?", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Insert, "OfferCapMap", "Exception while inserting OfferCapMap object in database.");
        }
        return (int)((decimal)idObj);
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("ocm", "insert", "exception"), "OfferCapMap could not be inserted. See exception for details.", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Insert, "OfferCapMap", "Exception while inserting OfferCapMap object in database. See inner exception for details.", ex);
      }
    }

    public override bool Update(ISqlConnectionInfo connection, OfferCapMap data)
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
        sqlCmdText = "UPDATE [core].[OfferCapMap] SET " +
												"[OfferID] = @OfferID, " + 
												"[Transactions] = @Transactions, " + 
												"[Locked] = @Locked, " + 
												"[Updated] = GETDATE() " + 
											"WHERE [OfferCapMapID] = @OfferCapMapID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@OfferID", data.Offer.ID);
				sqlCmd.Parameters.AddWithValue("@Transactions", data.Transactions).SqlDbType = SqlDbType.Int;
				sqlCmd.Parameters.AddWithValue("@Locked", data.Locked).SqlDbType = SqlDbType.Bit;
				sqlCmd.Parameters.AddWithValue("@Updated", data.Updated).SqlDbType = SqlDbType.DateTime2;
				sqlCmd.Parameters.AddWithValue("@OfferCapMapID", data.ID);

        int rowCount = sqlCmd.ExecuteNonQuery();
        if (rowCount < 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("ocm", "update", "norecord"), "OfferCapMap could not be updated as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "OfferCapMap", "Exception while updating OfferCapMap object in database. No record found for this id.");
        }
        else if (rowCount > 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("ocm", "update", "morerecords"), "OfferCapMap was updated but there was more than one record affected.", sqlCmdText, this, connection, data);
          if (this.Logger.IsFatalEnabled)
            this.Logger.Fatal(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "OfferCapMap", "Exception while updating OfferCapMap object in database. More than one record found for this statement (update statement where clause broken?!).");
        }
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("ocm", "update", "exception"), "OfferCapMap could not be updated. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Update, "OfferCapMap", "Exception while updating OfferCapMap object in database. See inner exception for details.", ex);
      }
    }

    public override bool Delete(ISqlConnectionInfo connection, OfferCapMap data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText =  "DELETE FROM [core].[OfferCapMap] WHERE OfferCapMapID = @OfferCapMapID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@OfferCapMapID", data.ID);

        int success = sqlCmd.ExecuteNonQuery();

        if (success == -1)        
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("ocm", "delete", "norecord"), "OfferCapMap could not be deleted as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Delete, "OfferCapMap", "Exception while deleting OfferCapMap object from database. No such record found.");
        }
        
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("ocm", "delete", "exception"), "OfferCapMap could not be deleted. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Delete, "OfferCapMap", "Exception while deleting OfferCapMap object from database. See inner exception for details.", ex);
      }
    }
  }
}

