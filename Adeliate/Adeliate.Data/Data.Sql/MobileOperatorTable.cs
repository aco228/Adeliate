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
  public class MobileOperatorTable : TableBase<MobileOperator>
  {
    public static string GetColumnNames()
    {
      return TableBase<MobileOperator>.GetColumnNames(string.Empty, MobileOperatorTable.Columns.Item);
    }

    public static string GetColumnNames(string tablePrefix)
    {
      return TableBase<MobileOperator>.GetColumnNames(tablePrefix, MobileOperatorTable.Columns.Item);
    }

    public class Columns
    {
		
			public static readonly ColumnDescription MobileOperatorID = new ColumnDescription("MobileOperatorID", 0, typeof(int));
			public static readonly ColumnDescription ExternalMobileOperatorID = new ColumnDescription("ExternalMobileOperatorID", 1, typeof(int));
			public static readonly ColumnDescription CountryID = new ColumnDescription("CountryID", 2, typeof(int));
			public static readonly ColumnDescription Name = new ColumnDescription("Name", 3, typeof(string));
			public static readonly ColumnDescription Updated = new ColumnDescription("Updated", 4, typeof(DateTime));
			public static readonly ColumnDescription Created = new ColumnDescription("Created", 5, typeof(DateTime));
			

			public static readonly ColumnDescription[] Item = new ColumnDescription[] 
			{
				MobileOperatorID,
				ExternalMobileOperatorID,
				CountryID,
				Name,
				Updated,
				Created
			};
    }

    public override int ColumnCount
    {
      get { return Columns.Item.Length; }
    }

    public MobileOperatorTable(SqlQuery query) : base(query) { }
    public MobileOperatorTable(SqlQuery query, ColumnSelectMode selectMode,
                           params ColumnDescription[] columns)
      : base(query, selectMode, columns) { }

    public ColumnDescription this[int index] { get { return Columns.Item[index]; } }

		public int MobileOperatorID { get { return this.Reader.GetInt32(this.GetIndex(Columns.MobileOperatorID)); } }
		
		public int? ExternalMobileOperatorID 
		{
			get
			{
				int index = this.GetIndex(Columns.ExternalMobileOperatorID);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetInt32(index);
			}
		}

		public int CountryID { get { return this.Reader.GetInt32(this.GetIndex(Columns.CountryID)); } }
		public string Name { get { return this.Reader.GetString(this.GetIndex(Columns.Name)); } }
		public DateTime Updated { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Updated)); } }
		public DateTime Created { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Created)); } }
		

	  public MobileOperator CreateInstance()  
		{ 
			if (!this.HasData) return null;
			return new MobileOperator(this.MobileOperatorID,this.ExternalMobileOperatorID,new Country(this.CountryID), this.Name,this.Updated,this.Created); 
		}
		public MobileOperator CreateInstance(Country country)  
		{ 
			if (!this.HasData) return null;
			return new MobileOperator(this.MobileOperatorID,this.ExternalMobileOperatorID,country ?? new Country(this.CountryID), this.Name,this.Updated,this.Created); 
		}
		

  }
}

