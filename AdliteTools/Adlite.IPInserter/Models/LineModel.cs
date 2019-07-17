using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Adlite.IPInserter.Models
{
  public class LineModel
  {
    public string From { get; set; }
    public string To { get; set; }
    public string CountryCode { get; set; }
    public string CountryName { get; set; }
    public string Region { get; set; }
    public string City { get; set; }
    public string Latitude { get; set; }
    public string Longtitude { get; set; }
    public string ISP_Name { get; set; }
    public string DomainName { get; set; }
    public string MCC { get; set; }
    public string MNC { get; set; }
    public string MobileBrand { get; set; }
    public string UsageType { get; set; }

    public LineModel() { }

    public LineModel(string line)
    {
      MatchCollection collection = Regex.Matches(line, "\"(.*?)\"");
      this.From = collection[0].ToString().Replace("\"", string.Empty).Replace(",", string.Empty).Trim().Replace("'", string.Empty);
      this.To = collection[1].ToString().Replace("\"", string.Empty).Replace(",", string.Empty).Trim().Replace("'", string.Empty);
      this.CountryCode = collection[2].ToString().Replace("\"", string.Empty).Replace(",", string.Empty).Trim().Replace("'", string.Empty);
      this.CountryName = collection[3].ToString().Replace("\"", string.Empty).Replace(",", string.Empty).Trim().Replace("'", string.Empty);
      this.Region = collection[4].ToString().Replace("\"", string.Empty).Replace(",", string.Empty).Trim().Replace("'", string.Empty);
      this.City = collection[5].ToString().Replace("\"", string.Empty).Replace(",", string.Empty).Trim().Replace("'", string.Empty);
      this.Latitude = collection[6].ToString().Replace("\"", string.Empty).Replace(",", string.Empty).Trim().Replace("'", string.Empty);
      this.Longtitude = collection[7].ToString().Replace("\"", string.Empty).Replace(",", string.Empty).Trim().Replace("'", string.Empty);
      this.ISP_Name = collection[8].ToString().Replace("\"", string.Empty).Replace(",", string.Empty).Trim().Replace("'", string.Empty);
      this.DomainName = collection[9].ToString().Replace("\"", string.Empty).Replace(",", string.Empty).Trim().Replace("'", string.Empty);
      this.MCC = collection[10].ToString().Replace("\"", string.Empty).Replace(",", string.Empty).Trim().Replace("'", string.Empty);
      this.MNC = collection[11].ToString().Replace("\"", string.Empty).Replace(",", string.Empty).Trim().Replace("'", string.Empty);
      this.MobileBrand = collection[12].ToString().Replace("\"", string.Empty).Replace(",", string.Empty).Trim().Replace("'", string.Empty);
      this.UsageType = collection[13].ToString().Replace("\"", string.Empty).Replace(",", string.Empty).Trim().Replace("'", string.Empty);
      
      CountryModel countryModel = (from c in Program.Countries where c.TwoLetterIsoCode.Equals(this.CountryCode) select c).FirstOrDefault();
      if (countryModel == null)
        return;
      
      MobileOperatorModel mobileOperatorModel = countryModel.GetMobileOperatorByMccMnc(this.MCC, this.MNC);
      
      Program.MobileOperatorTable.DataTable.Rows.Add(
        "-1", 
        From, 
        To, 
        CountryCode, 
        Region, 
        City, 
        Latitude, 
        Longtitude, 
        ISP_Name, 
        DomainName, 
        (mobileOperatorModel != null ? this.MCC : null), 
        (mobileOperatorModel != null ? this.MNC : null), 
        MobileBrand, 
        UsageType, 
        countryModel.ID, 
        (mobileOperatorModel != null ? mobileOperatorModel.ID.ToString() : null));
      Program.CURRENT_LIMIT++;
    }

    public static LineModel CreateCountryMap(string line)
    {
      LineModel model = new LineModel();
      MatchCollection collection = Regex.Matches(line, "\"(.*?)\"");
      model.From = collection[0].ToString().Replace("\"", string.Empty).Replace(",", string.Empty).Trim().Replace("'", string.Empty);
      model.To = collection[1].ToString().Replace("\"", string.Empty).Replace(",", string.Empty).Trim().Replace("'", string.Empty);
      model.CountryCode = collection[2].ToString().Replace("\"", string.Empty).Replace(",", string.Empty).Trim().Replace("'", string.Empty);

      CountryModel countryModel = (from c in Program.Countries where c.TwoLetterIsoCode.Equals(model.CountryCode) select c).FirstOrDefault();
      if (countryModel == null)
        return null;

      Program.CountryTable.DataTable.Rows.Add("-1",
        model.From,
        model.To,
        countryModel.ID);
      Program.CURRENT_LIMIT++;

      return model;
    }

  }
}
