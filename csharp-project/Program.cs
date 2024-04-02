using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Instrumentation;
using System.Windows.Forms;
using csharp_project.repository;
using csharp_project.service;

namespace csharp_project
{
    static class Program
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
            
            var service = new Service(participantServ, employeeServ, ageEventServ, registrationServ);
            
            Application.Run(new LogInForm(service));
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