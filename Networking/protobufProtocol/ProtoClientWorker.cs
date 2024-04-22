using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using csharp_project.service;
using Google.Protobuf;
using ModelRegistration = csharp_project.model.Registration;

namespace Protocol
{
    public class ProtoClientWorker : IObserver
    {
        private readonly IService _service;
        private readonly TcpClient _connection;
        private readonly NetworkStream _stream;
        private volatile bool _connected;
        
        public ProtoClientWorker(IService service, TcpClient connection)
        {
            _service = service;
            _connection = connection;

            try
            {
                _stream = connection.GetStream();
                _connected = true;
            }
            catch (IOException e)
            {
                Console.WriteLine(e);
            }
        }
        
        public void Run()
        {
            while (_connected)
            {
                try
                {
                    Console.WriteLine("ClientWorker Run");
                    
                    var request = Request.Parser.ParseDelimitedFrom(_stream);
                    
                    Console.WriteLine("ClientWorker request: " + request);
                
                    var response = HandleRequest(request);
                
                    Console.WriteLine("ClientWorker response: " + response);

                    if (response != null)
                        SendResponse(response);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }

                try
                {
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            try
            {
                _stream.Close();
                _connection.Close();   
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
            }
        }
        
        private Response HandleRequest(Request request)
        {
            var requestType = request.Type;

            switch (requestType)
            {
                case Request.Types.RequestType.Login:
                {
                    var employee = ProtoUtils.GetEmployee(request.Employee);
                    
                    try
                    {
                        var foundEmployee = _service.Login(employee, this);
                        var response = ProtoUtils.CreateOkResponse();
                        response.Employee = ProtoUtils.GetProtoEmployee(foundEmployee);
                        return response;
                    }
                    catch (Exception e)
                    {
                        _connected = false;
                        return ProtoUtils.CreateErrorResponse(e.Message);
                    }
                }
                case Request.Types.RequestType.Logout:
                {
                    var employeeId = request.Id;

                    try
                    { 
                        _service.Logout(employeeId);   
                        _connected = false;
                        return ProtoUtils.CreateOkResponse();
                    }
                    catch (Exception e)
                    {
                        _connected = false;
                        return ProtoUtils.CreateErrorResponse(e.Message);
                    }
                }
                case Request.Types.RequestType.AddRegistration:
                {
                    var registration = ProtoUtils.GetRegistration(request.Registration);

                    try
                    {
                        _service.AddRegistration(registration);
                        var response = ProtoUtils.CreateOkResponse();
                        response.Registration = ProtoUtils.GetProtoRegistration(registration);
                        return response;
                    }
                    catch (Exception e)
                    {
                        return ProtoUtils.CreateErrorResponse(e.Message);
                    }
                }
                case Request.Types.RequestType.AddParticipant:
                {
                    var participant = ProtoUtils.GetParticipant(request.Participant);

                    try
                    {
                        _service.AddParticipant(participant);
                        var response = ProtoUtils.CreateOkResponse();
                        response.Participant = ProtoUtils.GetProtoParticipant(participant);
                        return response;
                    }
                    catch (Exception e)
                    {
                        return ProtoUtils.CreateErrorResponse(e.Message);
                    }
                }
                case Request.Types.RequestType.GetEvents:
                {
                    try
                    {
                        var ageEvents = _service.GetAllAgeEvents();
                        var response = ProtoUtils.CreateGetEventsResponse(ageEvents);
                        return response;
                    }
                    catch (Exception e)
                    {
                        return ProtoUtils.CreateErrorResponse(e.Message);
                    }
                }
                case Request.Types.RequestType.GetParticipants:
                {
                    try
                    {
                        var participants = _service.GetAllParticipants();
                        var response = ProtoUtils.CreateGetParticipantsResponse(participants);
                        return response;
                    }
                    catch (Exception e)
                    {
                        return ProtoUtils.CreateErrorResponse(e.Message);
                    }
                }
                case Request.Types.RequestType.GetParticipant:
                {
                    var participantId = request.Id;

                    try
                    {
                        var foundParticipant = _service.FindOne(participantId);
                        var response = ProtoUtils.CreateOkResponse();
                        response.Participant = ProtoUtils.GetProtoParticipant(foundParticipant);
                        return response;
                    }
                    catch (Exception e)
                    {
                        return ProtoUtils.CreateErrorResponse(e.Message);
                    }
                }
                case Request.Types.RequestType.GetEmployee:
                {
                    var employee = ProtoUtils.GetEmployee(request.Employee);

                    try
                    {
                        var foundEmployee = _service.FindOneByUsernameAndPassword(employee.GetUsername(), employee.GetPassword());
                        var response = ProtoUtils.CreateOkResponse();
                        response.Employee = ProtoUtils.GetProtoEmployee(foundEmployee);
                        return response;
                    }
                    catch (Exception e)
                    {
                        return ProtoUtils.CreateErrorResponse(e.Message);
                    }
                }
                case Request.Types.RequestType.GetAgeEvent:
                {
                    var ageEvent = ProtoUtils.GetAgeEvent(request.AgeEvent);

                    try
                    {
                        var foundAgeEvent = _service.FindByAgeGroupAndSportsEvent(ageEvent.GetAgeGroup().ToString(), ageEvent.GetSportsEvent().ToString());
                        var response = ProtoUtils.CreateOkResponse();
                        response.AgeEvent = ProtoUtils.GetProtoAgeEvent(foundAgeEvent);
                        return response;
                    }
                    catch (Exception e)
                    {
                        return ProtoUtils.CreateErrorResponse(e.Message);
                    }
                }
                case Request.Types.RequestType.RegistrationByParticipant:
                {
                    var participant = ProtoUtils.GetParticipant(request.Participant);

                    try
                    {
                        var registrations = _service.FindByParticipant(participant);
                        var response = ProtoUtils.CreateOkResponse();
                        foreach (var protoRegistration in ProtoUtils.GetProtoRegistrations(registrations))
                        {
                            response.Registrations.Add(protoRegistration);
                        }
                        return response;
                    }
                    catch (Exception e)
                    {
                        return ProtoUtils.CreateErrorResponse(e.Message);
                    }
                }
                case Request.Types.RequestType.RegistrationByAgeEvent:
                {
                    var ageEvent = ProtoUtils.GetAgeEvent(request.AgeEvent);

                    try
                    {
                        var registrations = _service.FindByAgeEvent(ageEvent);
                        var response = ProtoUtils.CreateOkResponse();
                        foreach (var protoRegistration in ProtoUtils.GetProtoRegistrations(registrations))
                        {
                            response.Registrations.Add(protoRegistration);
                        }
                        return response;
                    }
                    catch (Exception e)
                    {
                        return ProtoUtils.CreateErrorResponse(e.Message);
                    }
                }
                case Request.Types.RequestType.ParticipantByNameAndAge:
                {
                    var participant = ProtoUtils.GetParticipant(request.Participant);

                    try
                    {
                        var foundParticipant = _service.FindOneByNameAndAge(participant.FirstName, participant.LastName, participant.Age);
                        var response = ProtoUtils.CreateOkResponse();
                        response.Participant = ProtoUtils.GetProtoParticipant(foundParticipant);
                        return response;
                    }
                    catch (Exception e)
                    {
                        return ProtoUtils.CreateErrorResponse(e.Message);
                    }
                }
                case Request.Types.RequestType.CountParticipants:
                {
                    var ageEvent = ProtoUtils.GetAgeEvent(request.AgeEvent);

                    try
                    {
                        var count = _service.CountParticipants(ageEvent);
                        var response = ProtoUtils.CreateOkResponse();
                        response.Count = count;
                        return response;
                    }
                    catch (Exception e)
                    {
                        return ProtoUtils.CreateErrorResponse(e.Message);
                    }
                }
                case Request.Types.RequestType.CountRegistrations:
                {
                    var participant = ProtoUtils.GetParticipant(request.Participant);

                    try
                    {
                        var count = _service.CountRegistrations(participant);
                        var response = ProtoUtils.CreateOkResponse();
                        response.Count = count;
                        return response;
                    }
                    catch (Exception e)
                    {
                        return ProtoUtils.CreateErrorResponse(e.Message);
                    }
                }
            }

            return null;
        }
        
        private void SendResponse(Response response)
        {
            lock (_stream) 
            {
                Console.WriteLine("SendResponse ClientWorker" + response);

                lock (_stream)
                {
                    response.WriteDelimitedTo(_stream);
                    _stream.Flush();
                }
            }
        }
        
        public virtual void NotifyAddRegistration(ModelRegistration registration)
        {
            var response = ProtoUtils.CreateNewRegistrationResponse(registration);

            try
            {
                SendResponse(response);
            }
            catch (Exception e)
            {
                throw new Exception("Adding registration error: " + e);
            }
        }
    }
}