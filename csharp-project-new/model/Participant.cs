namespace csharp_project_new.model
{
    public class Participant : Person
    {
        private int Age { get; set; }

        public Participant(long id, string firstName, string lastName, int age) : base(id, firstName, lastName)
        {
            Age = age;
        }
        
        public int GetAge()
        {
            return Age;
        }

        public override string ToString()
        {
            return "Participant " + base.ToString() + "\nage: " + Age;
        }
    }
}