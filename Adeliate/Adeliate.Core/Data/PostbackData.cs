using Senti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 

namespace Adlite.Data 
{
  public partial interface IPostbackDataManager 
  {


    List<PostbackData> Load(Click click);
    List<PostbackData> Load(IConnectionInfo connection, Click click);





  }

  public partial class PostbackData
  {
  }
}

