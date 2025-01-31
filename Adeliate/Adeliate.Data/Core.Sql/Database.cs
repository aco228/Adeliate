using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adlite.Data.Sql
{
  public enum DatabaseType
  {
		Unknown = 0,
		Adlite = 1
  }


  /// <summary>
  /// This class provides Cashflow specific implementation for database connectivity.
  /// </summary>
  public class Database : Senti.Data.Sql.SqlDatabase<SqlConnectionInfo>
  {
		private static readonly string AdliteConnectionString = @"Data Source=192.168.38.11;Initial Catalog=Adlite;uid=sa;pwd=j9L#1_dUk93.;";

    private DatabaseType _type = DatabaseType.Unknown;

    public DatabaseType Type { get { return this._type; } }

    /// <summary>
    /// Creates a new (closed) connection and the database object attached to it.
    /// </summary>
    /// <returns>Database object attached to the new closed connection</returns>
    public static Database Create(DatabaseType type)
    {
      return Database.Create(false, type);
    }

    /// <summary>
    /// Creates a new connection and the database object attached to it.
    /// </summary>
    /// <param name="openConnection">Indicates whether the connection should be opened or not.</param>
    /// <returns>Database object attached to the new connection.</returns>
    public static Database Create(bool openConnection, DatabaseType type)
    {
      ConnectionStringSettings cnSettings = ConfigurationManager.ConnectionStrings[type.ToString()];
      if (cnSettings == null ||
          string.IsNullOrEmpty(cnSettings.ConnectionString))
        throw new InvalidOperationException("No connectionstring with name " + type.ToString() + " provided in connection string section.");
      return new Database(System.Configuration.ConfigurationManager.ConnectionStrings[type.ToString()].ConnectionString, openConnection, type);
    }

    /// <summary>
    /// Converts an array of bytes to a string represting the array values as hexadecimal values (format used is "X2")
    /// </summary>
    /// <param name="bytes">The array of bytes which should be converted.</param>
    /// <returns>A string representing the array of bytes in hexadecimal values.</returns>
    public static string BytesToHex(byte[] bytes)
    {
      System.Text.StringBuilder sb = new System.Text.StringBuilder(bytes.Length * 2);
      for (int i = 0; i <= bytes.Length - 1; i += 1)
      {
        sb.Append(bytes[i].ToString("X2"));
      }
      return sb.ToString();
    }

    /// <summary>
    /// Initializes a database instance and creates a connection attached to it.
    /// </summary>
    /// <param name="connectionString">The connection string used to initialize the connection.</param>
    /// <param name="openConnection">A value indicating whether the connection should be opened.</param>
    protected Database(string connectionString, bool openConnection, DatabaseType type)
      : base(connectionString, openConnection)
    {
      this._type = type;
    }

    /// <summary>
    /// Creates all tables, stored procedures, views and other database entities required for Cashflow database.
    /// </summary>
    /// <returns>A result that indicates the success of the operation or any errors that occured.</returns>
    public override Senti.Data.DatabaseInstallationResult Install()
    {
      return new Senti.Data.DatabaseInstallationResult();
    }

    public override SqlConnectionInfo CreateConnectionInfo(Senti.Data.IConnectionInfo connection)
    {
      return new SqlConnectionInfo(connection, this.Type);
    }
  }
}

