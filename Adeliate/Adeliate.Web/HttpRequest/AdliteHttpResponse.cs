using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adeliate.Web
{
  public class AdliteHttpResponse
  {

    public int StatusCode = 0;
    public bool HasFatal = false;
    public string Response = string.Empty;
    public bool Successfull { get { return !this.HasFatal || this.StatusCode == 200; } }

  }
}
