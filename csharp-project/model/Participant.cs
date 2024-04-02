namespace csharp_project.model
{
    public class Participant : IEntity<long>
    {
        public Participant(long id, string firstName, string lastName, int age)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public long Id { get; set; }
        public string FirstName { get; }
        public string LastName { get; }
        public int Age { get; }

        public int GetAge()
        {
            return Age;
        }

        public string GetFirstName()
        {
            return FirstName;
        }
        
        public string GetLastName()
        {
            return LastName;
        }

        public override string ToString()
        {
            return "Participant " + base.ToString() + "\nage: " + Age;
        }
    }
}