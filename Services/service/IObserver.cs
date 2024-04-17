using csharp_project.model;

namespace csharp_project.service
{
    public interface IObserver
    {
        void NotifyAddRegistration(Registration registration);
        void NotifyAddParticipant(Participant participant);
    }
}