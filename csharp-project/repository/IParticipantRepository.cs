using csharp_project.model;

namespace csharp_project.repository
{
    public interface IParticipantRepository : IRepository<long, Participant>
    {
        Participant FindOneByNameAndAge(string firstName, string lastName, int age);
    }
}