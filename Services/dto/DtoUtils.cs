using csharp_project.model;

namespace Services.dto
{
    public class DtoUtils
    {
        public static Participant GetFromDto(ParticipantDto participantDto)
        {
            var id = participantDto.GetId();
            var nameParts = participantDto.GetName().Split(' ');
            var firstName = nameParts[0];
            var lastName = nameParts[1];
            var age = participantDto.GetAge();
            var participant = new Participant(id, firstName, lastName, age);
            return participant;
        }

        public static ParticipantDto GetDto(Participant participant)
        {
            var id = participant.Id;
            var name = $"{participant.FirstName} {participant.LastName}";
            var age = participant.Age;
            return new ParticipantDto(id, name, age);
        }

        public static AgeEvent GetFromDto(AgeEventDto ageEventDto)
        {
            var id = ageEventDto.GetId();
            var ageGroup = ageEventDto.GetAgeGroup();
            var sportsEvent = ageEventDto.GetSportsEvent();
            var ageEvent = new AgeEvent(id, ageGroup, sportsEvent);
            return ageEvent;
        }

        public static AgeEventDto GetDto(AgeEvent ageEvent)
        {
            var id = ageEvent.Id;
            var ageGroup = ageEvent.GetAgeGroup();
            var sportsEvent = ageEvent.GetSportsEvent();
            return new AgeEventDto(id, ageGroup, sportsEvent);
        }
    }
}