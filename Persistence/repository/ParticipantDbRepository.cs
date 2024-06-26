using System;
using System.Collections.Generic;
using csharp_project.model;
using log4net;

namespace csharp_project.repository
{
    public class ParticipantDbRepository : IParticipantRepository
    {
        private static readonly ILog log = LogManager.GetLogger("SortingTaskDbRepository");
        private readonly IDictionary<string, string> props;

        public ParticipantDbRepository(IDictionary<string, string> props)
        {
            log.Info("creating ParticipantDbRepository ");
            this.props = props;
        }
        
        public int CountRegistrations(long id)
        {
            log.InfoFormat("entering CountRegistrations with value {0}", id);
            var con = DbUtils.GetConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select count(*) as count from registrations where id_participant = @id";

                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        var count = dataR.GetInt32(0);

                        log.InfoFormat("exiting CountRegistrations with value {0}", count);
                        return count;
                    }
                }
            }

            log.InfoFormat("exiting CountRegistrations with value {0}", null);
            return 0;
        }
        
        public Participant FindOneByNameAndAge(string firstName, string lastName, int age)
        {
            log.InfoFormat("entering FindOneByNameAndAge with values: firstName={0}, lastName={1}, age={2}", firstName, lastName, age);
            var con = DbUtils.GetConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select id from participants where first_name = @first_name and last_name = @last_name and age = @age";

                var paramFirstName = comm.CreateParameter();
                paramFirstName.ParameterName = "@first_name";
                paramFirstName.Value = firstName;
                comm.Parameters.Add(paramFirstName);

                var paramLastName = comm.CreateParameter();
                paramLastName.ParameterName = "@last_name";
                paramLastName.Value = lastName;
                comm.Parameters.Add(paramLastName);

                var paramAge = comm.CreateParameter();
                paramAge.ParameterName = "@age";
                paramAge.Value = age;
                comm.Parameters.Add(paramAge);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        var id = dataR.GetInt32(0);
                        var participant = new Participant(id, firstName, lastName, age);

                        log.InfoFormat("exiting FindOneByNameAndAge with value: {0}", participant);
                        return participant;
                    }
                }
            }

            log.InfoFormat("exiting FindOneByNameAndAge with value: null");
            return null;
        }


        public Participant FindOne(long id)
        {
            log.InfoFormat("entering FindOne with value {0}", id);
            var con = DbUtils.GetConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select first_name, last_name, age from participants where id = @id";

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
                        var age = dataR.GetInt32(2);

                        var participant = new Participant(id, firstName, lastName, age);

                        log.InfoFormat("exiting FindOne with value {0}", participant);
                        return participant;
                    }
                }
            }

            log.InfoFormat("exiting FindOne with value {0}", null);
            return null;
        }

        public IEnumerable<Participant> FindAll()
        {
            log.InfoFormat("entering FindAll");
            var con = DbUtils.GetConnection(props);
            IList<Participant> participants = new List<Participant>();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from participants";

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        var id = dataR.GetInt64(0);
                        var firstName = dataR.GetString(1);
                        var lastName = dataR.GetString(2);
                        var age = dataR.GetInt32(3);

                        var participant = new Participant(id, firstName, lastName, age);

                        participants.Add(participant);
                    }
                }
            }

            log.InfoFormat("exiting FindAll");
            return participants;
        }

        public void Add(Participant entity)
        {
            log.InfoFormat("entering Add with value {0}", entity);
            var con = DbUtils.GetConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "insert into participants values (@id, @first_name, @last_name, @age)";

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

                var paramAge = comm.CreateParameter();
                paramAge.ParameterName = "@age";
                paramAge.Value = entity.GetAge();
                comm.Parameters.Add(paramAge);

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
                comm.CommandText = "delete from participants where id = @id";

                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);

                var result = comm.ExecuteNonQuery();
                log.InfoFormat("exiting Delete with value {0}", result);
            }
        }

        public void Update(long id, Participant entity)
        {
            throw new NotImplementedException();
        }
    }
}