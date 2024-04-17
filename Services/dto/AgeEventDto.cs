using csharp_project.model;

namespace Services.dto
{
    public class AgeEventDto
    {
        public long Id;
        public AgeGroup AgeGroup;
        public SportsEvent SportsEvent;
        public int NoParticipants;

        public AgeEventDto(long id, AgeGroup ageGroup, SportsEvent sportsEvent)
        {
            Id = id;
            AgeGroup = ageGroup;
            SportsEvent = sportsEvent;
        }
        
        public long GetId()
        {
            return Id;
        }
        
        public AgeGroup GetAgeGroup()
        {
            return AgeGroup;
        }

        public SportsEvent GetSportsEvent()
        {
            return SportsEvent;
        }
        
        public int GetNoParticipants()
        {
            return NoParticipants;
        }
        
        public void SetId(long id)
        {
            Id = id;
        }
        
        public void SetAgeGroup(AgeGroup ageGroup)
        {
            AgeGroup = ageGroup;
        }

        public void SetSportsEvent(SportsEvent sportsEvent)
        {
            SportsEvent = sportsEvent;
        }
        
        public void SetNoParticipants(int noParticipants)
        {
            NoParticipants = noParticipants;
        }
    }
}