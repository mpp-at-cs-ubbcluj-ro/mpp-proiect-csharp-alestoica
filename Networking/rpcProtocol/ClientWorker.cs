using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using csharp_project.model;
using csharp_project.service;

namespace Networking.rpcProtocol
{
    public class ClientWorker : IObserver
    {
        private readonly IService _service;
        private readonly TcpClient _connection;
        private readonly NetworkStream _stream;
        private readonly IFormatter _formatter;
        private volatile bool _connected;

        public ClientWorker(IService service, TcpClient connection)
        {
            _service = service;
            _connection = connection;

            try
            {
                _stream = connection.GetStream();
                _formatter = new BinaryFormatter();
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
                    var request = _formatter.Deserialize(_stream);
                    Console.WriteLine("ClientWorker request: " + request);
                    var response = HandleRequest((Request)request);
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
            if (request.GetType() == RequestType.Login)
            {
                var employee = (Employee)request.GetData();

                try
                {
                    var foundEmployee = _service.Login(employee, this);
                    return new Response.Builder().Type(ResponseType.Ok).Data(foundEmployee).Build();
                }
                catch (Exception e)
                {
                    _connected = false;
                    return new Response.Builder().Type(ResponseType.Error).Data(e.Message).Build();
                }
            }
            else if (request.GetType() == RequestType.Logout)
            {
                var employeeId = (long)request.GetData();

                try
                { 
                    _service.Logout(employeeId);   
                    _connected = false;
                    return new Response.Builder().Type(ResponseType.Ok).Build();
                }
                catch (Exception e)
                {
                    return new Response.Builder().Type(ResponseType.Error).Data(e.Message).Build();
                }
            }
            else if (request.GetType() == RequestType.AddRegistration)
            {
                var registration = (Registration)request.GetData();

                try
                {
                    _service.AddRegistration(registration);
                    return new Response.Builder().Type(ResponseType.Ok).Data(registration).Build();
                }
                catch (Exception e)
                {
                    return new Response.Builder().Type(ResponseType.Error).Data(e.Message).Build();
                }
            }
            else if (request.GetType() == RequestType.AddParticipant)
            {
                var participant = (Participant)request.GetData();

                try
                {
                    _service.AddParticipant(participant);
                    return new Response.Builder().Type(ResponseType.Ok).Data(participant).Build();
                }
                catch (Exception e)
                {
                    return new Response.Builder().Type(ResponseType.Error).Data(e.Message).Build();
                }
            }
            else if (request.GetType() == RequestType.GetEvents)
            {
                try
                {
                    var ageEvents = _service.GetAllAgeEvents();
                    return new Response.Builder().Type(ResponseType.GetEvents).Data(ageEvents).Build();
                }
                catch (Exception e)
                {
                    return new Response.Builder().Type(ResponseType.Error).Data(e.Message).Build();
                }
            }
            else if (request.GetType() == RequestType.GetParticipants)
            {
                try
                {
                    var participants = _service.GetAllParticipants();
                    return new Response.Builder().Type(ResponseType.GetParticipants).Data(participants).Build();
                }
                catch (Exception e)
                {
                    return new Response.Builder().Type(ResponseType.Error).Data(e.Message).Build();
                }
            }
            else if (request.GetType() == RequestType.GetParticipant)
            {
                var participantId = (long)request.GetData();

                try
                {
                    var foundParticipant = _service.FindOne(participantId);
                    return new Response.Builder().Type(ResponseType.Ok).Data(foundParticipant).Build();
                }
                catch (Exception e)
                {
                    return new Response.Builder().Type(ResponseType.Error).Data(e.Message).Build();
                }
            }
            else if (request.GetType() == RequestType.GetEmployee)
            {
                var employee = (Employee)request.GetData();

                try
                {
                    var foundEmployee = _service.FindOneByUsernameAndPassword(employee.GetUsername(), employee.GetPassword());
                    return new Response.Builder().Type(ResponseType.Ok).Data(foundEmployee).Build();
                }
                catch (Exception e)
                {
                    return new Response.Builder().Type(ResponseType.Error).Data(e.Message).Build();
                }
            }
            else if (request.GetType() == RequestType.GetAgeEvent)
            {
                var ageEvent = (AgeEvent)request.GetData();

                try
                {
                    var foundAgeEvent = _service.FindByAgeGroupAndSportsEvent(ageEvent.GetAgeGroup().ToString(), ageEvent.GetSportsEvent().ToString());
                    Console.WriteLine(foundAgeEvent);
                    return new Response.Builder().Type(ResponseType.Ok).Data(foundAgeEvent).Build();
                }
                catch (Exception e)
                {
                    return new Response.Builder().Type(ResponseType.Error).Data(e.Message).Build();
                }
            }
            else if (request.GetType() == RequestType.RegistrationByParticipant)
            {
                var participant = (Participant)request.GetData();

                try
                {
                    var registrations = _service.FindByParticipant(participant);
                    return new Response.Builder().Type(ResponseType.Ok).Data(registrations).Build();
                }
                catch (Exception e)
                {
                    return new Response.Builder().Type(ResponseType.Error).Data(e.Message).Build();
                }
            }
            else if (request.GetType() == RequestType.RegistrationByAgeEvent)
            {
                var ageEvent = (AgeEvent)request.GetData();

                try
                {
                    var registrations = _service.FindByAgeEvent(ageEvent);
                    return new Response.Builder().Type(ResponseType.Ok).Data(registrations).Build();
                }
                catch (Exception e)
                {
                    return new Response.Builder().Type(ResponseType.Error).Data(e.Message).Build();
                }
            }
            else if (request.GetType() == RequestType.ParticipantByNameAndAge)
            {
                var participant = (Participant)request.GetData();

                try
                {
                    var foundParticipant = _service.FindOneByNameAndAge(participant.FirstName, participant.LastName, participant.Age);
                    return new Response.Builder().Type(ResponseType.Ok).Data(foundParticipant).Build();
                }
                catch (Exception e)
                {
                    return new Response.Builder().Type(ResponseType.Error).Data(e.Message).Build();
                }
            }
            else if (request.GetType() == RequestType.CountParticipants)
            {
                var ageEvent = (AgeEvent)request.GetData();

                try
                {
                    var count = _service.CountParticipants(ageEvent);
                    return new Response.Builder().Type(ResponseType.Ok).Data(count).Build();
                }
                catch (Exception e)
                {
                    return new Response.Builder().Type(ResponseType.Error).Data(e.Message).Build();
                }
            }
            else if (request.GetType() == RequestType.CountRegistrations)
            {
                var participant = (Participant)request.GetData();

                try
                {
                    var count = _service.CountRegistrations(participant);
                    return new Response.Builder().Type(ResponseType.Ok).Data(count).Build();
                }
                catch (Exception e)
                {
                    return new Response.Builder().Type(ResponseType.Error).Data(e.Message).Build();
                }
            }
            else
                return null;
        }
        
        private void SendResponse(Response response)
        {
            lock (_stream) 
            {
                Console.WriteLine("SendResponse ClientWorker" + response);
                _formatter.Serialize(_stream, response);
                _stream.Flush();
            }
        }

        public virtual void NotifyAddRegistration(Registration registration)
        {
            var response = new Response.Builder().Type(ResponseType.NewRegistration).Data(registration).Build();

            try
            {
                SendResponse(response);
            }
            catch (Exception e)
            {
                throw new Exception("Adding registration error: " + e);
            }
        }

        /*public virtual void NotifyAddParticipant(Participant participant)
        {
            var response = new Response.Builder().Type(ResponseType.NewParticipant).Data(participant).Build();

            try
            {
                SendResponse(response);
            }
            catch (IOException e)
            {
                throw new Exception("Adding participant error: " + e);
            }
        }*/
    }
}