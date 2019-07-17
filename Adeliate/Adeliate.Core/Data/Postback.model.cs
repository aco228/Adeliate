using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;
using Adlite.Data;

 

namespace Adlite.Data 
{
  public partial interface IPostbackManager : IDataManager<Postback> 
  {
	

  }

  public partial class Postback : AdliteObject<IPostbackManager> 
  {
		private PostbackType _postbackType;
		private Affiliate _affiliate;
		private string _url = String.Empty;
		private bool _isActive = false;
		

		public PostbackType PostbackType 
		{
			get
			{
				if (this._postbackType != null &&
						this._postbackType.IsEmpty)
					if (this.ConnectionContext != null)
						this._postbackType = PostbackType.CreateManager().LazyLoad(this.ConnectionContext, this._postbackType.ID) as PostbackType;
					else
						this._postbackType = PostbackType.CreateManager().LazyLoad(this._postbackType.ID) as PostbackType;
				return this._postbackType;
			}
			set { _postbackType = value; }
		}

		public Affiliate Affiliate 
		{
			get
			{
				if (this._affiliate != null &&
						this._affiliate.IsEmpty)
					if (this.ConnectionContext != null)
						this._affiliate = Affiliate.CreateManager().LazyLoad(this.ConnectionContext, this._affiliate.ID) as Affiliate;
					else
						this._affiliate = Affiliate.CreateManager().LazyLoad(this._affiliate.ID) as Affiliate;
				return this._affiliate;
			}
			set { _affiliate = value; }
		}

		public string Url{ get {return this._url; } set { this._url = value;} }
		public bool IsActive {get {return this._isActive; } set { this._isActive = value;} }
		

    public override bool IsInsertable { get { return true;} }

    public override bool IsDeletable { get { return true;} }

    public override bool IsUpdatable { get { return true;} }

    public Postback()
    { 
    }

    public Postback(int id): base(id, DateTime.Now, DateTime.Now, true)
    {
    }

    public Postback(int id,  PostbackType postbackType, Affiliate affiliate, string url, bool isActive, DateTime updated, DateTime created): base(id, updated, created, false)
    {
			this._postbackType = postbackType;
			this._affiliate = affiliate;
			this._url = url;
			this._isActive = isActive;
			
    }

    public override object Clone(bool deepClone)
    {
      return new Postback(-1,  this.PostbackType, this.Affiliate,this.Url,this.IsActive, DateTime.Now, DateTime.Now);
    }
  }
}

