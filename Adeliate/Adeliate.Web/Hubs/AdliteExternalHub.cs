using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Adeliate.Web.Hubs
{
  public class AdliteExternalHub
  {
    private bool _hasError = false;
    private bool _isConnected = false;
    private string _serverUrl = string.Empty;
    private string _hubName = string.Empty;
    private HubConnection _hubConnection = null;
    private IHubProxy _hub = null;

    public bool HasError { get { return this._hasError; } }
    public string ServerUrl { get { return this._serverUrl; } }
    public string HubName { get { return this._hubName; } }

    public AdliteExternalHub(string serverUrl, string hubName)
    {
      this._serverUrl = serverUrl;
      this._hubName = hubName;
      this.Connect();
    }

    protected virtual void Connect()
    {
      try
      {
        this._hubConnection = new HubConnection(this._serverUrl + "/signalr", useDefaultUrl: false);
        this._hub = this._hubConnection.CreateHubProxy(this._hubName);
        this._hubConnection.Start().Wait();
        this._isConnected = true;
      }
      catch(Exception e)
      {
        this._hasError = true;
      }
    }

    public void Send(string method, params object[] args)
    {
      if (this._hasError || !this._isConnected)
        return;

      try { this._hub.Invoke(method, args).Wait(); }
      catch (Exception e) {
        int a = 0;
      }
    }

  }
}
