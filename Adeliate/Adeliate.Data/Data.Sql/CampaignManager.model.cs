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
  [DataManager(typeof(Campaign))] 
  public partial class CampaignManager : Adlite.Data.Sql.SqlManagerBase<Campaign>, ICampaignManager
  {
    public override DatabaseType Type
    {
      get { return DatabaseType.Adlite; }
    }

    protected override Campaign LoadInternal(ISqlConnectionInfo connection, int id)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "SELECT " + 
							CampaignTable.GetColumnNames("[c]") + 
							(this.Depth > 0 ? "," + CampaignGroupTable.GetColumnNames("[c_cg]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignGroupTable.GetColumnNames("[c_cg_cg]") : string.Empty) + 
							(this.Depth > 0 ? "," + CountryTable.GetColumnNames("[c_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + LanguageTable.GetColumnNames("[c_c_l]") : string.Empty) + 
							(this.Depth > 1 ? "," + CurrencyTable.GetColumnNames("[c_c_c]") : string.Empty) + 
							(this.Depth > 0 ? "," + PriceTable.GetColumnNames("[c_p]") : string.Empty) + 
							(this.Depth > 1 ? "," + CurrencyTable.GetColumnNames("[c_p_c]") : string.Empty) + 
					" FROM [core].[Campaign] AS [c] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[CampaignGroup] AS [c_cg] ON [c].[CampaignGroupID] = [c_cg].[CampaignGroupID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[CampaignGroup] AS [c_cg_cg] ON [c_cg].[FallbackGroupID] = [c_cg_cg].[CampaignGroupID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Country] AS [c_c] ON [c].[CountryID] = [c_c].[CountryID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Language] AS [c_c_l] ON [c_c].[LanguageID] = [c_c_l].[LanguageID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Currency] AS [c_c_c] ON [c_c].[CurrencyID] = [c_c_c].[CurrencyID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Price] AS [c_p] ON [c].[PriceID] = [c_p].[PriceID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Currency] AS [c_p_c] ON [c_p].[CurrencyID] = [c_p_c].[CurrencyID] ";
				sqlCmdText += "WHERE [c].[CampaignID] = @CampaignID;";

        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@CampaignID", id);
        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "loadinternal", "notfound"), "Campaign could not be loaded by id as it was not found.", sqlCmdText, this, connection, id);
          if (this.Logger.IsWarnEnabled)
            this.Logger.Warn(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				CampaignTable cTable = new CampaignTable(query);
				CampaignGroupTable c_cgTable = (this.Depth > 0) ? new CampaignGroupTable(query) : null;
				CampaignGroupTable c_cg_cgTable = (this.Depth > 1) ? new CampaignGroupTable(query) : null;
				CountryTable c_cTable = (this.Depth > 0) ? new CountryTable(query) : null;
				LanguageTable c_c_lTable = (this.Depth > 1) ? new LanguageTable(query) : null;
				CurrencyTable c_c_cTable = (this.Depth > 1) ? new CurrencyTable(query) : null;
				PriceTable c_pTable = (this.Depth > 0) ? new PriceTable(query) : null;
				CurrencyTable c_p_cTable = (this.Depth > 1) ? new CurrencyTable(query) : null;

        
				CampaignGroup c_cg_cgObject = (this.Depth > 1) ? c_cg_cgTable.CreateInstance() : null;
				CampaignGroup c_cgObject = (this.Depth > 0) ? c_cgTable.CreateInstance(c_cg_cgObject) : null;
				Language c_c_lObject = (this.Depth > 1) ? c_c_lTable.CreateInstance() : null;
				Currency c_c_cObject = (this.Depth > 1) ? c_c_cTable.CreateInstance() : null;
				Country c_cObject = (this.Depth > 0) ? c_cTable.CreateInstance(c_c_lObject, c_c_cObject) : null;
				Currency c_p_cObject = (this.Depth > 1) ? c_p_cTable.CreateInstance() : null;
				Price c_pObject = (this.Depth > 0) ? c_pTable.CreateInstance(c_p_cObject) : null;
				Campaign cObject = cTable.CreateInstance(c_cgObject, c_cObject, c_pObject);
				sqlReader.Close();

				return cObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "loadinternal", "exception"), "Campaign could not be loaded by id. See exception for details.", sqlCmdText, ex, this, connection, id);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "Campaign", "Exception while loading Campaign object from database. See inner exception for details.", ex);
      }
    }

    public Campaign Load(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							CampaignTable.GetColumnNames("[c]") + 
							(this.Depth > 0 ? "," + CampaignGroupTable.GetColumnNames("[c_cg]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignGroupTable.GetColumnNames("[c_cg_cg]") : string.Empty) + 
							(this.Depth > 0 ? "," + CountryTable.GetColumnNames("[c_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + LanguageTable.GetColumnNames("[c_c_l]") : string.Empty) + 
							(this.Depth > 1 ? "," + CurrencyTable.GetColumnNames("[c_c_c]") : string.Empty) + 
							(this.Depth > 0 ? "," + PriceTable.GetColumnNames("[c_p]") : string.Empty) + 
							(this.Depth > 1 ? "," + CurrencyTable.GetColumnNames("[c_p_c]") : string.Empty) +  
					" FROM [core].[Campaign] AS [c] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[CampaignGroup] AS [c_cg] ON [c].[CampaignGroupID] = [c_cg].[CampaignGroupID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[CampaignGroup] AS [c_cg_cg] ON [c_cg].[FallbackGroupID] = [c_cg_cg].[CampaignGroupID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Country] AS [c_c] ON [c].[CountryID] = [c_c].[CountryID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Language] AS [c_c_l] ON [c_c].[LanguageID] = [c_c_l].[LanguageID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Currency] AS [c_c_c] ON [c_c].[CurrencyID] = [c_c_c].[CurrencyID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Price] AS [c_p] ON [c].[PriceID] = [c_p].[PriceID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Currency] AS [c_p_c] ON [c_p].[CurrencyID] = [c_p_c].[CurrencyID] ";
				

        parameters.Top = 1;
        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "customload", "notfound"), "Campaign could not be loaded using custom logic as it was not found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				CampaignTable cTable = new CampaignTable(query);
				CampaignGroupTable c_cgTable = (this.Depth > 0) ? new CampaignGroupTable(query) : null;
				CampaignGroupTable c_cg_cgTable = (this.Depth > 1) ? new CampaignGroupTable(query) : null;
				CountryTable c_cTable = (this.Depth > 0) ? new CountryTable(query) : null;
				LanguageTable c_c_lTable = (this.Depth > 1) ? new LanguageTable(query) : null;
				CurrencyTable c_c_cTable = (this.Depth > 1) ? new CurrencyTable(query) : null;
				PriceTable c_pTable = (this.Depth > 0) ? new PriceTable(query) : null;
				CurrencyTable c_p_cTable = (this.Depth > 1) ? new CurrencyTable(query) : null;

        
				CampaignGroup c_cg_cgObject = (this.Depth > 1) ? c_cg_cgTable.CreateInstance() : null;
				CampaignGroup c_cgObject = (this.Depth > 0) ? c_cgTable.CreateInstance(c_cg_cgObject) : null;
				Language c_c_lObject = (this.Depth > 1) ? c_c_lTable.CreateInstance() : null;
				Currency c_c_cObject = (this.Depth > 1) ? c_c_cTable.CreateInstance() : null;
				Country c_cObject = (this.Depth > 0) ? c_cTable.CreateInstance(c_c_lObject, c_c_cObject) : null;
				Currency c_p_cObject = (this.Depth > 1) ? c_p_cTable.CreateInstance() : null;
				Price c_pObject = (this.Depth > 0) ? c_pTable.CreateInstance(c_p_cObject) : null;
				Campaign cObject = cTable.CreateInstance(c_cgObject, c_cObject, c_pObject);
				sqlReader.Close();

				return cObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "customload", "exception"), "Campaign could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "Campaign", "Exception while loading (custom/single) Campaign object from database. See inner exception for details.", ex);
      }
    }

    public List<Campaign> LoadMany(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							CampaignTable.GetColumnNames("[c]") + 
							(this.Depth > 0 ? "," + CampaignGroupTable.GetColumnNames("[c_cg]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignGroupTable.GetColumnNames("[c_cg_cg]") : string.Empty) + 
							(this.Depth > 0 ? "," + CountryTable.GetColumnNames("[c_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + LanguageTable.GetColumnNames("[c_c_l]") : string.Empty) + 
							(this.Depth > 1 ? "," + CurrencyTable.GetColumnNames("[c_c_c]") : string.Empty) + 
							(this.Depth > 0 ? "," + PriceTable.GetColumnNames("[c_p]") : string.Empty) + 
							(this.Depth > 1 ? "," + CurrencyTable.GetColumnNames("[c_p_c]") : string.Empty) +  
					" FROM [core].[Campaign] AS [c] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[CampaignGroup] AS [c_cg] ON [c].[CampaignGroupID] = [c_cg].[CampaignGroupID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[CampaignGroup] AS [c_cg_cg] ON [c_cg].[FallbackGroupID] = [c_cg_cg].[CampaignGroupID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Country] AS [c_c] ON [c].[CountryID] = [c_c].[CountryID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Language] AS [c_c_l] ON [c_c].[LanguageID] = [c_c_l].[LanguageID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Currency] AS [c_c_c] ON [c_c].[CurrencyID] = [c_c_c].[CurrencyID] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Price] AS [c_p] ON [c].[PriceID] = [c_p].[PriceID] ";
				if (this.Depth > 1)
				  sqlCmdText += "INNER  JOIN [core].[Currency] AS [c_p_c] ON [c_p].[CurrencyID] = [c_p_c].[CurrencyID] ";
				

        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "customloadmany", "notfound"), "Campaign list could not be loaded using custom logic as no items were found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return new List<Campaign>();
        }

        SqlQuery query = new SqlQuery(sqlReader);

				CampaignTable cTable = new CampaignTable(query);
				CampaignGroupTable c_cgTable = (this.Depth > 0) ? new CampaignGroupTable(query) : null;
				CampaignGroupTable c_cg_cgTable = (this.Depth > 1) ? new CampaignGroupTable(query) : null;
				CountryTable c_cTable = (this.Depth > 0) ? new CountryTable(query) : null;
				LanguageTable c_c_lTable = (this.Depth > 1) ? new LanguageTable(query) : null;
				CurrencyTable c_c_cTable = (this.Depth > 1) ? new CurrencyTable(query) : null;
				PriceTable c_pTable = (this.Depth > 0) ? new PriceTable(query) : null;
				CurrencyTable c_p_cTable = (this.Depth > 1) ? new CurrencyTable(query) : null;

        List<Campaign> result = new List<Campaign>();
        do
        {
          
					CampaignGroup c_cg_cgObject = (this.Depth > 1) ? c_cg_cgTable.CreateInstance() : null;
					CampaignGroup c_cgObject = (this.Depth > 0) ? c_cgTable.CreateInstance(c_cg_cgObject) : null;
					Language c_c_lObject = (this.Depth > 1) ? c_c_lTable.CreateInstance() : null;
					Currency c_c_cObject = (this.Depth > 1) ? c_c_cTable.CreateInstance() : null;
					Country c_cObject = (this.Depth > 0) ? c_cTable.CreateInstance(c_c_lObject, c_c_cObject) : null;
					Currency c_p_cObject = (this.Depth > 1) ? c_p_cTable.CreateInstance() : null;
					Price c_pObject = (this.Depth > 0) ? c_pTable.CreateInstance(c_p_cObject) : null;
					Campaign cObject = (this.Depth > -1) ? cTable.CreateInstance(c_cgObject, c_cObject, c_pObject) : null;
					result.Add(cObject);

        } while (sqlReader.Read());
        sqlReader.Close();

        return result;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "customloadmany", "exception"), "Campaign list could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "Campaign", "Exception while loading (custom/many) Campaign object from database. See inner exception for details.", ex);
      }
    }
    
    public override int? Insert(ISqlConnectionInfo connection, Campaign data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "INSERT INTO [core].[Campaign] ([CampaignGroupID],[CampaignContentTypeID],[CountryID],[PriceID],[Name],[CapValue],[Link],[FallbackLink],[Description],[Device],[Browser],[IPRanges],[RestrictCountryTraffic],[RestrictMobileTraffic]) VALUES(@CampaignGroupID,@CampaignContentTypeID,@CountryID,@PriceID,@Name,@CapValue,@Link,@FallbackLink,@Description,@Device,@Browser,@IPRanges,@RestrictCountryTraffic,@RestrictMobileTraffic); SELECT SCOPE_IDENTITY();";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@CampaignGroupID", data.CampaignGroup.ID);
				sqlCmd.Parameters.AddWithValue("@CampaignContentTypeID", (int)data.CampaignContentType);
				sqlCmd.Parameters.AddWithValue("@CountryID", data.Country.ID);
				sqlCmd.Parameters.AddWithValue("@PriceID", data.Price.ID);
				sqlCmd.Parameters.AddWithValue("@Name", data.Name).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@CapValue", data.CapValue).SqlDbType = SqlDbType.Int;
				sqlCmd.Parameters.AddWithValue("@Link", data.Link).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@FallbackLink", data.FallbackLink).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@Description", !string.IsNullOrEmpty(data.Description) ? (object)data.Description : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@Device", data.Device).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@Browser", data.Browser).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@IPRanges", !string.IsNullOrEmpty(data.IPRanges) ? (object)data.IPRanges : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@RestrictCountryTraffic", data.RestrictCountryTraffic).SqlDbType = SqlDbType.Bit;
				sqlCmd.Parameters.AddWithValue("@RestrictMobileTraffic", data.RestrictMobileTraffic).SqlDbType = SqlDbType.Bit;
				
        object idObj = sqlCmd.ExecuteScalar();
        if (idObj == null || DBNull.Value.Equals(idObj))
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "insert", "noprimarykey"), "Campaign could not be inserted or inserted primary key was not returned. Are you missing SELECT SCOPE_IDENTITY();?", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Insert, "Campaign", "Exception while inserting Campaign object in database.");
        }
        return (int)((decimal)idObj);
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "insert", "exception"), "Campaign could not be inserted. See exception for details.", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Insert, "Campaign", "Exception while inserting Campaign object in database. See inner exception for details.", ex);
      }
    }

    public override bool Update(ISqlConnectionInfo connection, Campaign data)
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
        sqlCmdText = "UPDATE [core].[Campaign] SET " +
												"[CampaignGroupID] = @CampaignGroupID, " + 
												"[CampaignContentTypeID] = @CampaignContentTypeID, " + 
												"[CountryID] = @CountryID, " + 
												"[PriceID] = @PriceID, " + 
												"[Name] = @Name, " + 
												"[CapValue] = @CapValue, " + 
												"[Link] = @Link, " + 
												"[FallbackLink] = @FallbackLink, " + 
												"[Description] = @Description, " + 
												"[Device] = @Device, " + 
												"[Browser] = @Browser, " + 
												"[IPRanges] = @IPRanges, " + 
												"[RestrictCountryTraffic] = @RestrictCountryTraffic, " + 
												"[RestrictMobileTraffic] = @RestrictMobileTraffic, " + 
												"[Updated] = GETDATE() " + 
											"WHERE [CampaignID] = @CampaignID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@CampaignGroupID", data.CampaignGroup.ID);
				sqlCmd.Parameters.AddWithValue("@CampaignContentTypeID", (int)data.CampaignContentType);
				sqlCmd.Parameters.AddWithValue("@CountryID", data.Country.ID);
				sqlCmd.Parameters.AddWithValue("@PriceID", data.Price.ID);
				sqlCmd.Parameters.AddWithValue("@Name", data.Name).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@CapValue", data.CapValue).SqlDbType = SqlDbType.Int;
				sqlCmd.Parameters.AddWithValue("@Link", data.Link).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@FallbackLink", data.FallbackLink).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@Description", !string.IsNullOrEmpty(data.Description) ? (object)data.Description : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@Device", data.Device).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@Browser", data.Browser).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@IPRanges", !string.IsNullOrEmpty(data.IPRanges) ? (object)data.IPRanges : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@RestrictCountryTraffic", data.RestrictCountryTraffic).SqlDbType = SqlDbType.Bit;
				sqlCmd.Parameters.AddWithValue("@RestrictMobileTraffic", data.RestrictMobileTraffic).SqlDbType = SqlDbType.Bit;
				sqlCmd.Parameters.AddWithValue("@Updated", data.Updated).SqlDbType = SqlDbType.DateTime2;
				sqlCmd.Parameters.AddWithValue("@CampaignID", data.ID);

        int rowCount = sqlCmd.ExecuteNonQuery();
        if (rowCount < 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "update", "norecord"), "Campaign could not be updated as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "Campaign", "Exception while updating Campaign object in database. No record found for this id.");
        }
        else if (rowCount > 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "update", "morerecords"), "Campaign was updated but there was more than one record affected.", sqlCmdText, this, connection, data);
          if (this.Logger.IsFatalEnabled)
            this.Logger.Fatal(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "Campaign", "Exception while updating Campaign object in database. More than one record found for this statement (update statement where clause broken?!).");
        }
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "update", "exception"), "Campaign could not be updated. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Update, "Campaign", "Exception while updating Campaign object in database. See inner exception for details.", ex);
      }
    }

    public override bool Delete(ISqlConnectionInfo connection, Campaign data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText =  "DELETE FROM [core].[Campaign] WHERE CampaignID = @CampaignID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@CampaignID", data.ID);

        int success = sqlCmd.ExecuteNonQuery();

        if (success == -1)        
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "delete", "norecord"), "Campaign could not be deleted as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Delete, "Campaign", "Exception while deleting Campaign object from database. No such record found.");
        }
        
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("c", "delete", "exception"), "Campaign could not be deleted. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Delete, "Campaign", "Exception while deleting Campaign object from database. See inner exception for details.", ex);
      }
    }
  }
}

