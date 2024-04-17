using System.Collections.Generic;
using csharp_project.model;
using csharp_project.repository;

namespace csharp_project.service
{
    public class RegistrationService
    {
        private readonly IRegistrationRepository _repo;

        public RegistrationService(IRegistrationRepository repo)
        {
            _repo = repo;
        }

        public ICollection<Registration> FindByAgeEvent(AgeEvent ageEvent)
        {
            return (ICollection<Registration>)_repo.FindByAgeEvent(ageEvent);
        }

        public ICollection<Registration> FindByParticipant(Participant participant)
        {
            return (ICollection<Registration>)_repo.FindByParticipant(participant);
        }

        public Registration FindOne(long id)
        {
            return _repo.FindOne(id);
        }

        public IEnumerable<Registration> FindAll()
        {
            return _repo.FindAll();
        }

        public void Add(Registration entity)
        {
            _repo.Add(entity);
        }

        public void Delete(long id)
        {
            _repo.Delete(id);
        }

        public void Update(long id, Registration entity)
        {
            _repo.Update(id, entity);
        }

        public ICollection<Registration> GetAll()
        {
            return (ICollection<Registration>)_repo.FindAll();
        }
    }
}