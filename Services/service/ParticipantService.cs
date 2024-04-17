using System.Collections.Generic;
using csharp_project.model;
using csharp_project.repository;

namespace csharp_project.service
{
    public class ParticipantService
    {
        private readonly IParticipantRepository _repo;

        public ParticipantService(IParticipantRepository repo)
        {
            _repo = repo;
        }

        public int CountRegistrations(long id)
        {
            return _repo.CountRegistrations(id);
        }
        
        public Participant FindOneByNameAndAge(string firstName, string lastName, int age) {
            return _repo.FindOneByNameAndAge(firstName, lastName, age);
        }
        
        public Participant FindOne(long id) {
            return _repo.FindOne(id);
        }

        public IEnumerable<Participant> FindAll()
        {
            return _repo.FindAll();
        }

        public void Add(Participant entity)
        {
            _repo.Add(entity);
        }

        public void Delete(long id)
        {
            _repo.Delete(id);
        }

        public void Update(long id, Participant entity)
        {
            _repo.Update(id, entity);
        }
        
        public ICollection<Participant> GetAll()
        {
            return (ICollection<Participant>)_repo.FindAll();
        }
    }
}