using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace csharp_project.dbUtils
{
    public abstract class ConnectionFactory
    {
        private static ConnectionFactory _instance;

        public static ConnectionFactory GetInstance()
        {
            if (_instance == null)
            {
                var assem = Assembly.GetExecutingAssembly();
                var types = assem.GetTypes();

                foreach (var type in types)
                    if (type.IsSubclassOf(typeof(ConnectionFactory)))
                        _instance = (ConnectionFactory)Activator.CreateInstance(type);
            }

            return _instance;
        }

        public abstract IDbConnection CreateConnection(IDictionary<string, string> props);
    }
}