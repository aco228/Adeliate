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
  [DataManager(typeof(ClickInformation))] 
  public partial class ClickInformationManager : Adlite.Data.Sql.SqlManagerBase<ClickInformation>, IClickInformationManager
  {
    public override DatabaseType Type
    {
      get { return DatabaseType.Adlite; }
    }

    protected override ClickInformation LoadInternal(ISqlConnectionInfo connection, int id)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "SELECT " + 
							ClickInformationTable.GetColumnNames("[ci]") + 
							(this.Depth > 0 ? "," + ClickTable.GetColumnNames("[ci_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + OfferTable.GetColumnNames("[ci_c_o]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTable.GetColumnNames("[ci_c_a]") : string.Empty) + 
							(this.Depth > 0 ? "," + CampaignTable.GetColumnNames("[c1]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignGroupTable.GetColumnNames("[c1_cg]") : string.Empty) + 
							(this.Depth > 1 ? "," + CountryTable.GetColumnNames("[c1_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + PriceTable.GetColumnNames("[c1_p]") : string.Empty) + 
							(this.Depth > 0 ? "," + CountryTable.GetColumnNames("[c2]") : string.Empty) + 
							(this.Depth > 1 ? "," + LanguageTable.GetColumnNames("[c2_l]") : string.Empty) + 
							(this.Depth > 1 ? "," + CurrencyTable.GetColumnNames("[c2_c]") : string.Empty) + 
							(this.Depth > 0 ? "," + MobileOperatorTable.GetColumnNames("[ci_mo]") : string.Empty) + 
							(this.Depth > 1 ? "," + CountryTable.GetColumnNames("[ci_mo_c]") : string.Empty) + 
							(this.Depth > 0 ? "," + PriceTable.GetColumnNames("[ci_p]") : string.Empty) + 
							(this.Depth > 1 ? "," + CurrencyTable.GetColumnNames("[ci_p_c]") : string.Empty) + 
							(this.Depth > 0 ? "," + DynamicPlatformTable.GetColumnNames("[ci_dp]") : string.Empty) + 
							(this.Depth > 0 ? "," + DynamicBrowserTable.GetColumnNames("[ci_db]") : string.Empty) + 
					" FROM [core].[ClickInformation] AS [ci] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Click] AS [ci_c] ON [ci].[ClickID] = [ci_c].[ClickID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Offer] AS [ci_c_o] ON [ci_c].[OfferID] = [ci_c_o].[OfferID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Affiliate] AS [ci_c_a] ON [ci_c].[AffiliateID] = [ci_c_a].[AffiliateID] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Campaign] AS [c1] ON [ci].[CampaignID] = [c1].[CampaignID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[CampaignGroup] AS [c1_cg] ON [c1].[CampaignGroupID] = [c1_cg].[CampaignGroupID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Country] AS [c1_c] ON [c1].[CountryID] = [c1_c].[CountryID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Price] AS [c1_p] ON [c1].[PriceID] = [c1_p].[PriceID] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Country] AS [c2] ON [ci].[CountryID] = [c2].[CountryID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Language] AS [c2_l] ON [c2].[LanguageID] = [c2_l].[LanguageID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Currency] AS [c2_c] ON [c2].[CurrencyID] = [c2_c].[CurrencyID] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[MobileOperator] AS [ci_mo] ON [ci].[MobileOperatorID] = [ci_mo].[MobileOperatorID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Country] AS [ci_mo_c] ON [ci_mo].[CountryID] = [ci_mo_c].[CountryID] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Price] AS [ci_p] ON [ci].[PriceID] = [ci_p].[PriceID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Currency] AS [ci_p_c] ON [ci_p].[CurrencyID] = [ci_p_c].[CurrencyID] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[DynamicPlatform] AS [ci_dp] ON [ci].[DynamicPlatformID] = [ci_dp].[DynamicPlatformID] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[DynamicBrowser] AS [ci_db] ON [ci].[DynamicBrowserID] = [ci_db].[DynamicBrowserID] ";
				sqlCmdText += "WHERE [ci].[ClickInformationID] = @ClickInformationID;";

        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@ClickInformationID", id);
        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("ci", "loadinternal", "notfound"), "ClickInformation could not be loaded by id as it was not found.", sqlCmdText, this, connection, id);
          if (this.Logger.IsWarnEnabled)
            this.Logger.Warn(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				ClickInformationTable ciTable = new ClickInformationTable(query);
				ClickTable ci_cTable = (this.Depth > 0) ? new ClickTable(query) : null;
				OfferTable ci_c_oTable = (this.Depth > 1) ? new OfferTable(query) : null;
				AffiliateTable ci_c_aTable = (this.Depth > 1) ? new AffiliateTable(query) : null;
				CampaignTable c1Table = (this.Depth > 0) ? new CampaignTable(query) : null;
				CampaignGroupTable c1_cgTable = (this.Depth > 1) ? new CampaignGroupTable(query) : null;
				CountryTable c1_cTable = (this.Depth > 1) ? new CountryTable(query) : null;
				PriceTable c1_pTable = (this.Depth > 1) ? new PriceTable(query) : null;
				CountryTable c2Table = (this.Depth > 0) ? new CountryTable(query) : null;
				LanguageTable c2_lTable = (this.Depth > 1) ? new LanguageTable(query) : null;
				CurrencyTable c2_cTable = (this.Depth > 1) ? new CurrencyTable(query) : null;
				MobileOperatorTable ci_moTable = (this.Depth > 0) ? new MobileOperatorTable(query) : null;
				CountryTable ci_mo_cTable = (this.Depth > 1) ? new CountryTable(query) : null;
				PriceTable ci_pTable = (this.Depth > 0) ? new PriceTable(query) : null;
				CurrencyTable ci_p_cTable = (this.Depth > 1) ? new CurrencyTable(query) : null;
				DynamicPlatformTable ci_dpTable = (this.Depth > 0) ? new DynamicPlatformTable(query) : null;
				DynamicBrowserTable ci_dbTable = (this.Depth > 0) ? new DynamicBrowserTable(query) : null;

        
				Offer ci_c_oObject = (this.Depth > 1) ? ci_c_oTable.CreateInstance() : null;
				Affiliate ci_c_aObject = (this.Depth > 1) ? ci_c_aTable.CreateInstance() : null;
				Click ci_cObject = (this.Depth > 0) ? ci_cTable.CreateInstance(ci_c_oObject, ci_c_aObject) : null;
				CampaignGroup c1_cgObject = (this.Depth > 1) ? c1_cgTable.CreateInstance() : null;
				Country c1_cObject = (this.Depth > 1) ? c1_cTable.CreateInstance() : null;
				Price c1_pObject = (this.Depth > 1) ? c1_pTable.CreateInstance() : null;
				Campaign c1Object = (this.Depth > 0) ? c1Table.CreateInstance(c1_cgObject, c1_cObject, c1_pObject) : null;
				Language c2_lObject = (this.Depth > 1) ? c2_lTable.CreateInstance() : null;
				Currency c2_cObject = (this.Depth > 1) ? c2_cTable.CreateInstance() : null;
				Country c2Object = (this.Depth > 0) ? c2Table.CreateInstance(c2_lObject, c2_cObject) : null;
				Country ci_mo_cObject = (this.Depth > 1) ? ci_mo_cTable.CreateInstance() : null;
				MobileOperator ci_moObject = (this.Depth > 0) ? ci_moTable.CreateInstance(ci_mo_cObject) : null;
				Currency ci_p_cObject = (this.Depth > 1) ? ci_p_cTable.CreateInstance() : null;
				Price ci_pObject = (this.Depth > 0) ? ci_pTable.CreateInstance(ci_p_cObject) : null;
				DynamicPlatform ci_dpObject = (this.Depth > 0) ? ci_dpTable.CreateInstance() : null;
				DynamicBrowser ci_dbObject = (this.Depth > 0) ? ci_dbTable.CreateInstance() : null;
				ClickInformation ciObject = ciTable.CreateInstance(ci_cObject, c1Object, c2Object, ci_moObject, ci_pObject, ci_dpObject, ci_dbObject);
				sqlReader.Close();

				return ciObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("ci", "loadinternal", "exception"), "ClickInformation could not be loaded by id. See exception for details.", sqlCmdText, ex, this, connection, id);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "ClickInformation", "Exception while loading ClickInformation object from database. See inner exception for details.", ex);
      }
    }

    public ClickInformation Load(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							ClickInformationTable.GetColumnNames("[ci]") + 
							(this.Depth > 0 ? "," + ClickTable.GetColumnNames("[ci_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + OfferTable.GetColumnNames("[ci_c_o]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTable.GetColumnNames("[ci_c_a]") : string.Empty) + 
							(this.Depth > 0 ? "," + CampaignTable.GetColumnNames("[c1]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignGroupTable.GetColumnNames("[c1_cg]") : string.Empty) + 
							(this.Depth > 1 ? "," + CountryTable.GetColumnNames("[c1_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + PriceTable.GetColumnNames("[c1_p]") : string.Empty) + 
							(this.Depth > 0 ? "," + CountryTable.GetColumnNames("[c2]") : string.Empty) + 
							(this.Depth > 1 ? "," + LanguageTable.GetColumnNames("[c2_l]") : string.Empty) + 
							(this.Depth > 1 ? "," + CurrencyTable.GetColumnNames("[c2_c]") : string.Empty) + 
							(this.Depth > 0 ? "," + MobileOperatorTable.GetColumnNames("[ci_mo]") : string.Empty) + 
							(this.Depth > 1 ? "," + CountryTable.GetColumnNames("[ci_mo_c]") : string.Empty) + 
							(this.Depth > 0 ? "," + PriceTable.GetColumnNames("[ci_p]") : string.Empty) + 
							(this.Depth > 1 ? "," + CurrencyTable.GetColumnNames("[ci_p_c]") : string.Empty) + 
							(this.Depth > 0 ? "," + DynamicPlatformTable.GetColumnNames("[ci_dp]") : string.Empty) + 
							(this.Depth > 0 ? "," + DynamicBrowserTable.GetColumnNames("[ci_db]") : string.Empty) +  
					" FROM [core].[ClickInformation] AS [ci] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Click] AS [ci_c] ON [ci].[ClickID] = [ci_c].[ClickID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Offer] AS [ci_c_o] ON [ci_c].[OfferID] = [ci_c_o].[OfferID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Affiliate] AS [ci_c_a] ON [ci_c].[AffiliateID] = [ci_c_a].[AffiliateID] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Campaign] AS [c1] ON [ci].[CampaignID] = [c1].[CampaignID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[CampaignGroup] AS [c1_cg] ON [c1].[CampaignGroupID] = [c1_cg].[CampaignGroupID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Country] AS [c1_c] ON [c1].[CountryID] = [c1_c].[CountryID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Price] AS [c1_p] ON [c1].[PriceID] = [c1_p].[PriceID] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Country] AS [c2] ON [ci].[CountryID] = [c2].[CountryID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Language] AS [c2_l] ON [c2].[LanguageID] = [c2_l].[LanguageID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Currency] AS [c2_c] ON [c2].[CurrencyID] = [c2_c].[CurrencyID] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[MobileOperator] AS [ci_mo] ON [ci].[MobileOperatorID] = [ci_mo].[MobileOperatorID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Country] AS [ci_mo_c] ON [ci_mo].[CountryID] = [ci_mo_c].[CountryID] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Price] AS [ci_p] ON [ci].[PriceID] = [ci_p].[PriceID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Currency] AS [ci_p_c] ON [ci_p].[CurrencyID] = [ci_p_c].[CurrencyID] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[DynamicPlatform] AS [ci_dp] ON [ci].[DynamicPlatformID] = [ci_dp].[DynamicPlatformID] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[DynamicBrowser] AS [ci_db] ON [ci].[DynamicBrowserID] = [ci_db].[DynamicBrowserID] ";
				

        parameters.Top = 1;
        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("ci", "customload", "notfound"), "ClickInformation could not be loaded using custom logic as it was not found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return null;
        }

        SqlQuery query = new SqlQuery(sqlReader);

				ClickInformationTable ciTable = new ClickInformationTable(query);
				ClickTable ci_cTable = (this.Depth > 0) ? new ClickTable(query) : null;
				OfferTable ci_c_oTable = (this.Depth > 1) ? new OfferTable(query) : null;
				AffiliateTable ci_c_aTable = (this.Depth > 1) ? new AffiliateTable(query) : null;
				CampaignTable c1Table = (this.Depth > 0) ? new CampaignTable(query) : null;
				CampaignGroupTable c1_cgTable = (this.Depth > 1) ? new CampaignGroupTable(query) : null;
				CountryTable c1_cTable = (this.Depth > 1) ? new CountryTable(query) : null;
				PriceTable c1_pTable = (this.Depth > 1) ? new PriceTable(query) : null;
				CountryTable c2Table = (this.Depth > 0) ? new CountryTable(query) : null;
				LanguageTable c2_lTable = (this.Depth > 1) ? new LanguageTable(query) : null;
				CurrencyTable c2_cTable = (this.Depth > 1) ? new CurrencyTable(query) : null;
				MobileOperatorTable ci_moTable = (this.Depth > 0) ? new MobileOperatorTable(query) : null;
				CountryTable ci_mo_cTable = (this.Depth > 1) ? new CountryTable(query) : null;
				PriceTable ci_pTable = (this.Depth > 0) ? new PriceTable(query) : null;
				CurrencyTable ci_p_cTable = (this.Depth > 1) ? new CurrencyTable(query) : null;
				DynamicPlatformTable ci_dpTable = (this.Depth > 0) ? new DynamicPlatformTable(query) : null;
				DynamicBrowserTable ci_dbTable = (this.Depth > 0) ? new DynamicBrowserTable(query) : null;

        
				Offer ci_c_oObject = (this.Depth > 1) ? ci_c_oTable.CreateInstance() : null;
				Affiliate ci_c_aObject = (this.Depth > 1) ? ci_c_aTable.CreateInstance() : null;
				Click ci_cObject = (this.Depth > 0) ? ci_cTable.CreateInstance(ci_c_oObject, ci_c_aObject) : null;
				CampaignGroup c1_cgObject = (this.Depth > 1) ? c1_cgTable.CreateInstance() : null;
				Country c1_cObject = (this.Depth > 1) ? c1_cTable.CreateInstance() : null;
				Price c1_pObject = (this.Depth > 1) ? c1_pTable.CreateInstance() : null;
				Campaign c1Object = (this.Depth > 0) ? c1Table.CreateInstance(c1_cgObject, c1_cObject, c1_pObject) : null;
				Language c2_lObject = (this.Depth > 1) ? c2_lTable.CreateInstance() : null;
				Currency c2_cObject = (this.Depth > 1) ? c2_cTable.CreateInstance() : null;
				Country c2Object = (this.Depth > 0) ? c2Table.CreateInstance(c2_lObject, c2_cObject) : null;
				Country ci_mo_cObject = (this.Depth > 1) ? ci_mo_cTable.CreateInstance() : null;
				MobileOperator ci_moObject = (this.Depth > 0) ? ci_moTable.CreateInstance(ci_mo_cObject) : null;
				Currency ci_p_cObject = (this.Depth > 1) ? ci_p_cTable.CreateInstance() : null;
				Price ci_pObject = (this.Depth > 0) ? ci_pTable.CreateInstance(ci_p_cObject) : null;
				DynamicPlatform ci_dpObject = (this.Depth > 0) ? ci_dpTable.CreateInstance() : null;
				DynamicBrowser ci_dbObject = (this.Depth > 0) ? ci_dbTable.CreateInstance() : null;
				ClickInformation ciObject = ciTable.CreateInstance(ci_cObject, c1Object, c2Object, ci_moObject, ci_pObject, ci_dpObject, ci_dbObject);
				sqlReader.Close();

				return ciObject;

      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("ci", "customload", "exception"), "ClickInformation could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "ClickInformation", "Exception while loading (custom/single) ClickInformation object from database. See inner exception for details.", ex);
      }
    }

    public List<ClickInformation> LoadMany(ISqlConnectionInfo connection, SqlQueryParameters parameters)
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
							ClickInformationTable.GetColumnNames("[ci]") + 
							(this.Depth > 0 ? "," + ClickTable.GetColumnNames("[ci_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + OfferTable.GetColumnNames("[ci_c_o]") : string.Empty) + 
							(this.Depth > 1 ? "," + AffiliateTable.GetColumnNames("[ci_c_a]") : string.Empty) + 
							(this.Depth > 0 ? "," + CampaignTable.GetColumnNames("[c1]") : string.Empty) + 
							(this.Depth > 1 ? "," + CampaignGroupTable.GetColumnNames("[c1_cg]") : string.Empty) + 
							(this.Depth > 1 ? "," + CountryTable.GetColumnNames("[c1_c]") : string.Empty) + 
							(this.Depth > 1 ? "," + PriceTable.GetColumnNames("[c1_p]") : string.Empty) + 
							(this.Depth > 0 ? "," + CountryTable.GetColumnNames("[c2]") : string.Empty) + 
							(this.Depth > 1 ? "," + LanguageTable.GetColumnNames("[c2_l]") : string.Empty) + 
							(this.Depth > 1 ? "," + CurrencyTable.GetColumnNames("[c2_c]") : string.Empty) + 
							(this.Depth > 0 ? "," + MobileOperatorTable.GetColumnNames("[ci_mo]") : string.Empty) + 
							(this.Depth > 1 ? "," + CountryTable.GetColumnNames("[ci_mo_c]") : string.Empty) + 
							(this.Depth > 0 ? "," + PriceTable.GetColumnNames("[ci_p]") : string.Empty) + 
							(this.Depth > 1 ? "," + CurrencyTable.GetColumnNames("[ci_p_c]") : string.Empty) + 
							(this.Depth > 0 ? "," + DynamicPlatformTable.GetColumnNames("[ci_dp]") : string.Empty) + 
							(this.Depth > 0 ? "," + DynamicBrowserTable.GetColumnNames("[ci_db]") : string.Empty) +  
					" FROM [core].[ClickInformation] AS [ci] ";
				if (this.Depth > 0)
				  sqlCmdText += "INNER  JOIN [core].[Click] AS [ci_c] ON [ci].[ClickID] = [ci_c].[ClickID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Offer] AS [ci_c_o] ON [ci_c].[OfferID] = [ci_c_o].[OfferID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Affiliate] AS [ci_c_a] ON [ci_c].[AffiliateID] = [ci_c_a].[AffiliateID] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Campaign] AS [c1] ON [ci].[CampaignID] = [c1].[CampaignID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[CampaignGroup] AS [c1_cg] ON [c1].[CampaignGroupID] = [c1_cg].[CampaignGroupID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Country] AS [c1_c] ON [c1].[CountryID] = [c1_c].[CountryID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Price] AS [c1_p] ON [c1].[PriceID] = [c1_p].[PriceID] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Country] AS [c2] ON [ci].[CountryID] = [c2].[CountryID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Language] AS [c2_l] ON [c2].[LanguageID] = [c2_l].[LanguageID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Currency] AS [c2_c] ON [c2].[CurrencyID] = [c2_c].[CurrencyID] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[MobileOperator] AS [ci_mo] ON [ci].[MobileOperatorID] = [ci_mo].[MobileOperatorID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Country] AS [ci_mo_c] ON [ci_mo].[CountryID] = [ci_mo_c].[CountryID] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Price] AS [ci_p] ON [ci].[PriceID] = [ci_p].[PriceID] ";
				if (this.Depth > 1)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[Currency] AS [ci_p_c] ON [ci_p].[CurrencyID] = [ci_p_c].[CurrencyID] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[DynamicPlatform] AS [ci_dp] ON [ci].[DynamicPlatformID] = [ci_dp].[DynamicPlatformID] ";
				if (this.Depth > 0)
				  sqlCmdText += "LEFT OUTER  JOIN [core].[DynamicBrowser] AS [ci_db] ON [ci].[DynamicBrowserID] = [ci_db].[DynamicBrowserID] ";
				

        sqlCmdText = parameters.BuildQuery(sqlCmdText);       
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        foreach (KeyValuePair<string, object> argument in parameters.Arguments)
          sqlCmd.Parameters.AddWithValue("@" + argument.Key, argument.Value);

        SqlDataReader sqlReader = database.Add(sqlCmd) as SqlDataReader;

        if (!sqlReader.HasRows || !sqlReader.Read())
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("ci", "customloadmany", "notfound"), "ClickInformation list could not be loaded using custom logic as no items were found.", sqlCmdText, this, connection, parameters);
          if (this.Logger.IsDebugEnabled)
            this.Logger.Debug(builder.ToString());
          sqlReader.Close();
          return new List<ClickInformation>();
        }

        SqlQuery query = new SqlQuery(sqlReader);

				ClickInformationTable ciTable = new ClickInformationTable(query);
				ClickTable ci_cTable = (this.Depth > 0) ? new ClickTable(query) : null;
				OfferTable ci_c_oTable = (this.Depth > 1) ? new OfferTable(query) : null;
				AffiliateTable ci_c_aTable = (this.Depth > 1) ? new AffiliateTable(query) : null;
				CampaignTable c1Table = (this.Depth > 0) ? new CampaignTable(query) : null;
				CampaignGroupTable c1_cgTable = (this.Depth > 1) ? new CampaignGroupTable(query) : null;
				CountryTable c1_cTable = (this.Depth > 1) ? new CountryTable(query) : null;
				PriceTable c1_pTable = (this.Depth > 1) ? new PriceTable(query) : null;
				CountryTable c2Table = (this.Depth > 0) ? new CountryTable(query) : null;
				LanguageTable c2_lTable = (this.Depth > 1) ? new LanguageTable(query) : null;
				CurrencyTable c2_cTable = (this.Depth > 1) ? new CurrencyTable(query) : null;
				MobileOperatorTable ci_moTable = (this.Depth > 0) ? new MobileOperatorTable(query) : null;
				CountryTable ci_mo_cTable = (this.Depth > 1) ? new CountryTable(query) : null;
				PriceTable ci_pTable = (this.Depth > 0) ? new PriceTable(query) : null;
				CurrencyTable ci_p_cTable = (this.Depth > 1) ? new CurrencyTable(query) : null;
				DynamicPlatformTable ci_dpTable = (this.Depth > 0) ? new DynamicPlatformTable(query) : null;
				DynamicBrowserTable ci_dbTable = (this.Depth > 0) ? new DynamicBrowserTable(query) : null;

        List<ClickInformation> result = new List<ClickInformation>();
        do
        {
          
					Offer ci_c_oObject = (this.Depth > 1) ? ci_c_oTable.CreateInstance() : null;
					Affiliate ci_c_aObject = (this.Depth > 1) ? ci_c_aTable.CreateInstance() : null;
					Click ci_cObject = (this.Depth > 0) ? ci_cTable.CreateInstance(ci_c_oObject, ci_c_aObject) : null;
					CampaignGroup c1_cgObject = (this.Depth > 1) ? c1_cgTable.CreateInstance() : null;
					Country c1_cObject = (this.Depth > 1) ? c1_cTable.CreateInstance() : null;
					Price c1_pObject = (this.Depth > 1) ? c1_pTable.CreateInstance() : null;
					Campaign c1Object = (this.Depth > 0) ? c1Table.CreateInstance(c1_cgObject, c1_cObject, c1_pObject) : null;
					Language c2_lObject = (this.Depth > 1) ? c2_lTable.CreateInstance() : null;
					Currency c2_cObject = (this.Depth > 1) ? c2_cTable.CreateInstance() : null;
					Country c2Object = (this.Depth > 0) ? c2Table.CreateInstance(c2_lObject, c2_cObject) : null;
					Country ci_mo_cObject = (this.Depth > 1) ? ci_mo_cTable.CreateInstance() : null;
					MobileOperator ci_moObject = (this.Depth > 0) ? ci_moTable.CreateInstance(ci_mo_cObject) : null;
					Currency ci_p_cObject = (this.Depth > 1) ? ci_p_cTable.CreateInstance() : null;
					Price ci_pObject = (this.Depth > 0) ? ci_pTable.CreateInstance(ci_p_cObject) : null;
					DynamicPlatform ci_dpObject = (this.Depth > 0) ? ci_dpTable.CreateInstance() : null;
					DynamicBrowser ci_dbObject = (this.Depth > 0) ? ci_dbTable.CreateInstance() : null;
					ClickInformation ciObject = (this.Depth > -1) ? ciTable.CreateInstance(ci_cObject, c1Object, c2Object, ci_moObject, ci_pObject, ci_dpObject, ci_dbObject) : null;
					result.Add(ciObject);

        } while (sqlReader.Read());
        sqlReader.Close();

        return result;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("ci", "customloadmany", "exception"), "ClickInformation list could not be loaded using custom logic. See exception for details.", sqlCmdText, ex, this, connection, parameters);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Load, "ClickInformation", "Exception while loading (custom/many) ClickInformation object from database. See inner exception for details.", ex);
      }
    }
    
    public override int? Insert(ISqlConnectionInfo connection, ClickInformation data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText = "INSERT INTO [core].[ClickInformation] ([ClickID],[CampaignID],[CountryID],[MobileOperatorID],[MobileOperatorName],[PriceID],[EntranceUrl],[RedirectUrl],[Referrer],[UserAgent],[IPAddress],[IsMobile],[ScreenPixelsHeight],[ScreenPixelsWidth],[HardwareVendor],[HardwareModel],[PlatformVendor],[DynamicPlatformID],[PlatformName],[PlatformVersion],[BrowserVendor],[DynamicBrowserID],[BrowserName],[BrowserVersion],[CountryCode],[Region],[City],[ISPName],[DomainName],[MobileBrand],[UsageType],[Note]) VALUES(@ClickID,@CampaignID,@CountryID,@MobileOperatorID,@MobileOperatorName,@PriceID,@EntranceUrl,@RedirectUrl,@Referrer,@UserAgent,@IPAddress,@IsMobile,@ScreenPixelsHeight,@ScreenPixelsWidth,@HardwareVendor,@HardwareModel,@PlatformVendor,@DynamicPlatformID,@PlatformName,@PlatformVersion,@BrowserVendor,@DynamicBrowserID,@BrowserName,@BrowserVersion,@CountryCode,@Region,@City,@ISPName,@DomainName,@MobileBrand,@UsageType,@Note); SELECT SCOPE_IDENTITY();";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@ClickID", data.Click.ID);
				sqlCmd.Parameters.AddWithValue("@CampaignID", data.Campaign == null ? DBNull.Value : (object)data.Campaign.ID);
				sqlCmd.Parameters.AddWithValue("@CountryID", data.Country == null ? DBNull.Value : (object)data.Country.ID);
				sqlCmd.Parameters.AddWithValue("@MobileOperatorID", data.MobileOperator == null ? DBNull.Value : (object)data.MobileOperator.ID);
				sqlCmd.Parameters.AddWithValue("@MobileOperatorName", !string.IsNullOrEmpty(data.MobileOperatorName) ? (object)data.MobileOperatorName : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@PriceID", data.Price == null ? DBNull.Value : (object)data.Price.ID);
				sqlCmd.Parameters.AddWithValue("@EntranceUrl", data.EntranceUrl).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@RedirectUrl", data.RedirectUrl).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@Referrer", data.Referrer).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@UserAgent", data.UserAgent).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@IPAddress", data.IPAddress).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@IsMobile", data.IsMobile.HasValue ? (object)data.IsMobile.Value : DBNull.Value).SqlDbType = SqlDbType.Bit;
				sqlCmd.Parameters.AddWithValue("@ScreenPixelsHeight", data.ScreenPixelsHeight.HasValue ? (object)data.ScreenPixelsHeight.Value : DBNull.Value).SqlDbType = SqlDbType.Int;
				sqlCmd.Parameters.AddWithValue("@ScreenPixelsWidth", data.ScreenPixelsWidth.HasValue ? (object)data.ScreenPixelsWidth.Value : DBNull.Value).SqlDbType = SqlDbType.Int;
				sqlCmd.Parameters.AddWithValue("@HardwareVendor", !string.IsNullOrEmpty(data.HardwareVendor) ? (object)data.HardwareVendor : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@HardwareModel", !string.IsNullOrEmpty(data.HardwareModel) ? (object)data.HardwareModel : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@PlatformVendor", !string.IsNullOrEmpty(data.PlatformVendor) ? (object)data.PlatformVendor : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@DynamicPlatformID", data.DynamicPlatform == null ? DBNull.Value : (object)data.DynamicPlatform.ID);
				sqlCmd.Parameters.AddWithValue("@PlatformName", !string.IsNullOrEmpty(data.PlatformName) ? (object)data.PlatformName : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@PlatformVersion", !string.IsNullOrEmpty(data.PlatformVersion) ? (object)data.PlatformVersion : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@BrowserVendor", !string.IsNullOrEmpty(data.BrowserVendor) ? (object)data.BrowserVendor : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@DynamicBrowserID", data.DynamicBrowser == null ? DBNull.Value : (object)data.DynamicBrowser.ID);
				sqlCmd.Parameters.AddWithValue("@BrowserName", !string.IsNullOrEmpty(data.BrowserName) ? (object)data.BrowserName : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@BrowserVersion", !string.IsNullOrEmpty(data.BrowserVersion) ? (object)data.BrowserVersion : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@CountryCode", !string.IsNullOrEmpty(data.CountryCode) ? (object)data.CountryCode : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@Region", !string.IsNullOrEmpty(data.Region) ? (object)data.Region : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@City", !string.IsNullOrEmpty(data.City) ? (object)data.City : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@ISPName", !string.IsNullOrEmpty(data.ISPName) ? (object)data.ISPName : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@DomainName", !string.IsNullOrEmpty(data.DomainName) ? (object)data.DomainName : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@MobileBrand", !string.IsNullOrEmpty(data.MobileBrand) ? (object)data.MobileBrand : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@UsageType", !string.IsNullOrEmpty(data.UsageType) ? (object)data.UsageType : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@Note", !string.IsNullOrEmpty(data.Note) ? (object)data.Note : DBNull.Value).SqlDbType = SqlDbType.NText;
				
        object idObj = sqlCmd.ExecuteScalar();
        if (idObj == null || DBNull.Value.Equals(idObj))
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("ci", "insert", "noprimarykey"), "ClickInformation could not be inserted or inserted primary key was not returned. Are you missing SELECT SCOPE_IDENTITY();?", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Insert, "ClickInformation", "Exception while inserting ClickInformation object in database.");
        }
        return (int)((decimal)idObj);
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("ci", "insert", "exception"), "ClickInformation could not be inserted. See exception for details.", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Insert, "ClickInformation", "Exception while inserting ClickInformation object in database. See inner exception for details.", ex);
      }
    }

    public override bool Update(ISqlConnectionInfo connection, ClickInformation data)
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
        sqlCmdText = "UPDATE [core].[ClickInformation] SET " +
												"[ClickID] = @ClickID, " + 
												"[CampaignID] = @CampaignID, " + 
												"[CountryID] = @CountryID, " + 
												"[MobileOperatorID] = @MobileOperatorID, " + 
												"[MobileOperatorName] = @MobileOperatorName, " + 
												"[PriceID] = @PriceID, " + 
												"[EntranceUrl] = @EntranceUrl, " + 
												"[RedirectUrl] = @RedirectUrl, " + 
												"[Referrer] = @Referrer, " + 
												"[UserAgent] = @UserAgent, " + 
												"[IPAddress] = @IPAddress, " + 
												"[IsMobile] = @IsMobile, " + 
												"[ScreenPixelsHeight] = @ScreenPixelsHeight, " + 
												"[ScreenPixelsWidth] = @ScreenPixelsWidth, " + 
												"[HardwareVendor] = @HardwareVendor, " + 
												"[HardwareModel] = @HardwareModel, " + 
												"[PlatformVendor] = @PlatformVendor, " + 
												"[DynamicPlatformID] = @DynamicPlatformID, " + 
												"[PlatformName] = @PlatformName, " + 
												"[PlatformVersion] = @PlatformVersion, " + 
												"[BrowserVendor] = @BrowserVendor, " + 
												"[DynamicBrowserID] = @DynamicBrowserID, " + 
												"[BrowserName] = @BrowserName, " + 
												"[BrowserVersion] = @BrowserVersion, " + 
												"[CountryCode] = @CountryCode, " + 
												"[Region] = @Region, " + 
												"[City] = @City, " + 
												"[ISPName] = @ISPName, " + 
												"[DomainName] = @DomainName, " + 
												"[MobileBrand] = @MobileBrand, " + 
												"[UsageType] = @UsageType, " + 
												"[Note] = @Note, " + 
												"[Updated] = GETDATE() " + 
											"WHERE [ClickInformationID] = @ClickInformationID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
		
				sqlCmd.Parameters.AddWithValue("@ClickID", data.Click.ID);
				sqlCmd.Parameters.AddWithValue("@CampaignID", data.Campaign == null ? DBNull.Value : (object)data.Campaign.ID);
				sqlCmd.Parameters.AddWithValue("@CountryID", data.Country == null ? DBNull.Value : (object)data.Country.ID);
				sqlCmd.Parameters.AddWithValue("@MobileOperatorID", data.MobileOperator == null ? DBNull.Value : (object)data.MobileOperator.ID);
				sqlCmd.Parameters.AddWithValue("@MobileOperatorName", !string.IsNullOrEmpty(data.MobileOperatorName) ? (object)data.MobileOperatorName : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@PriceID", data.Price == null ? DBNull.Value : (object)data.Price.ID);
				sqlCmd.Parameters.AddWithValue("@EntranceUrl", data.EntranceUrl).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@RedirectUrl", data.RedirectUrl).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@Referrer", data.Referrer).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@UserAgent", data.UserAgent).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@IPAddress", data.IPAddress).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@IsMobile", data.IsMobile.HasValue ? (object)data.IsMobile.Value : DBNull.Value).SqlDbType = SqlDbType.Bit;
				sqlCmd.Parameters.AddWithValue("@ScreenPixelsHeight", data.ScreenPixelsHeight.HasValue ? (object)data.ScreenPixelsHeight.Value : DBNull.Value).SqlDbType = SqlDbType.Int;
				sqlCmd.Parameters.AddWithValue("@ScreenPixelsWidth", data.ScreenPixelsWidth.HasValue ? (object)data.ScreenPixelsWidth.Value : DBNull.Value).SqlDbType = SqlDbType.Int;
				sqlCmd.Parameters.AddWithValue("@HardwareVendor", !string.IsNullOrEmpty(data.HardwareVendor) ? (object)data.HardwareVendor : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@HardwareModel", !string.IsNullOrEmpty(data.HardwareModel) ? (object)data.HardwareModel : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@PlatformVendor", !string.IsNullOrEmpty(data.PlatformVendor) ? (object)data.PlatformVendor : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@DynamicPlatformID", data.DynamicPlatform == null ? DBNull.Value : (object)data.DynamicPlatform.ID);
				sqlCmd.Parameters.AddWithValue("@PlatformName", !string.IsNullOrEmpty(data.PlatformName) ? (object)data.PlatformName : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@PlatformVersion", !string.IsNullOrEmpty(data.PlatformVersion) ? (object)data.PlatformVersion : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@BrowserVendor", !string.IsNullOrEmpty(data.BrowserVendor) ? (object)data.BrowserVendor : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@DynamicBrowserID", data.DynamicBrowser == null ? DBNull.Value : (object)data.DynamicBrowser.ID);
				sqlCmd.Parameters.AddWithValue("@BrowserName", !string.IsNullOrEmpty(data.BrowserName) ? (object)data.BrowserName : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@BrowserVersion", !string.IsNullOrEmpty(data.BrowserVersion) ? (object)data.BrowserVersion : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@CountryCode", !string.IsNullOrEmpty(data.CountryCode) ? (object)data.CountryCode : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@Region", !string.IsNullOrEmpty(data.Region) ? (object)data.Region : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@City", !string.IsNullOrEmpty(data.City) ? (object)data.City : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@ISPName", !string.IsNullOrEmpty(data.ISPName) ? (object)data.ISPName : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@DomainName", !string.IsNullOrEmpty(data.DomainName) ? (object)data.DomainName : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@MobileBrand", !string.IsNullOrEmpty(data.MobileBrand) ? (object)data.MobileBrand : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@UsageType", !string.IsNullOrEmpty(data.UsageType) ? (object)data.UsageType : DBNull.Value).SqlDbType = SqlDbType.NVarChar;
				sqlCmd.Parameters.AddWithValue("@Note", !string.IsNullOrEmpty(data.Note) ? (object)data.Note : DBNull.Value).SqlDbType = SqlDbType.NText;
				sqlCmd.Parameters.AddWithValue("@Updated", data.Updated).SqlDbType = SqlDbType.DateTime2;
				sqlCmd.Parameters.AddWithValue("@ClickInformationID", data.ID);

        int rowCount = sqlCmd.ExecuteNonQuery();
        if (rowCount < 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("ci", "update", "norecord"), "ClickInformation could not be updated as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "ClickInformation", "Exception while updating ClickInformation object in database. No record found for this id.");
        }
        else if (rowCount > 1)
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("ci", "update", "morerecords"), "ClickInformation was updated but there was more than one record affected.", sqlCmdText, this, connection, data);
          if (this.Logger.IsFatalEnabled)
            this.Logger.Fatal(builder.ToString());
          throw new DataOperationException(DataOperation.Update, "ClickInformation", "Exception while updating ClickInformation object in database. More than one record found for this statement (update statement where clause broken?!).");
        }
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("ci", "update", "exception"), "ClickInformation could not be updated. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Update, "ClickInformation", "Exception while updating ClickInformation object in database. See inner exception for details.", ex);
      }
    }

    public override bool Delete(ISqlConnectionInfo connection, ClickInformation data)
    {
      IDatabase database = connection.Database;
      if (database == null)
        throw new ArgumentNullException("database", "Error initializing database connection.");
      if (data == null)
        throw new ArgumentNullException("data");
      string sqlCmdText = string.Empty;
      try
      {
        sqlCmdText =  "DELETE FROM [core].[ClickInformation] WHERE ClickInformationID = @ClickInformationID;";
        SqlCommand sqlCmd = database.Add(sqlCmdText) as SqlCommand;
        sqlCmd.Parameters.AddWithValue("@ClickInformationID", data.ID);

        int success = sqlCmd.ExecuteNonQuery();

        if (success == -1)        
        {
          IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("ci", "delete", "norecord"), "ClickInformation could not be deleted as no matching record was found.", sqlCmdText, this, connection, data);
          if (this.Logger.IsErrorEnabled)
            this.Logger.Error(builder.ToString());
          throw new DataOperationException(DataOperation.Delete, "ClickInformation", "Exception while deleting ClickInformation object from database. No such record found.");
        }
        
        return true;
      }
      catch (Exception ex)
      {
        database.HandleException(ex);
        IMessageBuilder builder = new DbLogMessageBuilder(new LogErrorCode("ci", "delete", "exception"), "ClickInformation could not be deleted. See exception for details", sqlCmdText, ex, this, connection, data);
        if (this.Logger.IsErrorEnabled)
          this.Logger.Error(builder.ToString(), ex);
        throw new DataOperationException(DataOperation.Delete, "ClickInformation", "Exception while deleting ClickInformation object from database. See inner exception for details.", ex);
      }
    }
  }
}

