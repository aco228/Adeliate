using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;
using Senti.Data.Sql;

namespace Adlite.Data.Sql
{
  /// <summary>
  /// This class provides database connection specific properties for performing operations.
  /// </summary>
  public class SqlConnectionInfo : SqlConnectionInfoBase
  {
    private DatabaseType _type = DatabaseType.Unknown;

    public DatabaseType Type { get { return this._type; } }

    /// <summary>
    /// The database instance this connection info was created for.
    /// </summary>
    public new Adlite.Data.Sql.Database Database
    {
      get
      {
        return base.Database as Adlite.Data.Sql.Database;
      }
    }

    /// <summary>
    /// Creates a new instance of SqlConnectionInfo.
    /// </summary>
    public SqlConnectionInfo(DatabaseType type) : base(false)
    {
      this._type = type;
      this.SetupTransactionContext();
    }

    /// <summary>
    /// The underlying connection info which is accessed instead of this instances fields.<br />
    /// When not null, this object acts as a proxy to the connection object.
    /// </summary>
    /// <param name="connection">The connection object which should be encapsulated by this instance.</param>
    public SqlConnectionInfo(IConnectionInfo connection, DatabaseType type)
      : base(false, connection)
    {
      this._type = type;
      this.SetupTransactionContext();
    }

    protected override IDatabase CreateDatabase()
    {
      return Adlite.Data.Sql.Database.Create(true, this.Type);
    }
  }
}

