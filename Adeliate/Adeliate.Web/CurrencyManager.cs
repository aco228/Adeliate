using Adlite.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Adeliate.Web
{
  public class CurrencyManager
  {
    private List<KeyValuePair<string, decimal>> _values = null;

    public CurrencyManager()
    {
      this._values = this.GetCurrencyListFromWeb();
    }

    public decimal ConvertToEur(Price price)
    {
      return this.ConvertToEur(price.Currency.Abbreviation, price.Value);
    }

    public decimal ConvertToEur(string Abbreviation, decimal Value)
    {
      if (Abbreviation.Equals("EUR"))
        return Value;

      decimal? dif = null;
      foreach (KeyValuePair<string, decimal> pair in this._values)
        if (pair.Key.Equals(Abbreviation))
        {
          dif = pair.Value;
          break;
        }

      if (dif == null)
        return Value;

      return Value * dif.Value;
    }

    public List<KeyValuePair<string, decimal>> GetCurrencyListFromWeb()
    {
      List<KeyValuePair<string, decimal>> returnList = new List<KeyValuePair<string, decimal>>();
      string date = string.Empty;
      using (XmlReader xmlr = XmlReader.Create(@"http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml"))
      {
        xmlr.ReadToFollowing("Cube");
        while (xmlr.Read())
        {
          if (xmlr.NodeType != XmlNodeType.Element) continue;
          if (xmlr.GetAttribute("time") != null)
          {
            date = xmlr.GetAttribute("time");
          }
          else returnList.Add(new KeyValuePair<string, decimal>(xmlr.GetAttribute("currency"), decimal.Parse(xmlr.GetAttribute("rate"), CultureInfo.InvariantCulture)));
        }
        //currencyDate = DateTime.Parse(date);
      }
      returnList.Add(new KeyValuePair<string, decimal>("EUR", 1));
      return returnList;
    }

  }
}
