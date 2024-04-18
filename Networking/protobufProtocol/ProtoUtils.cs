using System;
using System.Collections.Generic;
using System.Linq;
using ModelEmployee = csharp_project.model.Employee;
using ModelParticipant = csharp_project.model.Participant;
using ModelAgeGroup = csharp_project.model.AgeGroup;
using ModelSportsEvent = csharp_project.model.SportsEvent;
using ModelAgeEvent = csharp_project.model.AgeEvent;
using ModelRegistration = csharp_project.model.Registration;


namespace Protocol
{
    public static class ProtoUtils
    {
        public static Employee GetProtoEmployee(ModelEmployee employee)
        {
            return new Employee
            {
                Id = employee.Id,
                FirstName = employee.GetFirstName(),
                LastName = employee.GetLastName(),
                Username = employee.GetUsername(),
                Password = employee.GetPassword()
            };
        }

        public static Participant GetProtoParticipant(ModelParticipant participant)
        {
            return participant != null
                ? new Participant
                {
                    Id = participant.Id,
                    FirstName = participant.FirstName,
                    LastName = participant.LastName,
                    Age = participant.Age
                }
                : new Participant();
        }

        public static AgeEvent GetProtoAgeEvent(ModelAgeEvent ageEvent)
        {
            return new AgeEvent
            {
                Id = ageEvent.Id,
                AgeGroup = (AgeEvent.Types.AgeGroup)Enum.Parse(typeof(AgeEvent.Types.AgeGroup), ageEvent.GetAgeGroup().ToString()),
                SportsEvent = (AgeEvent.Types.SportsEvent)Enum.Parse(typeof(AgeEvent.Types.SportsEvent), ageEvent.GetSportsEvent().ToString())
            };
        }

        public static Registration GetProtoRegistration(ModelRegistration registration)
        {
            return new Registration
            {
                Id = registration.Id,
                Participant = GetProtoParticipant(registration.GetParticipant()),
                AgeEvent = GetProtoAgeEvent(registration.GetAgeEvent()),
                Employee = GetProtoEmployee(registration.GetEmployee())
            };
        }

        public static List<Registration> GetProtoRegistrations(IEnumerable<ModelRegistration> registrations)
        {
            return registrations.Select(GetProtoRegistration).ToList();
        }

        public static string GetError(Response response)
        {
            return response.Error;
        }

        public static ModelEmployee GetEmployee(Employee protoEmployee)
        {
            var employee = new ModelEmployee(protoEmployee.Id, protoEmployee.FirstName, protoEmployee.LastName, protoEmployee.Username, protoEmployee.Password);
            return employee;
        }

        public static ModelParticipant GetParticipant(Participant protoParticipant)
        {
            var participant = new ModelParticipant(protoParticipant.Id, protoParticipant.FirstName, protoParticipant.LastName, protoParticipant.Age);
            return participant;
        }

        public static ModelAgeEvent GetAgeEvent(AgeEvent protoAgeEvent)
        {
            var ageEvent = new ModelAgeEvent(protoAgeEvent.Id, (ModelAgeGroup)Enum.Parse(typeof(ModelAgeGroup), protoAgeEvent.AgeGroup.ToString()), (ModelSportsEvent)Enum.Parse(typeof(ModelSportsEvent), protoAgeEvent.SportsEvent.ToString()));
            return ageEvent;
        }

        public static ModelRegistration GetRegistration(Registration protoRegistration)
        {
            var participant = GetParticipant(protoRegistration.Participant);
            var ageEvent = GetAgeEvent(protoRegistration.AgeEvent);
            var employee = GetEmployee(protoRegistration.Employee);

            var registration = new ModelRegistration(protoRegistration.Id, participant, ageEvent, employee);
            return registration;
        }
        
        public static IEnumerable<ModelAgeEvent> GetAgeEvents(IEnumerable<AgeEvent> protoAgeEvents)
        {
            return protoAgeEvents.Select(GetAgeEvent).ToList();
        }

        public static IEnumerable<ModelParticipant> GetParticipants(IEnumerable<Participant> protoParticipants)
        {
            return protoParticipants.Select(GetParticipant).ToList();
        }

        public static IEnumerable<ModelRegistration> GetRegistrations(IEnumerable<Registration> protoRegistrations)
        {
            return protoRegistrations.Select(GetRegistration).ToList();
        }
        
        public static Response SetCount(Response response, int count)
        {
            response.Count = count;
            return response;
        }

        public static Response SetEmployee(Response response, ModelEmployee employee)
        {
            response.Employee = GetProtoEmployee(employee);
            return response;
        }

        public static Response SetParticipant(Response response, ModelParticipant participant)
        {
            response.Participant = GetProtoParticipant(participant);
            return response;
        }

        public static Response SetAgeEvent(Response response, ModelAgeEvent ageEvent)
        {
            response.AgeEvent = GetProtoAgeEvent(ageEvent);
            return response;
        }

        public static Response SetRegistration(Response response, ModelRegistration registration)
        {
            response.Registration = GetProtoRegistration(registration);
            return response;
        }

