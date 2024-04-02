using csharp_project.model;

namespace csharp_project.repository
{
    public interface IEmployeeRepository : IRepository<long, Employee>
    {
        Employee FindOneByUsernameAndPassword(string username, string password);
    }
}