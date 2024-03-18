using System.Collections.Generic;
using csharp_project_new.model;

namespace csharp_project_new.repository
{
    public interface IAgeEventRepository : IRepository<long, AgeEvent>
    {
        IEnumerable<AgeEvent> FindByAgeGroupAndSportsEvent(string ageGroup, string sportsEvent);
    }
}