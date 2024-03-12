using csharp_project.model;

var employee = new Employee(12345, "Gigel", "Marin", "gigelmarin_", "123456gigel");
Console.WriteLine(employee);

var participant = new Participant(23456, "Dorel", "Lupu", 10);
Console.WriteLine(participant);

var ageEvent1 = new AgeEvent(34567, SportsEvent.Meters50, AgeGroup.Group68Years);
// AgeEvent ageEvent2 = null;
switch (participant.Age)
{
    case >= 6 and <= 8:
        ageEvent1 = new AgeEvent(34567, SportsEvent.Meters50, AgeGroup.Group68Years);
        // ageEvent2 = new AgeEvent(45678, SportsEvent.Meters100, AgeGroup.Group68Years);
        break;
    case >= 9 and <= 11:
        ageEvent1 = new AgeEvent(34567, SportsEvent.Meters100, AgeGroup.Group911Years);
        // ageEvent2 = new AgeEvent(45678, SportsEvent.Meters1000, AgeGroup.Group911Years);
        break;
    case >= 12 and <= 15:
        ageEvent1 = new AgeEvent(34567, SportsEvent.Meters1000, AgeGroup.Group1215Years);
        // ageEvent2 = new AgeEvent(45678, SportsEvent.Meters1500, AgeGroup.Group1215Years);
        break;
}
Console.WriteLine(ageEvent1);

var registration = new Registration(56789, participant.Id, ageEvent1.Id, employee.Id);
Console.WriteLine(registration);