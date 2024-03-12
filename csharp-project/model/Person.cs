using Microsoft.VisualBasic.CompilerServices;

namespace csharp_project.model;

public class Person : IEntity<long>
{
    public long Id { get; set; }
    private string FirstName { get; set; }
    private string LastName { get; set; }

    protected Person(long id, string firstName, string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }

    public override string ToString()
    {
        return Id + ": " + FirstName + " " + LastName;
    }
}