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
  [DataManager(typeof(Transaction))] 
  public partial class TransactionManager : Adlite.Data.Sql.SqlManagerBase<Transaction>, ITransactionManager
  {
    public override DatabaseType Type
    {
      get { return DatabaseType.Adlite; }
    }

    protected override Transaction LoadInternal(ISqlConnectionInfo connection, int id)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "SELECT " + 
							TransactionTable.GetColumnNames("[t]") + 
							(this.Depth > 0 ? "," + PriceTable.GetColumnNames("[t_p]") : string.Empty) + 
							(this.Depth > 1 ? "," + CurrencyTable.GetColumnNames("[t_p_c]") : string.Empty) + 
					" FROM [core].[Transaction] AS [t] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Price] AS [t_p] ON [t].[PriceID] = [t_p].[PriceID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Currency] AS [t_p_c] ON [t_p].[CurrencyID] = [t_p_c].[CurrencyID] ";
				sqlCmdText += "WHERE [t].[TransactionID] = @TransactionID;";

        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@TransactionID", id);
        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("t", "loadinternal", "notfound"), "Transaction could not be loaded by id as it was not found.", sqlCmdText, this, connection, id);
          if (this.Logger.IsWarnEnabled)
            this.Logger.Warn(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				TransactionTable tTable = new TransactionTable(query);
				PriceTable t_pTable = (this.Depth > 0) ? new PriceTable(query) : null;
				CurrencyTable t_p_cTable = (this.Depth > 1) ? new CurrencyTable(query) : null;

        
				Currency t_p_cObject = (this.Depth > 1) ? t_p_cTable.CreateInstance() : null;
				Price t_pObject = (this.Depth > 0) ? t_pTable.CreateInstance(t_p_cObject) : null;
				Transaction tObject = tTable.CreateInstance(t_pObject);
				sqlReader.Close();

				return tObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("t", "loadinternal", "exception"), "Transaction could not be loaded by id. See exception for details.", sqlCmdText, ex, this, connection, id);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "Transaction", "Exception while loading Transaction object from database. See inner exception for details.", ex);
      }
    }

    public Transaction Load(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							TransactionTable.GetColumnNames("[t]") + 
							(this.Depth > 0 ? "," + PriceTable.GetColumnNames("[t_p]") : string.Empty) + 
							(this.Depth > 1 ? "," + CurrencyTable.GetColumnNames("[t_p_c]") : string.Empty) +  
					" FROM [core].[Transaction] AS [t] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Price] AS [t_p] ON [t].[PriceID] = [t_p].[PriceID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Currency] AS [t_p_c] ON [t_p].[CurrencyID] = [t_p_c].[CurrencyID] ";
				

        parameters.Top = 1;
        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("t", "customload", "notfound"), "Transaction could not be loaded using custom logic as it was not found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				TransactionTable tTable = new TransactionTable(query);
				PriceTable t_pTable = (this.Depth > 0) ? new PriceTable(query) : null;
				CurrencyTable t_p_cTable = (this.Depth > 1) ? new CurrencyTable(query) : null;

        
				Currency t_p_cObject = (this.Depth > 1) ? t_p_cTable.CreateInstance() : null;
				Price t_pObject = (this.Depth > 0) ? t_pTable.CreateInstance(t_p_cObject) : null;
				Transaction tObject = tTable.CreateInstance(t_pObject);
				sqlReader.Close();

				return tObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("t", "customload", "exception"), "Transaction could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "Transaction", "Exception while loading (custom/single) Transaction object from database. See inner exception for details.", ex);
      }
    }

    public List<Transaction> LoadMany(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							TransactionTable.GetColumnNames("[t]") + 
							(this.Depth > 0 ? "," + PriceTable.GetColumnNames("[t_p]") : string.Empty) + 
							(this.Depth > 1 ? "," + CurrencyTable.GetColumnNames("[t_p_c]") : string.Empty) +  
					" FROM [core].[Transaction] AS [t] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Price] AS [t_p] ON [t].[PriceID] = [t_p].[PriceID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Currency] AS [t_p_c] ON [t_p].[CurrencyID] = [t_p_c].[CurrencyID] ";
				

        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("t", "customloadmany", "notfound"), "Transaction list could not be loaded using custom logic as no items were found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return new List<Transaction>();
        }

        SqlQuery query = new SqlQuery(sqlReader);

				TransactionTable tTable = new TransactionTable(query);
				PriceTable t_pTable = (this.Depth > 0) ? new PriceTable(query) : null;
				CurrencyTable t_p_cTable = (this.Depth > 1) ? new CurrencyTable(query) : null;

        List<Transaction> result = new List<Transaction>();
        do
        {
          
					Currency t_p_cObject = (this.Depth > 1) ? t_p_cTable.CreateInstance() : null;
					Price t_pObject = (this.Depth > 0) ? t_pTable.CreateInstance(t_p_cObject) : null;
					Transaction tObject = (this.Depth > -1) ? tTable.CreateInstance(t_pObject) : null;
					result.Add(tObject);

        } while (sqlReader.Read());
        sqlReader.Close();

        return result;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("t", "customloadmany", "exception"), "Transaction list could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "Transaction", "Exception while loading (custom/many) Transaction object from database. See inner exception for details.", ex);
      }
    }
    
    public override int? Insert(ISqlConnectionInfo connection, Transaction data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "INSERT INTO [core].[Transaction] ([ClickID],[PriceID],[OfferID],[CampaignID],[AffiliateID],[PriceInEuros]) VALUES(@ClickID,@PriceID,@OfferID,@CampaignID,@AffiliateID,@PriceInEuros); SELECT SCOPE_IDENTITY();";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@ClickID", data.ClickID).SqlDbType = SqlDbType.Int;
				sqlCmd.Parameters.AddWithValue("@PriceID", data.Price.ID);
				sqlCmd.Parameters.AddWithValue("@OfferID", data.OfferID).SqlDbType = SqlDbType.Int;
				sqlCmd.Parameters.AddWithValue("@CampaignID", data.CampaignID).SqlDbType = SqlDbType.Int;
				sqlCmd.Parameters.AddWithValue("@AffiliateID", data.AffiliateID).SqlDbType = SqlDbType.Int;
				sqlCmd.Parameters.AddWithValue("@PriceInEuros", data.PriceInEuros).SqlDbType = SqlDbType.Decimal;
				
        object idObj = sqlCmd.ExecuteScalar();
        if (idObj == null || DBNull.Value.Equals(idObj))
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("t", "insert", "noprimarykey"), "Transaction could not be inserted or inserted primary key was not returned. Are you missing SELECT SCOPE_IDENTITY();?", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Insert, "Transaction", "Exception while inserting Transaction object in database.");
        }
        return (int)((decimal)idObj);
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("t", "insert", "exception"), "Transaction could not be inserted. See exception for details.", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Insert, "Transaction", "Exception while inserting Transaction object in database. See inner exception for details.", ex);
      }
    }

    public override bool Update(ISqlConnectionInfo connection, Transaction data)
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
        sqlCmdText = "UPDATE [core].[Transaction] SET " +
												"[ClickID] = @ClickID, " + 
												"[PriceID] = @PriceID, " + 
												"[OfferID] = @OfferID, " + 
												"[CampaignID] = @CampaignID, " + 
												"[AffiliateID] = @AffiliateID, " + 
												"[PriceInEuros] = @PriceInEuros, " + 
												"[Updated] = GETDATE() " + 
											"WHERE [TransactionID] = @TransactionID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@ClickID", data.ClickID).SqlDbType = SqlDbType.Int;
				sqlCmd.Parameters.AddWithValue("@PriceID", data.Price.ID);
				sqlCmd.Parameters.AddWithValue("@OfferID", data.OfferID).SqlDbType = SqlDbType.Int;
				sqlCmd.Parameters.AddWithValue("@CampaignID", data.CampaignID).SqlDbType = SqlDbType.Int;
				sqlCmd.Parameters.AddWithValue("@AffiliateID", data.AffiliateID).SqlDbType = SqlDbType.Int;
				sqlCmd.Parameters.AddWithValue("@PriceInEuros", data.PriceInEuros).SqlDbType = SqlDbType.Decimal;
				sqlCmd.Parameters.AddWithValue("@Updated", data.Updated).SqlDbType = SqlDbType.DateTime2;
				sqlCmd.Parameters.AddWithValue("@TransactionID", data.ID);

        int rowCount = sqlCmd.ExecuteNonQuery();
        if (rowCount < 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("t", "update", "norecord"), "Transaction could not be updated as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "Transaction", "Exception while updating Transaction object in database. No record found for this id.");
        }
        else if (rowCount > 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("t", "update", "morerecords"), "Transaction was updated but there was more than one record affected.", sqlCmdText, this, connection, data);
          if (this.Logger.IsFatalEnabled)
            this.Logger.Fatal(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "Transaction", "Exception while updating Transaction object in database. More than one record found for this statement (update statement where clause broken?!).");
        }
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("t", "update", "exception"), "Transaction could not be updated. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Update, "Transaction", "Exception while updating Transaction object in database. See inner exception for details.", ex);
      }
    }

    public override bool Delete(ISqlConnectionInfo connection, Transaction data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText =  "DELETE FROM [core].[Transaction] WHERE TransactionID = @TransactionID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@TransactionID", data.ID);

        int success = sqlCmd.ExecuteNonQuery();

        if (success == -1)        
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("t", "delete", "norecord"), "Transaction could not be deleted as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Delete, "Transaction", "Exception while deleting Transaction object from database. No such record found.");
        }
        
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("t", "delete", "exception"), "Transaction could not be deleted. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Delete, "Transaction", "Exception while deleting Transaction object from database. See inner exception for details.", ex);
      }
    }
  }
}

