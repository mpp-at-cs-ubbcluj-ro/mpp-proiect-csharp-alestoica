using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using csharp_project.model;

namespace csharp_project.service
{
    public class Service : IService
    {
        private readonly ParticipantService _participantService;
        private readonly EmployeeService _employeeService;
        private readonly AgeEventService _ageEventService;
        private readonly RegistrationService _registrationService;
        private readonly IDictionary <long, IObserver> _loggedClients;

        public Service(ParticipantService participantService, EmployeeService employeeService, AgeEventService ageEventService, RegistrationService registrationService)
        {
            _participantService = participantService;
            _employeeService = employeeService;
            _ageEventService = ageEventService;
            _registrationService = registrationService;
            _loggedClients = new Dictionary<long, IObserver>();
        }

        public Employee Login(Employee employee, IObserver client)
        {
            var foundEmployee = FindOneByUsernameAndPassword(employee.GetUsername(), employee.GetPassword());

            if (foundEmployee != null)
            {
                if (_loggedClients.ContainsKey(foundEmployee.Id))
                    throw new Exception("Employee already logged in!");
                _loggedClients.Add(foundEmployee.Id, client);
            }
            else
            {
                throw new Exception("This account doesn't exist!");
            }

            return foundEmployee;
        }

        public void Logout(long id)
        {
            var localClient = _loggedClients[id];
            /*var localClient = _loggedClients.Remove(key: id);*/

            if (localClient ==  null)
                throw new Exception("Employee " + id + " is not logged in.");

            _loggedClients.Remove(id);
        }

        private void NotifyAddRegistration(Registration registration)
        {
            foreach (var client in _loggedClients)
            {
                Task.Run(() => client.Value.NotifyAddRegistration(registration));
            }
        }
        
        private void NotifyAddParticipant(Participant participant)
        {
            foreach (var client in _loggedClients)
            {
                Task.Run(() => client.Value.NotifyAddParticipant(participant));
            }
        }

        public Employee FindOneByUsernameAndPassword(string username, string password)
        {
            return _employeeService.FindOneByUsernameAndPassword(username, password);
        }

        public ICollection<AgeEvent> GetAllAgeEvents()
        {
            return _ageEventService.GetAll();
        }

        public ICollection<Participant> GetAllParticipants()
        {
            return _participantService.GetAll();
        }

        public ICollection<Registration> FindByAgeEvent(AgeEvent ageEvent)
        {
            return _registrationService.FindByAgeEvent(ageEvent);
        }

        public ICollection<Registration> FindByParticipant(Participant participant)
        {
            return _registrationService.FindByParticipant(participant);
        }

        public Participant FindOne(long id)
        {
            return _participantService.FindOne(id);
        }

        public Participant FindOneByNameAndAge(string firstName, string lastName, int age)
        {
            return _participantService.FindOneByNameAndAge(firstName, lastName, age);
        }

        public AgeEvent FindByAgeGroupAndSportsEvent(string ageGroup, string sportsEvent)
        {
            return _ageEventService.FindByAgeGroupAndSportsEvent(ageGroup, sportsEvent);
        }

        public void AddRegistration(Registration entity)
        {
            _registrationService.Add(entity);
            NotifyAddRegistration(entity);
        }

        public void AddParticipant(Participant entity)
        {
            _participantService.Add(entity);
            NotifyAddParticipant(entity);
        }
        
        public int CountParticipants(AgeEvent ageEvent)
        {
            return _ageEventService.CountParticipants(ageEvent.Id);
        }

        public int CountRegistrations(Participant participant)
        {
            return _participantService.CountRegistrations(participant.Id);
        }
    }
}