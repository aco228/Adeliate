using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adlite.Data.Sql
{
  /// <summary>
  /// This class is a basic implementation for all managers storing/retrieving data from Microsoft SQL Server.<br />
  /// It implements IDataManager and provides some abstract methods having a SQL-specific signature<br />
  /// (taking SqlConnectionInfo objects instead of ConnectionInfo objects).
  /// </summary>
  /// <typeparam name="DataType">Type of the data class which is subject of data manipulation / retrieval.</typeparam>
  public abstract class SqlManagerBase<DataType> : Senti.Data.Sql.SqlManagerBase<DataType>
    where DataType : class, IAdliteObject, new()
  {
    public abstract DatabaseType Type { get; }

    protected override Senti.Data.Sql.ISqlConnectionInfo CreateConnectionInfo(Senti.Data.IConnectionInfo connection)
    {
      Senti.Data.IDatabase db = connection.InfoObject as Senti.Data.IDatabase;
      if (db == null)
      {
        db = this.CreateDatabase();
        connection.InfoObject = db;
      }
      return db.CreateConnectionInfo(connection) as Senti.Data.Sql.ISqlConnectionInfo;
    }

    protected override Senti.Data.IDatabase CreateDatabase()
    {
      return Database.Create(true, this.Type);
    }
  }
}

