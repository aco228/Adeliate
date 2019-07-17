using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;
using Adlite.Data;

 

namespace Adlite.Data 
{
  public partial interface IImageManager : IDataManager<Image> 
  {
	

  }

  public partial class Image : AdliteObject<IImageManager> 
  {
		private byte[] _data = null;
		private int _size = -1;
		private int? _width = -1;
		private int? _height = -1;
		private string _mimeType = String.Empty;
		private string _extension = String.Empty;
		

		public byte[] Data { get { return this._data; } set { this._data = value;}  }
		public int Size{ get {return this._size; } set { this._size = value;} }
		public int? Width{ get {return this._width; } set { this._width = value;} }
		public int? Height{ get {return this._height; } set { this._height = value;} }
		public string MimeType{ get {return this._mimeType; } set { this._mimeType = value;} }
		public string Extension{ get {return this._extension; } set { this._extension = value;} }
		

    public override bool IsInsertable { get { return true;} }

    public override bool IsDeletable { get { return false;} }

    public override bool IsUpdatable { get { return true;} }

    public Image()
    { 
    }

    public Image(int id): base(id, DateTime.Now, DateTime.Now, true)
    {
    }

    public Image(int id,  byte[] data, int size, int? width, int? height, string mimeType, string extension, DateTime updated, DateTime created): base(id, updated, created, false)
    {
			this._data = data;
			this._size = size;
			this._width = width;
			this._height = height;
			this._mimeType = mimeType;
			this._extension = extension;
			
    }

    public override object Clone(bool deepClone)
    {
      return new Image(-1, this.Data,this.Size,this.Width,this.Height,this.MimeType,this.Extension, DateTime.Now, DateTime.Now);
    }
  }
}

