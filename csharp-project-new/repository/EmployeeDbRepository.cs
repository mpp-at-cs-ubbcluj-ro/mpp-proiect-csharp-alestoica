using System.Collections.Generic;
using csharp_project_new.model;
using log4net;

namespace csharp_project_new.repository
{
    public class EmployeeDbRepository : IEmployeeRepository
    {
        IDictionary<string, string> props;
        private static readonly ILog log = LogManager.GetLogger("SortingTaskDbRepository");
        
        public EmployeeDbRepository(IDictionary<string, string> props)
        {
            log.Info("creating EmployeeDbRepository ");
            this.props = props;
        }
        
        public Employee FindOne(long id)
        {
            log.InfoFormat("entering FindOne with value {0}", id);
            var con = DbUtils.GetConnection(props);
            
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select first_name, last_name, username, password from employees where id = @id";
                
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        var firstName = dataR.GetString(0);
                        var lastName = dataR.GetString(1);
                        var username = dataR.GetString(2);
                        var password = dataR.GetString(3);
                        
                        var employee = new Employee(id, firstName, lastName, username, password);
                        
                        log.InfoFormat("exiting FindOne with value {0}", employee);
                        return employee;
                    }
                }
            }
            log.InfoFormat("exiting FindOne with value {0}", null);
            return null;
        }

        public IEnumerable<Employee> FindAll()
        {
            log.InfoFormat("entering FindAll");
            var con = DbUtils.GetConnection(props);
            IList<Employee> employees = new List<Employee>();
            
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from employees";

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        var id = dataR.GetInt64(0);
                        var firstName = dataR.GetString(1);
                        var lastName = dataR.GetString(2);
                        var username = dataR.GetString(3);
                        var password = dataR.GetString(4);
                        
                        var employee = new Employee(id, firstName, lastName, username, password);
                        
                        employees.Add(employee);
                    }
                }
            }

            log.InfoFormat("exiting FindAll");
            return employees;
        }

        public void Add(Employee entity)
        {
            log.InfoFormat("entering Add with value {0}", entity);
            var con = DbUtils.GetConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "insert into employees values (@id, @first_name, @last_name, @username, @password)";
                
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = entity.Id;
                comm.Parameters.Add(paramId);

                var paramFirstName = comm.CreateParameter();
                paramFirstName.ParameterName = "@first_name";
                paramFirstName.Value = entity.GetFirstName();
                comm.Parameters.Add(paramFirstName);

                var paramLastName = comm.CreateParameter();
                paramLastName.ParameterName = "@last_name";
                paramLastName.Value = entity.GetLastName();
                comm.Parameters.Add(paramLastName);

                var paramUsername = comm.CreateParameter();
                paramUsername.ParameterName = "@username";
                paramUsername.Value = entity.GetUsername();
                comm.Parameters.Add(paramUsername);
                
                var paramPassword = comm.CreateParameter();
                paramPassword.ParameterName = "@password";
                paramPassword.Value = entity.GetPassword();
                comm.Parameters.Add(paramPassword);

                var result = comm.ExecuteNonQuery();
                log.InfoFormat("exiting Add with value {0}", result);
            }
        }

        public void Delete(long id)
        {
            log.InfoFormat("entering Delete with value {0}", id);
            var con = DbUtils.GetConnection(props);
            
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "delete from employees where id = @id";
                
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);
                
                var result = comm.ExecuteNonQuery();
                log.InfoFormat("exiting Delete with value {0}", result);
            }
        }

        public void Update(long id, Employee entity)
        {
            throw new System.NotImplementedException();
        }
    }
}