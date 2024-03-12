namespace csharp_project.model;

public class Participant : Person
{
    public int Age { get; set; }

    public Participant(long id, string firstName, string lastName, int age) : base(id, firstName, lastName)
    {
        Age = age;
    }

    public override string ToString()
    {
        return "Participant " + base.ToString() + "\nage: " + Age;
    }
}