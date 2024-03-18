using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using csharp_project_new.model;
using csharp_project_new.repository;
using csharp_project_new.tests;
using log4net.Config;

namespace csharp_project_new
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // XmlConfigurator.Configure(new System.IO.FileInfo(args[0]));
            
            
            IDictionary<string, string> props = new SortedList<string, string>();
            props.Add("ConnectionString", GetConnectionStringByName("competitionDB"));

            var participantDbRepository = new ParticipantDbRepository(props);
            
            Console.WriteLine("FindOne: ");
            
            Console.WriteLine(participantDbRepository.FindOne(1234));

            Console.WriteLine("FindAll: ");

            foreach (var participant in participantDbRepository.FindAll())
            {
                Console.WriteLine(participant);
            }
            
            Console.WriteLine("Add: ");

            var participantAdd = new Participant(1111, "Rachel", "Zane", 11);
            participantDbRepository.Add(participantAdd);
            
            foreach (var participant in participantDbRepository.FindAll())
            {
                Console.WriteLine(participant);
            }
            
            participantDbRepository.Delete(1111);

            Console.WriteLine("-------------------------------------------------------------------------------------");

            var employeeDbRepository = new EmployeeDbRepository(props);
            
            Console.WriteLine("FindOne: ");
            
            Console.WriteLine(employeeDbRepository.FindOne(1234));

            Console.WriteLine("FindAll: ");
            
            foreach (var employee in employeeDbRepository.FindAll())
            {
                Console.WriteLine(employee);
            }
            
            Console.WriteLine("Add: ");

            var employeeAdd = new Employee(1111, "Rachel", "Zane", "rachel_zane", "password");
            employeeDbRepository.Add(employeeAdd);
            
            foreach (var employee in employeeDbRepository.FindAll())
            {
                Console.WriteLine(employee);
            }
            
            employeeDbRepository.Delete(1111);

            Console.WriteLine("-------------------------------------------------------------------------------------");

            var ageEventDbRepository = new AgeEventDbRepository(props);
            
            Console.WriteLine("FindByAgeGroupAndSportsEvent: ");
            
            foreach (var ageEvent in ageEventDbRepository.FindByAgeGroupAndSportsEvent("Group68Years", "Meters50"))
            {
                Console.WriteLine(ageEvent);
            }

            Console.WriteLine("FindOne: ");
            
            Console.WriteLine(ageEventDbRepository.FindOne(1));

            Console.WriteLine("FindAll: ");
            
            foreach (var ageEvent in ageEventDbRepository.FindAll())
            {
                Console.WriteLine(ageEvent);
            }

            Console.WriteLine("-------------------------------------------------------------------------------------");

            var registrationDbRepository = new RegistrationDbRepository(props);
            
            Console.WriteLine("FindByAgeEvent: ");
            
            foreach (var registration in registrationDbRepository.FindByAgeEvent(1))
            {
                Console.WriteLine(registration);
            }
            
            Console.WriteLine("FindOne: ");
            
            Console.WriteLine(registrationDbRepository.FindOne(1001));

            Console.WriteLine("FindAll: ");
            
            foreach (var registration in registrationDbRepository.FindAll())
            {
                Console.WriteLine(registration);
            }
            
            Console.WriteLine("Add: ");

            var registrationAdd = new Registration(1111, 56789, 2, 5432);
            registrationDbRepository.Add(registrationAdd);
            
            foreach (var registration in registrationDbRepository.FindAll())
            {
                Console.WriteLine(registration);
            }
            
            registrationDbRepository.Delete(1111);
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