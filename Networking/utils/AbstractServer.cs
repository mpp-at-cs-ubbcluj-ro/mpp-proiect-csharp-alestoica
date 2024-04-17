using System;
using System.Net;
using System.Net.Sockets;

namespace Networking.utils
{
    public abstract class AbstractServer
    {
        private readonly int _port;
        private readonly string _host;
        private TcpListener _server = null;

        protected AbstractServer(string host, int port)
        {
            _host = host;
            _port = port;
        }

        public void Start()
        {
            try
            {
                var address = IPAddress.Parse(_host);
                var endPoint = new IPEndPoint(address, _port);
                _server = new TcpListener(endPoint);
                _server.Start();

                while (true)
                {
                    Console.WriteLine("Waiting for clients...");
                    var client = _server.AcceptTcpClient();
                    Console.WriteLine("Client connected...");
                    ProcessRequest(client);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Starting server error: " + e.Message);
            }
            finally
            {
                Stop();
            }
        }

        protected abstract void ProcessRequest(TcpClient client);

        private void Stop()
        {
            try
            {
                _server.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine("Closing server error: " + e.Message);
            }
        }
    }
}