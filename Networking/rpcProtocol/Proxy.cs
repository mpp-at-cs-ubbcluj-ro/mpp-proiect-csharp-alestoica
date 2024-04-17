using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using csharp_project.model;
using csharp_project.service;

namespace Networking.rpcProtocol
{
    public class Proxy : IService
    {
        private readonly string _host;
        private readonly int _port;
        private IObserver _client;
        private NetworkStream _stream;
        private IFormatter _formatter;
        private TcpClient _connection;
        private readonly Queue<Response> _responses;
        private volatile bool _finished;
        private EventWaitHandle _waitHandle;

        public Proxy(string host, int port)
        {
            _host = host;
            _port = port;
            _responses = new Queue<Response>();
        }

        private void StartReader()
        {
            var tw = new Thread(Run);
            tw.Start();
        }

        private void InitializeConnection()
        {
            try
            {
                _connection = new TcpClient(_host, _port);
                _stream = _connection.GetStream();
                _formatter = new BinaryFormatter();
                _finished = false;
                _waitHandle = new AutoResetEvent(false);
                StartReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void CloseConnection()
        {
            _finished = true;
            
            try
            {
                Console.WriteLine("CloseConnection1");
                _stream.Close();
                Console.WriteLine("CloseConnection2");
                _connection.Close();
                Console.WriteLine("CloseConnection3");
                _waitHandle.Close();
                Console.WriteLine("CloseConnection4");
                _client = null;
                Console.WriteLine("CloseConnection5");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private Response ReadResponse()
        {
            Response response = null;

            try
            {
                _waitHandle.WaitOne();

                lock (_responses)
                {
                    response = _responses.Dequeue();   
                    Console.WriteLine("ReadResponse Proxy Response: " + response);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return response;
        }

        private void SendRequest(Request request)
        {
            try
            {
                Console.WriteLine("SendResponse Proxy Response: " + request);
                _formatter.Serialize(_stream, request);
                _stream.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public Employee Login(Employee employee, IObserver client)
        {
            InitializeConnection();

            var request = new Request.Builder().Type(RequestType.Login).Data(employee).Build();
            Console.WriteLine(request);
            SendRequest(request);

            var response = ReadResponse();
            Console.WriteLine(response);

            if (response.GetType() == ResponseType.Ok)
            {
                _client = client;
                return (Employee)response.GetData();
            }
            else if (response.GetType() == ResponseType.Error)
            {
                var error = response.GetData().ToString();
                Console.WriteLine("Error " + error);
                CloseConnection();
                throw new Exception(error);
            }

            return null;
        }

        public void Logout(long id)
        {
            var request = new Request.Builder().Type(RequestType.Logout).Data(id).Build();
            SendRequest(request);

            var response = ReadResponse();
            Console.WriteLine("LogOut response" + response);
            CloseConnection();

            if (response.GetType() == ResponseType.Error)
            {
                var error = response.GetData().ToString();
                Console.WriteLine("Error " + error);
                /*CloseConnection();*/
                throw new Exception(error);
            }
        }

        public Employee FindOneByUsernameAndPassword(string username, string password)
        {
            var request = new Request.Builder().Type(RequestType.GetEmployee).Data(1).Build();
            Console.WriteLine("Proxy Request: " + request);
            SendRequest(request);

            var response = ReadResponse();
            Console.WriteLine("Proxy Response: " + response);

            if (response.GetType() == ResponseType.Error)
            {
                var error = response.GetData().ToString();
                Console.WriteLine(error);
            }

            return (Employee)response.GetData();
        }

        public ICollection<AgeEvent> GetAllAgeEvents()
        {
            var request = new Request.Builder().Type(RequestType.GetEvents).Data(1).Build();
            SendRequest(request);
            var response = ReadResponse();

            if (response.GetType() == ResponseType.Error)
            {
                var error = response.GetData().ToString();
                Console.WriteLine(error);
            }

            return (ICollection<AgeEvent>)response.GetData();
        }

        public ICollection<Participant> GetAllParticipants()
        {
            var request = new Request.Builder().Type(RequestType.GetParticipants).Data(1).Build();
            SendRequest(request);
            var response = ReadResponse();

            if (response.GetType() == ResponseType.Error)
            {
                var error = response.GetData().ToString();
                Console.WriteLine(error);
            }

            return (ICollection<Participant>)response.GetData();
        }

        public ICollection<Registration> FindByAgeEvent(AgeEvent ageEvent)
        {
            var request = new Request.Builder().Type(RequestType.RegistrationByAgeEvent).Data(ageEvent).Build();
            SendRequest(request);
            var response = ReadResponse();

            if (response.GetType() == ResponseType.Error)
            {
                var error = response.GetData().ToString();
                Console.WriteLine(error);
            }

            return (ICollection<Registration>)response.GetData();
        }
        
        public ICollection<Registration> FindByParticipant(Participant participant)
        {
            var request = new Request.Builder().Type(RequestType.RegistrationByParticipant).Data(participant).Build();
            SendRequest(request);
            var response = ReadResponse();

            if (response.GetType() == ResponseType.Error)
            {
                var error = response.GetData().ToString();
                Console.WriteLine(error);
                return null;
            }

            return (ICollection<Registration>)response.GetData();
        }

        public Participant FindOne(long id)
        {
            var request = new Request.Builder().Type(RequestType.GetParticipant).Data(id).Build();
            SendRequest(request);
            var response = ReadResponse();

            if (response.GetType() == ResponseType.Error)
            {
                var error = response.GetData().ToString();
                Console.WriteLine(error);
            }

            return (Participant)response.GetData();
        }

        public Participant FindOneByNameAndAge(string firstName, string lastName, int age)
        {
            var participant = new Participant(1, firstName, lastName, age);
            var request = new Request.Builder().Type(RequestType.ParticipantByNameAndAge).Data(participant).Build();
            SendRequest(request);
            var response = ReadResponse();

            if (response.GetType() == ResponseType.Error)
            {
                var error = response.GetData().ToString();
                Console.WriteLine(error);
            }

            return (Participant)response.GetData();
        }

        public AgeEvent FindByAgeGroupAndSportsEvent(string ageGroup, string sportsEvent)
        {
            var ageEvent = new AgeEvent(1, (AgeGroup)Enum.Parse(typeof(AgeGroup), ageGroup), (SportsEvent)Enum.Parse(typeof(SportsEvent), sportsEvent));
            var request = new Request.Builder().Type(RequestType.GetAgeEvent).Data(ageEvent).Build();
            SendRequest(request);
            var response = ReadResponse();

            if (response.GetType() == ResponseType.Error)
            {
                var error = response.GetData().ToString();
                Console.WriteLine(error);
            }

            return (AgeEvent)response.GetData();
        }

        public void AddRegistration(Registration entity)
        {
            var request = new Request.Builder().Type(RequestType.AddRegistration).Data(entity).Build();
            SendRequest(request);
            var response = ReadResponse();

            if (response.GetType() == ResponseType.Error)
            {
                var error = response.GetData().ToString();
                Console.WriteLine(error);
            }
        }

        public void AddParticipant(Participant entity)
        {
            var request = new Request.Builder().Type(RequestType.AddParticipant).Data(entity).Build();
            SendRequest(request);
            var response = ReadResponse();

            if (response.GetType() == ResponseType.Error)
            {
                var error = response.GetData().ToString();
                Console.WriteLine(error);
            }
        }
        
        public int CountParticipants(AgeEvent ageEvent)
        {
            var req = new Request.Builder().Type(RequestType.CountParticipants).Data(ageEvent).Build();
            SendRequest(req);
            var response = ReadResponse();

            if (response.GetType() == ResponseType.Error)
            {
                var error = response.GetData().ToString();
                Console.WriteLine(error);
            }

            return (int)response.GetData();
        }

        public int CountRegistrations(Participant participant)
        {
            var req = new Request.Builder().Type(RequestType.CountRegistrations).Data(participant).Build();
            SendRequest(req);
            var response = ReadResponse();

            if (response.GetType() == ResponseType.Error)
            {
                var error = response.GetData().ToString();
                Console.WriteLine(error);
            }

            return (int)response.GetData();
        }
        
        private static bool IsUpdate(Response response)
        {
            return response.GetType() == ResponseType.NewRegistration || response.GetType() == ResponseType.NewParticipant;
        }

        private void HandleUpdate(Response response)
        {
            if (response.GetType() == ResponseType.NewRegistration)
            {
                var registration = (Registration)response.GetData();

                try
                {
                    _client.NotifyAddRegistration(registration);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else if (response.GetType() == ResponseType.NewParticipant)
            {
                var participant = (Participant)response.GetData();

                try
                {
                    _client.NotifyAddParticipant(participant);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        
        protected virtual void Run()
        {
            while (!_finished)
            {
                try
                {
                    var response = _formatter.Deserialize(_stream);
                    Console.WriteLine("run response" + response);

                    if (IsUpdate((Response)response))
                    {
                        HandleUpdate((Response)response);
                    }
                    else
                    {
                        lock (_responses)
                        {
                            _responses.Enqueue((Response)response);
                        }
                        
                        _waitHandle.Set();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Reading error: " + e.ToString());
                }
            }
        }
    }
}
