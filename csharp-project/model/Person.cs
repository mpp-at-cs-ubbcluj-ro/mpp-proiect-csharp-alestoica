namespace csharp_project.model
{
    public class Person : IEntity<long>
    {
        protected Person(long id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        private string FirstName { get; }
        private string LastName { get; }
        public long Id { get; set; }

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
            return Id + ": " + FirstName + " " + LastName;
        }
    }
}