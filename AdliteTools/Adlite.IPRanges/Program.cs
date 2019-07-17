using Adlite.Direct.Data;
using Adlite.IPRanges.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adlite.IPRanges
{
  class Program
  {

    /*
      sqlcmd -S '192.168.38.11' -U 'sa' -P 'j9L#1_dUk93.' -d 'Adlite' -i SAVE.SQL
    */

    public static AdliteDirect Database = AdliteDirect.Instance;
    public static List<CountryModel> Countries = null;
    public static string FILE_LOCATION = @"C:\Users\ako\Desktop\session\DATA.CSV";
    public static string FILE_SQL_SAVE = @"C:\Users\ako\Desktop\session\SAVE.SQL";
    public static string TABLE_NAME = "IPCountryMap";
    public static int QUERY_LIMIT = 10000;
    public static InsertSqlCommands Commands = new InsertSqlCommands();

    static void Main(string[] args)
    {
      if (Program.Commands.HasError)
      {
        Console.ReadKey();
        return;
      }

      if (!File.Exists(Program.FILE_LOCATION))
      {
        Console.WriteLine("There is no DATA file");
        Console.ReadKey();
        return;
      }
      
      Program.Countries = CountryModel.LoadAll();
      
      Parallel.ForEach(File.ReadLines(Program.FILE_LOCATION),
        new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, (line, _, lineNumber) =>
      {
        LineModel lineModel = new LineModel(line);
        if (Program.Commands.Count > Program.QUERY_LIMIT)
          Program.Commands.Execute();
      });

      Program.Commands.Execute();

      Console.WriteLine("FINISHED");
      Console.WriteLine("");
      Console.ReadKey();
    }



    public static void DuplicateTable()
    {
      string sql = "";
      string guid = Guid.NewGuid().ToString().Replace("-", string.Empty);
      Program.TABLE_NAME = "IPCountryMap_" + guid;
      string pkName = "PK_IPCountryMap_" + guid;

      #region # SQL #

      sql = string.Format(@"
        CREATE TABLE [core].[{0}](
	        [IPCountryMapID] [int] IDENTITY(1,1) NOT NULL,
	        [FromAddress] [bigint] NOT NULL,
	        [ToAddress] [bigint] NOT NULL,
	        [CountryCode] [nvarchar](2) NOT NULL,
	        [Region] [nvarchar](56) NOT NULL,
	        [City] [nvarchar](56) NOT NULL,
	        [Latitude] [decimal](18, 0) NULL,
	        [Longtitude] [decimal](18, 0) NULL,
	        [ISP_Name] [nvarchar](512) NOT NULL,
	        [DomainName] [nvarchar](256) NOT NULL,
	        [MCC] [int] NULL,
	        [MNC] [int] NULL,
	        [MobileBrand] [nvarchar](256) NOT NULL,
	        [UsageType] [nvarchar](256) NOT NULL,
	        [CountryID] [int] NOT NULL,
	        [MobileOperatorID] [int] NULL,
         CONSTRAINT [{1}] PRIMARY KEY CLUSTERED 
        (
	        [IPCountryMapID] ASC
        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ) ON [PRIMARY]", Program.TABLE_NAME, pkName);

      #endregion
      
      Program.Database.Execute(sql);
      Console.WriteLine("Duplicate table created");
      Console.WriteLine("");
    }

    public static void ReplaceTables()
    {
      Program.Database.Execute("DROP TABLE Adlite.core.IPCountryMap");
      Program.Database.Execute(string.Format("exec Adlite..sp_rename 'core.{0}', 'IPCountryMap'", Program.TABLE_NAME));

      Console.WriteLine("Duplicate table removed");
      Console.WriteLine("");

    }

  }
}
