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
  public class MobileOperatorCodeTable : TableBase<MobileOperatorCode>
  {
    public static string GetColumnNames()
    {
      return TableBase<MobileOperatorCode>.GetColumnNames(string.Empty, MobileOperatorCodeTable.Columns.Item);
    }

    public static string GetColumnNames(string tablePrefix)
    {
      return TableBase<MobileOperatorCode>.GetColumnNames(tablePrefix, MobileOperatorCodeTable.Columns.Item);
    }

    public class Columns
    {
		
			public static readonly ColumnDescription MobileOperatorCodeID = new ColumnDescription("MobileOperatorCodeID", 0, typeof(int));
			public static readonly ColumnDescription MobileOperatorID = new ColumnDescription("MobileOperatorID", 1, typeof(int));
			public static readonly ColumnDescription MCC = new ColumnDescription("MCC", 2, typeof(int));
			public static readonly ColumnDescription MNC = new ColumnDescription("MNC", 3, typeof(int));
			public static readonly ColumnDescription IsDefault = new ColumnDescription("IsDefault", 4, typeof(bool));
			public static readonly ColumnDescription Updated = new ColumnDescription("Updated", 5, typeof(DateTime));
			public static readonly ColumnDescription Created = new ColumnDescription("Created", 6, typeof(DateTime));
			

			public static readonly ColumnDescription[] Item = new ColumnDescription[] 
			{
				MobileOperatorCodeID,
				MobileOperatorID,
				MCC,
				MNC,
				IsDefault,
				Updated,
				Created
			};
    }

    public override int ColumnCount
    {
      get { return Columns.Item.Length; }
    }

    public MobileOperatorCodeTable(SqlQuery query) : base(query) { }
    public MobileOperatorCodeTable(SqlQuery query, ColumnSelectMode selectMode,
                           params ColumnDescription[] columns)
      : base(query, selectMode, columns) { }

    public ColumnDescription this[int index] { get { return Columns.Item[index]; } }

		public int MobileOperatorCodeID { get { return this.Reader.GetInt32(this.GetIndex(Columns.MobileOperatorCodeID)); } }
		public int MobileOperatorID { get { return this.Reader.GetInt32(this.GetIndex(Columns.MobileOperatorID)); } }
		public int MCC { get { return this.Reader.GetInt32(this.GetIndex(Columns.MCC)); } }
		public int MNC { get { return this.Reader.GetInt32(this.GetIndex(Columns.MNC)); } }
		public bool IsDefault { get { return this.Reader.GetBoolean(this.GetIndex(Columns.IsDefault)); } }
		public DateTime Updated { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Updated)); } }
		public DateTime Created { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Created)); } }
		

	  public MobileOperatorCode CreateInstance()  
		{ 
			if (!this.HasData) return null;
			return new MobileOperatorCode(this.MobileOperatorCodeID,new MobileOperator(this.MobileOperatorID), this.MCC,this.MNC,this.IsDefault,this.Updated,this.Created); 
		}
		public MobileOperatorCode CreateInstance(MobileOperator mobileOperator)  
		{ 
			if (!this.HasData) return null;
			return new MobileOperatorCode(this.MobileOperatorCodeID,mobileOperator ?? new MobileOperator(this.MobileOperatorID), this.MCC,this.MNC,this.IsDefault,this.Updated,this.Created); 
		}
		

  }
}

