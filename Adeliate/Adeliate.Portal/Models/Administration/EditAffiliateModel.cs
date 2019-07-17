using Adlite.Data;
using Adlite.Data.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Administration
{
  public class EditAffiliateModel : AdliteModelBase
  {
    public Affiliate Affiliate { get; set; }
    public List<AffiliateType> AffiliateTypes { get; set; }
    public List<PostbackType> PostbackTypes { get; set; }
    public List<Postback> Postbacks { get; set; }

    public EditAffiliateModel()
    {
      PostbackTypes = PostbackType.CreateManager().Load();
      AffiliateTypes = AffiliateType.CreateManager().Load();
      if (PostbackTypes == null)
        PostbackTypes = new List<PostbackType>();
      if (AffiliateTypes == null)
        AffiliateTypes = new List<AffiliateType>();

      Postbacks = new List<Postback>();

      Affiliate = new Affiliate();
          
    }
    public EditAffiliateModel(int id)
    {
      Affiliate = Affiliate.CreateManager().Load(id);
      if (Affiliate == null)
        Affiliate = new Affiliate();

      PostbackTypes = PostbackType.CreateManager().Load();
      if (PostbackTypes == null)
        PostbackTypes = new List<PostbackType>();

      AffiliateTypes = AffiliateType.CreateManager().Load();
      if (AffiliateTypes == null)
        AffiliateTypes = new List<AffiliateType>();

      Postbacks = Postback.CreateManager().Load(Affiliate);
      if (Postbacks == null)
        Postbacks = new List<Postback>();


    }
  }
}