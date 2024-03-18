using System;
using System.Collections.Generic;
using System.Data;
using csharp_project_new.dbUtils;

namespace csharp_project_new.repository
{
    public static class DbUtils
    {
        private static IDbConnection instance = null;
    
        public static IDbConnection GetConnection(IDictionary<string, string> props)
        {
            if (instance == null || instance.State == ConnectionState.Closed)
            {
                instance = GetNewConnection(props);
                instance.Open();
            }
            return instance;
        }
    
        private static IDbConnection GetNewConnection(IDictionary<string,string> props)
        {
            return ConnectionFactory.GetInstance().CreateConnection(props);
        }
    }
}