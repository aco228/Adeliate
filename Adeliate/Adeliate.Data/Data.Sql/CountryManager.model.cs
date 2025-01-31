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
  [DataManager(typeof(Country))] 
  public partial class CountryManager : Adlite.Data.Sql.SqlManagerBase<Country>, ICountryManager
  {
    public override DatabaseType Type
    {
      get { return DatabaseType.Adlite; }
    }

    protected override Country LoadInternal(ISqlConnectionInfo connection, int id)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "SELECT " + 
							CountryTable.GetColumnNames("[c]") + 
							(this.Depth > 0 ? "," + LanguageTable.GetColumnNames("[c_l]") : string.Empty) + 
							(this.Depth > 0 ? "," + CurrencyTable.GetColumnNames("[c_c]") : string.Empty) + 
					" FROM [core].[Country] AS [c] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Language] AS [c_l] ON [c].[LanguageID] = [c_l].[LanguageID] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Currency] AS [c_c] ON [c].[CurrencyID] = [c_c].[CurrencyID] ";
				sqlCmdText += "WHERE [c].[CountryID] = @CountryID;";

        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@CountryID", id);
        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "loadinternal", "notfound"), "Country could not be loaded by id as it was not found.", sqlCmdText, this, connection, id);
          if (this.Logger.IsWarnEnabled)
            this.Logger.Warn(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				CountryTable cTable = new CountryTable(query);
				LanguageTable c_lTable = (this.Depth > 0) ? new LanguageTable(query) : null;
				CurrencyTable c_cTable = (this.Depth > 0) ? new CurrencyTable(query) : null;

        
				Language c_lObject = (this.Depth > 0) ? c_lTable.CreateInstance() : null;
				Currency c_cObject = (this.Depth > 0) ? c_cTable.CreateInstance() : null;
				Country cObject = cTable.CreateInstance(c_lObject, c_cObject);
				sqlReader.Close();

				return cObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "loadinternal", "exception"), "Country could not be loaded by id. See exception for details.", sqlCmdText, ex, this, connection, id);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "Country", "Exception while loading Country object from database. See inner exception for details.", ex);
      }
    }

    public Country Load(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							CountryTable.GetColumnNames("[c]") + 
							(this.Depth > 0 ? "," + LanguageTable.GetColumnNames("[c_l]") : string.Empty) + 
							(this.Depth > 0 ? "," + CurrencyTable.GetColumnNames("[c_c]") : string.Empty) +  
					" FROM [core].[Country] AS [c] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Language] AS [c_l] ON [c].[LanguageID] = [c_l].[LanguageID] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Currency] AS [c_c] ON [c].[CurrencyID] = [c_c].[CurrencyID] ";
				

        parameters.Top = 1;
        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "customload", "notfound"), "Country could not be loaded using custom logic as it was not found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				CountryTable cTable = new CountryTable(query);
				LanguageTable c_lTable = (this.Depth > 0) ? new LanguageTable(query) : null;
				CurrencyTable c_cTable = (this.Depth > 0) ? new CurrencyTable(query) : null;

        
				Language c_lObject = (this.Depth > 0) ? c_lTable.CreateInstance() : null;
				Currency c_cObject = (this.Depth > 0) ? c_cTable.CreateInstance() : null;
				Country cObject = cTable.CreateInstance(c_lObject, c_cObject);
				sqlReader.Close();

				return cObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "customload", "exception"), "Country could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "Country", "Exception while loading (custom/single) Country object from database. See inner exception for details.", ex);
      }
    }

    public List<Country> LoadMany(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							CountryTable.GetColumnNames("[c]") + 
							(this.Depth > 0 ? "," + LanguageTable.GetColumnNames("[c_l]") : string.Empty) + 
							(this.Depth > 0 ? "," + CurrencyTable.GetColumnNames("[c_c]") : string.Empty) +  
					" FROM [core].[Country] AS [c] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Language] AS [c_l] ON [c].[LanguageID] = [c_l].[LanguageID] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Currency] AS [c_c] ON [c].[CurrencyID] = [c_c].[CurrencyID] ";
				

        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "customloadmany", "notfound"), "Country list could not be loaded using custom logic as no items were found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return new List<Country>();
        }

        SqlQuery query = new SqlQuery(sqlReader);

				CountryTable cTable = new CountryTable(query);
				LanguageTable c_lTable = (this.Depth > 0) ? new LanguageTable(query) : null;
				CurrencyTable c_cTable = (this.Depth > 0) ? new CurrencyTable(query) : null;

        List<Country> result = new List<Country>();
        do
        {
          
					Language c_lObject = (this.Depth > 0) ? c_lTable.CreateInstance() : null;
					Currency c_cObject = (this.Depth > 0) ? c_cTable.CreateInstance() : null;
					Country cObject = (this.Depth > -1) ? cTable.CreateInstance(c_lObject, c_cObject) : null;
					result.Add(cObject);

        } while (sqlReader.Read());
        sqlReader.Close();

        return result;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "customloadmany", "exception"), "Country list could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "Country", "Exception while loading (custom/many) Country object from database. See inner exception for details.", ex);
      }
    }
    
    public override int? Insert(ISqlConnectionInfo connection, Country data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "INSERT INTO [core].[Country] ([LanguageID],[CurrencyID],[GlobalName],[LocalName],[CountryCode],[CultureCode],[TwoLetterIsoCode],[LCID],[CallingCode]) VALUES(@LanguageID,@CurrencyID,@GlobalName,@LocalName,@CountryCode,@CultureCode,@TwoLetterIsoCode,@LCID,@CallingCode); SELECT SCOPE_IDENTITY();";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@LanguageID", data.Language == null ? DBNull.Value : (object)data.Language.ID);
				sqlCmd.Parameters.AddWithValue("@CurrencyID", data.Currency == null ? DBNull.Value : (object)data.Currency.ID);
				sqlCmd.Parameters.AddWithValue("@GlobalName", data.GlobalName).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@LocalName", data.LocalName).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@CountryCode", data.CountryCode).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@CultureCode", data.CultureCode).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@TwoLetterIsoCode", data.TwoLetterIsoCode).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@LCID", data.LCID.HasValue ? (object)data.LCID.Value : DBNull.Value).SqlDbType = SqlDbType.Int;
				sqlCmd.Parameters.AddWithValue("@CallingCode", data.CallingCode.HasValue ? (object)data.CallingCode.Value : DBNull.Value).SqlDbType = SqlDbType.Int;
				
        object idObj = sqlCmd.ExecuteScalar();
        if (idObj == null || DBNull.Value.Equals(idObj))
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "insert", "noprimarykey"), "Country could not be inserted or inserted primary key was not returned. Are you missing SELECT SCOPE_IDENTITY();?", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Insert, "Country", "Exception while inserting Country object in database.");
        }
        return (int)((decimal)idObj);
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "insert", "exception"), "Country could not be inserted. See exception for details.", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Insert, "Country", "Exception while inserting Country object in database. See inner exception for details.", ex);
      }
    }

    public override bool Update(ISqlConnectionInfo connection, Country data)
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
        sqlCmdText = "UPDATE [core].[Country] SET " +
												"[LanguageID] = @LanguageID, " + 
												"[CurrencyID] = @CurrencyID, " + 
												"[GlobalName] = @GlobalName, " + 
												"[LocalName] = @LocalName, " + 
												"[CountryCode] = @CountryCode, " + 
												"[CultureCode] = @CultureCode, " + 
												"[TwoLetterIsoCode] = @TwoLetterIsoCode, " + 
												"[LCID] = @LCID, " + 
												"[CallingCode] = @CallingCode, " + 
												"[Updated] = GETDATE() " + 
											"WHERE [CountryID] = @CountryID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@LanguageID", data.Language == null ? DBNull.Value : (object)data.Language.ID);
				sqlCmd.Parameters.AddWithValue("@CurrencyID", data.Currency == null ? DBNull.Value : (object)data.Currency.ID);
				sqlCmd.Parameters.AddWithValue("@GlobalName", data.GlobalName).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@LocalName", data.LocalName).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@CountryCode", data.CountryCode).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@CultureCode", data.CultureCode).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@TwoLetterIsoCode", data.TwoLetterIsoCode).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@LCID", data.LCID.HasValue ? (object)data.LCID.Value : DBNull.Value).SqlDbType = SqlDbType.Int;
				sqlCmd.Parameters.AddWithValue("@CallingCode", data.CallingCode.HasValue ? (object)data.CallingCode.Value : DBNull.Value).SqlDbType = SqlDbType.Int;
				sqlCmd.Parameters.AddWithValue("@Updated", data.Updated).SqlDbType = SqlDbType.DateTime2;
				sqlCmd.Parameters.AddWithValue("@CountryID", data.ID);

        int rowCount = sqlCmd.ExecuteNonQuery();
        if (rowCount < 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "update", "norecord"), "Country could not be updated as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "Country", "Exception while updating Country object in database. No record found for this id.");
        }
        else if (rowCount > 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "update", "morerecords"), "Country was updated but there was more than one record affected.", sqlCmdText, this, connection, data);
          if (this.Logger.IsFatalEnabled)
            this.Logger.Fatal(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "Country", "Exception while updating Country object in database. More than one record found for this statement (update statement where clause broken?!).");
        }
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "update", "exception"), "Country could not be updated. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Update, "Country", "Exception while updating Country object in database. See inner exception for details.", ex);
      }
    }

    public override bool Delete(ISqlConnectionInfo connection, Country data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText =  "DELETE FROM [core].[Country] WHERE CountryID = @CountryID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@CountryID", data.ID);

        int success = sqlCmd.ExecuteNonQuery();

        if (success == -1)        
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "delete", "norecord"), "Country could not be deleted as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Delete, "Country", "Exception while deleting Country object from database. No such record found.");
        }
        
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "delete", "exception"), "Country could not be deleted. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Delete, "Country", "Exception while deleting Country object from database. See inner exception for details.", ex);
      }
    }
  }
}

