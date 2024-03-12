namespace csharp_project.model;

public class Employee : Person
{
    private string Username { get; set; }
    private string Password { get; set; }

    public Employee(long id, string firstName, string lastName, string username, string password) : base(id, firstName, lastName)
    {
        Username = username;
        Password = password;
    }

    public override string ToString()
    {
        return "Employee " + base.ToString() + "\nusername: " + Username;
    }
}