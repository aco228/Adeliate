using Senti.Data;
using Senti.Data.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Adlite.Data 
{
  public partial interface IPostbackManager 
  {

    List<Postback> Load();
    List<Postback> Load(IConnectionInfo connection);


    


    List<Postback> Load(Affiliate affiliate);
    List<Postback> Load(IConnectionInfo connection, Affiliate affiliate);

  }

  public partial class Postback
  {
  }
}

