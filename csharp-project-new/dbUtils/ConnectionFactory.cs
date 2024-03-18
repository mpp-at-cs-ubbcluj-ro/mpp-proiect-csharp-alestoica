using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace csharp_project_new.dbUtils
{
    public abstract class ConnectionFactory
    {
        private static ConnectionFactory instance;
    
        protected ConnectionFactory()
        {
        }
    
        public static ConnectionFactory GetInstance()
        {
            if (instance == null)
            {
                var assem = Assembly.GetExecutingAssembly();
                var types = assem.GetTypes();
            
                foreach (var type in types)
                {
                    if (type.IsSubclassOf(typeof(ConnectionFactory)))
                        instance = (ConnectionFactory)Activator.CreateInstance(type);
                }
            }
            return instance;
        }
    
        public abstract IDbConnection CreateConnection(IDictionary<string, string> props);
    }
}