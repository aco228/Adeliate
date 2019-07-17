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
  [DataManager(typeof(CampaignDeviceMap))] 
  public partial class CampaignDeviceMapManager : Adlite.Data.Sql.SqlManagerBase<CampaignDeviceMap>, ICampaignDeviceMapManager
  {
    public override DatabaseType Type
    {
      get { return DatabaseType.Adlite; }
    }

    protected override CampaignDeviceMap LoadInternal(ISqlConnectionInfo connection, int id)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "SELECT " + 
							CampaignDeviceMapTable.GetColumnNames("[cdm]") + 
							(this.Depth > 0 ? "," + CampaignTable.GetColumnNames("[cdm_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignGroupTable.GetColumnNames("[cdm_c_cg]") : string.Empty) + 
							(this.Depth > 1 ? "," + CountryTable.GetColumnNames("[cdm_c_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + PriceTable.GetColumnNames("[cdm_c_p]") : string.Empty) + 
					" FROM [core].[CampaignDeviceMap] AS [cdm] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Campaign] AS [cdm_c] ON [cdm].[CampaignID] = [cdm_c].[CampaignID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[CampaignGroup] AS [cdm_c_cg] ON [cdm_c].[CampaignGroupID] = [cdm_c_cg].[CampaignGroupID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Country] AS [cdm_c_c] ON [cdm_c].[CountryID] = [cdm_c_c].[CountryID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Price] AS [cdm_c_p] ON [cdm_c].[PriceID] = [cdm_c_p].[PriceID] ";
				sqlCmdText += "WHERE [cdm].[CampaignDeviceMapID] = @CampaignDeviceMapID;";

        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@CampaignDeviceMapID", id);
        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cdm", "loadinternal", "notfound"), "CampaignDeviceMap could not be loaded by id as it was not found.", sqlCmdText, this, connection, id);
          if (this.Logger.IsWarnEnabled)
            this.Logger.Warn(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				CampaignDeviceMapTable cdmTable = new CampaignDeviceMapTable(query);
				CampaignTable cdm_cTable = (this.Depth > 0) ? new CampaignTable(query) : null;
				CampaignGroupTable cdm_c_cgTable = (this.Depth > 1) ? new CampaignGroupTable(query) : null;
				CountryTable cdm_c_cTable = (this.Depth > 1) ? new CountryTable(query) : null;
				PriceTable cdm_c_pTable = (this.Depth > 1) ? new PriceTable(query) : null;

        
				CampaignGroup cdm_c_cgObject = (this.Depth > 1) ? cdm_c_cgTable.CreateInstance() : null;
				Country cdm_c_cObject = (this.Depth > 1) ? cdm_c_cTable.CreateInstance() : null;
				Price cdm_c_pObject = (this.Depth > 1) ? cdm_c_pTable.CreateInstance() : null;
				Campaign cdm_cObject = (this.Depth > 0) ? cdm_cTable.CreateInstance(cdm_c_cgObject, cdm_c_cObject, cdm_c_pObject) : null;
				CampaignDeviceMap cdmObject = cdmTable.CreateInstance(cdm_cObject);
				sqlReader.Close();

				return cdmObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cdm", "loadinternal", "exception"), "CampaignDeviceMap could not be loaded by id. See exception for details.", sqlCmdText, ex, this, connection, id);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "CampaignDeviceMap", "Exception while loading CampaignDeviceMap object from database. See inner exception for details.", ex);
      }
    }

    public CampaignDeviceMap Load(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							CampaignDeviceMapTable.GetColumnNames("[cdm]") + 
							(this.Depth > 0 ? "," + CampaignTable.GetColumnNames("[cdm_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignGroupTable.GetColumnNames("[cdm_c_cg]") : string.Empty) + 
							(this.Depth > 1 ? "," + CountryTable.GetColumnNames("[cdm_c_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + PriceTable.GetColumnNames("[cdm_c_p]") : string.Empty) +  
					" FROM [core].[CampaignDeviceMap] AS [cdm] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Campaign] AS [cdm_c] ON [cdm].[CampaignID] = [cdm_c].[CampaignID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[CampaignGroup] AS [cdm_c_cg] ON [cdm_c].[CampaignGroupID] = [cdm_c_cg].[CampaignGroupID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Country] AS [cdm_c_c] ON [cdm_c].[CountryID] = [cdm_c_c].[CountryID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Price] AS [cdm_c_p] ON [cdm_c].[PriceID] = [cdm_c_p].[PriceID] ";
				

        parameters.Top = 1;
        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cdm", "customload", "notfound"), "CampaignDeviceMap could not be loaded using custom logic as it was not found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				CampaignDeviceMapTable cdmTable = new CampaignDeviceMapTable(query);
				CampaignTable cdm_cTable = (this.Depth > 0) ? new CampaignTable(query) : null;
				CampaignGroupTable cdm_c_cgTable = (this.Depth > 1) ? new CampaignGroupTable(query) : null;
				CountryTable cdm_c_cTable = (this.Depth > 1) ? new CountryTable(query) : null;
				PriceTable cdm_c_pTable = (this.Depth > 1) ? new PriceTable(query) : null;

        
				CampaignGroup cdm_c_cgObject = (this.Depth > 1) ? cdm_c_cgTable.CreateInstance() : null;
				Country cdm_c_cObject = (this.Depth > 1) ? cdm_c_cTable.CreateInstance() : null;
				Price cdm_c_pObject = (this.Depth > 1) ? cdm_c_pTable.CreateInstance() : null;
				Campaign cdm_cObject = (this.Depth > 0) ? cdm_cTable.CreateInstance(cdm_c_cgObject, cdm_c_cObject, cdm_c_pObject) : null;
				CampaignDeviceMap cdmObject = cdmTable.CreateInstance(cdm_cObject);
				sqlReader.Close();

				return cdmObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cdm", "customload", "exception"), "CampaignDeviceMap could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "CampaignDeviceMap", "Exception while loading (custom/single) CampaignDeviceMap object from database. See inner exception for details.", ex);
      }
    }

    public List<CampaignDeviceMap> LoadMany(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							CampaignDeviceMapTable.GetColumnNames("[cdm]") + 
							(this.Depth > 0 ? "," + CampaignTable.GetColumnNames("[cdm_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignGroupTable.GetColumnNames("[cdm_c_cg]") : string.Empty) + 
							(this.Depth > 1 ? "," + CountryTable.GetColumnNames("[cdm_c_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + PriceTable.GetColumnNames("[cdm_c_p]") : string.Empty) +  
					" FROM [core].[CampaignDeviceMap] AS [cdm] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Campaign] AS [cdm_c] ON [cdm].[CampaignID] = [cdm_c].[CampaignID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[CampaignGroup] AS [cdm_c_cg] ON [cdm_c].[CampaignGroupID] = [cdm_c_cg].[CampaignGroupID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Country] AS [cdm_c_c] ON [cdm_c].[CountryID] = [cdm_c_c].[CountryID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Price] AS [cdm_c_p] ON [cdm_c].[PriceID] = [cdm_c_p].[PriceID] ";
				

        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cdm", "customloadmany", "notfound"), "CampaignDeviceMap list could not be loaded using custom logic as no items were found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return new List<CampaignDeviceMap>();
        }

        SqlQuery query = new SqlQuery(sqlReader);

				CampaignDeviceMapTable cdmTable = new CampaignDeviceMapTable(query);
				CampaignTable cdm_cTable = (this.Depth > 0) ? new CampaignTable(query) : null;
				CampaignGroupTable cdm_c_cgTable = (this.Depth > 1) ? new CampaignGroupTable(query) : null;
				CountryTable cdm_c_cTable = (this.Depth > 1) ? new CountryTable(query) : null;
				PriceTable cdm_c_pTable = (this.Depth > 1) ? new PriceTable(query) : null;

        List<CampaignDeviceMap> result = new List<CampaignDeviceMap>();
        do
        {
          
					CampaignGroup cdm_c_cgObject = (this.Depth > 1) ? cdm_c_cgTable.CreateInstance() : null;
					Country cdm_c_cObject = (this.Depth > 1) ? cdm_c_cTable.CreateInstance() : null;
					Price cdm_c_pObject = (this.Depth > 1) ? cdm_c_pTable.CreateInstance() : null;
					Campaign cdm_cObject = (this.Depth > 0) ? cdm_cTable.CreateInstance(cdm_c_cgObject, cdm_c_cObject, cdm_c_pObject) : null;
					CampaignDeviceMap cdmObject = (this.Depth > -1) ? cdmTable.CreateInstance(cdm_cObject) : null;
					result.Add(cdmObject);

        } while (sqlReader.Read());
        sqlReader.Close();

        return result;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cdm", "customloadmany", "exception"), "CampaignDeviceMap list could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "CampaignDeviceMap", "Exception while loading (custom/many) CampaignDeviceMap object from database. See inner exception for details.", ex);
      }
    }
    
    public override int? Insert(ISqlConnectionInfo connection, CampaignDeviceMap data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "INSERT INTO [core].[CampaignDeviceMap] ([CampaignID],[CampaignDeviceID]) VALUES(@CampaignID,@CampaignDeviceID); SELECT SCOPE_IDENTITY();";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@CampaignID", data.Campaign.ID);
				sqlCmd.Parameters.AddWithValue("@CampaignDeviceID", (int)data.CampaignDevice);
				
        object idObj = sqlCmd.ExecuteScalar();
        if (idObj == null || DBNull.Value.Equals(idObj))
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cdm", "insert", "noprimarykey"), "CampaignDeviceMap could not be inserted or inserted primary key was not returned. Are you missing SELECT SCOPE_IDENTITY();?", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Insert, "CampaignDeviceMap", "Exception while inserting CampaignDeviceMap object in database.");
        }
        return (int)((decimal)idObj);
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cdm", "insert", "exception"), "CampaignDeviceMap could not be inserted. See exception for details.", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Insert, "CampaignDeviceMap", "Exception while inserting CampaignDeviceMap object in database. See inner exception for details.", ex);
      }
    }

    public override bool Update(ISqlConnectionInfo connection, CampaignDeviceMap data)
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
        sqlCmdText = "UPDATE [core].[CampaignDeviceMap] SET " +
												"[CampaignID] = @CampaignID, " + 
												"[CampaignDeviceID] = @CampaignDeviceID, " + 
												"[Updated] = GETDATE() " + 
											"WHERE [CampaignDeviceMapID] = @CampaignDeviceMapID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@CampaignID", data.Campaign.ID);
				sqlCmd.Parameters.AddWithValue("@CampaignDeviceID", (int)data.CampaignDevice);
				sqlCmd.Parameters.AddWithValue("@Updated", data.Updated).SqlDbType = SqlDbType.DateTime2;
				sqlCmd.Parameters.AddWithValue("@CampaignDeviceMapID", data.ID);

        int rowCount = sqlCmd.ExecuteNonQuery();
        if (rowCount < 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cdm", "update", "norecord"), "CampaignDeviceMap could not be updated as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "CampaignDeviceMap", "Exception while updating CampaignDeviceMap object in database. No record found for this id.");
        }
        else if (rowCount > 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cdm", "update", "morerecords"), "CampaignDeviceMap was updated but there was more than one record affected.", sqlCmdText, this, connection, data);
          if (this.Logger.IsFatalEnabled)
            this.Logger.Fatal(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "CampaignDeviceMap", "Exception while updating CampaignDeviceMap object in database. More than one record found for this statement (update statement where clause broken?!).");
        }
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cdm", "update", "exception"), "CampaignDeviceMap could not be updated. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Update, "CampaignDeviceMap", "Exception while updating CampaignDeviceMap object in database. See inner exception for details.", ex);
      }
    }

    public override bool Delete(ISqlConnectionInfo connection, CampaignDeviceMap data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText =  "DELETE FROM [core].[CampaignDeviceMap] WHERE CampaignDeviceMapID = @CampaignDeviceMapID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@CampaignDeviceMapID", data.ID);

        int success = sqlCmd.ExecuteNonQuery();

        if (success == -1)        
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cdm", "delete", "norecord"), "CampaignDeviceMap could not be deleted as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Delete, "CampaignDeviceMap", "Exception while deleting CampaignDeviceMap object from database. No such record found.");
        }
        
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cdm", "delete", "exception"), "CampaignDeviceMap could not be deleted. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Delete, "CampaignDeviceMap", "Exception while deleting CampaignDeviceMap object from database. See inner exception for details.", ex);
      }
    }
  }
}

