using Senti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 

namespace Adlite.Data 
{
  public partial interface ITransactionManager 
  {

    List<Transaction> Load(Click click);
    List<Transaction> Load(IConnectionInfo connection, Click click);




  }

  public partial class Transaction
  {
  }
}

