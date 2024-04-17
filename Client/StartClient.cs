using System;
using System.Collections.Generic;
using System.Windows.Forms;
using csharp_project;
using csharp_project.repository;
using csharp_project.service;
using System.Configuration;
using Networking.rpcProtocol;

namespace Client
{
    static class StartClient
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
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
            
            /*var service = new Service(participantServ, employeeServ, ageEventServ, registrationServ);*/
            var service = new Proxy("127.0.0.1", 55556);
            
            Application.Run(new LogInForm(service));
        }
        
        private static string GetConnectionStringByName(string name)
        {
            string returnValue = null;
            
            var settings = ConfigurationManager.ConnectionStrings[name];
            
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }
    }
}