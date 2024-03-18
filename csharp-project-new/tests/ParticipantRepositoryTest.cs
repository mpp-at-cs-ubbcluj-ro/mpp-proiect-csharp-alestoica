using System.Collections.Generic;
using System.Configuration;
using csharp_project_new.model;
using csharp_project_new.repository;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;


namespace csharp_project_new.tests
{
    [TestFixture]
    public class ParticipantRepositoryTest
    {
        private IDictionary<string, string> props = new SortedList<string, string>();

        [Test]
        public void FillDatabase()
        {
            props.Add("ConnectionString", GetConnectionStringByName("competitionTestDB"));
            
            var participantDbRepository = new ParticipantDbRepository(props);
            
            var participant1 = new Participant(1234, "John", "Doe", 12);
            var participant2 = new Participant(2345, "Jane", "Smith", 9);
            var participant3 = new Participant(3456, "Clare", "Rogers", 7);
            participantDbRepository.Add(participant1);
            participantDbRepository.Add(participant2);
            participantDbRepository.Add(participant3);
        }

        [Test]
        public void ClearDatabase()
        {
            // props.Add("ConnectionString", GetConnectionStringByName("competitionTestDB"));
            
            var participantDbRepository = new ParticipantDbRepository(props);
            
            foreach (var participant in participantDbRepository.FindAll())
            {
                participantDbRepository.Delete(participant.Id);
            }
        }

        [Test]
        public void TestFindOne()
        {
            // props.Add("ConnectionString", GetConnectionStringByName("competitionTestDB"));
            
            var participantDbRepository = new ParticipantDbRepository(props);
            
            Debug.Assert(1234 == participantDbRepository.FindOne(1234).Id);
            Debug.Assert("John" == participantDbRepository.FindOne(1234).GetFirstName());
            Debug.Assert("Doe" == participantDbRepository.FindOne(1234).GetLastName());
            Debug.Assert(12 == participantDbRepository.FindOne(1234).GetAge());
        }

        [Test]
        public void TestFindAll()
        {
            // props.Add("ConnectionString", GetConnectionStringByName("competitionTestDB"));
            
            var participantDbRepository = new ParticipantDbRepository(props);
            
            Debug.Assert(participantDbRepository.FindAll().Count() == 3);
            Debug.Assert(participantDbRepository.FindAll().First().Id == 1234);
        }

        [Test]
        public void TestAdd()
        {
            // props.Add("ConnectionString", GetConnectionStringByName("competitionTestDB"));
            
            var participantDbRepository = new ParticipantDbRepository(props);
            
            var participant = new Participant(4567, "Rachel", "Ross", 10);
            participantDbRepository.Add(participant);
            
            Debug.Assert(participantDbRepository.FindAll().Count() == 4);
            Debug.Assert(participantDbRepository.FindAll().Last().Id == 4567);
        }
        
        private static string GetConnectionStringByName(string name)
        {
            // Assume failure.
            string returnValue = null;

            // Look for the name in the connectionStrings section.
            var settings = ConfigurationManager.ConnectionStrings[name];

            // If found, return the connection string.
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }
    }
}