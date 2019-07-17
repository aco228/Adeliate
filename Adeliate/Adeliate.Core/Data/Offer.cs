using Senti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 

namespace Adlite.Data 
{
  public partial interface IOfferManager 
  {


    Offer Load(string key);
    Offer Load(IConnectionInfo connection, string key);


    Offer Load(Campaign campign, Affiliate affiliate);
    Offer Load(IConnectionInfo connection, Campaign campign, Affiliate affiliate);


    List<Offer> Load(Affiliate affiliate);
    List<Offer> Load(IConnectionInfo connection, Affiliate affiliate);


  }

  public partial class Offer
  {

    public int CurrentCap { get { return this._capValue.HasValue ? this._capValue.Value : this._campaign.CapValue; } }
    public decimal CurrentPriceValue { get { return this._price == null ? this._campaign.Price.Value : this._price.Value; } }

  }
}

