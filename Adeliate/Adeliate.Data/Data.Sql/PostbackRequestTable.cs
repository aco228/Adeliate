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
  public class PostbackRequestTable : TableBase<PostbackRequest>
  {
    public static string GetColumnNames()
    {
      return TableBase<PostbackRequest>.GetColumnNames(string.Empty, PostbackRequestTable.Columns.Item);
    }

    public static string GetColumnNames(string tablePrefix)
    {
      return TableBase<PostbackRequest>.GetColumnNames(tablePrefix, PostbackRequestTable.Columns.Item);
    }

    public class Columns
    {
		
			public static readonly ColumnDescription PostbackRequestID = new ColumnDescription("PostbackRequestID", 0, typeof(int));
			public static readonly ColumnDescription PostbackRequestTypeID = new ColumnDescription("PostbackRequestTypeID", 1, typeof(int));
			public static readonly ColumnDescription RawUrl = new ColumnDescription("RawUrl", 2, typeof(string));
			public static readonly ColumnDescription Note = new ColumnDescription("Note", 3, typeof(string));
			public static readonly ColumnDescription IsSuccessful = new ColumnDescription("IsSuccessful", 4, typeof(bool));
			public static readonly ColumnDescription Updated = new ColumnDescription("Updated", 5, typeof(DateTime));
			public static readonly ColumnDescription Created = new ColumnDescription("Created", 6, typeof(DateTime));
			

			public static readonly ColumnDescription[] Item = new ColumnDescription[] 
			{
				PostbackRequestID,
				PostbackRequestTypeID,
				RawUrl,
				Note,
				IsSuccessful,
				Updated,
				Created
			};
    }

    public override int ColumnCount
    {
      get { return Columns.Item.Length; }
    }

    public PostbackRequestTable(SqlQuery query) : base(query) { }
    public PostbackRequestTable(SqlQuery query, ColumnSelectMode selectMode,
                           params ColumnDescription[] columns)
      : base(query, selectMode, columns) { }

    public ColumnDescription this[int index] { get { return Columns.Item[index]; } }

		public int PostbackRequestID { get { return this.Reader.GetInt32(this.GetIndex(Columns.PostbackRequestID)); } }
		public int PostbackRequestTypeID { get { return this.Reader.GetInt32(this.GetIndex(Columns.PostbackRequestTypeID)); } }
		public string RawUrl { get { return this.Reader.GetString(this.GetIndex(Columns.RawUrl)); } }
		
		public string Note 
		{
			get
			{
				int index = this.GetIndex(Columns.Note);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetString(index);
			}
		}

		public bool IsSuccessful { get { return this.Reader.GetBoolean(this.GetIndex(Columns.IsSuccessful)); } }
		public DateTime Updated { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Updated)); } }
		public DateTime Created { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Created)); } }
		

	  public PostbackRequest CreateInstance()  
		{ 
			if (!this.HasData) return null;
			return new PostbackRequest(this.PostbackRequestID,(PostbackRequestType)this.PostbackRequestTypeID,this.RawUrl,this.Note,this.IsSuccessful,this.Updated,this.Created); 
		}
		

  }
}

