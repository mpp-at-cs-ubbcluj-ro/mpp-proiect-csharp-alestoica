namespace csharp_project.model
{
    public class AgeEvent : IEntity<long>
    {
        public AgeEvent(long id, AgeGroup ageGroup, SportsEvent sportsEvent)
        {
            Id = id;
            SportsEvent = sportsEvent;
            AgeGroup = ageGroup;
        }

        private SportsEvent SportsEvent { get; }
        private AgeGroup AgeGroup { get; }
        public long Id { get; set; }

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