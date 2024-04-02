namespace csharp_project.model
{
    public class Employee : IEntity<long>
    {
        public Employee(long id, string firstName, string lastName, string username, string password)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
        }

        public long Id { get; set; }
        private string FirstName { get; }
        private string LastName { get; }
        private string Username { get; }
        private string Password { get; }

        public string GetFirstName()
        {
            return FirstName;
        }
        
        public string GetLastName()
        {
            return LastName;
        }
        
        public string GetUsername()
        {
            return Username;
        }

        public string GetPassword()
        {
            return Password;
        }

        public override string ToString()
        {
            return "Employee " + base.ToString() + "\nusername: " + Username;
        }
    }
}