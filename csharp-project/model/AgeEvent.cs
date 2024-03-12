namespace csharp_project.model;

public class AgeEvent : IEntity<long>
{
    public long Id { get; set; }
    private SportsEvent SportsEvent { get; set; }
    private AgeGroup AgeGroup { get; set; }

    public AgeEvent(long id, SportsEvent sportsEvent, AgeGroup ageGroup)
    {
        Id = id;
        SportsEvent = sportsEvent;
        AgeGroup = ageGroup;
    }

    public override string ToString()
    {
        return "Age Event " + Id + ": \nage group: " + AgeGroup + "; \nsports event: " + SportsEvent;
    }
}