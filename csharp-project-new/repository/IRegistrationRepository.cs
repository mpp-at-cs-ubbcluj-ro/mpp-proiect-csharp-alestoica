using System.Collections.Generic;
using csharp_project_new.model;

namespace csharp_project_new.repository
{
    public interface IRegistrationRepository : IRepository<long, Registration>
    {
        IEnumerable<Registration> FindByAgeEvent(long idAgeEvent);
    }
}