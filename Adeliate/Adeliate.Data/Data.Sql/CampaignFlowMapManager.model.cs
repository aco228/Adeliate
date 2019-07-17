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
  [DataManager(typeof(CampaignFlowMap))] 
  public partial class CampaignFlowMapManager : Adlite.Data.Sql.SqlManagerBase<CampaignFlowMap>, ICampaignFlowMapManager
  {
    public override DatabaseType Type
    {
      get { return DatabaseType.Adlite; }
    }

    protected override CampaignFlowMap LoadInternal(ISqlConnectionInfo connection, int id)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "SELECT " + 
							CampaignFlowMapTable.GetColumnNames("[cfm]") + 
							(this.Depth > 0 ? "," + CampaignTable.GetColumnNames("[cfm_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignGroupTable.GetColumnNames("[cfm_c_cg]") : string.Empty) + 
							(this.Depth > 1 ? "," + CountryTable.GetColumnNames("[cfm_c_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + PriceTable.GetColumnNames("[cfm_c_p]") : string.Empty) + 
					" FROM [core].[CampaignFlowMap] AS [cfm] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Campaign] AS [cfm_c] ON [cfm].[CampaignID] = [cfm_c].[CampaignID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[CampaignGroup] AS [cfm_c_cg] ON [cfm_c].[CampaignGroupID] = [cfm_c_cg].[CampaignGroupID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Country] AS [cfm_c_c] ON [cfm_c].[CountryID] = [cfm_c_c].[CountryID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Price] AS [cfm_c_p] ON [cfm_c].[PriceID] = [cfm_c_p].[PriceID] ";
				sqlCmdText += "WHERE [cfm].[CampaignFlowMapID] = @CampaignFlowMapID;";

        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@CampaignFlowMapID", id);
        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cfm", "loadinternal", "notfound"), "CampaignFlowMap could not be loaded by id as it was not found.", sqlCmdText, this, connection, id);
          if (this.Logger.IsWarnEnabled)
            this.Logger.Warn(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				CampaignFlowMapTable cfmTable = new CampaignFlowMapTable(query);
				CampaignTable cfm_cTable = (this.Depth > 0) ? new CampaignTable(query) : null;
				CampaignGroupTable cfm_c_cgTable = (this.Depth > 1) ? new CampaignGroupTable(query) : null;
				CountryTable cfm_c_cTable = (this.Depth > 1) ? new CountryTable(query) : null;
				PriceTable cfm_c_pTable = (this.Depth > 1) ? new PriceTable(query) : null;

        
				CampaignGroup cfm_c_cgObject = (this.Depth > 1) ? cfm_c_cgTable.CreateInstance() : null;
				Country cfm_c_cObject = (this.Depth > 1) ? cfm_c_cTable.CreateInstance() : null;
				Price cfm_c_pObject = (this.Depth > 1) ? cfm_c_pTable.CreateInstance() : null;
				Campaign cfm_cObject = (this.Depth > 0) ? cfm_cTable.CreateInstance(cfm_c_cgObject, cfm_c_cObject, cfm_c_pObject) : null;
				CampaignFlowMap cfmObject = cfmTable.CreateInstance(cfm_cObject);
				sqlReader.Close();

				return cfmObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cfm", "loadinternal", "exception"), "CampaignFlowMap could not be loaded by id. See exception for details.", sqlCmdText, ex, this, connection, id);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "CampaignFlowMap", "Exception while loading CampaignFlowMap object from database. See inner exception for details.", ex);
      }
    }

    public CampaignFlowMap Load(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							CampaignFlowMapTable.GetColumnNames("[cfm]") + 
							(this.Depth > 0 ? "," + CampaignTable.GetColumnNames("[cfm_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignGroupTable.GetColumnNames("[cfm_c_cg]") : string.Empty) + 
							(this.Depth > 1 ? "," + CountryTable.GetColumnNames("[cfm_c_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + PriceTable.GetColumnNames("[cfm_c_p]") : string.Empty) +  
					" FROM [core].[CampaignFlowMap] AS [cfm] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Campaign] AS [cfm_c] ON [cfm].[CampaignID] = [cfm_c].[CampaignID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[CampaignGroup] AS [cfm_c_cg] ON [cfm_c].[CampaignGroupID] = [cfm_c_cg].[CampaignGroupID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Country] AS [cfm_c_c] ON [cfm_c].[CountryID] = [cfm_c_c].[CountryID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Price] AS [cfm_c_p] ON [cfm_c].[PriceID] = [cfm_c_p].[PriceID] ";
				

        parameters.Top = 1;
        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cfm", "customload", "notfound"), "CampaignFlowMap could not be loaded using custom logic as it was not found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				CampaignFlowMapTable cfmTable = new CampaignFlowMapTable(query);
				CampaignTable cfm_cTable = (this.Depth > 0) ? new CampaignTable(query) : null;
				CampaignGroupTable cfm_c_cgTable = (this.Depth > 1) ? new CampaignGroupTable(query) : null;
				CountryTable cfm_c_cTable = (this.Depth > 1) ? new CountryTable(query) : null;
				PriceTable cfm_c_pTable = (this.Depth > 1) ? new PriceTable(query) : null;

        
				CampaignGroup cfm_c_cgObject = (this.Depth > 1) ? cfm_c_cgTable.CreateInstance() : null;
				Country cfm_c_cObject = (this.Depth > 1) ? cfm_c_cTable.CreateInstance() : null;
				Price cfm_c_pObject = (this.Depth > 1) ? cfm_c_pTable.CreateInstance() : null;
				Campaign cfm_cObject = (this.Depth > 0) ? cfm_cTable.CreateInstance(cfm_c_cgObject, cfm_c_cObject, cfm_c_pObject) : null;
				CampaignFlowMap cfmObject = cfmTable.CreateInstance(cfm_cObject);
				sqlReader.Close();

				return cfmObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cfm", "customload", "exception"), "CampaignFlowMap could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "CampaignFlowMap", "Exception while loading (custom/single) CampaignFlowMap object from database. See inner exception for details.", ex);
      }
    }

    public List<CampaignFlowMap> LoadMany(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							CampaignFlowMapTable.GetColumnNames("[cfm]") + 
							(this.Depth > 0 ? "," + CampaignTable.GetColumnNames("[cfm_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignGroupTable.GetColumnNames("[cfm_c_cg]") : string.Empty) + 
							(this.Depth > 1 ? "," + CountryTable.GetColumnNames("[cfm_c_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + PriceTable.GetColumnNames("[cfm_c_p]") : string.Empty) +  
					" FROM [core].[CampaignFlowMap] AS [cfm] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Campaign] AS [cfm_c] ON [cfm].[CampaignID] = [cfm_c].[CampaignID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[CampaignGroup] AS [cfm_c_cg] ON [cfm_c].[CampaignGroupID] = [cfm_c_cg].[CampaignGroupID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Country] AS [cfm_c_c] ON [cfm_c].[CountryID] = [cfm_c_c].[CountryID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Price] AS [cfm_c_p] ON [cfm_c].[PriceID] = [cfm_c_p].[PriceID] ";
				

        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cfm", "customloadmany", "notfound"), "CampaignFlowMap list could not be loaded using custom logic as no items were found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return new List<CampaignFlowMap>();
        }

        SqlQuery query = new SqlQuery(sqlReader);

				CampaignFlowMapTable cfmTable = new CampaignFlowMapTable(query);
				CampaignTable cfm_cTable = (this.Depth > 0) ? new CampaignTable(query) : null;
				CampaignGroupTable cfm_c_cgTable = (this.Depth > 1) ? new CampaignGroupTable(query) : null;
				CountryTable cfm_c_cTable = (this.Depth > 1) ? new CountryTable(query) : null;
				PriceTable cfm_c_pTable = (this.Depth > 1) ? new PriceTable(query) : null;

        List<CampaignFlowMap> result = new List<CampaignFlowMap>();
        do
        {
          
					CampaignGroup cfm_c_cgObject = (this.Depth > 1) ? cfm_c_cgTable.CreateInstance() : null;
					Country cfm_c_cObject = (this.Depth > 1) ? cfm_c_cTable.CreateInstance() : null;
					Price cfm_c_pObject = (this.Depth > 1) ? cfm_c_pTable.CreateInstance() : null;
					Campaign cfm_cObject = (this.Depth > 0) ? cfm_cTable.CreateInstance(cfm_c_cgObject, cfm_c_cObject, cfm_c_pObject) : null;
					CampaignFlowMap cfmObject = (this.Depth > -1) ? cfmTable.CreateInstance(cfm_cObject) : null;
					result.Add(cfmObject);

        } while (sqlReader.Read());
        sqlReader.Close();

        return result;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cfm", "customloadmany", "exception"), "CampaignFlowMap list could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "CampaignFlowMap", "Exception while loading (custom/many) CampaignFlowMap object from database. See inner exception for details.", ex);
      }
    }
    
    public override int? Insert(ISqlConnectionInfo connection, CampaignFlowMap data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "INSERT INTO [core].[CampaignFlowMap] ([CampaignID],[CampaignFlowID]) VALUES(@CampaignID,@CampaignFlowID); SELECT SCOPE_IDENTITY();";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@CampaignID", data.Campaign.ID);
				sqlCmd.Parameters.AddWithValue("@CampaignFlowID", (int)data.CampaignFlow);
				
        object idObj = sqlCmd.ExecuteScalar();
        if (idObj == null || DBNull.Value.Equals(idObj))
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cfm", "insert", "noprimarykey"), "CampaignFlowMap could not be inserted or inserted primary key was not returned. Are you missing SELECT SCOPE_IDENTITY();?", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Insert, "CampaignFlowMap", "Exception while inserting CampaignFlowMap object in database.");
        }
        return (int)((decimal)idObj);
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cfm", "insert", "exception"), "CampaignFlowMap could not be inserted. See exception for details.", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Insert, "CampaignFlowMap", "Exception while inserting CampaignFlowMap object in database. See inner exception for details.", ex);
      }
    }

    public override bool Update(ISqlConnectionInfo connection, CampaignFlowMap data)
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
        sqlCmdText = "UPDATE [core].[CampaignFlowMap] SET " +
												"[CampaignID] = @CampaignID, " + 
												"[CampaignFlowID] = @CampaignFlowID, " + 
												"[Updated] = GETDATE() " + 
											"WHERE [CampaignFlowMapID] = @CampaignFlowMapID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@CampaignID", data.Campaign.ID);
				sqlCmd.Parameters.AddWithValue("@CampaignFlowID", (int)data.CampaignFlow);
				sqlCmd.Parameters.AddWithValue("@Updated", data.Updated).SqlDbType = SqlDbType.DateTime2;
				sqlCmd.Parameters.AddWithValue("@CampaignFlowMapID", data.ID);

        int rowCount = sqlCmd.ExecuteNonQuery();
        if (rowCount < 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cfm", "update", "norecord"), "CampaignFlowMap could not be updated as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "CampaignFlowMap", "Exception while updating CampaignFlowMap object in database. No record found for this id.");
        }
        else if (rowCount > 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cfm", "update", "morerecords"), "CampaignFlowMap was updated but there was more than one record affected.", sqlCmdText, this, connection, data);
          if (this.Logger.IsFatalEnabled)
            this.Logger.Fatal(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "CampaignFlowMap", "Exception while updating CampaignFlowMap object in database. More than one record found for this statement (update statement where clause broken?!).");
        }
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cfm", "update", "exception"), "CampaignFlowMap could not be updated. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Update, "CampaignFlowMap", "Exception while updating CampaignFlowMap object in database. See inner exception for details.", ex);
      }
    }

    public override bool Delete(ISqlConnectionInfo connection, CampaignFlowMap data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText =  "DELETE FROM [core].[CampaignFlowMap] WHERE CampaignFlowMapID = @CampaignFlowMapID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@CampaignFlowMapID", data.ID);

        int success = sqlCmd.ExecuteNonQuery();

        if (success == -1)        
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cfm", "delete", "norecord"), "CampaignFlowMap could not be deleted as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Delete, "CampaignFlowMap", "Exception while deleting CampaignFlowMap object from database. No such record found.");
        }
        
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cfm", "delete", "exception"), "CampaignFlowMap could not be deleted. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Delete, "CampaignFlowMap", "Exception while deleting CampaignFlowMap object from database. See inner exception for details.", ex);
      }
    }
  }
}

