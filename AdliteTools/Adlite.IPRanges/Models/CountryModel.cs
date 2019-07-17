using Direct.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adlite.IPRanges.Models
{
  public class CountryModel
  {
    public int ID { get; set; }
    public string TwoLetterIsoCode { get; set; }
    public List<MobileOperatorModel> MobileOperators { get; set; }

    public static List<CountryModel> LoadAll()
    {
      Console.Write("Start loading cache countries");

      DirectContainer dc = Program.Database.LoadContainer("SELECT CountryID, TwoLetterIsoCode FROM Adlite.core.Country");
      List<CountryModel> result = new List<CountryModel>();
      foreach(DirectContainerRow row in dc.Rows)
      {
        CountryModel newModel = new CountryModel();
        newModel.ID = row.GetInt("CountryID").Value;
        newModel.TwoLetterIsoCode = row.GetString("TwoLetterIsoCode");
        newModel.MobileOperators = new List<MobileOperatorModel>();

        DirectContainer dcmno = Program.Database.LoadContainer("SELECT MobileOperatorID, Name FROM Adlite.core.MobileOperator WHERE CountryID=" + newModel.ID);
        if(dcmno.HasValue && dcmno.RowsCount > 0)
          foreach(DirectContainerRow dcrMno in dcmno.Rows)
          {
            MobileOperatorModel mnoModel = new MobileOperatorModel();
            mnoModel.ID = dcrMno.GetInt("MobileOperatorID").Value;
            mnoModel.Name = dcrMno.GetString("Name");
            mnoModel.MncMccCombination = new List<MccMncCombination>();

            DirectContainer dcMcc = Program.Database.LoadContainer("SELECT MCC, MNC FROM Adlite.core.MobileOperatorCode WHERE MobileOperatorID=" + mnoModel.ID);
            if(dcMcc.HasValue && dcMcc.RowsCount > 0)
              foreach(DirectContainerRow rMnc in dcMcc.Rows)
              {
                MccMncCombination comb = new MccMncCombination();
                comb.MCC = rMnc.GetInt("MCC").Value;
                comb.MNC = rMnc.GetInt("MNC").Value;
                mnoModel.MncMccCombination.Add(comb);
              }

            newModel.MobileOperators.Add(mnoModel);
          }
        
        result.Add(newModel);
      }

      Console.WriteLine("Cache countries loaded");
      Console.WriteLine("");

      return result;
    }

    public MobileOperatorModel GetMobileOperatorByMccMnc(string mcc, string mnc)
    {
      int _mcc, _mnc;
      if (!Int32.TryParse(mcc, out _mcc) || !Int32.TryParse(mnc, out _mnc))
        return null;

      foreach(MobileOperatorModel mno in this.MobileOperators)
      {
        MccMncCombination comb = (from c in mno.MncMccCombination where c.MCC == _mcc && c.MNC == _mnc select c).FirstOrDefault();
        if (comb != null)
          return mno;
      }

      return null;
    }

  }
}
