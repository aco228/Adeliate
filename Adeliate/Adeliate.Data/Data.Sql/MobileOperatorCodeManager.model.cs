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
  [DataManager(typeof(MobileOperatorCode))] 
  public partial class MobileOperatorCodeManager : Adlite.Data.Sql.SqlManagerBase<MobileOperatorCode>, IMobileOperatorCodeManager
  {
    public override DatabaseType Type
    {
      get { return DatabaseType.Adlite; }
    }

    protected override MobileOperatorCode LoadInternal(ISqlConnectionInfo connection, int id)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "SELECT " + 
							MobileOperatorCodeTable.GetColumnNames("[moc]") + 
							(this.Depth > 0 ? "," + MobileOperatorTable.GetColumnNames("[moc_mo]") : string.Empty) + 
							(this.Depth > 1 ? "," + CountryTable.GetColumnNames("[moc_mo_c]") : string.Empty) + 
					" FROM [core].[MobileOperatorCode] AS [moc] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[MobileOperator] AS [moc_mo] ON [moc].[MobileOperatorID] = [moc_mo].[MobileOperatorID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Country] AS [moc_mo_c] ON [moc_mo].[CountryID] = [moc_mo_c].[CountryID] ";
				sqlCmdText += "WHERE [moc].[MobileOperatorCodeID] = @MobileOperatorCodeID;";

        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@MobileOperatorCodeID", id);
        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("moc", "loadinternal", "notfound"), "MobileOperatorCode could not be loaded by id as it was not found.", sqlCmdText, this, connection, id);
          if (this.Logger.IsWarnEnabled)
            this.Logger.Warn(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				MobileOperatorCodeTable mocTable = new MobileOperatorCodeTable(query);
				MobileOperatorTable moc_moTable = (this.Depth > 0) ? new MobileOperatorTable(query) : null;
				CountryTable moc_mo_cTable = (this.Depth > 1) ? new CountryTable(query) : null;

        
				Country moc_mo_cObject = (this.Depth > 1) ? moc_mo_cTable.CreateInstance() : null;
				MobileOperator moc_moObject = (this.Depth > 0) ? moc_moTable.CreateInstance(moc_mo_cObject) : null;
				MobileOperatorCode mocObject = mocTable.CreateInstance(moc_moObject);
				sqlReader.Close();

				return mocObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("moc", "loadinternal", "exception"), "MobileOperatorCode could not be loaded by id. See exception for details.", sqlCmdText, ex, this, connection, id);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "MobileOperatorCode", "Exception while loading MobileOperatorCode object from database. See inner exception for details.", ex);
      }
    }

    public MobileOperatorCode Load(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							MobileOperatorCodeTable.GetColumnNames("[moc]") + 
							(this.Depth > 0 ? "," + MobileOperatorTable.GetColumnNames("[moc_mo]") : string.Empty) + 
							(this.Depth > 1 ? "," + CountryTable.GetColumnNames("[moc_mo_c]") : string.Empty) +  
					" FROM [core].[MobileOperatorCode] AS [moc] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[MobileOperator] AS [moc_mo] ON [moc].[MobileOperatorID] = [moc_mo].[MobileOperatorID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Country] AS [moc_mo_c] ON [moc_mo].[CountryID] = [moc_mo_c].[CountryID] ";
				

        parameters.Top = 1;
        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("moc", "customload", "notfound"), "MobileOperatorCode could not be loaded using custom logic as it was not found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				MobileOperatorCodeTable mocTable = new MobileOperatorCodeTable(query);
				MobileOperatorTable moc_moTable = (this.Depth > 0) ? new MobileOperatorTable(query) : null;
				CountryTable moc_mo_cTable = (this.Depth > 1) ? new CountryTable(query) : null;

        
				Country moc_mo_cObject = (this.Depth > 1) ? moc_mo_cTable.CreateInstance() : null;
				MobileOperator moc_moObject = (this.Depth > 0) ? moc_moTable.CreateInstance(moc_mo_cObject) : null;
				MobileOperatorCode mocObject = mocTable.CreateInstance(moc_moObject);
				sqlReader.Close();

				return mocObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("moc", "customload", "exception"), "MobileOperatorCode could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "MobileOperatorCode", "Exception while loading (custom/single) MobileOperatorCode object from database. See inner exception for details.", ex);
      }
    }

    public List<MobileOperatorCode> LoadMany(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							MobileOperatorCodeTable.GetColumnNames("[moc]") + 
							(this.Depth > 0 ? "," + MobileOperatorTable.GetColumnNames("[moc_mo]") : string.Empty) + 
							(this.Depth > 1 ? "," + CountryTable.GetColumnNames("[moc_mo_c]") : string.Empty) +  
					" FROM [core].[MobileOperatorCode] AS [moc] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[MobileOperator] AS [moc_mo] ON [moc].[MobileOperatorID] = [moc_mo].[MobileOperatorID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Country] AS [moc_mo_c] ON [moc_mo].[CountryID] = [moc_mo_c].[CountryID] ";
				

        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("moc", "customloadmany", "notfound"), "MobileOperatorCode list could not be loaded using custom logic as no items were found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return new List<MobileOperatorCode>();
        }

        SqlQuery query = new SqlQuery(sqlReader);

				MobileOperatorCodeTable mocTable = new MobileOperatorCodeTable(query);
				MobileOperatorTable moc_moTable = (this.Depth > 0) ? new MobileOperatorTable(query) : null;
				CountryTable moc_mo_cTable = (this.Depth > 1) ? new CountryTable(query) : null;

        List<MobileOperatorCode> result = new List<MobileOperatorCode>();
        do
        {
          
					Country moc_mo_cObject = (this.Depth > 1) ? moc_mo_cTable.CreateInstance() : null;
					MobileOperator moc_moObject = (this.Depth > 0) ? moc_moTable.CreateInstance(moc_mo_cObject) : null;
					MobileOperatorCode mocObject = (this.Depth > -1) ? mocTable.CreateInstance(moc_moObject) : null;
					result.Add(mocObject);

        } while (sqlReader.Read());
        sqlReader.Close();

        return result;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("moc", "customloadmany", "exception"), "MobileOperatorCode list could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "MobileOperatorCode", "Exception while loading (custom/many) MobileOperatorCode object from database. See inner exception for details.", ex);
      }
    }
    
    public override int? Insert(ISqlConnectionInfo connection, MobileOperatorCode data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "INSERT INTO [core].[MobileOperatorCode] ([MobileOperatorID],[MCC],[MNC],[IsDefault]) VALUES(@MobileOperatorID,@MCC,@MNC,@IsDefault); SELECT SCOPE_IDENTITY();";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@MobileOperatorID", data.MobileOperator.ID);
				sqlCmd.Parameters.AddWithValue("@MCC", data.MCC).SqlDbType = SqlDbType.Int;
				sqlCmd.Parameters.AddWithValue("@MNC", data.MNC).SqlDbType = SqlDbType.Int;
				sqlCmd.Parameters.AddWithValue("@IsDefault", data.IsDefault).SqlDbType = SqlDbType.Bit;
				
        object idObj = sqlCmd.ExecuteScalar();
        if (idObj == null || DBNull.Value.Equals(idObj))
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("moc", "insert", "noprimarykey"), "MobileOperatorCode could not be inserted or inserted primary key was not returned. Are you missing SELECT SCOPE_IDENTITY();?", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Insert, "MobileOperatorCode", "Exception while inserting MobileOperatorCode object in database.");
        }
        return (int)((decimal)idObj);
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("moc", "insert", "exception"), "MobileOperatorCode could not be inserted. See exception for details.", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Insert, "MobileOperatorCode", "Exception while inserting MobileOperatorCode object in database. See inner exception for details.", ex);
      }
    }

    public override bool Update(ISqlConnectionInfo connection, MobileOperatorCode data)
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
        sqlCmdText = "UPDATE [core].[MobileOperatorCode] SET " +
												"[MobileOperatorID] = @MobileOperatorID, " + 
												"[MCC] = @MCC, " + 
												"[MNC] = @MNC, " + 
												"[IsDefault] = @IsDefault, " + 
												"[Updated] = GETDATE() " + 
											"WHERE [MobileOperatorCodeID] = @MobileOperatorCodeID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@MobileOperatorID", data.MobileOperator.ID);
				sqlCmd.Parameters.AddWithValue("@MCC", data.MCC).SqlDbType = SqlDbType.Int;
				sqlCmd.Parameters.AddWithValue("@MNC", data.MNC).SqlDbType = SqlDbType.Int;
				sqlCmd.Parameters.AddWithValue("@IsDefault", data.IsDefault).SqlDbType = SqlDbType.Bit;
				sqlCmd.Parameters.AddWithValue("@Updated", data.Updated).SqlDbType = SqlDbType.DateTime2;
				sqlCmd.Parameters.AddWithValue("@MobileOperatorCodeID", data.ID);

        int rowCount = sqlCmd.ExecuteNonQuery();
        if (rowCount < 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("moc", "update", "norecord"), "MobileOperatorCode could not be updated as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "MobileOperatorCode", "Exception while updating MobileOperatorCode object in database. No record found for this id.");
        }
        else if (rowCount > 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("moc", "update", "morerecords"), "MobileOperatorCode was updated but there was more than one record affected.", sqlCmdText, this, connection, data);
          if (this.Logger.IsFatalEnabled)
            this.Logger.Fatal(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "MobileOperatorCode", "Exception while updating MobileOperatorCode object in database. More than one record found for this statement (update statement where clause broken?!).");
        }
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("moc", "update", "exception"), "MobileOperatorCode could not be updated. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Update, "MobileOperatorCode", "Exception while updating MobileOperatorCode object in database. See inner exception for details.", ex);
      }
    }

    public override bool Delete(ISqlConnectionInfo connection, MobileOperatorCode data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText =  "DELETE FROM [core].[MobileOperatorCode] WHERE MobileOperatorCodeID = @MobileOperatorCodeID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@MobileOperatorCodeID", data.ID);

        int success = sqlCmd.ExecuteNonQuery();

        if (success == -1)        
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("moc", "delete", "norecord"), "MobileOperatorCode could not be deleted as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Delete, "MobileOperatorCode", "Exception while deleting MobileOperatorCode object from database. No such record found.");
        }
        
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("moc", "delete", "exception"), "MobileOperatorCode could not be deleted. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Delete, "MobileOperatorCode", "Exception while deleting MobileOperatorCode object from database. See inner exception for details.", ex);
      }
    }
  }
}

