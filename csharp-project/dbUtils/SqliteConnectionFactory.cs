using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace csharp_project.dbUtils
{
    public class SqliteConnectionFactory : ConnectionFactory
    {
        public override IDbConnection CreateConnection(IDictionary<string, string> props)
        {
            // Mono Sqlite Connection
            // String connectionString = "URI=file:/Users/grigo/didactic/MPP/ExempleCurs/2017/database/tasks.db,Version=3";
            // String connectionString = props["ConnectionString"];
            // Console.WriteLine("SQLite ---Se deschide o conexiune la  ... {0}", connectionString);
            // return new SqliteConnection(connectionString);

            // Windows SQLite Connection, fisierul .db ar trebuie sa fie in directorul debug/bin
            // const string connectionString = "Data Source=tasks.db;Version=3";
            var connectionString = props["ConnectionString"];
            return new SQLiteConnection(connectionString);
        }
    }
}