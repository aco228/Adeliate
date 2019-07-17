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
  public class ImageTable : TableBase<Image>
  {
    public static string GetColumnNames()
    {
      return TableBase<Image>.GetColumnNames(string.Empty, ImageTable.Columns.Item);
    }

    public static string GetColumnNames(string tablePrefix)
    {
      return TableBase<Image>.GetColumnNames(tablePrefix, ImageTable.Columns.Item);
    }

    public class Columns
    {
		
			public static readonly ColumnDescription ImageID = new ColumnDescription("ImageID", 0, typeof(int));
			public static readonly ColumnDescription Data = new ColumnDescription("Data", 1, typeof(byte[]));
			public static readonly ColumnDescription Size = new ColumnDescription("Size", 2, typeof(int));
			public static readonly ColumnDescription Width = new ColumnDescription("Width", 3, typeof(int));
			public static readonly ColumnDescription Height = new ColumnDescription("Height", 4, typeof(int));
			public static readonly ColumnDescription MimeType = new ColumnDescription("MimeType", 5, typeof(string));
			public static readonly ColumnDescription Extension = new ColumnDescription("Extension", 6, typeof(string));
			public static readonly ColumnDescription Updated = new ColumnDescription("Updated", 7, typeof(DateTime));
			public static readonly ColumnDescription Created = new ColumnDescription("Created", 8, typeof(DateTime));
			

			public static readonly ColumnDescription[] Item = new ColumnDescription[] 
			{
				ImageID,
				Data,
				Size,
				Width,
				Height,
				MimeType,
				Extension,
				Updated,
				Created
			};
    }

    public override int ColumnCount
    {
      get { return Columns.Item.Length; }
    }

    public ImageTable(SqlQuery query) : base(query) { }
    public ImageTable(SqlQuery query, ColumnSelectMode selectMode,
                           params ColumnDescription[] columns)
      : base(query, selectMode, columns) { }

    public ColumnDescription this[int index] { get { return Columns.Item[index]; } }

		public int ImageID { get { return this.Reader.GetInt32(this.GetIndex(Columns.ImageID)); } }
		
		public byte[] Data {
			 get
			{
				int index = this.GetIndex(Columns.Data);
				if (this.Reader.IsDBNull(index)) return null;
				long len = Reader.GetBytes(index, 0, null, 0, 0);
				Byte[] path = new Byte[len]; 
				Reader.GetBytes(index, 0, path, 0, (int)len);
				return path;
			}
		}

		public int Size { get { return this.Reader.GetInt32(this.GetIndex(Columns.Size)); } }
		
		public int? Width 
		{
			get
			{
				int index = this.GetIndex(Columns.Width);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetInt32(index);
			}
		}

		
		public int? Height 
		{
			get
			{
				int index = this.GetIndex(Columns.Height);
				if (this.Reader.IsDBNull(index)) return null;
				return this.Reader.GetInt32(index);
			}
		}

		public string MimeType { get { return this.Reader.GetString(this.GetIndex(Columns.MimeType)); } }
		public string Extension { get { return this.Reader.GetString(this.GetIndex(Columns.Extension)); } }
		public DateTime Updated { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Updated)); } }
		public DateTime Created { get { return this.Reader.GetDateTime(this.GetIndex(Columns.Created)); } }
		

	  public Image CreateInstance()  
		{ 
			if (!this.HasData) return null;
			return new Image(this.ImageID,this.Data,this.Size,this.Width,this.Height,this.MimeType,this.Extension,this.Updated,this.Created); 
		}
		

  }
}

