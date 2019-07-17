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
  [DataManager(typeof(CampaignMobileOperatorMap))] 
  public partial class CampaignMobileOperatorMapManager : Adlite.Data.Sql.SqlManagerBase<CampaignMobileOperatorMap>, ICampaignMobileOperatorMapManager
  {
    public override DatabaseType Type
    {
      get { return DatabaseType.Adlite; }
    }

    protected override CampaignMobileOperatorMap LoadInternal(ISqlConnectionInfo connection, int id)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "SELECT " + 
							CampaignMobileOperatorMapTable.GetColumnNames("[cmom]") + 
							(this.Depth > 0 ? "," + CampaignTable.GetColumnNames("[cmom_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignGroupTable.GetColumnNames("[cmom_c_cg]") : string.Empty) + 
							(this.Depth > 1 ? "," + CountryTable.GetColumnNames("[cmom_c_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + PriceTable.GetColumnNames("[cmom_c_p]") : string.Empty) + 
							(this.Depth > 0 ? "," + MobileOperatorTable.GetColumnNames("[cmom_mo]") : string.Empty) + 
							(this.Depth > 1 ? "," + CountryTable.GetColumnNames("[cmom_mo_c]") : string.Empty) + 
					" FROM [core].[CampaignMobileOperatorMap] AS [cmom] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Campaign] AS [cmom_c] ON [cmom].[CampaignID] = [cmom_c].[CampaignID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[CampaignGroup] AS [cmom_c_cg] ON [cmom_c].[CampaignGroupID] = [cmom_c_cg].[CampaignGroupID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Country] AS [cmom_c_c] ON [cmom_c].[CountryID] = [cmom_c_c].[CountryID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Price] AS [cmom_c_p] ON [cmom_c].[PriceID] = [cmom_c_p].[PriceID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[MobileOperator] AS [cmom_mo] ON [cmom].[MobileOperatorID] = [cmom_mo].[MobileOperatorID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Country] AS [cmom_mo_c] ON [cmom_mo].[CountryID] = [cmom_mo_c].[CountryID] ";
				sqlCmdText += "WHERE [cmom].[CampaignMobileOperatorMapID] = @CampaignMobileOperatorMapID;";

        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@CampaignMobileOperatorMapID", id);
        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cmom", "loadinternal", "notfound"), "CampaignMobileOperatorMap could not be loaded by id as it was not found.", sqlCmdText, this, connection, id);
          if (this.Logger.IsWarnEnabled)
            this.Logger.Warn(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				CampaignMobileOperatorMapTable cmomTable = new CampaignMobileOperatorMapTable(query);
				CampaignTable cmom_cTable = (this.Depth > 0) ? new CampaignTable(query) : null;
				CampaignGroupTable cmom_c_cgTable = (this.Depth > 1) ? new CampaignGroupTable(query) : null;
				CountryTable cmom_c_cTable = (this.Depth > 1) ? new CountryTable(query) : null;
				PriceTable cmom_c_pTable = (this.Depth > 1) ? new PriceTable(query) : null;
				MobileOperatorTable cmom_moTable = (this.Depth > 0) ? new MobileOperatorTable(query) : null;
				CountryTable cmom_mo_cTable = (this.Depth > 1) ? new CountryTable(query) : null;

        
				CampaignGroup cmom_c_cgObject = (this.Depth > 1) ? cmom_c_cgTable.CreateInstance() : null;
				Country cmom_c_cObject = (this.Depth > 1) ? cmom_c_cTable.CreateInstance() : null;
				Price cmom_c_pObject = (this.Depth > 1) ? cmom_c_pTable.CreateInstance() : null;
				Campaign cmom_cObject = (this.Depth > 0) ? cmom_cTable.CreateInstance(cmom_c_cgObject, cmom_c_cObject, cmom_c_pObject) : null;
				Country cmom_mo_cObject = (this.Depth > 1) ? cmom_mo_cTable.CreateInstance() : null;
				MobileOperator cmom_moObject = (this.Depth > 0) ? cmom_moTable.CreateInstance(cmom_mo_cObject) : null;
				CampaignMobileOperatorMap cmomObject = cmomTable.CreateInstance(cmom_cObject, cmom_moObject);
				sqlReader.Close();

				return cmomObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cmom", "loadinternal", "exception"), "CampaignMobileOperatorMap could not be loaded by id. See exception for details.", sqlCmdText, ex, this, connection, id);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "CampaignMobileOperatorMap", "Exception while loading CampaignMobileOperatorMap object from database. See inner exception for details.", ex);
      }
    }

    public CampaignMobileOperatorMap Load(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							CampaignMobileOperatorMapTable.GetColumnNames("[cmom]") + 
							(this.Depth > 0 ? "," + CampaignTable.GetColumnNames("[cmom_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignGroupTable.GetColumnNames("[cmom_c_cg]") : string.Empty) + 
							(this.Depth > 1 ? "," + CountryTable.GetColumnNames("[cmom_c_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + PriceTable.GetColumnNames("[cmom_c_p]") : string.Empty) + 
							(this.Depth > 0 ? "," + MobileOperatorTable.GetColumnNames("[cmom_mo]") : string.Empty) + 
							(this.Depth > 1 ? "," + CountryTable.GetColumnNames("[cmom_mo_c]") : string.Empty) +  
					" FROM [core].[CampaignMobileOperatorMap] AS [cmom] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Campaign] AS [cmom_c] ON [cmom].[CampaignID] = [cmom_c].[CampaignID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[CampaignGroup] AS [cmom_c_cg] ON [cmom_c].[CampaignGroupID] = [cmom_c_cg].[CampaignGroupID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Country] AS [cmom_c_c] ON [cmom_c].[CountryID] = [cmom_c_c].[CountryID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Price] AS [cmom_c_p] ON [cmom_c].[PriceID] = [cmom_c_p].[PriceID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[MobileOperator] AS [cmom_mo] ON [cmom].[MobileOperatorID] = [cmom_mo].[MobileOperatorID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Country] AS [cmom_mo_c] ON [cmom_mo].[CountryID] = [cmom_mo_c].[CountryID] ";
				

        parameters.Top = 1;
        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cmom", "customload", "notfound"), "CampaignMobileOperatorMap could not be loaded using custom logic as it was not found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				CampaignMobileOperatorMapTable cmomTable = new CampaignMobileOperatorMapTable(query);
				CampaignTable cmom_cTable = (this.Depth > 0) ? new CampaignTable(query) : null;
				CampaignGroupTable cmom_c_cgTable = (this.Depth > 1) ? new CampaignGroupTable(query) : null;
				CountryTable cmom_c_cTable = (this.Depth > 1) ? new CountryTable(query) : null;
				PriceTable cmom_c_pTable = (this.Depth > 1) ? new PriceTable(query) : null;
				MobileOperatorTable cmom_moTable = (this.Depth > 0) ? new MobileOperatorTable(query) : null;
				CountryTable cmom_mo_cTable = (this.Depth > 1) ? new CountryTable(query) : null;

        
				CampaignGroup cmom_c_cgObject = (this.Depth > 1) ? cmom_c_cgTable.CreateInstance() : null;
				Country cmom_c_cObject = (this.Depth > 1) ? cmom_c_cTable.CreateInstance() : null;
				Price cmom_c_pObject = (this.Depth > 1) ? cmom_c_pTable.CreateInstance() : null;
				Campaign cmom_cObject = (this.Depth > 0) ? cmom_cTable.CreateInstance(cmom_c_cgObject, cmom_c_cObject, cmom_c_pObject) : null;
				Country cmom_mo_cObject = (this.Depth > 1) ? cmom_mo_cTable.CreateInstance() : null;
				MobileOperator cmom_moObject = (this.Depth > 0) ? cmom_moTable.CreateInstance(cmom_mo_cObject) : null;
				CampaignMobileOperatorMap cmomObject = cmomTable.CreateInstance(cmom_cObject, cmom_moObject);
				sqlReader.Close();

				return cmomObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cmom", "customload", "exception"), "CampaignMobileOperatorMap could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "CampaignMobileOperatorMap", "Exception while loading (custom/single) CampaignMobileOperatorMap object from database. See inner exception for details.", ex);
      }
    }

    public List<CampaignMobileOperatorMap> LoadMany(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							CampaignMobileOperatorMapTable.GetColumnNames("[cmom]") + 
							(this.Depth > 0 ? "," + CampaignTable.GetColumnNames("[cmom_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignGroupTable.GetColumnNames("[cmom_c_cg]") : string.Empty) + 
							(this.Depth > 1 ? "," + CountryTable.GetColumnNames("[cmom_c_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + PriceTable.GetColumnNames("[cmom_c_p]") : string.Empty) + 
							(this.Depth > 0 ? "," + MobileOperatorTable.GetColumnNames("[cmom_mo]") : string.Empty) + 
							(this.Depth > 1 ? "," + CountryTable.GetColumnNames("[cmom_mo_c]") : string.Empty) +  
					" FROM [core].[CampaignMobileOperatorMap] AS [cmom] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Campaign] AS [cmom_c] ON [cmom].[CampaignID] = [cmom_c].[CampaignID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[CampaignGroup] AS [cmom_c_cg] ON [cmom_c].[CampaignGroupID] = [cmom_c_cg].[CampaignGroupID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Country] AS [cmom_c_c] ON [cmom_c].[CountryID] = [cmom_c_c].[CountryID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Price] AS [cmom_c_p] ON [cmom_c].[PriceID] = [cmom_c_p].[PriceID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[MobileOperator] AS [cmom_mo] ON [cmom].[MobileOperatorID] = [cmom_mo].[MobileOperatorID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Country] AS [cmom_mo_c] ON [cmom_mo].[CountryID] = [cmom_mo_c].[CountryID] ";
				

        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cmom", "customloadmany", "notfound"), "CampaignMobileOperatorMap list could not be loaded using custom logic as no items were found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return new List<CampaignMobileOperatorMap>();
        }

        SqlQuery query = new SqlQuery(sqlReader);

				CampaignMobileOperatorMapTable cmomTable = new CampaignMobileOperatorMapTable(query);
				CampaignTable cmom_cTable = (this.Depth > 0) ? new CampaignTable(query) : null;
				CampaignGroupTable cmom_c_cgTable = (this.Depth > 1) ? new CampaignGroupTable(query) : null;
				CountryTable cmom_c_cTable = (this.Depth > 1) ? new CountryTable(query) : null;
				PriceTable cmom_c_pTable = (this.Depth > 1) ? new PriceTable(query) : null;
				MobileOperatorTable cmom_moTable = (this.Depth > 0) ? new MobileOperatorTable(query) : null;
				CountryTable cmom_mo_cTable = (this.Depth > 1) ? new CountryTable(query) : null;

        List<CampaignMobileOperatorMap> result = new List<CampaignMobileOperatorMap>();
        do
        {
          
					CampaignGroup cmom_c_cgObject = (this.Depth > 1) ? cmom_c_cgTable.CreateInstance() : null;
					Country cmom_c_cObject = (this.Depth > 1) ? cmom_c_cTable.CreateInstance() : null;
					Price cmom_c_pObject = (this.Depth > 1) ? cmom_c_pTable.CreateInstance() : null;
					Campaign cmom_cObject = (this.Depth > 0) ? cmom_cTable.CreateInstance(cmom_c_cgObject, cmom_c_cObject, cmom_c_pObject) : null;
					Country cmom_mo_cObject = (this.Depth > 1) ? cmom_mo_cTable.CreateInstance() : null;
					MobileOperator cmom_moObject = (this.Depth > 0) ? cmom_moTable.CreateInstance(cmom_mo_cObject) : null;
					CampaignMobileOperatorMap cmomObject = (this.Depth > -1) ? cmomTable.CreateInstance(cmom_cObject, cmom_moObject) : null;
					result.Add(cmomObject);

        } while (sqlReader.Read());
        sqlReader.Close();

        return result;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cmom", "customloadmany", "exception"), "CampaignMobileOperatorMap list could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "CampaignMobileOperatorMap", "Exception while loading (custom/many) CampaignMobileOperatorMap object from database. See inner exception for details.", ex);
      }
    }
    
    public override int? Insert(ISqlConnectionInfo connection, CampaignMobileOperatorMap data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "INSERT INTO [core].[CampaignMobileOperatorMap] ([CampaignID],[MobileOperatorID]) VALUES(@CampaignID,@MobileOperatorID); SELECT SCOPE_IDENTITY();";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@CampaignID", data.Campaign.ID);
				sqlCmd.Parameters.AddWithValue("@MobileOperatorID", data.MobileOperator.ID);
				
        object idObj = sqlCmd.ExecuteScalar();
        if (idObj == null || DBNull.Value.Equals(idObj))
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cmom", "insert", "noprimarykey"), "CampaignMobileOperatorMap could not be inserted or inserted primary key was not returned. Are you missing SELECT SCOPE_IDENTITY();?", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Insert, "CampaignMobileOperatorMap", "Exception while inserting CampaignMobileOperatorMap object in database.");
        }
        return (int)((decimal)idObj);
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cmom", "insert", "exception"), "CampaignMobileOperatorMap could not be inserted. See exception for details.", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Insert, "CampaignMobileOperatorMap", "Exception while inserting CampaignMobileOperatorMap object in database. See inner exception for details.", ex);
      }
    }

    public override bool Update(ISqlConnectionInfo connection, CampaignMobileOperatorMap data)
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
        sqlCmdText = "UPDATE [core].[CampaignMobileOperatorMap] SET " +
												"[CampaignID] = @CampaignID, " + 
												"[MobileOperatorID] = @MobileOperatorID, " + 
												"[Updated] = GETDATE() " + 
											"WHERE [CampaignMobileOperatorMapID] = @CampaignMobileOperatorMapID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@CampaignID", data.Campaign.ID);
				sqlCmd.Parameters.AddWithValue("@MobileOperatorID", data.MobileOperator.ID);
				sqlCmd.Parameters.AddWithValue("@Updated", data.Updated).SqlDbType = SqlDbType.DateTime2;
				sqlCmd.Parameters.AddWithValue("@CampaignMobileOperatorMapID", data.ID);

        int rowCount = sqlCmd.ExecuteNonQuery();
        if (rowCount < 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cmom", "update", "norecord"), "CampaignMobileOperatorMap could not be updated as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "CampaignMobileOperatorMap", "Exception while updating CampaignMobileOperatorMap object in database. No record found for this id.");
        }
        else if (rowCount > 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cmom", "update", "morerecords"), "CampaignMobileOperatorMap was updated but there was more than one record affected.", sqlCmdText, this, connection, data);
          if (this.Logger.IsFatalEnabled)
            this.Logger.Fatal(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "CampaignMobileOperatorMap", "Exception while updating CampaignMobileOperatorMap object in database. More than one record found for this statement (update statement where clause broken?!).");
        }
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cmom", "update", "exception"), "CampaignMobileOperatorMap could not be updated. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Update, "CampaignMobileOperatorMap", "Exception while updating CampaignMobileOperatorMap object in database. See inner exception for details.", ex);
      }
    }

    public override bool Delete(ISqlConnectionInfo connection, CampaignMobileOperatorMap data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText =  "DELETE FROM [core].[CampaignMobileOperatorMap] WHERE CampaignMobileOperatorMapID = @CampaignMobileOperatorMapID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@CampaignMobileOperatorMapID", data.ID);

        int success = sqlCmd.ExecuteNonQuery();

        if (success == -1)        
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cmom", "delete", "norecord"), "CampaignMobileOperatorMap could not be deleted as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Delete, "CampaignMobileOperatorMap", "Exception while deleting CampaignMobileOperatorMap object from database. No such record found.");
        }
        
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("cmom", "delete", "exception"), "CampaignMobileOperatorMap could not be deleted. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Delete, "CampaignMobileOperatorMap", "Exception while deleting CampaignMobileOperatorMap object from database. See inner exception for details.", ex);
      }
    }
  }
}

