using Adlite.Direct.Data;
using Adlite.IPInserter.Models;
using Direct.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adlite.IPInserter
{
  static class Program
  {

    public static IPDataDirect IPDataDatabase = IPDataDirect.Instance;
    public static AdliteDirect AdliteDatabase = AdliteDirect.Instance;
    public static List<CountryModel> Countries = null;
    public static DirectContainer MobileOperatorTable = null;
    public static DirectContainer CountryTable = null;
    public static string FILE_MNO_LOCATION = "";
    public static string FILE_COUNTRY_LOCATION = "";
    public static int QUERY_LIMIT = 100000;
    public static ulong INSERTED_LIMIT = 0;
    public static int CURRENT_LIMIT = 0;

    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new Form1());
    }


  }
}
