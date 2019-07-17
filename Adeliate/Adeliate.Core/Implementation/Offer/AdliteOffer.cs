using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adeliate
{

  public class AdliteOffer
  {

    private Adlite.Data.Offer _offer = null;
    
    public Adlite.Data.Offer OfferData { get { return this._offer; } }

    public AdliteOffer(Adlite.Data.Offer offer)
    {
      this._offer = offer;
    }
  }
}
