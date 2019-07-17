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
  [DataManager(typeof(Offer))] 
  public partial class OfferManager : Adlite.Data.Sql.SqlManagerBase<Offer>, IOfferManager
  {
    public override DatabaseType Type
    {
      get { return DatabaseType.Adlite; }
    }

    protected override Offer LoadInternal(ISqlConnectionInfo connection, int id)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "SELECT " + 
							OfferTable.GetColumnNames("[o]") + 
							(this.Depth > 0 ? "," + CampaignTable.GetColumnNames("[o_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignGroupTable.GetColumnNames("[o_c_cg]") : string.Empty) + 
							(this.Depth > 1 ? "," + CountryTable.GetColumnNames("[o_c_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + PriceTable.GetColumnNames("[o_c_p]") : string.Empty) + 
							(this.Depth > 0 ? "," + AffiliateTable.GetColumnNames("[o_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTypeTable.GetColumnNames("[o_a_at]") : string.Empty) + 
							(this.Depth > 0 ? "," + PriceTable.GetColumnNames("[o_p]") : string.Empty) + 
							(this.Depth > 1 ? "," + CurrencyTable.GetColumnNames("[o_p_c]") : string.Empty) + 
					" FROM [core].[Offer] AS [o] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Campaign] AS [o_c] ON [o].[CampaignID] = [o_c].[CampaignID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[CampaignGroup] AS [o_c_cg] ON [o_c].[CampaignGroupID] = [o_c_cg].[CampaignGroupID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Country] AS [o_c_c] ON [o_c].[CountryID] = [o_c_c].[CountryID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Price] AS [o_c_p] ON [o_c].[PriceID] = [o_c_p].[PriceID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Affiliate] AS [o_a] ON [o].[AffiliateID] = [o_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[AffiliateType] AS [o_a_at] ON [o_a].[AffiliateTypeID] = [o_a_at].[AffiliateTypeID] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Price] AS [o_p] ON [o].[PriceID] = [o_p].[PriceID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Currency] AS [o_p_c] ON [o_p].[CurrencyID] = [o_p_c].[CurrencyID] ";
				sqlCmdText += "WHERE [o].[OfferID] = @OfferID;";

        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@OfferID", id);
        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("o", "loadinternal", "notfound"), "Offer could not be loaded by id as it was not found.", sqlCmdText, this, connection, id);
          if (this.Logger.IsWarnEnabled)
            this.Logger.Warn(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				OfferTable oTable = new OfferTable(query);
				CampaignTable o_cTable = (this.Depth > 0) ? new CampaignTable(query) : null;
				CampaignGroupTable o_c_cgTable = (this.Depth > 1) ? new CampaignGroupTable(query) : null;
				CountryTable o_c_cTable = (this.Depth > 1) ? new CountryTable(query) : null;
				PriceTable o_c_pTable = (this.Depth > 1) ? new PriceTable(query) : null;
				AffiliateTable o_aTable = (this.Depth > 0) ? new AffiliateTable(query) : null;
				AffiliateTypeTable o_a_atTable = (this.Depth > 1) ? new AffiliateTypeTable(query) : null;
				PriceTable o_pTable = (this.Depth > 0) ? new PriceTable(query) : null;
				CurrencyTable o_p_cTable = (this.Depth > 1) ? new CurrencyTable(query) : null;

        
				CampaignGroup o_c_cgObject = (this.Depth > 1) ? o_c_cgTable.CreateInstance() : null;
				Country o_c_cObject = (this.Depth > 1) ? o_c_cTable.CreateInstance() : null;
				Price o_c_pObject = (this.Depth > 1) ? o_c_pTable.CreateInstance() : null;
				Campaign o_cObject = (this.Depth > 0) ? o_cTable.CreateInstance(o_c_cgObject, o_c_cObject, o_c_pObject) : null;
				AffiliateType o_a_atObject = (this.Depth > 1) ? o_a_atTable.CreateInstance() : null;
				Affiliate o_aObject = (this.Depth > 0) ? o_aTable.CreateInstance(o_a_atObject) : null;
				Currency o_p_cObject = (this.Depth > 1) ? o_p_cTable.CreateInstance() : null;
				Price o_pObject = (this.Depth > 0) ? o_pTable.CreateInstance(o_p_cObject) : null;
				Offer oObject = oTable.CreateInstance(o_cObject, o_aObject, o_pObject);
				sqlReader.Close();

				return oObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("o", "loadinternal", "exception"), "Offer could not be loaded by id. See exception for details.", sqlCmdText, ex, this, connection, id);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "Offer", "Exception while loading Offer object from database. See inner exception for details.", ex);
      }
    }

    public Offer Load(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							OfferTable.GetColumnNames("[o]") + 
							(this.Depth > 0 ? "," + CampaignTable.GetColumnNames("[o_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignGroupTable.GetColumnNames("[o_c_cg]") : string.Empty) + 
							(this.Depth > 1 ? "," + CountryTable.GetColumnNames("[o_c_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + PriceTable.GetColumnNames("[o_c_p]") : string.Empty) + 
							(this.Depth > 0 ? "," + AffiliateTable.GetColumnNames("[o_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTypeTable.GetColumnNames("[o_a_at]") : string.Empty) + 
							(this.Depth > 0 ? "," + PriceTable.GetColumnNames("[o_p]") : string.Empty) + 
							(this.Depth > 1 ? "," + CurrencyTable.GetColumnNames("[o_p_c]") : string.Empty) +  
					" FROM [core].[Offer] AS [o] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Campaign] AS [o_c] ON [o].[CampaignID] = [o_c].[CampaignID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[CampaignGroup] AS [o_c_cg] ON [o_c].[CampaignGroupID] = [o_c_cg].[CampaignGroupID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Country] AS [o_c_c] ON [o_c].[CountryID] = [o_c_c].[CountryID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Price] AS [o_c_p] ON [o_c].[PriceID] = [o_c_p].[PriceID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Affiliate] AS [o_a] ON [o].[AffiliateID] = [o_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[AffiliateType] AS [o_a_at] ON [o_a].[AffiliateTypeID] = [o_a_at].[AffiliateTypeID] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Price] AS [o_p] ON [o].[PriceID] = [o_p].[PriceID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Currency] AS [o_p_c] ON [o_p].[CurrencyID] = [o_p_c].[CurrencyID] ";
				

        parameters.Top = 1;
        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("o", "customload", "notfound"), "Offer could not be loaded using custom logic as it was not found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				OfferTable oTable = new OfferTable(query);
				CampaignTable o_cTable = (this.Depth > 0) ? new CampaignTable(query) : null;
				CampaignGroupTable o_c_cgTable = (this.Depth > 1) ? new CampaignGroupTable(query) : null;
				CountryTable o_c_cTable = (this.Depth > 1) ? new CountryTable(query) : null;
				PriceTable o_c_pTable = (this.Depth > 1) ? new PriceTable(query) : null;
				AffiliateTable o_aTable = (this.Depth > 0) ? new AffiliateTable(query) : null;
				AffiliateTypeTable o_a_atTable = (this.Depth > 1) ? new AffiliateTypeTable(query) : null;
				PriceTable o_pTable = (this.Depth > 0) ? new PriceTable(query) : null;
				CurrencyTable o_p_cTable = (this.Depth > 1) ? new CurrencyTable(query) : null;

        
				CampaignGroup o_c_cgObject = (this.Depth > 1) ? o_c_cgTable.CreateInstance() : null;
				Country o_c_cObject = (this.Depth > 1) ? o_c_cTable.CreateInstance() : null;
				Price o_c_pObject = (this.Depth > 1) ? o_c_pTable.CreateInstance() : null;
				Campaign o_cObject = (this.Depth > 0) ? o_cTable.CreateInstance(o_c_cgObject, o_c_cObject, o_c_pObject) : null;
				AffiliateType o_a_atObject = (this.Depth > 1) ? o_a_atTable.CreateInstance() : null;
				Affiliate o_aObject = (this.Depth > 0) ? o_aTable.CreateInstance(o_a_atObject) : null;
				Currency o_p_cObject = (this.Depth > 1) ? o_p_cTable.CreateInstance() : null;
				Price o_pObject = (this.Depth > 0) ? o_pTable.CreateInstance(o_p_cObject) : null;
				Offer oObject = oTable.CreateInstance(o_cObject, o_aObject, o_pObject);
				sqlReader.Close();

				return oObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("o", "customload", "exception"), "Offer could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "Offer", "Exception while loading (custom/single) Offer object from database. See inner exception for details.", ex);
      }
    }

    public List<Offer> LoadMany(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							OfferTable.GetColumnNames("[o]") + 
							(this.Depth > 0 ? "," + CampaignTable.GetColumnNames("[o_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignGroupTable.GetColumnNames("[o_c_cg]") : string.Empty) + 
							(this.Depth > 1 ? "," + CountryTable.GetColumnNames("[o_c_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + PriceTable.GetColumnNames("[o_c_p]") : string.Empty) + 
							(this.Depth > 0 ? "," + AffiliateTable.GetColumnNames("[o_a]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTypeTable.GetColumnNames("[o_a_at]") : string.Empty) + 
							(this.Depth > 0 ? "," + PriceTable.GetColumnNames("[o_p]") : string.Empty) + 
							(this.Depth > 1 ? "," + CurrencyTable.GetColumnNames("[o_p_c]") : string.Empty) +  
					" FROM [core].[Offer] AS [o] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Campaign] AS [o_c] ON [o].[CampaignID] = [o_c].[CampaignID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[CampaignGroup] AS [o_c_cg] ON [o_c].[CampaignGroupID] = [o_c_cg].[CampaignGroupID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Country] AS [o_c_c] ON [o_c].[CountryID] = [o_c_c].[CountryID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Price] AS [o_c_p] ON [o_c].[PriceID] = [o_c_p].[PriceID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Affiliate] AS [o_a] ON [o].[AffiliateID] = [o_a].[AffiliateID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[AffiliateType] AS [o_a_at] ON [o_a].[AffiliateTypeID] = [o_a_at].[AffiliateTypeID] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Price] AS [o_p] ON [o].[PriceID] = [o_p].[PriceID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Currency] AS [o_p_c] ON [o_p].[CurrencyID] = [o_p_c].[CurrencyID] ";
				

        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("o", "customloadmany", "notfound"), "Offer list could not be loaded using custom logic as no items were found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return new List<Offer>();
        }

        SqlQuery query = new SqlQuery(sqlReader);

				OfferTable oTable = new OfferTable(query);
				CampaignTable o_cTable = (this.Depth > 0) ? new CampaignTable(query) : null;
				CampaignGroupTable o_c_cgTable = (this.Depth > 1) ? new CampaignGroupTable(query) : null;
				CountryTable o_c_cTable = (this.Depth > 1) ? new CountryTable(query) : null;
				PriceTable o_c_pTable = (this.Depth > 1) ? new PriceTable(query) : null;
				AffiliateTable o_aTable = (this.Depth > 0) ? new AffiliateTable(query) : null;
				AffiliateTypeTable o_a_atTable = (this.Depth > 1) ? new AffiliateTypeTable(query) : null;
				PriceTable o_pTable = (this.Depth > 0) ? new PriceTable(query) : null;
				CurrencyTable o_p_cTable = (this.Depth > 1) ? new CurrencyTable(query) : null;

        List<Offer> result = new List<Offer>();
        do
        {
          
					CampaignGroup o_c_cgObject = (this.Depth > 1) ? o_c_cgTable.CreateInstance() : null;
					Country o_c_cObject = (this.Depth > 1) ? o_c_cTable.CreateInstance() : null;
					Price o_c_pObject = (this.Depth > 1) ? o_c_pTable.CreateInstance() : null;
					Campaign o_cObject = (this.Depth > 0) ? o_cTable.CreateInstance(o_c_cgObject, o_c_cObject, o_c_pObject) : null;
					AffiliateType o_a_atObject = (this.Depth > 1) ? o_a_atTable.CreateInstance() : null;
					Affiliate o_aObject = (this.Depth > 0) ? o_aTable.CreateInstance(o_a_atObject) : null;
					Currency o_p_cObject = (this.Depth > 1) ? o_p_cTable.CreateInstance() : null;
					Price o_pObject = (this.Depth > 0) ? o_pTable.CreateInstance(o_p_cObject) : null;
					Offer oObject = (this.Depth > -1) ? oTable.CreateInstance(o_cObject, o_aObject, o_pObject) : null;
					result.Add(oObject);

        } while (sqlReader.Read());
        sqlReader.Close();

        return result;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("o", "customloadmany", "exception"), "Offer list could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "Offer", "Exception while loading (custom/many) Offer object from database. See inner exception for details.", ex);
      }
    }
    
    public override int? Insert(ISqlConnectionInfo connection, Offer data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "INSERT INTO [core].[Offer] ([CampaignID],[AffiliateID],[PriceID],[CapValue],[Key],[IsActive]) VALUES(@CampaignID,@AffiliateID,@PriceID,@CapValue,@Key,@IsActive); SELECT SCOPE_IDENTITY();";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@CampaignID", data.Campaign.ID);
				sqlCmd.Parameters.AddWithValue("@AffiliateID", data.Affiliate.ID);
				sqlCmd.Parameters.AddWithValue("@PriceID", data.Price == null ? DBNull.Value : (object)data.Price.ID);
				sqlCmd.Parameters.AddWithValue("@CapValue", data.CapValue.HasValue ? (object)data.CapValue.Value : DBNull.Value).SqlDbType = SqlDbType.Int;
				sqlCmd.Parameters.AddWithValue("@Key", data.Key).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@IsActive", data.IsActive).SqlDbType = SqlDbType.Bit;
				
        object idObj = sqlCmd.ExecuteScalar();
        if (idObj == null || DBNull.Value.Equals(idObj))
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("o", "insert", "noprimarykey"), "Offer could not be inserted or inserted primary key was not returned. Are you missing SELECT SCOPE_IDENTITY();?", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Insert, "Offer", "Exception while inserting Offer object in database.");
        }
        return (int)((decimal)idObj);
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("o", "insert", "exception"), "Offer could not be inserted. See exception for details.", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Insert, "Offer", "Exception while inserting Offer object in database. See inner exception for details.", ex);
      }
    }

    public override bool Update(ISqlConnectionInfo connection, Offer data)
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
        sqlCmdText = "UPDATE [core].[Offer] SET " +
												"[CampaignID] = @CampaignID, " + 
												"[AffiliateID] = @AffiliateID, " + 
												"[PriceID] = @PriceID, " + 
												"[CapValue] = @CapValue, " + 
												"[Key] = @Key, " + 
												"[IsActive] = @IsActive, " + 
												"[Updated] = GETDATE() " + 
											"WHERE [OfferID] = @OfferID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@CampaignID", data.Campaign.ID);
				sqlCmd.Parameters.AddWithValue("@AffiliateID", data.Affiliate.ID);
				sqlCmd.Parameters.AddWithValue("@PriceID", data.Price == null ? DBNull.Value : (object)data.Price.ID);
				sqlCmd.Parameters.AddWithValue("@CapValue", data.CapValue.HasValue ? (object)data.CapValue.Value : DBNull.Value).SqlDbType = SqlDbType.Int;
				sqlCmd.Parameters.AddWithValue("@Key", data.Key).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@IsActive", data.IsActive).SqlDbType = SqlDbType.Bit;
				sqlCmd.Parameters.AddWithValue("@Updated", data.Updated).SqlDbType = SqlDbType.DateTime2;
				sqlCmd.Parameters.AddWithValue("@OfferID", data.ID);

        int rowCount = sqlCmd.ExecuteNonQuery();
        if (rowCount < 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("o", "update", "norecord"), "Offer could not be updated as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "Offer", "Exception while updating Offer object in database. No record found for this id.");
        }
        else if (rowCount > 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("o", "update", "morerecords"), "Offer was updated but there was more than one record affected.", sqlCmdText, this, connection, data);
          if (this.Logger.IsFatalEnabled)
            this.Logger.Fatal(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "Offer", "Exception while updating Offer object in database. More than one record found for this statement (update statement where clause broken?!).");
        }
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("o", "update", "exception"), "Offer could not be updated. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Update, "Offer", "Exception while updating Offer object in database. See inner exception for details.", ex);
      }
    }

    public override bool Delete(ISqlConnectionInfo connection, Offer data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText =  "DELETE FROM [core].[Offer] WHERE OfferID = @OfferID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@OfferID", data.ID);

        int success = sqlCmd.ExecuteNonQuery();

        if (success == -1)        
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("o", "delete", "norecord"), "Offer could not be deleted as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Delete, "Offer", "Exception while deleting Offer object from database. No such record found.");
        }
        
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("o", "delete", "exception"), "Offer could not be deleted. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Delete, "Offer", "Exception while deleting Offer object from database. See inner exception for details.", ex);
      }
    }
  }
}

