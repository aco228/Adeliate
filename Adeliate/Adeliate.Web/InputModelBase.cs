using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Adeliate.Web
{
  public abstract class InputModelBase
  {
    private bool _hasError = false;
    private bool _parseError = false;
    private string _errorMessage = string.Empty;
    private List<string> _parseErrorProperties = new List<string>();

    private string[] _ignoreProperties = new string[]
    { "HasError", "ParseError", "ErrorMessage", "ParseErrorProperties" };

    public bool HasError { get { return this._hasError; } protected set { this._hasError = value; } }
    public bool ParseError { get { return this._parseError; } protected set { this._parseError = value; } }
    public string ErrorMessage { get { return this._errorMessage; } protected set { this._errorMessage = value; } }
    public List<string> ParseErrorProperties { get { return this._parseErrorProperties; } protected set { this._parseErrorProperties = value; } }

    public InputModelBase(bool strictMode)
    {
      foreach (var prop in this.GetType().GetProperties())
      {
        if (this._ignoreProperties.Contains(prop.Name))
          continue;

        if (HttpContext.Current.Request[prop.Name] == null)
        {
          if (strictMode) { this.HasError = true; return; }
          continue;
        }

        switch (prop.PropertyType.Name)
        {
          case "Decimal":
            decimal decimalValue = 0.0m;
            if(decimal.TryParse(HttpContext.Current.Request[prop.Name].Replace(".",","), out decimalValue))
              this.GetType().GetProperty(prop.Name).SetValue(this, decimalValue, null);
            else
            {
              this.ParseError = true;
              this.ParseErrorProperties.Add(prop.Name);
            }
            break;
          case "Int32":
            int intValue;
            if (int.TryParse(HttpContext.Current.Request[prop.Name], out intValue))
              this.GetType().GetProperty(prop.Name).SetValue(this, intValue, null);
            else
            {
              this.ParseError = true;
              this.ParseErrorProperties.Add(prop.Name);
            }
            break;
          case "DateTime":
            DateTime dateValue;
            if (DateTime.TryParse(HttpContext.Current.Request[prop.Name], out dateValue))
              this.GetType().GetProperty(prop.Name).SetValue(this, dateValue, null);
            else
            {
              this.ParseError = true;
              this.ParseErrorProperties.Add(prop.Name);
            }
            break;
          case "String":
            this.GetType().GetProperty(prop.Name).SetValue(this, HttpContext.Current.Request[prop.Name], null);
            break;
          case "Boolean":
            this.GetType().GetProperty(prop.Name).SetValue(this, (HttpContext.Current.Request[prop.Name].Equals("1") || HttpContext.Current.Request[prop.Name].ToLower().Equals("true")) , null);
            break;
          default:
            break;
        }
      }
      this.Validation();
    }

    public abstract void Validation();

  }
}
