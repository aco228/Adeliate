using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;
using Adlite.Data;

 

namespace Adlite.Data 
{
  public partial interface IPostbackRequestManager : IDataManager<PostbackRequest> 
  {
	

  }

  public partial class PostbackRequest : AdliteObject<IPostbackRequestManager> 
  {
		private PostbackRequestType _postbackRequestType;
		private string _rawUrl = String.Empty;
		private string _note = String.Empty;
		private bool _isSuccessful = false;
		

		public PostbackRequestType PostbackRequestType  { get { return this._postbackRequestType; } set { this._postbackRequestType = value; } }
		public string RawUrl{ get {return this._rawUrl; } set { this._rawUrl = value;} }
		public string Note{ get {return this._note; } set { this._note = value;} }
		public bool IsSuccessful {get {return this._isSuccessful; } set { this._isSuccessful = value;} }
		

    public override bool IsInsertable { get { return true;} }

    public override bool IsDeletable { get { return false;} }

    public override bool IsUpdatable { get { return true;} }

    public PostbackRequest()
    { 
    }

    public PostbackRequest(int id): base(id, DateTime.Now, DateTime.Now, true)
    {
    }

    public PostbackRequest(int id,  PostbackRequestType postbackRequestType, string rawUrl, string note, bool isSuccessful, DateTime updated, DateTime created): base(id, updated, created, false)
    {
			this._postbackRequestType = postbackRequestType;
			this._rawUrl = rawUrl;
			this._note = note;
			this._isSuccessful = isSuccessful;
			
    }

    public override object Clone(bool deepClone)
    {
      return new PostbackRequest(-1,  this.PostbackRequestType,this.RawUrl,this.Note,this.IsSuccessful, DateTime.Now, DateTime.Now);
    }
  }
}

