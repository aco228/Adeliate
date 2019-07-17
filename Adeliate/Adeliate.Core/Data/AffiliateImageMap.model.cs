using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;
using Adlite.Data;

 

namespace Adlite.Data 
{
  public partial interface IAffiliateImageMapManager : IDataManager<AffiliateImageMap> 
  {
	

  }

  public partial class AffiliateImageMap : AdliteObject<IAffiliateImageMapManager> 
  {
		private Affiliate _affiliate;
		private Image _image;
		

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

		public Image Image 
		{
			get
			{
				if (this._image != null &&
						this._image.IsEmpty)
					if (this.ConnectionContext != null)
						this._image = Image.CreateManager().LazyLoad(this.ConnectionContext, this._image.ID) as Image;
					else
						this._image = Image.CreateManager().LazyLoad(this._image.ID) as Image;
				return this._image;
			}
			set { _image = value; }
		}

		

    public override bool IsInsertable { get { return true;} }

    public override bool IsDeletable { get { return false;} }

    public override bool IsUpdatable { get { return true;} }

    public AffiliateImageMap()
    { 
    }

    public AffiliateImageMap(int id): base(id, DateTime.Now, DateTime.Now, true)
    {
    }

    public AffiliateImageMap(int id,  Affiliate affiliate, Image image, DateTime updated, DateTime created): base(id, updated, created, false)
    {
			this._affiliate = affiliate;
			this._image = image;
			
    }

    public override object Clone(bool deepClone)
    {
      return new AffiliateImageMap(-1,  this.Affiliate, this.Image, DateTime.Now, DateTime.Now);
    }
  }
}

