using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adlite.IPRanges.Models
{
  public class InsertSqlCommands
  {
    public bool HasError = false;
    public int InsertCount = 0;
    public int Count = 0;
    public string Command = "";
    public object LOCK_OBJECT = new object();

    public InsertSqlCommands()
    {
      if(!File.Exists(Program.FILE_SQL_SAVE))
      {
        Console.WriteLine("There is no save file");
        this.HasError = true;
        return;
      }

      File.WriteAllText(Program.FILE_SQL_SAVE, "");
    }

    public void Insert(string sql)
    {
      this.Count++;
      this.Command += sql + Environment.NewLine;
    }

    public void Execute()
    {
      if (string.IsNullOrEmpty(this.Command))
        return;

      //Program.Database.Execute(this.Command);

      lock(LOCK_OBJECT)
      {
        File.AppendAllText(Program.FILE_SQL_SAVE, this.Command);

        this.InsertCount++;
        this.Count = 0;
        this.Command = "";

        long num = this.InsertCount * Program.QUERY_LIMIT;
        Console.WriteLine("Added " + num);
      }
    }
    
  }
}
