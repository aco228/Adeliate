using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adeliate.Web
{
  public abstract class AdliteApplication : System.Web.HttpApplication
  {
    protected void Application_Start()
    {
      this.Init();
    }

    protected abstract void Init();


  }
}
