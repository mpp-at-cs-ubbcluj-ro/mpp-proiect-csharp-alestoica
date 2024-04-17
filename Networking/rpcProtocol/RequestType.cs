namespace Networking.rpcProtocol
{
    public enum RequestType
    {
        Login,
        Logout,
        AddRegistration,
        AddParticipant,
        GetEvents,
        GetParticipants,
        GetParticipant,
        GetEmployee,
        GetAgeEvent,
        RegistrationByAgeEvent,
        RegistrationByParticipant,
        ParticipantByNameAndAge,
        CountParticipants,
        CountRegistrations
    }
}