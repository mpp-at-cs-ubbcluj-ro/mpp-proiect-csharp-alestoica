namespace csharp_project_new.model
{
    public class AgeEvent : IEntity<long>
    {
        public long Id { get; set; }
        private SportsEvent SportsEvent { get; set; }
        private AgeGroup AgeGroup { get; set; }

        public AgeEvent(long id, AgeGroup ageGroup, SportsEvent sportsEvent)
        {
            Id = id;
            SportsEvent = sportsEvent;
            AgeGroup = ageGroup;
        }
        
        public SportsEvent GetSportsEvent()
        {
            return SportsEvent;
        }
        
        public AgeGroup GetAgeGroup()
        {
            return AgeGroup;
        }

        public override string ToString()
        {
            return "Age Event " + Id + ": \nage group: " + AgeGroup + "; \nsports event: " + SportsEvent;
        }
    }
}