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
  [DataManager(typeof(MobileOperator))] 
  public partial class MobileOperatorManager : Adlite.Data.Sql.SqlManagerBase<MobileOperator>, IMobileOperatorManager
  {
    public override DatabaseType Type
    {
      get { return DatabaseType.Adlite; }
    }

    protected override MobileOperator LoadInternal(ISqlConnectionInfo connection, int id)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "SELECT " + 
							MobileOperatorTable.GetColumnNames("[mo]") + 
							(this.Depth > 0 ? "," + CountryTable.GetColumnNames("[mo_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + LanguageTable.GetColumnNames("[mo_c_l]") : string.Empty) + 
							(this.Depth > 1 ? "," + CurrencyTable.GetColumnNames("[mo_c_c]") : string.Empty) + 
					" FROM [core].[MobileOperator] AS [mo] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Country] AS [mo_c] ON [mo].[CountryID] = [mo_c].[CountryID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Language] AS [mo_c_l] ON [mo_c].[LanguageID] = [mo_c_l].[LanguageID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Currency] AS [mo_c_c] ON [mo_c].[CurrencyID] = [mo_c_c].[CurrencyID] ";
				sqlCmdText += "WHERE [mo].[MobileOperatorID] = @MobileOperatorID;";

        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@MobileOperatorID", id);
        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("mo", "loadinternal", "notfound"), "MobileOperator could not be loaded by id as it was not found.", sqlCmdText, this, connection, id);
          if (this.Logger.IsWarnEnabled)
            this.Logger.Warn(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				MobileOperatorTable moTable = new MobileOperatorTable(query);
				CountryTable mo_cTable = (this.Depth > 0) ? new CountryTable(query) : null;
				LanguageTable mo_c_lTable = (this.Depth > 1) ? new LanguageTable(query) : null;
				CurrencyTable mo_c_cTable = (this.Depth > 1) ? new CurrencyTable(query) : null;

        
				Language mo_c_lObject = (this.Depth > 1) ? mo_c_lTable.CreateInstance() : null;
				Currency mo_c_cObject = (this.Depth > 1) ? mo_c_cTable.CreateInstance() : null;
				Country mo_cObject = (this.Depth > 0) ? mo_cTable.CreateInstance(mo_c_lObject, mo_c_cObject) : null;
				MobileOperator moObject = moTable.CreateInstance(mo_cObject);
				sqlReader.Close();

				return moObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("mo", "loadinternal", "exception"), "MobileOperator could not be loaded by id. See exception for details.", sqlCmdText, ex, this, connection, id);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "MobileOperator", "Exception while loading MobileOperator object from database. See inner exception for details.", ex);
      }
    }

    public MobileOperator Load(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							MobileOperatorTable.GetColumnNames("[mo]") + 
							(this.Depth > 0 ? "," + CountryTable.GetColumnNames("[mo_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + LanguageTable.GetColumnNames("[mo_c_l]") : string.Empty) + 
							(this.Depth > 1 ? "," + CurrencyTable.GetColumnNames("[mo_c_c]") : string.Empty) +  
					" FROM [core].[MobileOperator] AS [mo] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Country] AS [mo_c] ON [mo].[CountryID] = [mo_c].[CountryID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Language] AS [mo_c_l] ON [mo_c].[LanguageID] = [mo_c_l].[LanguageID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Currency] AS [mo_c_c] ON [mo_c].[CurrencyID] = [mo_c_c].[CurrencyID] ";
				

        parameters.Top = 1;
        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("mo", "customload", "notfound"), "MobileOperator could not be loaded using custom logic as it was not found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				MobileOperatorTable moTable = new MobileOperatorTable(query);
				CountryTable mo_cTable = (this.Depth > 0) ? new CountryTable(query) : null;
				LanguageTable mo_c_lTable = (this.Depth > 1) ? new LanguageTable(query) : null;
				CurrencyTable mo_c_cTable = (this.Depth > 1) ? new CurrencyTable(query) : null;

        
				Language mo_c_lObject = (this.Depth > 1) ? mo_c_lTable.CreateInstance() : null;
				Currency mo_c_cObject = (this.Depth > 1) ? mo_c_cTable.CreateInstance() : null;
				Country mo_cObject = (this.Depth > 0) ? mo_cTable.CreateInstance(mo_c_lObject, mo_c_cObject) : null;
				MobileOperator moObject = moTable.CreateInstance(mo_cObject);
				sqlReader.Close();

				return moObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("mo", "customload", "exception"), "MobileOperator could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "MobileOperator", "Exception while loading (custom/single) MobileOperator object from database. See inner exception for details.", ex);
      }
    }

    public List<MobileOperator> LoadMany(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							MobileOperatorTable.GetColumnNames("[mo]") + 
							(this.Depth > 0 ? "," + CountryTable.GetColumnNames("[mo_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + LanguageTable.GetColumnNames("[mo_c_l]") : string.Empty) + 
							(this.Depth > 1 ? "," + CurrencyTable.GetColumnNames("[mo_c_c]") : string.Empty) +  
					" FROM [core].[MobileOperator] AS [mo] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Country] AS [mo_c] ON [mo].[CountryID] = [mo_c].[CountryID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Language] AS [mo_c_l] ON [mo_c].[LanguageID] = [mo_c_l].[LanguageID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Currency] AS [mo_c_c] ON [mo_c].[CurrencyID] = [mo_c_c].[CurrencyID] ";
				

        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("mo", "customloadmany", "notfound"), "MobileOperator list could not be loaded using custom logic as no items were found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return new List<MobileOperator>();
        }

        SqlQuery query = new SqlQuery(sqlReader);

				MobileOperatorTable moTable = new MobileOperatorTable(query);
				CountryTable mo_cTable = (this.Depth > 0) ? new CountryTable(query) : null;
				LanguageTable mo_c_lTable = (this.Depth > 1) ? new LanguageTable(query) : null;
				CurrencyTable mo_c_cTable = (this.Depth > 1) ? new CurrencyTable(query) : null;

        List<MobileOperator> result = new List<MobileOperator>();
        do
        {
          
					Language mo_c_lObject = (this.Depth > 1) ? mo_c_lTable.CreateInstance() : null;
					Currency mo_c_cObject = (this.Depth > 1) ? mo_c_cTable.CreateInstance() : null;
					Country mo_cObject = (this.Depth > 0) ? mo_cTable.CreateInstance(mo_c_lObject, mo_c_cObject) : null;
					MobileOperator moObject = (this.Depth > -1) ? moTable.CreateInstance(mo_cObject) : null;
					result.Add(moObject);

        } while (sqlReader.Read());
        sqlReader.Close();

        return result;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("mo", "customloadmany", "exception"), "MobileOperator list could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "MobileOperator", "Exception while loading (custom/many) MobileOperator object from database. See inner exception for details.", ex);
      }
    }
    
    public override int? Insert(ISqlConnectionInfo connection, MobileOperator data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "INSERT INTO [core].[MobileOperator] ([ExternalMobileOperatorID],[CountryID],[Name]) VALUES(@ExternalMobileOperatorID,@CountryID,@Name); SELECT SCOPE_IDENTITY();";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@ExternalMobileOperatorID", data.ExternalMobileOperatorID.HasValue ? (object)data.ExternalMobileOperatorID.Value : DBNull.Value).SqlDbType = SqlDbType.Int;
				sqlCmd.Parameters.AddWithValue("@CountryID", data.Country.ID);
				sqlCmd.Parameters.AddWithValue("@Name", data.Name).SqlDbType = SqlDbType.NVarChar;
				
        object idObj = sqlCmd.ExecuteScalar();
        if (idObj == null || DBNull.Value.Equals(idObj))
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("mo", "insert", "noprimarykey"), "MobileOperator could not be inserted or inserted primary key was not returned. Are you missing SELECT SCOPE_IDENTITY();?", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Insert, "MobileOperator", "Exception while inserting MobileOperator object in database.");
        }
        return (int)((decimal)idObj);
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("mo", "insert", "exception"), "MobileOperator could not be inserted. See exception for details.", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Insert, "MobileOperator", "Exception while inserting MobileOperator object in database. See inner exception for details.", ex);
      }
    }

    public override bool Update(ISqlConnectionInfo connection, MobileOperator data)
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
        sqlCmdText = "UPDATE [core].[MobileOperator] SET " +
												"[ExternalMobileOperatorID] = @ExternalMobileOperatorID, " + 
												"[CountryID] = @CountryID, " + 
												"[Name] = @Name, " + 
												"[Updated] = GETDATE() " + 
											"WHERE [MobileOperatorID] = @MobileOperatorID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@ExternalMobileOperatorID", data.ExternalMobileOperatorID.HasValue ? (object)data.ExternalMobileOperatorID.Value : DBNull.Value).SqlDbType = SqlDbType.Int;
				sqlCmd.Parameters.AddWithValue("@CountryID", data.Country.ID);
				sqlCmd.Parameters.AddWithValue("@Name", data.Name).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@Updated", data.Updated).SqlDbType = SqlDbType.DateTime2;
				sqlCmd.Parameters.AddWithValue("@MobileOperatorID", data.ID);

        int rowCount = sqlCmd.ExecuteNonQuery();
        if (rowCount < 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("mo", "update", "norecord"), "MobileOperator could not be updated as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "MobileOperator", "Exception while updating MobileOperator object in database. No record found for this id.");
        }
        else if (rowCount > 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("mo", "update", "morerecords"), "MobileOperator was updated but there was more than one record affected.", sqlCmdText, this, connection, data);
          if (this.Logger.IsFatalEnabled)
            this.Logger.Fatal(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "MobileOperator", "Exception while updating MobileOperator object in database. More than one record found for this statement (update statement where clause broken?!).");
        }
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("mo", "update", "exception"), "MobileOperator could not be updated. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Update, "MobileOperator", "Exception while updating MobileOperator object in database. See inner exception for details.", ex);
      }
    }

    public override bool Delete(ISqlConnectionInfo connection, MobileOperator data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText =  "DELETE FROM [core].[MobileOperator] WHERE MobileOperatorID = @MobileOperatorID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@MobileOperatorID", data.ID);

        int success = sqlCmd.ExecuteNonQuery();

        if (success == -1)        
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("mo", "delete", "norecord"), "MobileOperator could not be deleted as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Delete, "MobileOperator", "Exception while deleting MobileOperator object from database. No such record found.");
        }
        
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("mo", "delete", "exception"), "MobileOperator could not be deleted. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Delete, "MobileOperator", "Exception while deleting MobileOperator object from database. See inner exception for details.", ex);
      }
    }
  }
}

