using System.Collections.Generic;
using csharp_project.model;

namespace csharp_project.service
{
    public class Service : IService
    {
        private readonly ParticipantService _participantService;
        private readonly EmployeeService _employeeService;
        private readonly AgeEventService _ageEventService;
        private readonly RegistrationService _registrationService;

        public Service(ParticipantService participantService, EmployeeService employeeService, AgeEventService ageEventService, RegistrationService registrationService)
        {
            _participantService = participantService;
            _employeeService = employeeService;
            _ageEventService = ageEventService;
            _registrationService = registrationService;
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
        }

        public void AddParticipant(Participant entity)
        {
            _participantService.Add(entity);
        }
    }
}