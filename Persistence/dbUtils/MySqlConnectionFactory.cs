using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;

namespace csharp_project.dbUtils
{
    /*public class MySqlConnectionFactory : ConnectionFactory
    {
        public override IDbConnection createConnection(IDictionary<string,string> props)
        {
            // MySql Connection
            // String connectionString = "Database=mpp;" +
            //							"Data Source=localhost;" +
            //							"User id=test;" +
            //							"Password=passtest;";
            var connectionString = props["ConnectionString"];
            Console.WriteLine("MySql: a connection is being created to --> {0}", connectionString);
            return new MySqlConnection(connectionString);
        }
    }*/
}