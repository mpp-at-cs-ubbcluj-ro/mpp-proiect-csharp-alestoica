using System;
using System.Collections.Generic;
using System.Configuration;
using csharp_project.repository;
using csharp_project.service;
using Networking.utils;

namespace Server
{
    internal class StartServer
    {
        private const int DefaultPort = 55556;
        private const string DefaultIp = "127.0.0.1";

        public static void Main(string[] args)
        {
            Console.WriteLine("Reading properties from app.config ...");
            
            var port = DefaultPort;
            var ip = DefaultIp;
            var serverPort = ConfigurationManager.AppSettings["port"];
            
            if (serverPort == null)
                Console.WriteLine("Port property not set. Using default value " + DefaultPort);
            else
            {
                var result = Int32.TryParse(serverPort, out port);
                
                if (!result)
                {
                    Console.WriteLine("Port property not a number. Using default value " + DefaultPort);
                    port = DefaultPort;
                }
            }
            
            var serverIp = ConfigurationManager.AppSettings["ip"];
           
            if (serverIp == null)
            {
                Console.WriteLine("Port property not set. Using default value " + DefaultIp);
            }
            
            IDictionary<string, string> props = new SortedList<string, string>();
            props.Add("ConnectionString", GetConnectionStringByName("competitionDB"));
            
            var participantRepo = new ParticipantDbRepository(props);
            var participantServ = new ParticipantService(participantRepo);
            
            var employeeRepo = new EmployeeDbRepository(props);
            var employeeServ = new EmployeeService(employeeRepo);
            
            var ageEventRepo = new AgeEventDbRepository(props);
            var ageEventServ = new AgeEventService(ageEventRepo);
            
            var registrationRepo = new RegistrationDbRepository(props, participantRepo, employeeRepo, ageEventRepo);
            var registrationServ = new RegistrationService(registrationRepo);
            
            var service = new Service(participantServ, employeeServ, ageEventServ, registrationServ);

            /*var server = new ConcurrentServer(ip, port, service);*/
            var server = new ProtoConcurrentServer(ip, port, service);
            server.Start();
            Console.WriteLine("Server started ...");
        }
        
        static string GetConnectionStringByName(string name)
        {
            string returnValue = null;
            
            var settings = ConfigurationManager.ConnectionStrings[name];
            
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }
    }
}