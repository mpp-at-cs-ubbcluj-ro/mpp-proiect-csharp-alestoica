using System.Collections.Generic;
using csharp_project_new.model;
using log4net;

namespace csharp_project_new.repository
{
    public class RegistrationDbRepository : IRegistrationRepository
    {
        IDictionary<string, string> props;
        private static readonly ILog log = LogManager.GetLogger("SortingTaskDbRepository");

        public RegistrationDbRepository(IDictionary<string, string> props)
        {
            log.Info("creating RegistrationDbRepository ");
            this.props = props;
        }

        public IEnumerable<Registration> FindByAgeEvent(long idAgeEvent)
        {
            log.InfoFormat("entering FindByAgeEvent");
            var con = DbUtils.GetConnection(props);
            IList<Registration> registrations = new List<Registration>();
            
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from registrations where id_age_event = @id";
                
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = idAgeEvent;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        var id = dataR.GetInt32(0);
                        var idParticipant = dataR.GetInt32(1);
                        var idEmployee = dataR.GetInt32(3);
                        
                        var registration = new Registration(id, idParticipant, idAgeEvent, idEmployee);
                        
                        registrations.Add(registration);
                    }
                }
            }

            log.InfoFormat("exiting FindByAgeEvent");
            return registrations;
        }
        
        public Registration FindOne(long id)
        {
            log.InfoFormat("entering FindOne with value {0}", id);
            var con = DbUtils.GetConnection(props);
            
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select id_participant, id_age_event, id_employee from registrations where id = @id";
                
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        var idParticipant = dataR.GetInt32(0);
                        var idAgeEvent = dataR.GetInt32(1);
                        var idEmployee = dataR.GetInt32(2);
                        
                        var registration = new Registration(id, idParticipant, idAgeEvent, idEmployee);
                        
                        log.InfoFormat("exiting FindOne with value {0}", registration);
                        return registration;
                    }
                }
            }
            log.InfoFormat("exiting FindOne with value {0}", null);
            return null;
        }

        public IEnumerable<Registration> FindAll()
        {
            log.InfoFormat("entering FindAll");
            var con = DbUtils.GetConnection(props);
            IList<Registration> registrations = new List<Registration>();
            
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from registrations";

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        var id = dataR.GetInt64(0);
                        var idParticipant = dataR.GetInt32(1);
                        var idAgeEvent = dataR.GetInt32(2);
                        var idEmployee = dataR.GetInt32(3);
                        
                        var registration = new Registration(id, idParticipant, idAgeEvent, idEmployee);
                        
                        registrations.Add(registration);
                    }
                }
            }

            log.InfoFormat("exiting FindAll");
            return registrations;
        }

        public void Add(Registration entity)
        {
            log.InfoFormat("entering Add with value {0}", entity);
            var con = DbUtils.GetConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "insert into registrations values (@id, @id_participant, @id_age_event, @id_employee)";
                
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = entity.Id;
                comm.Parameters.Add(paramId);

                var paramIdParticipant = comm.CreateParameter();
                paramIdParticipant.ParameterName = "@id_participant";
                paramIdParticipant.Value = entity.GetIdParticipant();
                comm.Parameters.Add(paramIdParticipant);

                var paramIdAgeEvent = comm.CreateParameter();
                paramIdAgeEvent.ParameterName = "@id_age_event";
                paramIdAgeEvent.Value = entity.GetIdAgeEvent();
                comm.Parameters.Add(paramIdAgeEvent);

                var paramIdEmployee = comm.CreateParameter();
                paramIdEmployee.ParameterName = "@id_employee";
                paramIdEmployee.Value = entity.GetIdEmployee();
                comm.Parameters.Add(paramIdEmployee);

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
                comm.CommandText = "delete from registrations where id = @id";
                
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);
                
                var result = comm.ExecuteNonQuery();
                log.InfoFormat("exiting Delete with value {0}", result);
            }
        }

        public void Update(long id, Registration entity)
        {
            throw new System.NotImplementedException();
        }
    }
}