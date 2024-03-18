namespace csharp_project_new.model
{
    public class Employee : Person
    {
        private string Username { get; set; }
        private string Password { get; set; }

        public Employee(long id, string firstName, string lastName, string username, string password) : base(id, firstName, lastName)
        {
            Username = username;
            Password = password;
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