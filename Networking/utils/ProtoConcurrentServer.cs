using System;
using System.Net.Sockets;
using System.Threading;
using csharp_project.service;
using Protocol;

namespace Networking.utils
{
    public class ProtoConcurrentServer : AbstractConcurrentServer
    {
        private readonly IService _server;

        public ProtoConcurrentServer(string host, int port, IService server) : base(host, port)
        {
            _server = server;
        }

        protected override Thread CreateWorker(TcpClient client)
        {
            var worker = new ProtoClientWorker(_server, client);
            Console.WriteLine("ConcurrentServer CreateWorker");
            return new Thread(worker.Run);
        }
    }
}