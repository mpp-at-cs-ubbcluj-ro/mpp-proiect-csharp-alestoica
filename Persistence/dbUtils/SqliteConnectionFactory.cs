using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace csharp_project.dbUtils
{
    public class SqliteConnectionFactory : ConnectionFactory
    {
        public override IDbConnection CreateConnection(IDictionary<string, string> props)
        {
            var connectionString = props["ConnectionString"];
            return new SQLiteConnection(connectionString);
        }
    }
}