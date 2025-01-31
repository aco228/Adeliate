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
  public class LanguageTable : TableBase<Language>
  {
    public static string GetColumnNames()
    {
      return TableBase<Language>.GetColumnNames(string.Empty, LanguageTable.Columns.Item);
    }

    public static string GetColumnNames(string tablePrefix)
    {
      return TableBase<Language>.GetColumnNames(tablePrefix, LanguageTable.Columns.Item);
    }

    public class Columns
    {
		
			public static readonly ColumnDescription LanguageID = new ColumnDescription("LanguageID", 0, typeof(int));
			public static readonly ColumnDescription GlobalName = new ColumnDescription("GlobalName", 1, typeof(string));
			public static readonly ColumnDescription LocalName = new ColumnDescription("LocalName", 2, typeof(string));
			public static readonly ColumnDescription Charset = new ColumnDescription("Charset", 3, typeof(string));
			public static readonly ColumnDescription TwoLetterIsoCode = new ColumnDescription("TwoLetterIsoCode", 4, typeof(string));
			public static readonly ColumnDescription IsSuported = new ColumnDescription("IsSuported", 5, typeof(bool));
			public static readonly ColumnDescription Updated = new ColumnDescription("Updated", 6, typeof(DateTime));
			public static readonly ColumnDescription Created = new ColumnDescription("Created", 7, typeof(DateTime));
			

			public static readonly ColumnDescription[] Item = new ColumnDescription[] 
			{
				LanguageID,
				GlobalName,
				LocalName,
				Charset,
				TwoLetterIsoCode,
				IsSuported,
				Updated,
				Created
			};
    }

    public override int ColumnCount
    {
      get { return Columns.Item.Length; }
    }

    public LanguageTable(SqlQuery query) : base(query) { }
    public LanguageTable(SqlQuery query, ColumnSelectMode selectMode,
                           params ColumnDescription[] columns)
      : base(query, selectMode, columns) { }

    public ColumnDescription this[int index] { get { return Columns.Item[index]; } }

		public int LanguageID { get { return this.Reader.GetInt32(this.GetIndex(Columns.LanguageID)); } }
		public string GlobalName { get { return this.Reader.GetString(this.GetIndex(Columns.GlobalName)); } }
		public string LocalName { get { return this.Reader.GetString(this.GetIndex(Columns.LocalName)); } }
		public string Charset { get { return this.Reader.GetString(this.GetIndex(Columns.Charset)); } }
		public string TwoLetterIsoCode { get { return this.Reader.GetString(this.GetIndex(Columns.TwoLetterIsoCode)); } }
		public bool IsSuported { get { return this.Reader.GetBoolean(this.GetIndex(Columns.IsSuported)); } }
		public DateTime Updated { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Updated)); } }
		public DateTime Created { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Created)); } }
		

	  public Language CreateInstance()  
		{ 
			if (!this.HasData) return null;
			return new Language(this.LanguageID,this.GlobalName,this.LocalName,this.Charset,this.TwoLetterIsoCode,this.IsSuported,this.Updated,this.Created); 
		}
		

  }
}

