using System.Collections.Generic;
using csharp_project.model;

namespace csharp_project.service
{
    public interface IService
    {
        Employee Login(Employee employee, IObserver client);
        
        void Logout(long id);
        Employee FindOneByUsernameAndPassword(string username, string password);

        ICollection<AgeEvent> GetAllAgeEvents();

        ICollection<Participant> GetAllParticipants();

        ICollection<Registration> FindByAgeEvent(AgeEvent ageEvent);

        ICollection<Registration> FindByParticipant(Participant participant);

        Participant FindOne(long id);

        Participant FindOneByNameAndAge(string firstName, string lastName, int age);

        AgeEvent FindByAgeGroupAndSportsEvent(string ageGroup, string sportsEvent);

        void AddRegistration(Registration entity);

        void AddParticipant(Participant entity);
        
        int CountParticipants(AgeEvent ageEvent);

        int CountRegistrations(Participant participant);
    }
}