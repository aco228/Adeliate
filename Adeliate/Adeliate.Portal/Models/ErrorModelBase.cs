using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adeliate.Portal.Models
{
  public class ErrorModelBase : AdliteModelBase
  {
    private string _title = "Adlite | Error";
    private string _code = "33228";
    private string _errorTitle = "Oops! Something is wrong";
    private string _errorDescription = "The page you are looking for was not found.";

    public string Title { get { return this._title; } set { this._title = value; } }
    public string Code { get { return this._code; } set { this._code = value; } }
    public string ErrorTitle { get { return this._errorTitle; } set { this._errorTitle = value; } }
    public string ErrorDescription { get { return this._errorDescription; } set { this._errorDescription = value; } }

  }
}