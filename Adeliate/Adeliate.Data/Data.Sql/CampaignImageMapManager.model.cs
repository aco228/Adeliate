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
  [DataManager(typeof(CampaignImageMap))] 
  public partial class CampaignImageMapManager : Adlite.Data.Sql.SqlManagerBase<CampaignImageMap>, ICampaignImageMapManager
  {
    public override DatabaseType Type
    {
      get { return DatabaseType.Adlite; }
    }

    protected override CampaignImageMap LoadInternal(ISqlConnectionInfo connection, int id)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "SELECT " + 
							CampaignImageMapTable.GetColumnNames("[cim]") + 
							(this.Depth > 0 ? "," + CampaignTable.GetColumnNames("[cim_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignGroupTable.GetColumnNames("[cim_c_cg]") : string.Empty) + 
							(this.Depth > 1 ? "," + CountryTable.GetColumnNames("[cim_c_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + PriceTable.GetColumnNames("[cim_c_p]") : string.Empty) + 
							(this.Depth > 0 ? "," + ImageTable.GetColumnNames("[cim_i]") : string.Empty) + 
					" FROM [core].[CampaignImageMap] AS [cim] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Campaign] AS [cim_c] ON [cim].[CampaignID] = [cim_c].[CampaignID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[CampaignGroup] AS [cim_c_cg] ON [cim_c].[CampaignGroupID] = [cim_c_cg].[CampaignGroupID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Country] AS [cim_c_c] ON [cim_c].[CountryID] = [cim_c_c].[CountryID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Price] AS [cim_c_p] ON [cim_c].[PriceID] = [cim_c_p].[PriceID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Image] AS [cim_i] ON [cim].[ImageID] = [cim_i].[ImageID] ";
				sqlCmdText += "WHERE [cim].[CampaignImageMapID] = @CampaignImageMapID;";

        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@CampaignImageMapID", id);
        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cim", "loadinternal", "notfound"), "CampaignImageMap could not be loaded by id as it was not found.", sqlCmdText, this, connection, id);
          if (this.Logger.IsWarnEnabled)
            this.Logger.Warn(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				CampaignImageMapTable cimTable = new CampaignImageMapTable(query);
				CampaignTable cim_cTable = (this.Depth > 0) ? new CampaignTable(query) : null;
				CampaignGroupTable cim_c_cgTable = (this.Depth > 1) ? new CampaignGroupTable(query) : null;
				CountryTable cim_c_cTable = (this.Depth > 1) ? new CountryTable(query) : null;
				PriceTable cim_c_pTable = (this.Depth > 1) ? new PriceTable(query) : null;
				ImageTable cim_iTable = (this.Depth > 0) ? new ImageTable(query) : null;

        
				CampaignGroup cim_c_cgObject = (this.Depth > 1) ? cim_c_cgTable.CreateInstance() : null;
				Country cim_c_cObject = (this.Depth > 1) ? cim_c_cTable.CreateInstance() : null;
				Price cim_c_pObject = (this.Depth > 1) ? cim_c_pTable.CreateInstance() : null;
				Campaign cim_cObject = (this.Depth > 0) ? cim_cTable.CreateInstance(cim_c_cgObject, cim_c_cObject, cim_c_pObject) : null;
				Image cim_iObject = (this.Depth > 0) ? cim_iTable.CreateInstance() : null;
				CampaignImageMap cimObject = cimTable.CreateInstance(cim_cObject, cim_iObject);
				sqlReader.Close();

				return cimObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cim", "loadinternal", "exception"), "CampaignImageMap could not be loaded by id. See exception for details.", sqlCmdText, ex, this, connection, id);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "CampaignImageMap", "Exception while loading CampaignImageMap object from database. See inner exception for details.", ex);
      }
    }

    public CampaignImageMap Load(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							CampaignImageMapTable.GetColumnNames("[cim]") + 
							(this.Depth > 0 ? "," + CampaignTable.GetColumnNames("[cim_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignGroupTable.GetColumnNames("[cim_c_cg]") : string.Empty) + 
							(this.Depth > 1 ? "," + CountryTable.GetColumnNames("[cim_c_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + PriceTable.GetColumnNames("[cim_c_p]") : string.Empty) + 
							(this.Depth > 0 ? "," + ImageTable.GetColumnNames("[cim_i]") : string.Empty) +  
					" FROM [core].[CampaignImageMap] AS [cim] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Campaign] AS [cim_c] ON [cim].[CampaignID] = [cim_c].[CampaignID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[CampaignGroup] AS [cim_c_cg] ON [cim_c].[CampaignGroupID] = [cim_c_cg].[CampaignGroupID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Country] AS [cim_c_c] ON [cim_c].[CountryID] = [cim_c_c].[CountryID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Price] AS [cim_c_p] ON [cim_c].[PriceID] = [cim_c_p].[PriceID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Image] AS [cim_i] ON [cim].[ImageID] = [cim_i].[ImageID] ";
				

        parameters.Top = 1;
        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cim", "customload", "notfound"), "CampaignImageMap could not be loaded using custom logic as it was not found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				CampaignImageMapTable cimTable = new CampaignImageMapTable(query);
				CampaignTable cim_cTable = (this.Depth > 0) ? new CampaignTable(query) : null;
				CampaignGroupTable cim_c_cgTable = (this.Depth > 1) ? new CampaignGroupTable(query) : null;
				CountryTable cim_c_cTable = (this.Depth > 1) ? new CountryTable(query) : null;
				PriceTable cim_c_pTable = (this.Depth > 1) ? new PriceTable(query) : null;
				ImageTable cim_iTable = (this.Depth > 0) ? new ImageTable(query) : null;

        
				CampaignGroup cim_c_cgObject = (this.Depth > 1) ? cim_c_cgTable.CreateInstance() : null;
				Country cim_c_cObject = (this.Depth > 1) ? cim_c_cTable.CreateInstance() : null;
				Price cim_c_pObject = (this.Depth > 1) ? cim_c_pTable.CreateInstance() : null;
				Campaign cim_cObject = (this.Depth > 0) ? cim_cTable.CreateInstance(cim_c_cgObject, cim_c_cObject, cim_c_pObject) : null;
				Image cim_iObject = (this.Depth > 0) ? cim_iTable.CreateInstance() : null;
				CampaignImageMap cimObject = cimTable.CreateInstance(cim_cObject, cim_iObject);
				sqlReader.Close();

				return cimObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cim", "customload", "exception"), "CampaignImageMap could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "CampaignImageMap", "Exception while loading (custom/single) CampaignImageMap object from database. See inner exception for details.", ex);
      }
    }

    public List<CampaignImageMap> LoadMany(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							CampaignImageMapTable.GetColumnNames("[cim]") + 
							(this.Depth > 0 ? "," + CampaignTable.GetColumnNames("[cim_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignGroupTable.GetColumnNames("[cim_c_cg]") : string.Empty) + 
							(this.Depth > 1 ? "," + CountryTable.GetColumnNames("[cim_c_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + PriceTable.GetColumnNames("[cim_c_p]") : string.Empty) + 
							(this.Depth > 0 ? "," + ImageTable.GetColumnNames("[cim_i]") : string.Empty) +  
					" FROM [core].[CampaignImageMap] AS [cim] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Campaign] AS [cim_c] ON [cim].[CampaignID] = [cim_c].[CampaignID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[CampaignGroup] AS [cim_c_cg] ON [cim_c].[CampaignGroupID] = [cim_c_cg].[CampaignGroupID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Country] AS [cim_c_c] ON [cim_c].[CountryID] = [cim_c_c].[CountryID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Price] AS [cim_c_p] ON [cim_c].[PriceID] = [cim_c_p].[PriceID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Image] AS [cim_i] ON [cim].[ImageID] = [cim_i].[ImageID] ";
				

        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cim", "customloadmany", "notfound"), "CampaignImageMap list could not be loaded using custom logic as no items were found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return new List<CampaignImageMap>();
        }

        SqlQuery query = new SqlQuery(sqlReader);

				CampaignImageMapTable cimTable = new CampaignImageMapTable(query);
				CampaignTable cim_cTable = (this.Depth > 0) ? new CampaignTable(query) : null;
				CampaignGroupTable cim_c_cgTable = (this.Depth > 1) ? new CampaignGroupTable(query) : null;
				CountryTable cim_c_cTable = (this.Depth > 1) ? new CountryTable(query) : null;
				PriceTable cim_c_pTable = (this.Depth > 1) ? new PriceTable(query) : null;
				ImageTable cim_iTable = (this.Depth > 0) ? new ImageTable(query) : null;

        List<CampaignImageMap> result = new List<CampaignImageMap>();
        do
        {
          
					CampaignGroup cim_c_cgObject = (this.Depth > 1) ? cim_c_cgTable.CreateInstance() : null;
					Country cim_c_cObject = (this.Depth > 1) ? cim_c_cTable.CreateInstance() : null;
					Price cim_c_pObject = (this.Depth > 1) ? cim_c_pTable.CreateInstance() : null;
					Campaign cim_cObject = (this.Depth > 0) ? cim_cTable.CreateInstance(cim_c_cgObject, cim_c_cObject, cim_c_pObject) : null;
					Image cim_iObject = (this.Depth > 0) ? cim_iTable.CreateInstance() : null;
					CampaignImageMap cimObject = (this.Depth > -1) ? cimTable.CreateInstance(cim_cObject, cim_iObject) : null;
					result.Add(cimObject);

        } while (sqlReader.Read());
        sqlReader.Close();

        return result;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cim", "customloadmany", "exception"), "CampaignImageMap list could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "CampaignImageMap", "Exception while loading (custom/many) CampaignImageMap object from database. See inner exception for details.", ex);
      }
    }
    
    public override int? Insert(ISqlConnectionInfo connection, CampaignImageMap data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "INSERT INTO [core].[CampaignImageMap] ([CampaignID],[ImageID],[IsDefault]) VALUES(@CampaignID,@ImageID,@IsDefault); SELECT SCOPE_IDENTITY();";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@CampaignID", data.Campaign.ID);
				sqlCmd.Parameters.AddWithValue("@ImageID", data.Image.ID);
				sqlCmd.Parameters.AddWithValue("@IsDefault", data.IsDefault).SqlDbType = SqlDbType.Bit;
				
        object idObj = sqlCmd.ExecuteScalar();
        if (idObj == null || DBNull.Value.Equals(idObj))
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cim", "insert", "noprimarykey"), "CampaignImageMap could not be inserted or inserted primary key was not returned. Are you missing SELECT SCOPE_IDENTITY();?", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Insert, "CampaignImageMap", "Exception while inserting CampaignImageMap object in database.");
        }
        return (int)((decimal)idObj);
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cim", "insert", "exception"), "CampaignImageMap could not be inserted. See exception for details.", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Insert, "CampaignImageMap", "Exception while inserting CampaignImageMap object in database. See inner exception for details.", ex);
      }
    }

    public override bool Update(ISqlConnectionInfo connection, CampaignImageMap data)
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
        sqlCmdText = "UPDATE [core].[CampaignImageMap] SET " +
												"[CampaignID] = @CampaignID, " + 
												"[ImageID] = @ImageID, " + 
												"[IsDefault] = @IsDefault, " + 
												"[Updated] = GETDATE() " + 
											"WHERE [CampaignImageMapID] = @CampaignImageMapID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@CampaignID", data.Campaign.ID);
				sqlCmd.Parameters.AddWithValue("@ImageID", data.Image.ID);
				sqlCmd.Parameters.AddWithValue("@IsDefault", data.IsDefault).SqlDbType = SqlDbType.Bit;
				sqlCmd.Parameters.AddWithValue("@Updated", data.Updated).SqlDbType = SqlDbType.DateTime2;
				sqlCmd.Parameters.AddWithValue("@CampaignImageMapID", data.ID);

        int rowCount = sqlCmd.ExecuteNonQuery();
        if (rowCount < 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cim", "update", "norecord"), "CampaignImageMap could not be updated as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "CampaignImageMap", "Exception while updating CampaignImageMap object in database. No record found for this id.");
        }
        else if (rowCount > 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cim", "update", "morerecords"), "CampaignImageMap was updated but there was more than one record affected.", sqlCmdText, this, connection, data);
          if (this.Logger.IsFatalEnabled)
            this.Logger.Fatal(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "CampaignImageMap", "Exception while updating CampaignImageMap object in database. More than one record found for this statement (update statement where clause broken?!).");
        }
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cim", "update", "exception"), "CampaignImageMap could not be updated. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Update, "CampaignImageMap", "Exception while updating CampaignImageMap object in database. See inner exception for details.", ex);
      }
    }

    public override bool Delete(ISqlConnectionInfo connection, CampaignImageMap data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText =  "DELETE FROM [core].[CampaignImageMap] WHERE CampaignImageMapID = @CampaignImageMapID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@CampaignImageMapID", data.ID);

        int success = sqlCmd.ExecuteNonQuery();

        if (success == -1)        
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cim", "delete", "norecord"), "CampaignImageMap could not be deleted as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Delete, "CampaignImageMap", "Exception while deleting CampaignImageMap object from database. No such record found.");
        }
        
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cim", "delete", "exception"), "CampaignImageMap could not be deleted. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Delete, "CampaignImageMap", "Exception while deleting CampaignImageMap object from database. See inner exception for details.", ex);
      }
    }
  }
}

