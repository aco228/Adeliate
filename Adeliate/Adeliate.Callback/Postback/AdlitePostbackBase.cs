using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Adlite.Data;

namespace Adeliate.Callback.Postback
{
  public class AdlitePostbackBase : AdlitePostback
  {
    public AdlitePostbackBase(Adlite.Data.Postback postback) : base(postback)
    { }

    public override void Billing()
    {
      if (this.Context.Click.HasTransaction)
        return;
      
      ClickInformation informations = ClickInformation.CreateManager().Load(this.Context.Click);
      if(informations == null)
      {
        // TODO: Figure what we should do in this case
        return;
      }

      decimal addedRevenue = AdliteCallbackApplication.CurrencyManager.ConvertToEur(informations.Price);
      Transaction transaction = new Transaction(
        -1, 
        this.Context.Click.ID, informations.Price, this.Context.Click.Offer.ID, 
        this.Context.Click.Offer.Campaign.ID,
        this.Context.Click.Affiliate.ID, addedRevenue, DateTime.Now, DateTime.Now);
      transaction.Insert();

      this.Context.Click.HasTransaction = true;
      this.Context.Click.Update();
      this.SendNotificationToPortal();

      string url = this.Data.Url.Replace("[ID]", this.Context.Click.SubID);
      this.OutsideNotification(url);

      StatInfo.UpdateTransactions(this.Context.Click.Offer, addedRevenue);

      OfferCapMap capMap = OfferCapMap.CreateManager().Load(this.Context.Click.Offer);
      if(capMap == null)
      {
        capMap = new OfferCapMap(-1, this.Context.Click.Offer, 1, false, DateTime.Now, DateTime.Now);
        capMap.Insert();
      }
      {
        capMap.Transactions++;
        capMap.Update();
      }


    }

    public override void Cancellation()
    {
    }

    public override void Rebilling()
    {
    }

    public override void Refund()
    {
    }
  }
}