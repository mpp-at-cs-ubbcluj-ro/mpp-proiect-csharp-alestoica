namespace csharp_project_new.model
{
    public class Registration : IEntity<long>
    {
        public long Id { get; set; }
        private long IdParticipant { get; set; }
        private long IdAgeEvent { get; set; }
        private long IdEmployee { get; set; }

        public Registration(long id, long idParticipant, long idAgeEvent, long idEmployee)
        {
            Id = id;
            IdParticipant = idParticipant;
            IdAgeEvent = idAgeEvent;
            IdEmployee = idEmployee;
        }
        
        public long GetIdParticipant()
        {
            return IdParticipant;
        }
        
        public long GetIdAgeEvent()
        {
            return IdAgeEvent;
        }
        
        public long GetIdEmployee()
        {
            return IdEmployee;
        }

        public override string ToString()
        {
            return "Registration " + Id + ": \nparticipant: " + IdParticipant + "; \nage event: " + IdAgeEvent +
                   "; \nemployee: " + IdEmployee;
        }
    }
}