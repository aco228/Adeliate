using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti;
using Senti.Data;
using Senti.Data.Sql;

using Adlite.Data;


namespace Adlite.Data.Sql
{
  public class CountryTable : TableBase<Country>
  {
    public static string GetColumnNames()
    {
      return TableBase<Country>.GetColumnNames(string.Empty, CountryTable.Columns.Item);
    }

    public static string GetColumnNames(string tablePrefix)
    {
      return TableBase<Country>.GetColumnNames(tablePrefix, CountryTable.Columns.Item);
    }

    public class Columns
    {
		
			public static readonly ColumnDescription CountryID = new ColumnDescription("CountryID", 0, typeof(int));
			public static readonly ColumnDescription LanguageID = new ColumnDescription("LanguageID", 1, typeof(int));
			public static readonly ColumnDescription CurrencyID = new ColumnDescription("CurrencyID", 2, typeof(int));
			public static readonly ColumnDescription GlobalName = new ColumnDescription("GlobalName", 3, typeof(string));
			public static readonly ColumnDescription LocalName = new ColumnDescription("LocalName", 4, typeof(string));
			public static readonly ColumnDescription CountryCode = new ColumnDescription("CountryCode", 5, typeof(string));
			public static readonly ColumnDescription CultureCode = new ColumnDescription("CultureCode", 6, typeof(string));
			public static readonly ColumnDescription TwoLetterIsoCode = new ColumnDescription("TwoLetterIsoCode", 7, typeof(string));
			public static readonly ColumnDescription LCID = new ColumnDescription("LCID", 8, typeof(int));
			public static readonly ColumnDescription CallingCode = new ColumnDescription("CallingCode", 9, typeof(int));
			public static readonly ColumnDescription Updated = new ColumnDescription("Updated", 10, typeof(DateTime));
			public static readonly ColumnDescription Created = new ColumnDescription("Created", 11, typeof(DateTime));
			

			public static readonly ColumnDescription[] Item = new ColumnDescription[] 
			{
				CountryID,
				LanguageID,
				CurrencyID,
				GlobalName,
				LocalName,
				CountryCode,
				CultureCode,
				TwoLetterIsoCode,
				LCID,
				CallingCode,
				Updated,
				Created
			};
    }

    public override int ColumnCount
    {
      get { return Columns.Item.Length; }
    }

    public CountryTable(SqlQuery query) : base(query) { }
    public CountryTable(SqlQuery query, ColumnSelectMode selectMode,
                           params ColumnDescription[] columns)
      : base(query, selectMode, columns) { }

    public ColumnDescription this[int index] { get { return Columns.Item[index]; } }

		public int CountryID { get { return this.Reader.GetInt32(this.GetIndex(Columns.CountryID)); } }
		
		public int? LanguageID 
		{
			get
			{
				int index = this.GetIndex(Columns.LanguageID);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetInt32(index);
			}
		}

		
		public int? CurrencyID 
		{
			get
			{
				int index = this.GetIndex(Columns.CurrencyID);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetInt32(index);
			}
		}

		public string GlobalName { get { return this.Reader.GetString(this.GetIndex(Columns.GlobalName)); } }
		public string LocalName { get { return this.Reader.GetString(this.GetIndex(Columns.LocalName)); } }
		public string CountryCode { get { return this.Reader.GetString(this.GetIndex(Columns.CountryCode)); } }
		public string CultureCode { get { return this.Reader.GetString(this.GetIndex(Columns.CultureCode)); } }
		public string TwoLetterIsoCode { get { return this.Reader.GetString(this.GetIndex(Columns.TwoLetterIsoCode)); } }
		
		public int? LCID 
		{
			get
			{
				int index = this.GetIndex(Columns.LCID);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetInt32(index);
			}
		}

		
		public int? CallingCode 
		{
			get
			{
				int index = this.GetIndex(Columns.CallingCode);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetInt32(index);
			}
		}

		public DateTime Updated { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Updated)); } }
		public DateTime Created { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Created)); } }
		

	  public Country CreateInstance()  
		{ 
			if (!this.HasData) return null;
			return new Country(this.CountryID,(LanguageID.HasValue ? new Language(this.LanguageID.Value) : null), (CurrencyID.HasValue ? new Currency(this.CurrencyID.Value) : null), this.GlobalName,this.LocalName,this.CountryCode,this.CultureCode,this.TwoLetterIsoCode,this.LCID,this.CallingCode,this.Updated,this.Created); 
		}
		public Country CreateInstance(Currency currency)  
		{ 
			if (!this.HasData) return null;
			return new Country(this.CountryID,(LanguageID.HasValue ? new Language(this.LanguageID.Value) : null), currency ?? (this.CurrencyID.HasValue ? new Currency(this.CurrencyID.Value) : null), this.GlobalName,this.LocalName,this.CountryCode,this.CultureCode,this.TwoLetterIsoCode,this.LCID,this.CallingCode,this.Updated,this.Created); 
		}
		public Country CreateInstance(Language language)  
		{ 
			if (!this.HasData) return null;
			return new Country(this.CountryID,language ?? (this.LanguageID.HasValue ? new Language(this.LanguageID.Value) : null), (CurrencyID.HasValue ? new Currency(this.CurrencyID.Value) : null), this.GlobalName,this.LocalName,this.CountryCode,this.CultureCode,this.TwoLetterIsoCode,this.LCID,this.CallingCode,this.Updated,this.Created); 
		}
		public Country CreateInstance(Language language, Currency currency)  
		{ 
			if (!this.HasData) return null;
			return new Country(this.CountryID,language ?? (this.LanguageID.HasValue ? new Language(this.LanguageID.Value) : null), currency ?? (this.CurrencyID.HasValue ? new Currency(this.CurrencyID.Value) : null), this.GlobalName,this.LocalName,this.CountryCode,this.CultureCode,this.TwoLetterIsoCode,this.LCID,this.CallingCode,this.Updated,this.Created); 
		}
		

  }
}

