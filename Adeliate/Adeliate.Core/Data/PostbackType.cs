using Senti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 

namespace Adlite.Data 
{
  public partial interface IPostbackTypeManager 
  {

    List<PostbackType> Load();
    List<PostbackType> Load(IConnectionInfo connection);

  }

  public partial class PostbackType
  {
  }
}

