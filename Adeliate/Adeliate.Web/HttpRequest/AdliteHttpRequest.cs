using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Adeliate.Web
{
  public class AdliteHttpRequest
  {
    
    public static AdliteHttpResponse Request(string url)
    {
      try
      {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        using (Stream stream = response.GetResponseStream())
        using (StreamReader reader = new StreamReader(stream))
        {
          return new AdliteHttpResponse()
          {
            StatusCode = (int)response.StatusCode,
            Response = reader.ReadToEnd()
          };
        }
      }
      catch(Exception e)
      {
        return new AdliteHttpResponse()
        {
          HasFatal = true,
          Response = e.ToString()
        };
      }
    }

  }
}
