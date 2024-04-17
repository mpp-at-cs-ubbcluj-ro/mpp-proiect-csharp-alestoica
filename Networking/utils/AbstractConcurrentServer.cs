using System.Net.Sockets;
using System.Threading;

namespace Networking.utils
{
    public abstract class AbstractConcurrentServer : AbstractServer
    {
        protected AbstractConcurrentServer(string host, int port) : base(host, port) { }

        protected override void ProcessRequest(TcpClient client)
        {
            var workerThread = CreateWorker(client);
            workerThread.Start();
        }

        protected abstract Thread CreateWorker(TcpClient client);
    }
}