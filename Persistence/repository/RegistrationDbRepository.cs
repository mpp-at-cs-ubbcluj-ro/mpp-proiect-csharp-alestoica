using System;
using System.Collections.Generic;
using csharp_project.model;
using log4net;

namespace csharp_project.repository
{
    public class RegistrationDbRepository : IRegistrationRepository
    {
        private static readonly ILog log = LogManager.GetLogger("SortingTaskDbRepository");
        private readonly IDictionary<string, string> props;
        private readonly IParticipantRepository _participantRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAgeEventRepository _ageEventRepository;

        public RegistrationDbRepository(IDictionary<string, string> props, IParticipantRepository participantRepository, IEmployeeRepository employeeRepository, IAgeEventRepository ageEventRepository)
        {
            log.Info("creating RegistrationDbRepository ");
            this.props = props;
            _participantRepository = participantRepository;
            _employeeRepository = employeeRepository;
            _ageEventRepository = ageEventRepository;
        }

        public IEnumerable<Registration> FindByAgeEvent(AgeEvent ageEvent)
        {
            log.InfoFormat("entering FindByAgeEvent");
            var con = DbUtils.GetConnection(props);
            IList<Registration> registrations = new List<Registration>();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from registrations where id_age_event = @id";

                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ageEvent.Id;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        var id = dataR.GetInt32(0);
                        var idParticipant = dataR.GetInt32(1);
                        var idEmployee = dataR.GetInt32(3);

                        var participant = _participantRepository.FindOne(idParticipant);
                        var employee = _employeeRepository.FindOne(idEmployee);

                        var registration = new Registration(id, participant, ageEvent, employee);

                        registrations.Add(registration);
                    }
                }
            }

            log.InfoFormat("exiting FindByAgeEvent");
            return registrations;
        }
        
        public IEnumerable<Registration> FindByParticipant(Participant participant)
        {
            log.InfoFormat("entering FindByParticipant");
            var con = DbUtils.GetConnection(props);
            IList<Registration> registrations = new List<Registration>();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from registrations where id_participant = @id";

                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = participant.Id;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        var id = dataR.GetInt32(0);
                        var idAgeEvent = dataR.GetInt32(2);
                        var idEmployee = dataR.GetInt32(3);

                        var ageEvent = _ageEventRepository.FindOne(idAgeEvent);
                        var employee = _employeeRepository.FindOne(idEmployee);

                        var registration = new Registration(id, participant, ageEvent, employee);

                        registrations.Add(registration);
                    }
                }
            }

            log.InfoFormat("exiting FindByParticipant");
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
                        
                        var participant = _participantRepository.FindOne(idParticipant);
                        var ageEvent = _ageEventRepository.FindOne(idAgeEvent);
                        var employee = _employeeRepository.FindOne(idEmployee);

                        var registration = new Registration(id, participant, ageEvent, employee);

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
                        
                        var participant = _participantRepository.FindOne(idParticipant);
                        var ageEvent = _ageEventRepository.FindOne(idAgeEvent);
                        var employee = _employeeRepository.FindOne(idEmployee);

                        var registration = new Registration(id, participant, ageEvent, employee);

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
                comm.CommandText =
                    "insert into registrations values (@id, @id_participant, @id_age_event, @id_employee)";

                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = entity.Id;
                comm.Parameters.Add(paramId);

                var paramIdParticipant = comm.CreateParameter();
                paramIdParticipant.ParameterName = "@id_participant";
                paramIdParticipant.Value = entity.GetParticipant().Id;
                comm.Parameters.Add(paramIdParticipant);

                var paramIdAgeEvent = comm.CreateParameter();
                paramIdAgeEvent.ParameterName = "@id_age_event";
                paramIdAgeEvent.Value = entity.GetAgeEvent().Id;
                comm.Parameters.Add(paramIdAgeEvent);

                var paramIdEmployee = comm.CreateParameter();
                paramIdEmployee.ParameterName = "@id_employee";
                paramIdEmployee.Value = entity.GetEmployee().Id;
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
            throw new NotImplementedException();
        }
    }
}