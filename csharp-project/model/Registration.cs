namespace csharp_project.model
{
    public class Registration : IEntity<long>
    {
        public Registration(long id, Participant participant, AgeEvent ageEvent, Employee employee)
        {
            Id = id;
            Participant = participant;
            AgeEvent = ageEvent;
            Employee = employee;
        }

        private Participant Participant { get; set; }
        private AgeEvent AgeEvent { get; set; }
        private Employee Employee { get; set; }
        public long Id { get; set; }

        public Participant GetParticipant()
        {
            return Participant;
        }

        public AgeEvent GetAgeEvent()
        {
            return AgeEvent;
        }

        public Employee GetEmployee()
        {
            return Employee;
        }

        public override string ToString()
        {
            return "Registration " + Id + ": \nparticipant: " + Participant + "; \nage event: " + AgeEvent +
                   "; \nemployee: " + Employee;
        }
    }
}