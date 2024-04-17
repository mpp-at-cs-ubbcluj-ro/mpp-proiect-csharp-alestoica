using System.Collections.Generic;
using csharp_project.model;
using csharp_project.repository;

namespace csharp_project.service
{
    public class AgeEventService
    {
        private readonly IAgeEventRepository _repo;

        public AgeEventService(IAgeEventRepository repo)
        {
            _repo = repo;
        }

        public int CountParticipants(long id)
        {
            return _repo.CountParticipants(id);
        }

        public AgeEvent FindByAgeGroupAndSportsEvent(string ageGroup, string sportsEvent)
        {
            return _repo.FindByAgeGroupAndSportsEvent(ageGroup, sportsEvent);
        }

        public AgeEvent FindOne(long id)
        {
            return _repo.FindOne(id);
        }

        public IEnumerable<AgeEvent> FindAll()
        {
            return _repo.FindAll();
        }

        public void Add(AgeEvent entity)
        {
            _repo.Add(entity);
        }

        public void Delete(long id)
        {
            _repo.Delete(id);
        }

        public void Update(long id, AgeEvent entity)
        {
            _repo.Update(id, entity);
        }

        public ICollection<AgeEvent> GetAll()
        {
            return (ICollection<AgeEvent>)_repo.FindAll();
        }
    }
}