        public static Response SetRegistrations(Response response, IEnumerable<ModelRegistration> registrations)
        {
            foreach (var registration in registrations.Select(GetProtoRegistration).ToList())
            {
                response.Registrations.Add(registration);    
            }
            
            return response;
        }

        public static Request CreateLoginRequest(ModelEmployee employee)
        {
            var protoEmployee = GetProtoEmployee(employee);

            return new Request
            {
                Type = Request.Types.RequestType.Login,
                Employee = protoEmployee
            };
        }

        public static Request CreateLogoutRequest(long id)
        {
            return new Request
            {
                Type = Request.Types.RequestType.Logout,
                Id = id
            };
        }

        public static Request CreateAddRegistrationRequest(ModelRegistration registration)
        {
            var protoRegistration = GetProtoRegistration(registration);

            return new Request
            {
                Type = Request.Types.RequestType.AddRegistration,
                Registration = protoRegistration
            };
        }

        public static Request CreateAddParticipantRequest(ModelParticipant participant)
        {
            var protoParticipant = GetProtoParticipant(participant);

            return new Request
            {
                Type = Request.Types.RequestType.AddParticipant,
                Participant = protoParticipant
            };
        }

        public static Request CreateGetEventsRequest()
        {
            return new Request
            {
                Type = Request.Types.RequestType.GetEvents
            };
        }

        public static Request CreateGetParticipantsRequest()
        {
            return new Request
            {
                Type = Request.Types.RequestType.GetParticipants
            };
        }

        public static Request CreateGetParticipantRequest(long id)
        {
            return new Request
            {
                Type = Request.Types.RequestType.GetParticipant,
                Id = id
            };
        }

        public static Request CreateGetEmployeeRequest()
        {
            return new Request
            {
                Type = Request.Types.RequestType.GetEmployee
            };
        }

        public static Request CreateGetAgeEventRequest(ModelAgeEvent ageEvent)
        {
            var protoAgeEvent = GetProtoAgeEvent(ageEvent);

            return new Request
            {
                Type = Request.Types.RequestType.GetAgeEvent,
                AgeEvent = protoAgeEvent
            };
        }

        public static Request CreateGetRegistrationByAgeEventRequest(ModelAgeEvent ageEvent)
        {
            var protoAgeEvent = GetProtoAgeEvent(ageEvent);

            Console.WriteLine($"createGetRegistrationByAgeEventRequest {protoAgeEvent}");

            return new Request
            {
                Type = Request.Types.RequestType.RegistrationByAgeEvent,
                AgeEvent = protoAgeEvent
            };
        }

        public static Request CreateGetRegistrationByParticipantRequest(ModelParticipant participant)
        {
            var protoParticipant = GetProtoParticipant(participant);

            return new Request
            {
                Type = Request.Types.RequestType.RegistrationByParticipant,
                Participant = protoParticipant
            };
        }

        public static Request CreateGetParticipantByNameAndAgeRequest(ModelParticipant participant)
        {
            var protoParticipant = GetProtoParticipant(participant);

            return new Request
            {
                Type = Request.Types.RequestType.ParticipantByNameAndAge,
                Participant = protoParticipant
            };
        }

        public static Request CreateCountParticipantsRequest(ModelAgeEvent ageEvent)
        {
            var protoAgeEvent = GetProtoAgeEvent(ageEvent);

            return new Request
            {
                Type = Request.Types.RequestType.CountParticipants,
                AgeEvent = protoAgeEvent
            };
        }

        public static Request CreateCountRegistrationsRequest(ModelParticipant participant)
        {
            var protoParticipant = GetProtoParticipant(participant);

            return new Request
            {
                Type = Request.Types.RequestType.CountRegistrations,
                Participant = protoParticipant
            };
        }
        
        public static Response CreateOkResponse()
        {
            return new Response
            {
                Type = Response.Types.ResponseType.Ok
            };
        }

        public static Response CreateErrorResponse(string error)
        {
            return new Response
            {
                Type = Response.Types.ResponseType.Error,
                Error = error
            };
        }

        public static Response CreateGetEventsResponse(IEnumerable<ModelAgeEvent> ageEvents)
        {
            var response = new Response
            {
                Type = Response.Types.ResponseType.GetEvents
            };

            foreach (var ageEvent in ageEvents)
            {
                var protoAgeEvent = GetProtoAgeEvent(ageEvent);
                response.AgeEvents.Add(protoAgeEvent);
            }

            return response;
        }

        public static Response CreateGetParticipantsResponse(IEnumerable<ModelParticipant> participants)
        {
            var response = new Response
            {
                Type = Response.Types.ResponseType.GetParticipants
            };

            foreach (var participant in participants)
            {
                var protoParticipant = GetProtoParticipant(participant);
                response.Participants.Add(protoParticipant);
            }

            return response;
        }

        public static Response CreateNewRegistrationResponse(ModelRegistration registration)
        {
            var protoRegistration = GetProtoRegistration(registration);

            return new Response
            {
                Type = Response.Types.ResponseType.NewRegistration,
                Registration = protoRegistration
            };
        }

    }
}
