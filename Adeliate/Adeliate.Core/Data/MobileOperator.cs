using Senti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 

namespace Adlite.Data 
{
  public partial interface IMobileOperatorManager 
  {

    List<MobileOperator> Load();
    List<MobileOperator> Load(IConnectionInfo connection);


    List<MobileOperator> Load(Campaign campaign);
    List<MobileOperator> Load(IConnectionInfo connection, Campaign campaign);



  }

  public partial class MobileOperator
  {
  }
}

