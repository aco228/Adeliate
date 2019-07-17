using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;
using Adlite.Data;

 

namespace Adlite.Data 
{
  public partial interface IPostbackDataManager : IDataManager<PostbackData> 
  {
	

  }

  public partial class PostbackData : AdliteObject<IPostbackDataManager> 
  {
		private Postback _postback;
		private Affiliate _affiliate;
		private Click _click;
		private PostbackRequest _postbackRequest;
		private string _entranceUrl = String.Empty;
		private string _postbackUrl = String.Empty;
		

		public Postback Postback 
		{
			get
			{
				if (this._postback != null &&
						this._postback.IsEmpty)
					if (this.ConnectionContext != null)
						this._postback = Postback.CreateManager().LazyLoad(this.ConnectionContext, this._postback.ID) as Postback;
					else
						this._postback = Postback.CreateManager().LazyLoad(this._postback.ID) as Postback;
				return this._postback;
			}
			set { _postback = value; }
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

		public Click Click 
		{
			get
			{
				if (this._click != null &&
						this._click.IsEmpty)
					if (this.ConnectionContext != null)
						this._click = Click.CreateManager().LazyLoad(this.ConnectionContext, this._click.ID) as Click;
					else
						this._click = Click.CreateManager().LazyLoad(this._click.ID) as Click;
				return this._click;
			}
			set { _click = value; }
		}

		public PostbackRequest PostbackRequest 
		{
			get
			{
				if (this._postbackRequest != null &&
						this._postbackRequest.IsEmpty)
					if (this.ConnectionContext != null)
						this._postbackRequest = PostbackRequest.CreateManager().LazyLoad(this.ConnectionContext, this._postbackRequest.ID) as PostbackRequest;
					else
						this._postbackRequest = PostbackRequest.CreateManager().LazyLoad(this._postbackRequest.ID) as PostbackRequest;
				return this._postbackRequest;
			}
			set { _postbackRequest = value; }
		}

		public string EntranceUrl{ get {return this._entranceUrl; } set { this._entranceUrl = value;} }
		public string PostbackUrl{ get {return this._postbackUrl; } set { this._postbackUrl = value;} }
		

    public override bool IsInsertable { get { return true;} }

    public override bool IsDeletable { get { return false;} }

    public override bool IsUpdatable { get { return true;} }

    public PostbackData()
    { 
    }

    public PostbackData(int id): base(id, DateTime.Now, DateTime.Now, true)
    {
    }

    public PostbackData(int id,  Postback postback, Affiliate affiliate, Click click, PostbackRequest postbackRequest, string entranceUrl, string postbackUrl, DateTime updated, DateTime created): base(id, updated, created, false)
    {
			this._postback = postback;
			this._affiliate = affiliate;
			this._click = click;
			this._postbackRequest = postbackRequest;
			this._entranceUrl = entranceUrl;
			this._postbackUrl = postbackUrl;
			
    }

    public override object Clone(bool deepClone)
    {
      return new PostbackData(-1,  this.Postback, this.Affiliate, this.Click, this.PostbackRequest,this.EntranceUrl,this.PostbackUrl, DateTime.Now, DateTime.Now);
    }
  }
}

