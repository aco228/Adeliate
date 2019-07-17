using Adeliate.Portal.Code.Cache;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models.Clicks
{
  public class ClickInformationModel : AdliteModelBase
  {

    public Click Click = null;
    public ClickInformation ClickInformation = null;
    public List<Transaction> Transactions = null;
    public List<PostbackData> PostbackData = null;

    public ClickInformationModel()
    {
    }

  }
}