using System;
using System.Net.Sockets;
using System.Threading;
using csharp_project.service;
using Networking.rpcProtocol;

namespace Networking.utils
{
    public class ConcurrentServer : AbstractConcurrentServer
    {
        private readonly IService _server;

        public ConcurrentServer(string host, int port, IService server) : base(host, port)
        {
            _server = server;
        }

        protected override Thread CreateWorker(TcpClient client)
        {
            var worker = new ClientWorker(_server, client);
            Console.WriteLine("ConcurrentServer CreateWorker");
            return new Thread(worker.Run);
        }
    }
}