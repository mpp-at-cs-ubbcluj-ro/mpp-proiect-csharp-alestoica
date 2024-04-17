using System.Collections.Generic;
using csharp_project.model;

namespace csharp_project.repository
{
    public interface IAgeEventRepository : IRepository<long, AgeEvent>
    {
        AgeEvent FindByAgeGroupAndSportsEvent(string ageGroup, string sportsEvent);
        int CountParticipants(long id);
    }
}