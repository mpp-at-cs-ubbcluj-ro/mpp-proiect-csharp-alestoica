using System;
using System.Collections.Generic;
using csharp_project.model;
using log4net;

namespace csharp_project.repository
{
    public class AgeEventDbRepository : IAgeEventRepository
    {
        private static readonly ILog log = LogManager.GetLogger("SortingTaskDbRepository");
        private readonly IDictionary<string, string> props;

        public AgeEventDbRepository(IDictionary<string, string> props)
        {
            log.Info("creating AgeEventDbRepository ");
            this.props = props;
        }

        public int CountParticipants(long id)
        {
            log.InfoFormat("entering CountParticipants with value {0}", id);
            var con = DbUtils.GetConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select count(*) as count from participants p inner join registrations r on p.id == r.id_participant inner join age_events ae on ae.id = r.id_age_event where ae.id == @id";

                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        var count = dataR.GetInt32(0);

                        log.InfoFormat("exiting CountParticipants with value {0}", count);
                        return count;
                    }
                }
            }

            log.InfoFormat("exiting CountParticipants with value {0}", null);
            return 0;
        }

        public AgeEvent FindByAgeGroupAndSportsEvent(string ageGroup, string sportsEvent)
        {
            log.InfoFormat("entering FindByAgeGroupAndSportsEvent");
            var con = DbUtils.GetConnection(props);
            IList<AgeEvent> ageEvents = new List<AgeEvent>();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText =
                    "select * from age_events where age_group = @age_group and sports_event = @sports_event";

                var paramAgeGroup = comm.CreateParameter();
                paramAgeGroup.ParameterName = "@age_group";
                paramAgeGroup.Value = ageGroup;
                comm.Parameters.Add(paramAgeGroup);

                var paramSportsEvent = comm.CreateParameter();
                paramSportsEvent.ParameterName = "@sports_event";
                paramSportsEvent.Value = sportsEvent;
                comm.Parameters.Add(paramSportsEvent);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        var id = dataR.GetInt64(0);
                        var ageGroupEnum = (AgeGroup)Enum.Parse(typeof(AgeGroup), dataR.GetString(1));
                        var sportsEventEnum = (SportsEvent)Enum.Parse(typeof(SportsEvent), dataR.GetString(2));

                        var ageEvent = new AgeEvent(id, ageGroupEnum, sportsEventEnum);

                        log.InfoFormat("exiting FindByAgeGroupAndSportsEvent with value {0}", ageEvent);
                        return ageEvent;
                    }
                }
            }

            log.InfoFormat("exiting FindByAgeGroupAndSportsEvent with value {0}", null);
            return null;
        }

        public AgeEvent FindOne(long id)
        {
            log.InfoFormat("entering FindOne with value {0}", id);
            var con = DbUtils.GetConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select age_group, sports_event from age_events where id = @id";

                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        var ageGroup = (AgeGroup)Enum.Parse(typeof(AgeGroup), dataR.GetString(0));
                        var sportsEvent = (SportsEvent)Enum.Parse(typeof(SportsEvent), dataR.GetString(1));

                        var ageEvent = new AgeEvent(id, ageGroup, sportsEvent);

                        log.InfoFormat("exiting FindOne with value {0}", ageEvent);
                        return ageEvent;
                    }
                }
            }

            log.InfoFormat("exiting FindOne with value {0}", null);
            return null;
        }

        public IEnumerable<AgeEvent> FindAll()
        {
            log.InfoFormat("entering FindAll");
            var con = DbUtils.GetConnection(props);
            IList<AgeEvent> ageEvents = new List<AgeEvent>();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from age_events";

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        var id = dataR.GetInt64(0);
                        var ageGroup = (AgeGroup)Enum.Parse(typeof(AgeGroup), dataR.GetString(1));
                        var sportsEvent = (SportsEvent)Enum.Parse(typeof(SportsEvent), dataR.GetString(2));

                        var ageEvent = new AgeEvent(id, ageGroup, sportsEvent);

                        ageEvents.Add(ageEvent);
                    }
                }
            }

            log.InfoFormat("exiting FindAll");
            return ageEvents;
        }

        public void Add(AgeEvent entity)
        {
            log.InfoFormat("entering Add with value {0}", entity);
            var con = DbUtils.GetConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "insert into age_events values (@id, @age_group, @sports_event)";

                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = entity.Id;
                comm.Parameters.Add(paramId);

                var paramAgeGroup = comm.CreateParameter();
                paramAgeGroup.ParameterName = "@age_group";
                paramAgeGroup.Value = entity.GetSportsEvent();
                comm.Parameters.Add(paramAgeGroup);

                var paramSportsEvent = comm.CreateParameter();
                paramSportsEvent.ParameterName = "@sports_event";
                paramSportsEvent.Value = entity.GetAgeGroup();
                comm.Parameters.Add(paramSportsEvent);

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
                comm.CommandText = "delete from age_events where id = @id";

                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);

                var result = comm.ExecuteNonQuery();
                log.InfoFormat("exiting Delete with value {0}", result);
            }
        }

        public void Update(long id, AgeEvent entity)
        {
            throw new NotImplementedException();
        }
    }
}