using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Adlite.IPRanges.Models
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
    
    public int CountryID = 0;
    public int MobileOperatorID = 0;
    
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

      if (this.Region.Length > 56) this.Region = this.Region.Substring(0, 56);
      if (this.City.Length > 56) this.City = this.City.Substring(0, 56);
      if (this.ISP_Name.Length > 512) this.ISP_Name = this.ISP_Name.Substring(0, 512);
      if (this.DomainName.Length > 256) this.DomainName = this.DomainName.Substring(0, 256);
      if (this.MobileBrand.Length > 256) this.MobileBrand = this.MobileBrand.Substring(0, 256);
      if (this.UsageType.Length > 256) this.UsageType = this.UsageType.Substring(0, 256);

      CountryModel countryModel = (from c in Program.Countries where c.TwoLetterIsoCode.Equals(this.CountryCode) select c).FirstOrDefault();
      if (countryModel == null)
        return;
      
      MobileOperatorModel mobileOperatorModel = countryModel.GetMobileOperatorByMccMnc(this.MCC, this.MNC);

      string sqlCommand = @"INSERT INTO Adlite.core." + Program.TABLE_NAME
        + " (FromAddress, ToAddress, CountryCode, Region, City, Latitude, Longtitude, ISP_Name, DomainName, MCC, MNC, MobileBrand, UsageType, CountryID, MobileOperatorID) "
        + " VALUES (" + this.From + ", " + this.To + ",'" + this.CountryCode + "','" + this.Region + "','" + this.CountryCode + "'," + this.Latitude + "," + this.Longtitude + ",'" + this.ISP_Name + "','" + this.DomainName + "',"
        + (mobileOperatorModel != null ? this.MCC : "0") + "," + (mobileOperatorModel != null ? this.MNC : "0") + ",'" + this.MobileBrand + "','" + this.UsageType + "'," + countryModel.ID + "," + (mobileOperatorModel != null ? mobileOperatorModel.ID.ToString() : "NULL") + ");";

      Program.Commands.Insert(sqlCommand);

      //DataRow row = Program.DataTable.NewRow();
      //row["FromAddress"] = long.Parse(this.From);
      //row["ToAddress"] = long.Parse(this.To);
      //row["CountryCode"] = this.CountryCode;
      //row["Region"] = this.Region;
      //row["City"] = this.City;
      //row["Latitude"] = decimal.Parse(this.Latitude);
      //row["Longtitude"] = decimal.Parse(this.Longtitude);
      //row["ISP_Name"] = this.ISP_Name;
      //row["DomainName"] = this.DomainName;
      //row["MCC"] = (mobileOperatorModel != null ? int.Parse(this.MCC) : 0);
      //row["MNC"] =(mobileOperatorModel != null ? int.Parse(this.MNC) : 0);
      //row["MobileBrand"] = this.MobileBrand;
      //row["UsageType"] = this.UsageType;
      //row["CountryID"] = (countryModel != null ? countryModel.ID : 0);
      //row["MobileOperatorID"] = (mobileOperatorModel != null ? mobileOperatorModel.ID : 0);
      //Program.DataTable.Rows.Add(row);

    }

  }
}
