using System.Collections.Generic;
using csharp_project.model;
using csharp_project.repository;

namespace csharp_project.service
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeService(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public Employee FindOneByUsernameAndPassword(string username, string password)
        {
            return _repo.FindOneByUsernameAndPassword(username, password);
        }

        public Employee FindOne(long id)
        {
            return _repo.FindOne(id);
        }

        public IEnumerable<Employee> FindAll()
        {
            return _repo.FindAll();
        }

        public void Add(Employee entity)
        {
            _repo.Add(entity);
        }

        public void Delete(long id)
        {
            _repo.Delete(id);
        }

        public void Update(long id, Employee entity)
        {
            _repo.Update(id, entity);
        }
        
        public ICollection<Employee> GetAll()
        {
            return (ICollection<Employee>)_repo.FindAll();
        }
    }
}