using Adeliate.Core.Data;
using Senti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 

namespace Adlite.Data 
{
  public partial interface ICountryManager 
  {

    List<Country> Load();
    List<Country> Load(IConnectionInfo connection);
    
    Country Load(string value, CountryIdentifier identifier);
    Country Load(IConnectionInfo connection, string value, CountryIdentifier identifier);

  }

  public partial class Country
  {
  }
}

