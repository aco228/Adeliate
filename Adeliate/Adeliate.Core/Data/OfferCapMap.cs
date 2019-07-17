using Senti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 

namespace Adlite.Data 
{
  public partial interface IOfferCapMapManager 
  {


    OfferCapMap Load(Offer offer);
    OfferCapMap Load(IConnectionInfo connection, Offer offer);




  }

  public partial class OfferCapMap
  {
  }
}

