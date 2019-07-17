using Adeliate.Callback.Code;
using Adeliate.Web;
using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Adeliate.Callback
{
  public abstract class AdlitePostback
  {
    private AdliteContext _context = null;
    private string _rawUrl = string.Empty;
    private Adlite.Data.Postback _postback = null;

    protected AdliteContext Context { get { return this._context; } }
    public string RawUrl { get { return this._rawUrl; } protected set { this._rawUrl = value; } }
    public Adlite.Data.Postback Data { get { return this._postback; } protected set { this._postback = value; } }

    public AdlitePostback(Adlite.Data.Postback postback)
    {
      this._postback = postback;
    }
    
    // SUMMARY: Method for calling manager from main thread
    public void Call(AdliteContext context)
    {
      this._context = context;
      MethodInfo methodInfo = this.GetType().GetMethod(this._context.ActionName);
      if (methodInfo == null)
        return;
      
      methodInfo.Invoke(this, null);
    }

    // SUMMARY: Shared method for sending notification to portal
    public virtual void SendNotificationToPortal()
    {
      AdliteHttpRequest.Request(string.Format("http://portal.adlite.local/callback/ontransaction?click_id={0}", this._context.Click.ID));
    }

    // SUMMARY: This method is called from abstracts methods
    public void OutsideNotification(string url)
    {
      PostbackRequest postbackRequest = new PostbackRequest(-1, PostbackRequestType.Out, url, string.Empty, true, DateTime.Now, DateTime.Now);
      PostbackData postbackData = new PostbackData(-1, 
        this._postback, 
        this._context.Click.Affiliate, 
        this._context.Click,
        postbackRequest, this._context.RawUrl, url, DateTime.Now, DateTime.Now);

      AdliteHttpResponse response = AdliteHttpRequest.Request(url);
      if (!response.Successfull)
      {
        postbackRequest.IsSuccessful = false;
        postbackRequest.Note = string.Format("(status={0}) ()", response.StatusCode.ToString(), response.Response);
      }

      postbackRequest.Insert();
      postbackData.Insert();
    }

    // SUMARY: Abstraction
    public void Index() { this.Billing(); }
    public abstract void Billing();
    public abstract void Rebilling();
    public abstract void Cancellation();
    public abstract void Refund();

    
    
  }
}