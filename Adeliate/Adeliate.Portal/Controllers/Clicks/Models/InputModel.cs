using Adeliate.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Controllers.Clicks.Models
{
  public class InputModel : InputModelBase
  {

    public string Limit { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public string ClickID { get; set; }
    public string SubID { get; set; }
    public string IPAddress { get; set; }
    public string Offers { get; set; }
    public string Countries { get; set; }
    public string Browsers { get; set; }
    public string Platforms { get; set; }
    public string OnlyWithTransaction { get; set; }

    public InputModel() : base(false)
    {
    }

    public override void Validation()
    {
      int referer;
      if(!Int32.TryParse(this.Limit, out referer))
      {
        this.ErrorMessage = "Limit must be numeric";
        this.HasError = true;
        return;
      }

      if(referer > 5000)
      {
        this.ErrorMessage = "Maximum limit is 5000";
        this.HasError = true;
        return;
      }

      DateTime dt;
      if(!DateTime.TryParse(this.From, out dt))
      {
        this.ErrorMessage = "From date is not in correct format";
        this.HasError = true;
        return;
      }

      if (!DateTime.TryParse(this.To, out dt))
      {
        this.ErrorMessage = "To date is not in correct format";
        this.HasError = true;
        return;
      }
      
    }
  }
}