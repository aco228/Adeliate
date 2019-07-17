using Senti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 

namespace Adlite.Data 
{
  public partial interface IClickInformationManager 
  {


    ClickInformation Load(Click click);
    ClickInformation Load(IConnectionInfo connection, Click click);

  }

  public partial class ClickInformation
  {
  }
}

