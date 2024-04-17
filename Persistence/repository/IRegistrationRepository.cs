using System.Collections.Generic;
using csharp_project.model;

namespace csharp_project.repository
{
    public interface IRegistrationRepository : IRepository<long, Registration>
    {
        IEnumerable<Registration> FindByAgeEvent(AgeEvent ageEvent);
        IEnumerable<Registration> FindByParticipant(Participant participant);
    }
}